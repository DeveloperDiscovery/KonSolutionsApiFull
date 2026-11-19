using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/movimientos-almacen-transporte")]
[ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN_TRANSPORTE")]
public class MovimientosAlmacenTransporteController : BaseController
{
    private readonly IMovimientosAlmacenTransporteService _service;
    public MovimientosAlmacenTransporteController(IMovimientosAlmacenTransporteService service) => _service = service;

    // El movimiento es obligatorio: el SP lo exige y no tiene valor por defecto.
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.GetAllAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/{COD_TRANSPORTE}")]
    public async Task<IActionResult> GetById(decimal NUM_MOVIMIENTO_ALMACEN, string COD_TRANSPORTE)
    { var r = await _service.GetByIdAsync(NUM_MOVIMIENTO_ALMACEN, COD_TRANSPORTE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] MovimientosAlmacenTransporteSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] MovimientosAlmacenTransporteInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] MovimientosAlmacenTransporteUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_MOVIMIENTO_ALMACEN}/{COD_TRANSPORTE}")]
    public async Task<IActionResult> Delete(decimal NUM_MOVIMIENTO_ALMACEN, string COD_TRANSPORTE)
    { var r = await _service.DeleteAsync(NUM_MOVIMIENTO_ALMACEN, COD_TRANSPORTE); return r.Success ? Ok(r) : BadRequest(r); }
}
