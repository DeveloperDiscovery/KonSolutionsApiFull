using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/tipos-empresa")]
[ExigePermiso("ESTANDAR.TIPOS_EMPRESA")]
public class TiposEmpresaController : BaseController
{
    private readonly ITiposEmpresaService _service;
    public TiposEmpresaController(ITiposEmpresaService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_EMPRESA}")]
    public async Task<IActionResult> GetById(string COD_TIPO_EMPRESA)
    { var r = await _service.GetByIdAsync(COD_TIPO_EMPRESA); return r.Success ? Ok(r) : NotFound(r); }

    // Antes [AllowAnonymous]: sin datos sensibles, pero ningún consumidor del Web
    // lo llama antes de autenticar — se retira la excepción innecesaria.
    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string? search = null)
    { var r = await _service.GetComboboxAsync(search); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposEmpresaSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposEmpresaInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposEmpresaUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_EMPRESA}")]
    public async Task<IActionResult> Delete(string COD_TIPO_EMPRESA)
    { var r = await _service.DeleteAsync(COD_TIPO_EMPRESA); return r.Success ? Ok(r) : BadRequest(r); }

}