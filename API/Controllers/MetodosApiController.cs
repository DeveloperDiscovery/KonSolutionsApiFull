using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Common;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Inventario de métodos de un Controller, por reflexión.
///
/// Lo usa el combo «Método de la API» al configurar los botones de una opción
/// (AplicacionesOpcionesBotonesDialog). Vivía dentro de DeveloperMetadataController, que se
/// retiró junto con el Diseñador de Pantallas; pero esto no era parte de esa funcionalidad
/// —es una ayuda de Seguridad— y no toca la base: sólo lee los tipos del ensamblado.
///
/// Conserva la MISMA ruta que antes para no tener que cambiar la pantalla que lo consume.</summary>
[Authorize]
[SinControlPermisos("Metadatos de la API para la herramienta de desarrollo. NO administra datos del "
                    + "negocio, pero SÍ enumera la superficie del servicio: conviene dejarlo fuera "
                    + "del despliegue productivo o restringirlo por rol.")]
[Route("api/v1/developer/metadata")]
public class MetodosApiController : BaseController
{
    [HttpGet("metodos-controller")]
    public IActionResult GetMetodosController([FromQuery] string entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad))
            return BadRequest(ApiResponse<List<ComboboxItem>>.Fail("entidad requerida"));

        var nombreEntidad = NormalizarNombreEntidad(entidad);
        if (string.IsNullOrWhiteSpace(nombreEntidad))
            return BadRequest(ApiResponse<List<ComboboxItem>>.Fail("No se pudo determinar la entidad."));

        var nombreClase = $"{nombreEntidad}Controller";
        var controllers = typeof(MetodosApiController).Assembly.GetTypes()
            .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
            .ToList();

        var tipoController = controllers.FirstOrDefault(t => t.Name == nombreClase);
        if (tipoController is null)
        {
            // El nombre de la opción no siempre coincide 1:1 con el Controller (ej. opción
            // "Tipo de Cambio" → entidad "TipoCambio", pero el Controller real es
            // "MonedasTipoCambioController"). Se acepta también un Controller cuyo nombre
            // TERMINE en "{Entidad}Controller", siempre que sea el único candidato: con dos
            // o más se prefiere pedir el nombre exacto antes que adivinar mal.
            var candidatos = controllers.Where(t => t.Name.EndsWith(nombreClase, StringComparison.Ordinal)).ToList();
            if (candidatos.Count == 1) tipoController = candidatos[0];
        }

        if (tipoController is null)
            return BadRequest(ApiResponse<List<ComboboxItem>>.Fail(
                $"No se encontró el Controller \"{nombreClase}\". Verifique que la entidad tenga un Controller con ese nombre exacto."));

        var metodos = tipoController
            .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(m => !m.IsSpecialName)   // descarta get_/set_ de propiedades
            .Select(m => new ComboboxItem
            {
                Id = m.Name,
                Texto = $"{m.Name} ({VerboHttp(m)})"
            })
            .OrderBy(c => c.Texto)
            .ToList();

        return Ok(ApiResponse<List<ComboboxItem>>.Ok(metodos, total: metodos.Count));
    }

    /// <summary>Se queda con lo que esté después de la última "/" o ".", de modo que sirve
    /// tanto una ruta de formulario ("Logistica/Almacenes.razor") como un "SCHEMA.TABLA".</summary>
    private static string NormalizarNombreEntidad(string entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad)) return "";
        var segmento = entidad.Trim();

        var idxSlash = segmento.LastIndexOfAny(new[] { '/', '\\' });
        if (idxSlash >= 0) segmento = segmento[(idxSlash + 1)..];

        var idxPunto = segmento.LastIndexOf('.');
        if (idxPunto >= 0) segmento = segmento[(idxPunto + 1)..];

        if (segmento.EndsWith("razor", StringComparison.OrdinalIgnoreCase))
            segmento = segmento[..^"razor".Length].TrimEnd('.');

        return segmento;
    }

    private static string VerboHttp(MethodInfo m)
    {
        if (m.GetCustomAttribute<HttpGetAttribute>()    is not null) return "GET";
        if (m.GetCustomAttribute<HttpPostAttribute>()   is not null) return "POST";
        if (m.GetCustomAttribute<HttpPutAttribute>()    is not null) return "PUT";
        if (m.GetCustomAttribute<HttpDeleteAttribute>() is not null) return "DELETE";
        if (m.GetCustomAttribute<HttpPatchAttribute>()  is not null) return "PATCH";
        return "—";
    }
}
