using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/reservas-stock-detalle")]
[ExigePermiso("LOGISTICA.RESERVAS_STOCK_DETALLE")]
public class ReservasStockDetalleController : BaseController
{
    private readonly IReservasStockDetalleService _service;
    public ReservasStockDetalleController(IReservasStockDetalleService service) => _service = service;

    // El detalle se lee siempre dentro de su documento: el procedimiento lo exige.
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int NUM_RESERVA_STOCK)
    { var r = await _service.GetAllAsync(NUM_RESERVA_STOCK); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_RESERVA_STOCK}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(int NUM_RESERVA_STOCK, int NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(NUM_RESERVA_STOCK, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("por-cabecera/{NUM_RESERVA_STOCK}")]
    public async Task<IActionResult> GetPorCabecera(int NUM_RESERVA_STOCK)
    { var r = await _service.GetAllAsync(NUM_RESERVA_STOCK); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ReservasStockDetalleSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ReservasStockDetalleInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ReservasStockDetalleUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_RESERVA_STOCK}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(int NUM_RESERVA_STOCK, int NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(NUM_RESERVA_STOCK, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

}