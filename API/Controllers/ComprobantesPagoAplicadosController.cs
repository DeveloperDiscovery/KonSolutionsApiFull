using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/finanzas/comprobantes-pago-aplicados")]
[ExigePermiso("FINANZAS.COMPROBANTES_PAGO_APLICADOS")]
public class ComprobantesPagoAplicadosController : BaseController
{
    private readonly IComprobantesPagoAplicadosService _service;
    public ComprobantesPagoAplicadosController(IComprobantesPagoAplicadosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_REGISTRO}/{NUM_SECUENCIA}/{NUM_REGISTRO_ASOCIADO}")]
    public async Task<IActionResult> GetById(decimal NUM_REGISTRO, int NUM_SECUENCIA, decimal NUM_REGISTRO_ASOCIADO)
    { var r = await _service.GetByIdAsync(NUM_REGISTRO, NUM_SECUENCIA, NUM_REGISTRO_ASOCIADO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ComprobantesPagoAplicadosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ComprobantesPagoAplicadosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_REGISTRO}/{NUM_SECUENCIA}/{NUM_REGISTRO_ASOCIADO}")]
    public async Task<IActionResult> Delete(decimal NUM_REGISTRO, int NUM_SECUENCIA, decimal NUM_REGISTRO_ASOCIADO)
    { var r = await _service.DeleteAsync(NUM_REGISTRO, NUM_SECUENCIA, NUM_REGISTRO_ASOCIADO); return r.Success ? Ok(r) : BadRequest(r); }

}