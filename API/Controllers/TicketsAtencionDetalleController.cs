using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Soporte;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/soporte/tickets-atencion-detalle")]
[ExigePermiso("SOPORTE.TICKETS_ATENCION_DETALLE")]
public class TicketsAtencionDetalleController : BaseController
{
    private readonly ITicketsAtencionDetalleService _service;
    public TicketsAtencionDetalleController(ITicketsAtencionDetalleService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_TICKET}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(int NUM_TICKET, int NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(NUM_TICKET, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TicketsAtencionDetalleSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT de este módulo ya no hace upsert.
    // POST = crear, PUT = modificar.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TicketsAtencionDetalleInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TicketsAtencionDetalleUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_TICKET}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(int NUM_TICKET, int NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(NUM_TICKET, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

}