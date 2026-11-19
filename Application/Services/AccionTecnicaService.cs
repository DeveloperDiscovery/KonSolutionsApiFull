using System.Reflection;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Diagnostico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace KonSolutionsApi.Application.Services;

public interface IAccionTecnicaService
{
    AccionTecnica Resolver(string? formulario, string? verbo, string? boton);
}

/// <summary>
/// Resuelve Clase / Método / SP de un botón de acción POR REFLEXIÓN sobre el código.
///
/// POR QUÉ POR REFLEXIÓN Y NO POR CONVENCIÓN NI POR BD
/// ---------------------------------------------------
/// - Por BD no se puede: DES_METODO existe en la tabla de botones pero no se está
///   registrando, así que estaría vacío en casi todos.
/// - Por convención (armar "PROC_" + schema + entidad + verbo a partir del nombre del
///   razor) acertaría en el CRUD y MENTIRÍA en todo lo demás, sin manera de que quien
///   mira el diálogo se dé cuenta.
/// - Por reflexión se leen el controller real y la constante real de SP. Si mañana alguien
///   cambia un SP o renombra un método, el diálogo lo refleja solo. Y cuando algo no se
///   puede resolver, se dice en Observacion en vez de completarlo con una suposición.
///
/// CÓMO ENCADENA
/// -------------
///   DES_FORMULARIO "Logistica/Inventarios.razor"
///     -> entidad "Inventarios"
///     -> tipo "InventariosController" en este ensamblado
///     -> método según el verbo (los HttpGet/HttpPut/... declarados en el controller)
///     -> constante en SP.SCHEMA*.Inventarios según la operación del método
///
/// El eslabón entidad->controller funciona porque en esta solución el razor, el controller
/// y el bloque de SP se llaman igual. Cuando NO coinciden, no se inventa nada: queda sin
/// resolver y con el motivo escrito.
/// </summary>
public class AccionTecnicaService : IAccionTecnicaService
{
    // Los resultados no cambian mientras el proceso viva (dependen del ensamblado, no de
    // datos), así que se cachean: la reflexión es cara y esto se dispara con cada clic
    // derecho.
    private static readonly Dictionary<string, AccionTecnica> _cache = new(StringComparer.OrdinalIgnoreCase);
    private static readonly object _lock = new();

    /// <summary>
    /// Método del controller -> propiedad de la clase de SP. Es el único punto donde se
    /// asume algo, y es una correspondencia que se puede leer y corregir de un vistazo, no
    /// una cadena armada con concatenaciones.
    /// </summary>
    private static readonly (string Metodo, string Operacion)[] _mapaOperacion =
    {
        ("GetAll",      "Showall"),
        ("GetById",     "Showbyid"),
        ("GetCombobox", "Showcombox"),
        ("Search",      "Search"),
        ("Insert",      "Insert"),
        ("Update",      "Update"),
        ("Delete",      "Delete"),
    };

    public AccionTecnica Resolver(string? formulario, string? verbo, string? boton)
    {
        var clave = $"{formulario}|{verbo}";
        lock (_lock)
        {
            if (_cache.TryGetValue(clave, out var cacheado))
                return Clonar(cacheado, boton);
        }

        var r = ResolverSinCache(formulario, verbo);
        lock (_lock) { _cache[clave] = r; }
        return Clonar(r, boton);
    }

    private static AccionTecnica Clonar(AccionTecnica o, string? boton) => new()
    {
        Formulario = o.Formulario, Clase = o.Clase, Metodo = o.Metodo, Ruta = o.Ruta,
        StoredProcedure = o.StoredProcedure, Verbo = o.Verbo, Observacion = o.Observacion,
        Resuelto = o.Resuelto, Boton = boton
    };

