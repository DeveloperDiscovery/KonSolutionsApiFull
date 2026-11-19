using System.Collections.Concurrent;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Domain.Models.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace KonSolutionsApi.API.Filters;

/// <summary>Guardián de FLUJO de la API — hermano de PermisoAuthorizationFilter, mismo diseño,
/// distinta pregunta.
///
/// Permisos pregunta "¿este ROL puede hacer esto?". Este pregunta "¿este REGISTRO, en el
/// estado en que está AHORA, admite esta acción?" — es la validación que ya existía del lado
/// del front (AccionesBotones.razor, contra
/// PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_FLUJOS_DEPENDENCIA_VALIDA) y que hasta hoy el API
/// no repetía: alguien que llamara al endpoint directo, sin pasar por la pantalla, podía
/// modificar un registro que el flujo dice que no debería tocarse en ese estado.
///
/// NO ES GLOBAL COMO EL DE PERMISOS. Se activa por controlador con [ExigeFlujo], a propósito:
/// requiere que el controlador YA declare [ExigePermiso] —de ahí saca la tabla— y que la
/// tabla tenga COD_TIPO_ESTADO + COD_ESTADO. No todos los controladores cumplen eso, así que
/// aplicarlo a ciegas a los 477 auditaría contra tablas que no tienen estado y no diría nada
/// útil. Se declara donde se sabe que hace falta —empezando por Requerimientos de Stock— y se
/// extiende deliberadamente, controlador por controlador.
///
/// SIGUE EL MISMO MODO QUE PERMISOS (Flujos:Modo en appsettings, mismo enum ModoPermisos):
/// arranca en Auditoría — registra lo que habría rechazado y deja pasar— y pasa a Bloqueo
/// cuando se revisó qué faltaba configurar. Nunca tumba el request: cualquier fallo interno
/// (SP caído, JSON raro, botón ambiguo) se registra y se deja pasar — un guardián que puede
/// romper el sistema por su propio error es peor que no tener guardián.</summary>
public class FlujoAuthorizationFilter : IAsyncAuthorizationFilter
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(FlujoAuthorizationFilter);

    private readonly IPermisosApiRepository _permisosRepo;
    private readonly IAplicacionesOpcionesBotonesRepository _botonesRepo;
    private readonly IOpcionesProcesosEstadosFlujosDependenciaService _flujo;
    private readonly IMemoryCache _cache;
    private readonly ErrorLogService _log;
    private readonly ModoPermisos _modo;

    private static readonly TimeSpan VentanaAuditoria = TimeSpan.FromMinutes(10);

    private static readonly ConcurrentDictionary<string, MetadatosEndpoint> _metadatos = new();

    private sealed record MetadatosEndpoint(
        string? DesObjeto,
        string? CodAplicacion,
        string? CodOpcionAplicacion,
        bool DeclaraFlujo);

    public FlujoAuthorizationFilter(IPermisosApiRepository permisosRepo,
                                    IAplicacionesOpcionesBotonesRepository botonesRepo,
                                    IOpcionesProcesosEstadosFlujosDependenciaService flujo,
                                    IMemoryCache cache, ErrorLogService log, IConfiguration config)
    {
        _permisosRepo = permisosRepo;
        _botonesRepo = botonesRepo;
        _flujo = flujo;
        _cache = cache;
        _log = log;
        _modo = Enum.TryParse<ModoPermisos>(config["Flujos:Modo"], ignoreCase: true, out var m)
            ? m : ModoPermisos.Auditoria;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (_modo == ModoPermisos.Desactivado) return;
        if (context.ActionDescriptor is not ControllerActionDescriptor endpoint) return;

        var meta = _metadatos.GetOrAdd(endpoint.Id, _ => Resolver(endpoint));
        if (!meta.DeclaraFlujo) return;

        // Sin [ExigePermiso] en el mismo controlador no hay tabla de la que leer el estado.
        // Se audita para que aparezca en la revisión —es una configuración a medias, no un
        // ataque— y se deja pasar: no es culpa del usuario.
        if (string.IsNullOrWhiteSpace(meta.DesObjeto) || meta.CodAplicacion is null || meta.CodOpcionAplicacion is null)
        {
            await AuditarAsync(context, "(SIN OBJETO O SIN OPCION)", context.HttpContext.Request.Method);
            return;
        }

        var verbo = context.HttpContext.Request.Method;
        // Sólo lo que escribe sobre un registro EXISTENTE tiene un "estado actual" que
        // validar. GET no cambia nada; un POST de alta no tiene fila previa —ver la nota en
        // ResolverJsonAsync sobre por qué eso hace que, para altas, esto casi siempre se
        // auto-excluya solo, sin necesidad de una lista de verbos más corta acá.
        if (!HttpMethods.IsPost(verbo) && !HttpMethods.IsPut(verbo) && !HttpMethods.IsDelete(verbo)) return;

        // ── 1. El botón que representa esta acción, para esta opción y este verbo ────────
        var claveBoton = $"flujo-boton:{meta.CodAplicacion}:{meta.CodOpcionAplicacion}:{verbo}";
        var codBoton = await _cache.GetOrCreateAsync(claveBoton, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await ResolverBotonAsync(meta.CodAplicacion, meta.CodOpcionAplicacion, verbo);
        });

        // Sin un único botón que represente el verbo en esa opción no hay contra qué
        // validar: 0 coincidencias es "no configurado", más de 1 es ambiguo. En los dos
        // casos se deja pasar — es lo mismo que decidió el mensaje que ya dejé en
        // OpcionesProcesosEstadosFlujosDependenciaController: "avisa y lo saco a un
        // controller aparte" aplicado acá a "avisa y no se bloquea nada".
        if (string.IsNullOrWhiteSpace(codBoton))
        {
            await AuditarAsync(context, meta.DesObjeto, $"{verbo} (sin boton unico)");
            return;
        }

        // ── 2. El estado ACTUAL del registro, leído de la base, no de lo que manda el cliente ──
        // Deliberado: si se confiara en un COD_ESTADO que viaja en el cuerpo, alguien podría
        // mandar el estado que le conviene para pasar la validación. Se lee siempre de la
        // fila real, con la misma resolución de tabla+PK que ya usa Permisos.
        string? json;
        try { json = await ResolverJsonAsync(context.HttpContext, verbo); }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(ResolverJsonAsync));
            return; // no se pudo armar el JSON -> no se puede validar -> se deja pasar
        }
        if (json is null) return;

        (short? codTipoEstado, short? codEstado) estado;
        try { estado = await _permisosRepo.EstadoActualAsync(meta.DesObjeto, json); }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(OnAuthorizationAsync), "EstadoActualAsync");
            return;
        }
        if (estado.codTipoEstado is null || estado.codEstado is null) return;

        // ── 3. La MISMA validación que ya usa el front, llamada en proceso ──────────────
        // En proceso y no por HTTP: así no pasa por el [ExigePermiso] del controlador de
        // Flujos, que exige un permiso de administración de configuración que un usuario
        // normal editando un requerimiento no tiene.
        OpcionesProcesosEstadosFlujosDependenciaValidaResult resultado;
        try
        {
            var r = await _flujo.ValidarAsync(meta.CodAplicacion, meta.CodOpcionAplicacion,
                        codBoton, estado.codTipoEstado.Value, estado.codEstado.Value);
            if (!r.Success || r.Data is null) return; // el servicio falló -> se deja pasar
            resultado = r.Data;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(OnAuthorizationAsync), "ValidarAsync");
            return;
        }

        if (resultado.FLG_VALIDO) return;

        await AuditarAsync(context, meta.DesObjeto, verbo);
        if (_modo == ModoPermisos.Bloqueo)
            context.Result = Rechazado(resultado.DES_MENSAJE);
    }

    /// <summary>Lee los atributos del endpoint una sola vez. [ExigePermiso] puede estar en el
    /// método o en la clase (mismo criterio que usa PermisoAuthorizationFilter); [ExigeFlujo]
    /// se busca igual.</summary>
    private static MetadatosEndpoint Resolver(ControllerActionDescriptor endpoint)
    {
        var flujoAttr = endpoint.MethodInfo.GetCustomAttributes(typeof(ExigeFlujoAttribute), true)
                            .Cast<ExigeFlujoAttribute>().FirstOrDefault()
                     ?? endpoint.ControllerTypeInfo.GetCustomAttributes(typeof(ExigeFlujoAttribute), true)
                            .Cast<ExigeFlujoAttribute>().FirstOrDefault();

        if (flujoAttr is null) return new MetadatosEndpoint(null, null, null, DeclaraFlujo: false);

        var objeto = endpoint.MethodInfo.GetCustomAttributes(typeof(ExigePermisoAttribute), true)
                            .Cast<ExigePermisoAttribute>().FirstOrDefault()?.Objeto
                  ?? endpoint.ControllerTypeInfo.GetCustomAttributes(typeof(ExigePermisoAttribute), true)
                            .Cast<ExigePermisoAttribute>().FirstOrDefault()?.Objeto;

        return new MetadatosEndpoint(objeto, flujoAttr.CodAplicacion, flujoAttr.CodOpcionAplicacion, DeclaraFlujo: true);
    }

    /// <summary>El botón cuyo flag de verbo coincide, entre los configurados para esa opción.
    /// Null si no hay exactamente uno.</summary>
    private async Task<string?> ResolverBotonAsync(string codAplicacion, string codOpcionAplicacion, string verbo)
    {
        try
        {
            var botones = (await _botonesRepo.ShowallAsync(codAplicacion, codOpcionAplicacion)).ToList();
            var candidatos = botones.Where(b => CoincideVerbo(b, verbo)).ToList();
            return candidatos.Count == 1 ? candidatos[0].COD_BOTON_OPCION : null;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(ResolverBotonAsync));
            return null;
        }
    }

    private static bool CoincideVerbo(AplicacionesOpcionesBotones b, string verbo) => verbo.ToUpperInvariant() switch
    {
        "POST" => b.FLG_ACCION_POST == true,
        "PUT" or "PATCH" => b.FLG_ACCION_PUT == true,
        "DELETE" => b.FLG_ACCION_DELETE == true,
        _ => false
    };

    /// <summary>El JSON contra el que EstadoActualAsync busca la fila.
    ///
    /// En POST/PUT es el CUERPO tal cual —mismo patrón que
    /// PermisoAuthorizationFilter.LeerCuerpoAsync, con su mismo motivo: hay que rebobinar el
    /// stream porque el model binding todavía no corrió.
    ///
    /// En DELETE la clave casi nunca viaja en el cuerpo —viaja en la RUTA
    /// (`{NUM_REQUERIMIENTO_STOCK}`)—, así que se arma un JSON mínimo con los valores de
    /// ruta cuyo nombre coincide con una columna de la tabla. Como el SP resuelve la PK por
    /// nombre de columna vía JSON_VALUE, alcanza con que los nombres de parámetro de ruta
    /// coincidan con las columnas —que es la convención que ya siguen los controladores
    /// existentes (`Delete(int NUM_REQUERIMIENTO_STOCK)`).
    ///
    /// EN POST DE ALTA ESTO CASI SIEMPRE VUELVE NULL DE FORMA NATURAL: un alta no tiene fila
    /// previa, así que EstadoActualAsync no encuentra nada y el filtro se auto-excluye sin
    /// necesitar lógica aparte para distinguir "alta" de "modificación por POST" —el mismo
    /// problema que PermisoAuthorizationFilter sí tiene que resolver con ExisteRegistroAsync,
    /// pero que acá no hace falta resolver: no encontrar el registro es, para este chequeo,
    /// la respuesta correcta ("no hay estado previo que validar").</summary>
    private static async Task<string?> ResolverJsonAsync(HttpContext http, string verbo)
    {
        if (HttpMethods.IsDelete(verbo))
        {
            var valores = http.Request.RouteValues
                .Where(kv => kv.Value is not null)
                .ToDictionary(kv => kv.Key, kv => kv.Value!.ToString());
            return valores.Count == 0 ? null : JsonSerializer.Serialize(valores);
        }

        if (http.Request.ContentLength is null or 0) return null;
        http.Request.EnableBuffering();
        http.Request.Body.Position = 0;
        using var lector = new StreamReader(http.Request.Body, Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        var texto = await lector.ReadToEndAsync();
        http.Request.Body.Position = 0;
        return string.IsNullOrWhiteSpace(texto) ? null : texto;
    }

    private async Task AuditarAsync(AuthorizationFilterContext context, string objeto, string verbo)
    {
        try
        {
            var user = context.HttpContext.User;
            var codUsuario = user.FindFirst("nameid")?.Value
                          ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "SISTEMA";
            var rol = user.FindFirst("rol")?.Value ?? "";

            // Prefijo FLUJO: en la misma tabla de auditoría que Permisos, distinguible por
            // verbo -así una revisión de SEGURIDAD.PERMISOS_AUDITORIA no confunde un rechazo
            // de permiso con uno de flujo.
            var claveVerbo = $"FLUJO:{verbo}";
            var clave = $"auditoria-flujo:{codUsuario}:{rol}:{objeto}:{claveVerbo}:{context.ActionDescriptor.Id}";
            if (_cache.TryGetValue(clave, out _)) return;
            _cache.Set(clave, true, VentanaAuditoria);

            var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "LOCALHOST";
            await _permisosRepo.AuditarAsync(
                codUsuario, rol, objeto, claveVerbo,
                $"{verbo} {context.HttpContext.Request.Path}{context.HttpContext.Request.QueryString}",
                ip == "::1" ? "LOCALHOST" : ip);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(AuditarAsync));
        }
    }

    private static IActionResult Rechazado(string? mensaje)
        => new ObjectResult(ApiResponse<object>.Fail(
               mensaje ?? "El registro no está en un estado que permita esta acción."))
        { StatusCode = StatusCodes.Status409Conflict };
}
