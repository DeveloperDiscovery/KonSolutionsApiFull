using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/entidades-documentos-identidad")]
[ExigePermiso("ESTANDAR.ENTIDADES_DOCUMENTOS_IDENTIDAD")]
public class EntidadesDocumentosIdentidadController : BaseController
{
    private readonly IEntidadesDocumentosIdentidadService _service;
    public EntidadesDocumentosIdentidadController(IEntidadesDocumentosIdentidadService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal codEntidad)
    { var r = await _service.GetAllAsync(codEntidad); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ENTIDAD}/{COD_TIPO_DOCUMENTO_IDENTIDAD}")]
    public async Task<IActionResult> GetById(decimal COD_ENTIDAD, int COD_TIPO_DOCUMENTO_IDENTIDAD)
    { var r = await _service.GetByIdAsync(COD_ENTIDAD, COD_TIPO_DOCUMENTO_IDENTIDAD); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesDocumentosIdentidadSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesDocumentosIdentidadInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EntidadesDocumentosIdentidadUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ENTIDAD}/{COD_TIPO_DOCUMENTO_IDENTIDAD}")]
    public async Task<IActionResult> Delete(decimal COD_ENTIDAD, int COD_TIPO_DOCUMENTO_IDENTIDAD)
    { var r = await _service.DeleteAsync(COD_ENTIDAD, COD_TIPO_DOCUMENTO_IDENTIDAD); return r.Success ? Ok(r) : BadRequest(r); }

}