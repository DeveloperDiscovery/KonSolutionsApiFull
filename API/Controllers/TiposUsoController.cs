using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/tipos-uso")]
[ExigePermiso("ESTANDAR.TIPOS_USO")]
public class TiposUsoController : BaseController
{
    private readonly ITiposUsoService _service;
    public TiposUsoController(ITiposUsoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_USO}")]
    public async Task<IActionResult> GetById(string COD_TIPO_USO)
    { var r = await _service.GetByIdAsync(COD_TIPO_USO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    // Sin endpoint /search: ESTANDAR.TIPOS_USO no tiene SP SEARCH.
    // La pantalla filtra en cliente sobre el listado completo.

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposUsoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_USO}")]
    public async Task<IActionResult> Delete(string COD_TIPO_USO)
    { var r = await _service.DeleteAsync(COD_TIPO_USO); return r.Success ? Ok(r) : BadRequest(r); }
}
