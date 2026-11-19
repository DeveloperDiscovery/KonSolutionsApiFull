using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/reservas-stock")]
[ExigePermiso("LOGISTICA.RESERVAS_STOCK")]
public class ReservasStockController : BaseController
{
    private readonly IReservasStockService _service;
    public ReservasStockController(IReservasStockService service) => _service = service;

    // El filtro por Tipo de Reserva va [FromQuery] y es OPCIONAL: una llamada sin nada sigue
    // devolviendo el listado completo, como siempre.
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ReservasStockSearchDto? filtros)
    { var r = await _service.GetAllAsync(filtros); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_RESERVA_STOCK}")]
    public async Task<IActionResult> GetById(int NUM_RESERVA_STOCK)
    { var r = await _service.GetByIdAsync(NUM_RESERVA_STOCK); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ReservasStockSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ReservasStockInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ReservasStockUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_RESERVA_STOCK}")]
    public async Task<IActionResult> Delete(int NUM_RESERVA_STOCK)
    { var r = await _service.DeleteAsync(NUM_RESERVA_STOCK); return r.Success ? Ok(r) : BadRequest(r); }

}