using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/finanzas/comprobantes-pago-cronograma-cancelacion")]
[ExigePermiso("FINANZAS.COMPROBANTES_PAGO_CRONOGRAMA_CANCELACION")]
public class ComprobantesPagoCronogramaCancelacionController : BaseController
{
    private readonly IComprobantesPagoCronogramaCancelacionService _service;
    public ComprobantesPagoCronogramaCancelacionController(IComprobantesPagoCronogramaCancelacionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_REGISTRO}/{FEC_VENCIMIENTO}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO, int NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(NUM_REGISTRO, FEC_VENCIMIENTO, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ComprobantesPagoCronogramaCancelacionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ComprobantesPagoCronogramaCancelacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ComprobantesPagoCronogramaCancelacionUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_REGISTRO}/{FEC_VENCIMIENTO}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO, int NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(NUM_REGISTRO, FEC_VENCIMIENTO, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

}