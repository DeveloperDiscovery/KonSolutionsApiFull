using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/clases-desarrollo")]
[ExigePermiso("INGENIERIA.CLASES_DESARROLLO")]
public class ClasesDesarrolloController : BaseController
{
    private readonly IClasesDesarrolloService _service;
    public ClasesDesarrolloController(IClasesDesarrolloService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_DESARROLLO}")]
    public async Task<IActionResult> GetById(string COD_CLASE_DESARROLLO)
    { var r = await _service.GetByIdAsync(COD_CLASE_DESARROLLO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    // Sin endpoint /search: INGENIERIA.CLASES_DESARROLLO no tiene SP SEARCH.
    // La pantalla filtra en cliente sobre el listado completo.

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ClasesDesarrolloInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_DESARROLLO}")]
    public async Task<IActionResult> Delete(string COD_CLASE_DESARROLLO)
    { var r = await _service.DeleteAsync(COD_CLASE_DESARROLLO); return r.Success ? Ok(r) : BadRequest(r); }
}
