using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/unidades-negocio")]
[ExigePermiso("COMERCIAL.UNIDADES_NEGOCIO")]
public class UnidadesNegocioController : BaseController
{
    private readonly IUnidadesNegocioService _service;
    public UnidadesNegocioController(IUnidadesNegocioService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_UNIDAD_NEGOCIO}")]
    public async Task<IActionResult> GetById(int NUM_UNIDAD_NEGOCIO)
    { var r = await _service.GetByIdAsync(NUM_UNIDAD_NEGOCIO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] UnidadesNegocioSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] UnidadesNegocioInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UnidadesNegocioUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_UNIDAD_NEGOCIO}")]
    public async Task<IActionResult> Delete(int NUM_UNIDAD_NEGOCIO)
    { var r = await _service.DeleteAsync(NUM_UNIDAD_NEGOCIO); return r.Success ? Ok(r) : BadRequest(r); }

}