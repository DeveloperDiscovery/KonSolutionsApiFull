using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/finanzas/comprobantes-pago-movimientos-almacen")]
[ExigePermiso("FINANZAS.COMPROBANTES_PAGO_MOVIMIENTOS_ALMACEN")]
public class ComprobantesPagoMovimientosAlmacenController : BaseController
{
    private readonly IComprobantesPagoMovimientosAlmacenService _service;
    public ComprobantesPagoMovimientosAlmacenController(IComprobantesPagoMovimientosAlmacenService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_REGISTRO}/{NUM_SECUENCIA_COMPROBANTE}/{NUM_MOVIMIENTO_ALMACEN}")]
    public async Task<IActionResult> GetById(decimal NUM_REGISTRO, int NUM_SECUENCIA_COMPROBANTE, decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.GetByIdAsync(NUM_REGISTRO, NUM_SECUENCIA_COMPROBANTE, NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ComprobantesPagoMovimientosAlmacenSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ComprobantesPagoMovimientosAlmacenInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_REGISTRO}/{NUM_SECUENCIA_COMPROBANTE}/{NUM_MOVIMIENTO_ALMACEN}")]
    public async Task<IActionResult> Delete(decimal NUM_REGISTRO, int NUM_SECUENCIA_COMPROBANTE, decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.DeleteAsync(NUM_REGISTRO, NUM_SECUENCIA_COMPROBANTE, NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

}