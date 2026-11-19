using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/proyectos/tipos-documento-compromiso")]
[ExigePermiso("PROYECTOS.TIPOS_DOCUMENTO_COMPROMISO")]
public class TiposDocumentoCompromisoController : BaseController
{
    private readonly ITiposDocumentoCompromisoService _service;
    public TiposDocumentoCompromisoController(ITiposDocumentoCompromisoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_DOCUMENTO_COMPROMISO}")]
    public async Task<IActionResult> GetById(string COD_TIPO_DOCUMENTO_COMPROMISO) { var r = await _service.GetByIdAsync(COD_TIPO_DOCUMENTO_COMPROMISO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposDocumentoCompromisoSearchDto dto) { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposDocumentoCompromisoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposDocumentoCompromisoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); var r = await _service.UpdateAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_DOCUMENTO_COMPROMISO}")]
    public async Task<IActionResult> Delete(string COD_TIPO_DOCUMENTO_COMPROMISO) { var r = await _service.DeleteAsync(COD_TIPO_DOCUMENTO_COMPROMISO); return r.Success ? Ok(r) : BadRequest(r); }
}
