using System.Collections.Concurrent;
using System.Security.Claims;
using System.Text;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Caching.Memory;

namespace KonSolutionsApi.API.Filters;

/// <summary>Guardián de permisos de la API.
///
/// Hasta hoy el control de roles vivía SÓLO en la interfaz: los botones se ocultaban, pero el
/// endpoint quedaba abierto para cualquiera con un token válido. Esto lo cierra del lado del
/// servidor, que es el único lado que cuenta.
///
/// Corre como filtro global y no como atributo repetido en 477 controladores: si dependiera de
/// acordarse de ponerlo, el que se olvide queda sin protección y nadie se entera.
///
/// ARRANCA EN MODO AUDITORÍA. En ese modo no bloquea nada: sólo registra en
/// SEGURIDAD.PERMISOS_AUDITORIA lo que habría rechazado. Se deja correr unos días, se mira qué
/// aparece y recién entonces se pasa a Bloqueo.
///
/// COSTO POR REQUEST. Está en el camino de los 477 controladores, así que todo lo que hace de
/// más se paga 477 veces. Por eso: los metadatos del endpoint se resuelven UNA vez por acción
/// y se cachean, la auditoría se deduplica en memoria, y el cuerpo sólo se lee en los POST
/// donde de verdad sirve leerlo.</summary>
public class PermisoAuthorizationFilter : IAsyncAuthorizationFilter
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PermisoAuthorizationFilter);

    /// <summary>Cada combinación se registra una vez por ventana. Sin esto, un endpoint que se
    /// sondea —ControlCambios pregunta cada 10 segundos por usuario— escribiría una fila cada
    /// 10 segundos para siempre: decenas de miles por día que repiten el mismo dato. Lo que
    /// interesa saber es QUE pasa y cuándo fue la última vez, no cuántas veces.</summary>
    private static readonly TimeSpan VentanaAuditoria = TimeSpan.FromMinutes(10);

    private readonly IPermisosApiService _permisos;
    private readonly IPermisosApiRepository _repo;
    private readonly IMemoryCache _cache;
    private readonly ErrorLogService _log;
    private readonly ModoPermisos _modo;

    /// <summary>Metadatos por acción. La reflexión sobre atributos no es gratis y el endpoint
    /// nunca cambia en caliente, así que se resuelve la primera vez y se reutiliza.</summary>
    private static readonly ConcurrentDictionary<string, MetadatosEndpoint> _metadatos = new();

    private sealed record MetadatosEndpoint(
        string? Objeto,
        bool Exento,
        bool EsUpsertDeTabla);

    public PermisoAuthorizationFilter(IPermisosApiService permisos, IPermisosApiRepository repo,
                                      IMemoryCache cache, ErrorLogService log, IConfiguration config)
    {
        _permisos = permisos;
        _repo = repo;
        _cache = cache;
        _log = log;
        _modo = Enum.TryParse<ModoPermisos>(config["Permisos:Modo"], ignoreCase: true, out var m)
            ? m : ModoPermisos.Auditoria;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (_modo == ModoPermisos.Desactivado) return;

        if (context.ActionDescriptor is not ControllerActionDescriptor endpoint) return;

        var meta = _metadatos.GetOrAdd(endpoint.Id, _ => Resolver(endpoint));

        // Lo anónimo y lo exento no pasa por acá: el login no puede pedir permisos para poder
        // loguearse.
        if (meta.Exento) return;

        // Sin usuario autenticado no hay nada que evaluar: de eso ya se ocupa [Authorize],
        // que corre en este mismo pipeline.
        var user = context.HttpContext.User;
        if (user?.Identity?.IsAuthenticated != true) return;

        var verbo = context.HttpContext.Request.Method;

        // Controlador que todavía no declaró su objeto. No se bloquea ni siquiera en modo
        // Bloqueo: sería castigar al usuario por algo que falta configurar del lado nuestro.
        // Queda registrado —una vez por ventana— para que aparezca en la revisión.
        if (string.IsNullOrWhiteSpace(meta.Objeto))
        {
            await AuditarAsync(context, endpoint, "(SIN DECLARAR)", verbo);
            return;
        }

        var rol = user.FindFirst("rol")?.Value;
        if (string.IsNullOrWhiteSpace(rol))
        {
            await AuditarAsync(context, endpoint, meta.Objeto, verbo);
            if (_modo == ModoPermisos.Bloqueo) context.Result = Prohibido(meta.Objeto, verbo);
            return;
        }

        // El cuerpo sólo se lee en el POST que de verdad es un upsert de la entidad.
        //
        // En los POST con sub-ruta (/asignar, /desasignar, /move, /generar…) el cuerpo no es
        // una fila de la tabla, así que buscarle la PK no puede dar un resultado útil: devolvía
        // "no existe" y clasificaba un movimiento de opciones como si fuera un alta. Además de
        // equivocarse, costaba una lectura a la base y el buffering del cuerpo entero —que en
        // los endpoints de carga de archivos o íconos en base64 no es poco—.
        var cuerpo = meta.EsUpsertDeTabla && HttpMethods.IsPost(verbo)
            ? await LeerCuerpoAsync(context.HttpContext)
            : null;

        bool puede;
        try
        {
            puede = await _permisos.PuedeAsync(rol, meta.Objeto, verbo, cuerpo);
        }
        catch (Exception ex)
        {
            // Si el chequeo se rompe, no se cierra el sistema: se registra y se deja pasar.
            // Un bug acá no puede convertirse en una caída total del ERP.
            _log.LogError(ex, Programa, Clase, nameof(OnAuthorizationAsync));
            return;
        }

        if (puede) return;

        await AuditarAsync(context, endpoint, meta.Objeto, verbo);
        if (_modo == ModoPermisos.Bloqueo) context.Result = Prohibido(meta.Objeto, verbo);
    }

    /// <summary>Lee los atributos del endpoint una sola vez.</summary>
    private static MetadatosEndpoint Resolver(ControllerActionDescriptor endpoint)
    {
        var exento = Tiene<AllowAnonymousAttribute>(endpoint)
                  || Tiene<SinControlPermisosAttribute>(endpoint);

        var objeto = endpoint.MethodInfo.GetCustomAttributes(typeof(ExigePermisoAttribute), true)
                              .Cast<ExigePermisoAttribute>().FirstOrDefault()?.Objeto
                   ?? endpoint.ControllerTypeInfo.GetCustomAttributes(typeof(ExigePermisoAttribute), true)
                              .Cast<ExigePermisoAttribute>().FirstOrDefault()?.Objeto;

        // Un [HttpPost] sin plantilla es el alta/modificación de la tabla del controlador.
        // Un [HttpPost("algo")] es una operación con nombre propio, y su cuerpo no es una fila.
        var esUpsert = endpoint.MethodInfo
            .GetCustomAttributes(typeof(HttpPostAttribute), true)
            .Cast<HttpPostAttribute>()
            .Any(a => string.IsNullOrEmpty(a.Template));

        return new MetadatosEndpoint(objeto, exento, esUpsert);
    }

    private static bool Tiene<T>(ControllerActionDescriptor endpoint) where T : Attribute
        => endpoint.MethodInfo.GetCustomAttributes(typeof(T), true).Any()
        || endpoint.ControllerTypeInfo.GetCustomAttributes(typeof(T), true).Any();

    /// <summary>Lee el cuerpo sin consumirlo: hay que rebobinar el stream porque el model
    /// binding todavía no corrió y va a necesitarlo entero.</summary>
    private async Task<string?> LeerCuerpoAsync(HttpContext http)
    {
        try
        {
            if (http.Request.ContentLength is null or 0) return null;
            http.Request.EnableBuffering();
            http.Request.Body.Position = 0;
            using var lector = new StreamReader(http.Request.Body, Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false, leaveOpen: true);
            var texto = await lector.ReadToEndAsync();
            http.Request.Body.Position = 0;
            return texto;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(LeerCuerpoAsync));
            return null;
        }
    }

    private async Task AuditarAsync(AuthorizationFilterContext context, ControllerActionDescriptor endpoint,
                                    string objeto, string verbo)
    {
        try
        {
            var user = context.HttpContext.User;
            var codUsuario = user.FindFirst("nameid")?.Value
                          ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "SISTEMA";
            var rol = user.FindFirst("rol")?.Value ?? "";

            // Se agrupa por acción y no por ruta: la ruta lleva los códigos del registro, así
            // que "editar el almacén 00001" y "editar el 00002" serían dos claves distintas y
            // la deduplicación no serviría de nada.
            var clave = $"auditoria:{codUsuario}:{rol}:{objeto}:{verbo}:{endpoint.Id}";
            if (_cache.TryGetValue(clave, out _)) return;
            _cache.Set(clave, true, VentanaAuditoria);

            var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "LOCALHOST";
            await _repo.AuditarAsync(
                codUsuario, rol, objeto, verbo,
                $"{verbo} {context.HttpContext.Request.Path}{context.HttpContext.Request.QueryString}",
                ip == "::1" ? "LOCALHOST" : ip);
        }
        catch (Exception ex)
        {
            // La auditoría es un registro, no una condición: nunca debe tumbar el request.
            _log.LogError(ex, Programa, Clase, nameof(AuditarAsync));
        }
    }

    /// <summary>403 y no 401: el usuario está bien identificado, lo que falta es el permiso.
    /// Importa porque el frontend redirige al login ante un 401 — y mandarlo a loguearse de
    /// nuevo cuando el problema es otro sólo confunde.</summary>
    private static IActionResult Prohibido(string objeto, string verbo)
        => new ObjectResult(ApiResponse<object>.Fail(
               $"No tiene permiso para esta operación ({verbo} sobre {objeto})."))
        { StatusCode = StatusCodes.Status403Forbidden };
}
