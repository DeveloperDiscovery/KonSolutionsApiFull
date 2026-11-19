using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/finanzas/bancos-cuentas-bancarias")]
[ExigePermiso("FINANZAS.BANCOS_CUENTAS_BANCARIAS")]
public class BancosCuentasBancariasController : BaseController
{
    private readonly IBancosCuentasBancariasService _service;
    public BancosCuentasBancariasController(IBancosCuentasBancariasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codBanco)
    { var r = await _service.GetAllAsync(codBanco); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_BANCO}/{NUM_CUENTA_BANCARIA}")]
    public async Task<IActionResult> GetById(string COD_BANCO, string NUM_CUENTA_BANCARIA)
    { var r = await _service.GetByIdAsync(COD_BANCO, NUM_CUENTA_BANCARIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] BancosCuentasBancariasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] BancosCuentasBancariasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] BancosCuentasBancariasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_BANCO}/{NUM_CUENTA_BANCARIA}")]
    public async Task<IActionResult> Delete(string COD_BANCO, string NUM_CUENTA_BANCARIA)
    { var r = await _service.DeleteAsync(COD_BANCO, NUM_CUENTA_BANCARIA); return r.Success ? Ok(r) : BadRequest(r); }

}