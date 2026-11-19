using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/finanzas/rendicion-cuentas-detalle")]
[ExigePermiso("FINANZAS.RENDICION_CUENTAS_DETALLE")]
public class RendicionCuentasDetalleController : BaseController
{
    private readonly IRendicionCuentasDetalleService _service;
    public RendicionCuentasDetalleController(IRendicionCuentasDetalleService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_RENDICION_CUENTAS}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(int NUM_RENDICION_CUENTAS, int NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(NUM_RENDICION_CUENTAS, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] RendicionCuentasDetalleSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] RendicionCuentasDetalleInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RendicionCuentasDetalleUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_RENDICION_CUENTAS}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(int NUM_RENDICION_CUENTAS, int NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(NUM_RENDICION_CUENTAS, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

}