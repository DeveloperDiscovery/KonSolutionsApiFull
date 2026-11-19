using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/entorno/dominios-empresa")]
[ExigePermiso("ENTORNO.DOMINIOS_EMPRESA")]
public class DominiosEmpresaController : BaseController
{
    private readonly IDominiosEmpresaService _service;
    public DominiosEmpresaController(IDominiosEmpresaService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_DOMINIO_EMPRESA}")]
    public async Task<IActionResult> GetById(byte COD_DOMINIO_EMPRESA)
    { var r = await _service.GetByIdAsync(COD_DOMINIO_EMPRESA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] DominiosEmpresaInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_DOMINIO_EMPRESA}")]
    public async Task<IActionResult> Delete(byte COD_DOMINIO_EMPRESA)
    { var r = await _service.DeleteAsync(COD_DOMINIO_EMPRESA); return r.Success ? Ok(r) : BadRequest(r); }
}