    private AccionTecnica ResolverSinCache(string? formulario, string? verbo)
    {
        var r = new AccionTecnica { Formulario = formulario, Verbo = verbo };

        if (string.IsNullOrWhiteSpace(formulario))
        {
            r.Observacion = "La opción no tiene DES_FORMULARIO configurado, así que no hay por dónde empezar a resolver.";
            return r;
        }

        // Un botón sin ningún FLG_ACCION_* marcado es un botón de proceso: la pantalla lo
        // despacha por nombre, no por verbo. En Inventarios todos esos caen en AccionProceso,
        // que hoy sólo muestra un aviso. No hay método ni SP que mostrar, y decirlo es
        // justamente el dato útil.
        if (string.IsNullOrWhiteSpace(verbo))
        {
            r.Observacion = "El botón no declara ningún verbo HTTP. La pantalla lo despacha por nombre "
                          + "y, salvo que tenga una acción propia programada, no ejecuta ningún SP.";
            return r;
        }

        // "Logistica/Inventarios.razor" -> "Inventarios"
        var entidad = Path.GetFileNameWithoutExtension(formulario.Replace('\\', '/').Split('/').Last());
        if (string.IsNullOrWhiteSpace(entidad))
        {
            r.Observacion = $"No se pudo deducir la entidad desde DES_FORMULARIO '{formulario}'.";
            return r;
        }

        var asm = typeof(AccionTecnicaService).Assembly;
        var controller = asm.GetTypes().FirstOrDefault(t =>
            t.IsClass && !t.IsAbstract &&
            t.Name.Equals($"{entidad}Controller", StringComparison.OrdinalIgnoreCase));

        if (controller is null)
        {
            r.Observacion = $"No existe un controller llamado '{entidad}Controller'. "
                          + "Suele pasar cuando la pantalla usa el controller de otra entidad "
                          + "(un panel o una sub-grilla) — en ese caso el dato hay que buscarlo en el razor.";
            return r;
        }

        r.Clase = controller.Name;
        var rutaBase = controller.GetCustomAttribute<RouteAttribute>()?.Template;

        var metodo = ElegirMetodo(controller, verbo!);
        if (metodo is null)
        {
            r.Observacion = $"{controller.Name} no expone ninguna acción {verbo}.";
            return r;
        }

        r.Metodo = metodo.Name;
        var plantilla = metodo.GetCustomAttributes<HttpMethodAttribute>().FirstOrDefault()?.Template;
        r.Ruta = string.IsNullOrWhiteSpace(plantilla)
            ? $"{verbo} {rutaBase}"
            : $"{verbo} {rutaBase}/{plantilla}";

        r.StoredProcedure = BuscarSp(entidad, metodo.Name);
        if (r.StoredProcedure is null)
        {
            r.Observacion = $"Se resolvió el método pero no la constante de SP para '{entidad}.{metodo.Name}'. "
                          + "Puede ser un método que no llame a un SP directo (compone varios, o delega en otro servicio).";
            return r;
        }

        r.Resuelto = true;
        r.Observacion = "Resuelto por reflexión sobre el controller y las constantes de SP.";
        return r;
    }

    /// <summary>
    /// Entre las acciones del mismo verbo elige la que corresponde. Con GET hay varias
    /// (GetAll, GetById, GetCombobox, Search) y hay que desempatar: sin plantilla de ruta es
    /// el listado, con "{...}" es por id. Se prioriza el listado porque es lo que dispara el
    /// botón de consulta de una grilla.
    /// </summary>
    private static MethodInfo? ElegirMetodo(Type controller, string verbo)
    {
        var acciones = controller.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Select(m => (Metodo: m, Attr: m.GetCustomAttributes<HttpMethodAttribute>().FirstOrDefault()))
            .Where(x => x.Attr is not null &&
                        x.Attr!.HttpMethods.Any(h => h.Equals(verbo, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        if (acciones.Count == 0) return null;
        if (acciones.Count == 1) return acciones[0].Metodo;

        var sinPlantilla = acciones.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Attr!.Template));
        return sinPlantilla.Metodo ?? acciones[0].Metodo;
    }

    /// <summary>
    /// Busca la constante real dentro de SP. La clase está anidada como
    /// SP.SCHEMA&lt;Modulo&gt;.&lt;Entidad&gt;, y el nombre del schema no se puede deducir del
    /// razor de forma confiable (hay entidades cuyo módulo de menú no coincide con su schema),
    /// así que se recorren todos los bloques y se toma el que tenga una entidad con ese
    /// nombre. Si hubiera dos iguales en schemas distintos, se devuelven ambos para que quien
    /// lee el diálogo decida, en vez de elegir uno en silencio.
    /// </summary>
    private static string? BuscarSp(string entidad, string metodo)
    {
        var operacion = _mapaOperacion.FirstOrDefault(x =>
            x.Metodo.Equals(metodo, StringComparison.OrdinalIgnoreCase)).Operacion;
        if (string.IsNullOrWhiteSpace(operacion)) return null;

        var encontrados = new List<string>();
        foreach (var schema in typeof(SP).GetNestedTypes(BindingFlags.Public | BindingFlags.Static))
        {
            var tipoEntidad = schema.GetNestedTypes(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(t => t.Name.Equals(entidad, StringComparison.OrdinalIgnoreCase));
            if (tipoEntidad is null) continue;

            var campo = tipoEntidad.GetField(operacion, BindingFlags.Public | BindingFlags.Static);
            if (campo?.GetRawConstantValue() is string valor && !string.IsNullOrWhiteSpace(valor))
                encontrados.Add(valor);
        }

        return encontrados.Count switch
        {
            0 => null,
            1 => encontrados[0],
            _ => string.Join("  ·  ", encontrados.Distinct())
        };
    }
}
