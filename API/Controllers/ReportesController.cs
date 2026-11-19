using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Reportes;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Reporteador: generar un reporte desde el botón de una opción (lo usan todas las pantallas a través de
/// AccionesBotones).
///
/// No va con [ExigePermiso] porque no administra una tabla: el permiso de un reporte es el del botón que lo
/// dispara, y ese lo valida PROC_REPORTES_DEFINICION (el usuario es del rol y el rol tiene el botón habilitado).
/// Por eso aquí siempre se pide por botón: el número de diseño que mande el navegador se descarta.
///
/// El rol llega en la consulta y en el cuerpo, y no se toma del token: el usuario puede haber elegido otro rol en la
/// barra superior.</summary>
[Authorize]
[Route("api/v1/reportes")]
[SinControlPermisos("El permiso del reporte es el del botón: lo valida PROC_REPORTES_DEFINICION con el rol.")]
public class ReportesController : BaseController
{
    private readonly IReportesService _service;
    public ReportesController(IReportesService service) => _service = service;

    /// <summary>Los botones de la opción que generan un reporte, habilitados para el rol.</summary>
    [HttpGet("opcion-botones")]
    public async Task<IActionResult> OpcionBotones([FromQuery] string? COD_APLICACION, [FromQuery] string? COD_OPCION_APLICACION, [FromQuery] string? COD_ROL_USUARIO)
    { var r = await _service.OpcionBotonesAsync(COD_APLICACION, COD_OPCION_APLICACION, COD_ROL_USUARIO ?? GetCodRol()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Los parámetros y formatos del reporte del botón, para el diálogo de filtros.</summary>
    [HttpGet("dialogo")]
    public async Task<IActionResult> Dialogo([FromQuery] string? COD_APLICACION, [FromQuery] string? COD_OPCION_APLICACION,
                                             [FromQuery] string? COD_BOTON_OPCION, [FromQuery] string? COD_ROL_USUARIO)
    {
        if (string.IsNullOrWhiteSpace(COD_BOTON_OPCION)) return BadRequest(Common.ApiResponse<object>.Fail("Falta el botón."));
        var r = await _service.DialogoAsync(null, COD_APLICACION, COD_OPCION_APLICACION, COD_BOTON_OPCION, COD_ROL_USUARIO ?? GetCodRol(), GetCodUsuario());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpPost("combo")]
    public async Task<IActionResult> Combo([FromBody] ReporteComboDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.COD_BOTON_OPCION)) return BadRequest(Common.ApiResponse<object>.Fail("Falta el botón."));
        dto.NUM_DISENO = null;
        var r = await _service.ComboAsync(dto, GetCodUsuario(), GetCodRol(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    /// <summary>Genera el reporte. Devuelve el archivo en base64 dentro de la respuesta (con su nombre y tipo).</summary>
    [HttpPost("generar")]
    public async Task<IActionResult> Generar([FromBody] ReporteGenerarDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.COD_BOTON_OPCION)) return BadRequest(Common.ApiResponse<object>.Fail("Falta el botón."));
        dto.NUM_DISENO = null;
        var r = await _service.GenerarAsync(dto, GetCodUsuario(), GetCodRol(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }
}
