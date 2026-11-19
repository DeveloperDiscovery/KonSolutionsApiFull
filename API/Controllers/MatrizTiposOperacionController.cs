using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Matriz de tipos de operacion de una clase: que valores de calculo y que tipos de comprobante tiene cada tipo.
/// Los POST marcan (ACTIVO = true) o desmarcan (false) una celda.</summary>
[Authorize]
[Route("api/v1/estandar/matriz-tipos-operacion")]
public class MatrizTiposOperacionController : BaseController
{
    private readonly IMatrizTiposOperacionService _service;
    public MatrizTiposOperacionController(IMatrizTiposOperacionService service) => _service = service;

    [HttpGet("calculo")]
    [ExigePermiso("COMERCIAL.TIPOS_OPERACION_CALCULO")]
    public async Task<IActionResult> Calculo([FromQuery] string codClaseOperacion)
    { var r = await _service.CalculoAsync(codClaseOperacion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("calculo")]
    [ExigePermiso("COMERCIAL.TIPOS_OPERACION_CALCULO")]
    public async Task<IActionResult> CalculoSet([FromBody] MatrizCalculoSetDto dto)
    { var r = await _service.CalculoSetAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("comprobantes")]
    [ExigePermiso("COMERCIAL.TIPOS_OPERACION_TIPOS_COMPROBANTE_PAGO")]
    public async Task<IActionResult> Comprobantes([FromQuery] string codClaseOperacion)
    { var r = await _service.ComprobantesAsync(codClaseOperacion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("comprobantes")]
    [ExigePermiso("COMERCIAL.TIPOS_OPERACION_TIPOS_COMPROBANTE_PAGO")]
    public async Task<IActionResult> ComprobanteSet([FromBody] MatrizComprobanteSetDto dto)
    { var r = await _service.ComprobanteSetAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Formulas (valores que suma cada valor de calculo) de todos los tipos de operacion de la clase.</summary>
    [HttpGet("formulas")]
    [ExigePermiso("COMERCIAL.TIPOS_OPERACION_CALCULO")]
    public async Task<IActionResult> Formulas([FromQuery] string codClaseOperacion)
    { var r = await _service.FormulasAsync(codClaseOperacion); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Reemplaza la formula de un valor en un tipo de operacion (y, si se pide, su orden y flags).</summary>
    [HttpPost("formulas")]
    [ExigePermiso("COMERCIAL.TIPOS_OPERACION_CALCULO")]
    public async Task<IActionResult> FormulaSet([FromBody] FormulaSetDto dto)
    { var r = await _service.FormulaSetAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }
}
