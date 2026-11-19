using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/finanzas/comprobantes-pago")]
[ExigePermiso("FINANZAS.COMPROBANTES_PAGO")]
public class ComprobantesPagoController : BaseController
{
    private readonly IComprobantesPagoService _service;
    public ComprobantesPagoController(IComprobantesPagoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_REGISTRO}")]
    public async Task<IActionResult> GetById(decimal NUM_REGISTRO)
    { var r = await _service.GetByIdAsync(NUM_REGISTRO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ComprobantesPagoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ComprobantesPagoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ComprobantesPagoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_REGISTRO}")]
    public async Task<IActionResult> Delete(decimal NUM_REGISTRO)
    { var r = await _service.DeleteAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

}