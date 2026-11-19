using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/finanzas/entidades-cuentas-bancarias")]
[ExigePermiso("FINANZAS.ENTIDADES_CUENTAS_BANCARIAS")]
public class EntidadesCuentasBancariasController : BaseController
{
    private readonly IEntidadesCuentasBancariasService _service;
    public EntidadesCuentasBancariasController(IEntidadesCuentasBancariasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal codEntidad)
    { var r = await _service.GetAllAsync(codEntidad); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ENTIDAD_BANCARIA}/{COD_BANCO}/{NUM_CUENTA_BANCARIA}")]
    public async Task<IActionResult> GetById(decimal COD_ENTIDAD_BANCARIA, string COD_BANCO, string NUM_CUENTA_BANCARIA)
    { var r = await _service.GetByIdAsync(COD_ENTIDAD_BANCARIA, COD_BANCO, NUM_CUENTA_BANCARIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesCuentasBancariasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesCuentasBancariasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EntidadesCuentasBancariasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ENTIDAD_BANCARIA}/{COD_BANCO}/{NUM_CUENTA_BANCARIA}")]
    public async Task<IActionResult> Delete(decimal COD_ENTIDAD_BANCARIA, string COD_BANCO, string NUM_CUENTA_BANCARIA)
    { var r = await _service.DeleteAsync(COD_ENTIDAD_BANCARIA, COD_BANCO, NUM_CUENTA_BANCARIA); return r.Success ? Ok(r) : BadRequest(r); }

}