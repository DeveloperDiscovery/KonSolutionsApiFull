using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/contabilidad/movimientos-contables-detalle")]
[ExigePermiso("CONTABILIDAD.MOVIMIENTOS_CONTABLES_DETALLE")]
public class MovimientosContablesDetalleController : BaseController
{
    private readonly IMovimientosContablesDetalleService _service;
    public MovimientosContablesDetalleController(IMovimientosContablesDetalleService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_PERIODO_CONTABLE}/{COD_ASIENTO_CONTABLE}/{NUM_ASIENTO_CONTABLE}")]
    public async Task<IActionResult> GetById(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE)
    { var r = await _service.GetByIdAsync(NUM_PERIODO_CONTABLE, COD_ASIENTO_CONTABLE, NUM_ASIENTO_CONTABLE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] MovimientosContablesDetalleSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] MovimientosContablesDetalleInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] MovimientosContablesDetalleUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_PERIODO_CONTABLE}/{COD_ASIENTO_CONTABLE}/{NUM_ASIENTO_CONTABLE}")]
    public async Task<IActionResult> Delete(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE)
    { var r = await _service.DeleteAsync(NUM_PERIODO_CONTABLE, COD_ASIENTO_CONTABLE, NUM_ASIENTO_CONTABLE); return r.Success ? Ok(r) : BadRequest(r); }

}