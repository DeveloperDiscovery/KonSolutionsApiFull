using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Reportes;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Reporteador: las fuentes de datos (los SP registrados), con sus parámetros, resultados y columnas.
/// Pantalla Reportes/ReportesFuentes.razor.</summary>
[Authorize]
[Route("api/v1/reportes/fuentes")]
[ExigePermiso("REPORTES.FUENTES")]
public class ReportesFuentesController : BaseController
{
    private readonly IReportesService _service;
    public ReportesFuentesController(IReportesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Listar()
    { var r = await _service.FuentesAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_FUENTE:int}")]
    public async Task<IActionResult> Uno(int NUM_FUENTE)
    { var r = await _service.FuenteAsync(NUM_FUENTE); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Los SP de la base, para elegir la fuente o el SP de un combo.</summary>
    [HttpGet("procedimientos")]
    public async Task<IActionResult> Procedimientos([FromQuery] string? buscar)
    { var r = await _service.ProcedimientosAsync(buscar); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Los parámetros que declara un SP.</summary>
    [HttpGet("parametros-sp")]
    public async Task<IActionResult> ParametrosSp([FromQuery] string? esquema, [FromQuery] string? procedimiento)
    { var r = await _service.ParametrosSpAsync(esquema, procedimiento); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Las etiquetas del diccionario para una lista de columnas.</summary>
    [HttpPost("etiquetas")]
    public async Task<IActionResult> Etiquetas([FromBody] List<string>? columnas)
    { var r = await _service.EtiquetasAsync(columnas); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Ejecuta el SP con valores de ejemplo y devuelve sus resultados, columnas y unas filas.</summary>
    [HttpPost("probar")]
    public async Task<IActionResult> Probar([FromBody] ReportePruebaDto dto)
    { var r = await _service.ProbarAsync(dto, GetCodUsuario(), GetCodRol(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Graba la fuente (nueva si NUM_FUENTE viene vacío) con sus parámetros, resultados y columnas.</summary>
    [HttpPost]
    public async Task<IActionResult> Grabar([FromBody] ReporteFuenteDetalle dto)
    { var r = await _service.FuenteGrabarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_FUENTE:int}")]
    public async Task<IActionResult> Eliminar(int NUM_FUENTE)
    { var r = await _service.FuenteEliminarAsync(NUM_FUENTE); return r.Success ? Ok(r) : BadRequest(r); }
}
