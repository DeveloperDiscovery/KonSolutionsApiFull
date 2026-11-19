using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/finanzas/operaciones-caja-bancos-detalle-calculo")]
[ExigePermiso("FINANZAS.OPERACIONES_CAJA_BANCOS_DETALLE_CALCULO")]
public class OperacionesCajaBancosDetalleCalculoController : BaseController
{
    private readonly IOperacionesCajaBancosDetalleCalculoService _service;
    public OperacionesCajaBancosDetalleCalculoController(IOperacionesCajaBancosDetalleCalculoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_OPERACION}/{NUM_SECUENCIA}/{COD_TIPO_VALOR_CALCULO}")]
    public async Task<IActionResult> GetById(decimal NUM_OPERACION, int NUM_SECUENCIA, string COD_TIPO_VALOR_CALCULO)
    { var r = await _service.GetByIdAsync(NUM_OPERACION, NUM_SECUENCIA, COD_TIPO_VALOR_CALCULO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] OperacionesCajaBancosDetalleCalculoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] OperacionesCajaBancosDetalleCalculoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] OperacionesCajaBancosDetalleCalculoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_OPERACION}/{NUM_SECUENCIA}/{COD_TIPO_VALOR_CALCULO}")]
    public async Task<IActionResult> Delete(decimal NUM_OPERACION, int NUM_SECUENCIA, string COD_TIPO_VALOR_CALCULO)
    { var r = await _service.DeleteAsync(NUM_OPERACION, NUM_SECUENCIA, COD_TIPO_VALOR_CALCULO); return r.Success ? Ok(r) : BadRequest(r); }

}