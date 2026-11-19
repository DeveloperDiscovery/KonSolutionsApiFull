using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/unidades-comerciales")]
[ExigePermiso("COMERCIAL.UNIDADES_COMERCIALES")]
public class UnidadesComercialesController : BaseController
{
    private readonly IUnidadesComercialesService _service;
    public UnidadesComercialesController(IUnidadesComercialesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_UNIDAD_COMERCIAL}")]
    public async Task<IActionResult> GetById(int NUM_UNIDAD_COMERCIAL)
    { var r = await _service.GetByIdAsync(NUM_UNIDAD_COMERCIAL); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] UnidadesComercialesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] UnidadesComercialesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UnidadesComercialesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_UNIDAD_COMERCIAL}")]
    public async Task<IActionResult> Delete(int NUM_UNIDAD_COMERCIAL)
    { var r = await _service.DeleteAsync(NUM_UNIDAD_COMERCIAL); return r.Success ? Ok(r) : BadRequest(r); }

}