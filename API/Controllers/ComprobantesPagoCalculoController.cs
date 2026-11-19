using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/finanzas/comprobantes-pago-calculo")]
[ExigePermiso("FINANZAS.COMPROBANTES_PAGO_CALCULO")]
public class ComprobantesPagoCalculoController : BaseController
{
    private readonly IComprobantesPagoCalculoService _service;
    public ComprobantesPagoCalculoController(IComprobantesPagoCalculoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_REGISTRO}/{COD_TIPO_VALOR_CALCULO}")]
    public async Task<IActionResult> GetById(decimal NUM_REGISTRO, string COD_TIPO_VALOR_CALCULO)
    { var r = await _service.GetByIdAsync(NUM_REGISTRO, COD_TIPO_VALOR_CALCULO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ComprobantesPagoCalculoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ComprobantesPagoCalculoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ComprobantesPagoCalculoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_REGISTRO}/{COD_TIPO_VALOR_CALCULO}")]
    public async Task<IActionResult> Delete(decimal NUM_REGISTRO, string COD_TIPO_VALOR_CALCULO)
    { var r = await _service.DeleteAsync(NUM_REGISTRO, COD_TIPO_VALOR_CALCULO); return r.Success ? Ok(r) : BadRequest(r); }

}