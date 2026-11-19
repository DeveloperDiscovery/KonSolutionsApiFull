using System.Collections.Concurrent;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KonSolutionsApi.API.Filters;

/// <summary>Descarta el caché de permisos cuando alguien toca las tablas que lo alimentan.
///
/// Sin esto, editar los botones de un rol no se sentía hasta que venciera el TTL de cinco
/// minutos: el usuario seguía trabajando con los permisos viejos. En modo Auditoría no se
/// nota; en modo Bloqueo es la diferencia entre "le saqué el permiso" y "se lo saqué dentro
/// de un rato".
///
/// Va como filtro global y se apoya en el [ExigePermiso] que ya llevan los controladores, en
/// vez de llamar a Invalidar() desde cada servicio que escribe. El criterio es el mismo que
/// el del guardián: si depende de que alguien se acuerde de agregarlo, el día que se agregue
/// una pantalla nueva que toque permisos nadie se va a acordar, y el síntoma —permisos que
/// tardan en aplicarse— es de los más difíciles de atribuir.</summary>
public class InvalidacionPermisosFilter : IAsyncActionFilter
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(InvalidacionPermisosFilter);

    /// <summary>Tablas de las que sale un permiso. Las cuatro primeras son las que arma
    /// PROC_SEGURIDAD_PERMISOS_ROL_SHOWALL; ROLES_USUARIO entra porque dar de baja un rol
    /// también cambia el resultado.</summary>
    private static readonly HashSet<string> Sensibles = new(StringComparer.OrdinalIgnoreCase)
    {
        "SEGURIDAD.ROLES_USUARIOS_APLICACIONES_OPCIONES",
        "SEGURIDAD.ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES",
        "SEGURIDAD.APLICACIONES_OPCIONES",
        "SEGURIDAD.APLICACIONES_OPCIONES_BOTONES",
        "SEGURIDAD.ROLES_USUARIO"
    };

    /// <summary>¿La acción toca una tabla de permisos? Se calcula una vez y se reutiliza.</summary>
    private static readonly ConcurrentDictionary<string, bool> _sensiblePorAccion = new();

    private readonly IPermisosApiService _permisos;
    private readonly ErrorLogService _log;

    public InvalidacionPermisosFilter(IPermisosApiService permisos, ErrorLogService log)
    {
        _permisos = permisos; _log = log;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var ejecutado = await next();

        // Una lectura no cambia nada, y una escritura que falló tampoco.
        if (HttpMethods.IsGet(context.HttpContext.Request.Method)) return;
        if (ejecutado.Exception is not null && !ejecutado.ExceptionHandled) return;
        var estado = context.HttpContext.Response.StatusCode;
        if (estado is < 200 or >= 300) return;

        if (context.ActionDescriptor is not ControllerActionDescriptor accion) return;

        // Se resuelve una vez por acción: la reflexión sobre atributos corre en cada request
        // de los 477 controladores, y el endpoint no cambia en caliente.
        var sensible = _sensiblePorAccion.GetOrAdd(accion.Id, _ =>
        {
            var objeto = accion.MethodInfo.GetCustomAttributes(typeof(ExigePermisoAttribute), true)
                                .Cast<ExigePermisoAttribute>().FirstOrDefault()?.Objeto
                       ?? accion.ControllerTypeInfo.GetCustomAttributes(typeof(ExigePermisoAttribute), true)
                                .Cast<ExigePermisoAttribute>().FirstOrDefault()?.Objeto;
            return objeto is not null && Sensibles.Contains(objeto);
        });

        if (!sensible) return;

        try
        {
            // Se limpia TODO y no sólo el rol tocado: una opción movida o un botón editado
            // afectan a cualquier rol que llegue a esa opción, y averiguar a cuáles saldría
            // más caro que rehacer un caché que se rearma con una consulta.
            _permisos.Invalidar();
        }
        catch (Exception ex)
        {
            // Invalidar de más nunca rompe nada; que falle tampoco debe romper el request
            // que ya se ejecutó bien.
            _log.LogError(ex, Programa, Clase, nameof(OnActionExecutionAsync));
        }
    }
}
