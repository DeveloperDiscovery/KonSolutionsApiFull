using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/tipos-documento-identidad")]
[ExigePermiso("ESTANDAR.TIPOS_DOCUMENTO_IDENTIDAD")]
public class TiposDocumentoIdentidadController : BaseController
{
    private readonly ITiposDocumentoIdentidadService _service;
    public TiposDocumentoIdentidadController(ITiposDocumentoIdentidadService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_DOCUMENTO_IDENTIDAD}")]
    public async Task<IActionResult> GetById(int COD_TIPO_DOCUMENTO_IDENTIDAD)
    { var r = await _service.GetByIdAsync(COD_TIPO_DOCUMENTO_IDENTIDAD); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposDocumentoIdentidadSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposDocumentoIdentidadInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut("{COD_TIPO_DOCUMENTO_IDENTIDAD}")]
    public async Task<IActionResult> Update(int COD_TIPO_DOCUMENTO_IDENTIDAD, [FromBody] TiposDocumentoIdentidadInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(COD_TIPO_DOCUMENTO_IDENTIDAD, dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_DOCUMENTO_IDENTIDAD}")]
    public async Task<IActionResult> Delete(int COD_TIPO_DOCUMENTO_IDENTIDAD)
    { var r = await _service.DeleteAsync(COD_TIPO_DOCUMENTO_IDENTIDAD); return r.Success ? Ok(r) : BadRequest(r); }

}