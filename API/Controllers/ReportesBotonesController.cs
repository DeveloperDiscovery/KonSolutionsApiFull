using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Reportes;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Reporteador: qué diseño dispara cada botón de las opciones y de dónde sale cada parámetro.
/// Pantalla Reportes/ReportesBotones.razor.</summary>
[Authorize]
[Route("api/v1/reportes/botones")]
[ExigePermiso("REPORTES.OPCIONES_BOTONES_REPORTES")]
public class ReportesBotonesController : BaseController
{
    private readonly IReportesService _service;
    public ReportesBotonesController(IReportesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? COD_APLICACION, [FromQuery] string? buscar, [FromQuery] bool soloAsignados = false)
    { var r = await _service.BotonesAsync(COD_APLICACION, buscar, soloAsignados); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Un botón con los parámetros del diseño (el asignado o el que se está eligiendo).</summary>
    [HttpGet("detalle")]
    public async Task<IActionResult> Detalle([FromQuery] string? COD_APLICACION, [FromQuery] string? COD_OPCION_APLICACION,
                                             [FromQuery] string? COD_BOTON_OPCION, [FromQuery] int? NUM_DISENO)
    { var r = await _service.BotonAsync(COD_APLICACION, COD_OPCION_APLICACION, COD_BOTON_OPCION, NUM_DISENO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Los diseños, para elegir el del botón (con el permiso de esta pantalla).</summary>
    [HttpGet("disenos")]
    public async Task<IActionResult> Disenos()
    { var r = await _service.DisenosAsync(null); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Las rutas de archivos (ENTORNO.PATHFILES) donde el botón puede guardar una copia de lo que genera.</summary>
    [HttpGet("rutas")]
    public async Task<IActionResult> Rutas()
    { var r = await _service.RutasAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Grabar([FromBody] ReporteBotonGrabarDto dto)
    { var r = await _service.BotonGrabarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Quita el reporte del botón; el botón sigue en la opción.</summary>
    [HttpDelete]
    public async Task<IActionResult> Quitar([FromQuery] string? COD_APLICACION, [FromQuery] string? COD_OPCION_APLICACION, [FromQuery] string? COD_BOTON_OPCION)
    { var r = await _service.BotonEliminarAsync(COD_APLICACION, COD_OPCION_APLICACION, COD_BOTON_OPCION); return r.Success ? Ok(r) : BadRequest(r); }
}
