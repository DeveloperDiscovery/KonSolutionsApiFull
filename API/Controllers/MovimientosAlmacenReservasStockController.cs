using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/movimientos-almacen-reservas-stock")]
[ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN_RESERVAS_STOCK")]
public class MovimientosAlmacenReservasStockController : BaseController
{
    private readonly IMovimientosAlmacenReservasStockService _service;
    public MovimientosAlmacenReservasStockController(IMovimientosAlmacenReservasStockService service) => _service = service;

    // El movimiento es obligatorio: el SP lo exige y no tiene valor por defecto.
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.GetAllAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/{NUM_RESERVA_STOCK}")]
    public async Task<IActionResult> GetById(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK)
    { var r = await _service.GetByIdAsync(NUM_MOVIMIENTO_ALMACEN, NUM_RESERVA_STOCK); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] MovimientosAlmacenReservasStockSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] MovimientosAlmacenReservasStockInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    // Sin PUT, y no es un olvido: en una tabla de enlace la fila ENTERA es la clave, asi que
    // no hay nada que modificar. Su SP de alta ademas hace upsert, con lo cual volver a
    // asignar algo ya asignado es inofensivo.

    [HttpDelete("{NUM_MOVIMIENTO_ALMACEN}/{NUM_RESERVA_STOCK}")]
    public async Task<IActionResult> Delete(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK)
    { var r = await _service.DeleteAsync(NUM_MOVIMIENTO_ALMACEN, NUM_RESERVA_STOCK); return r.Success ? Ok(r) : BadRequest(r); }
}
