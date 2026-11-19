using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Common;
using KonSolutionsApi.Infrastructure.Realtime;

namespace KonSolutionsApi.API.Controllers;

/// <summary>
/// Expone las versiones de cambio por tabla (desde memoria, sin tocar la BD).
/// El frontend consulta esto cada cierto intervalo para saber qué grillas refrescar.
/// </summary>
[Authorize]
[ExigePermiso("ENTORNO.CONTROL_CAMBIOS")]
[Route("api/v1/entorno/control-cambios")]
public class ControlCambiosController : BaseController
{
    private readonly ControlCambiosStore _store;
    public ControlCambiosController(ControlCambiosStore store) => _store = store;

    /// <summary>Devuelve todas las versiones conocidas (tabla → versión).</summary>
    [HttpGet]
    public IActionResult GetTodas()
    {
        var data = _store.Todas();
        return Ok(ApiResponse<Dictionary<string, long>>.Ok(data));
    }

    /// <summary>Versión de una tabla específica (por query string para soportar el punto del schema).</summary>
    [HttpGet("version")]
    public IActionResult GetVersion([FromQuery] string desTabla)
    {
        var v = _store.VersionDe(desTabla);
        return Ok(ApiResponse<long>.Ok(v));
    }
}
