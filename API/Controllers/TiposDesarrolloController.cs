using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/tipos-desarrollo")]
[ExigePermiso("INGENIERIA.TIPOS_DESARROLLO")]
public class TiposDesarrolloController : BaseController
{
    private readonly ITiposDesarrolloService _service;
    public TiposDesarrolloController(ITiposDesarrolloService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codClaseDesarrollo)
    { var r = await _service.GetAllAsync(codClaseDesarrollo); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_DESARROLLO}/{COD_TIPO_DESARROLLO}")]
    public async Task<IActionResult> GetById(string COD_CLASE_DESARROLLO, string COD_TIPO_DESARROLLO)
    { var r = await _service.GetByIdAsync(COD_CLASE_DESARROLLO, COD_TIPO_DESARROLLO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string codClaseDesarrollo)
    { var r = await _service.GetComboboxAsync(codClaseDesarrollo); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string codClaseDesarrollo, [FromQuery] TiposDesarrolloSearchDto dto)
    { var r = await _service.SearchAsync(codClaseDesarrollo, dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposDesarrolloInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_DESARROLLO}/{COD_TIPO_DESARROLLO}")]
    public async Task<IActionResult> Delete(string COD_CLASE_DESARROLLO, string COD_TIPO_DESARROLLO)
    { var r = await _service.DeleteAsync(COD_CLASE_DESARROLLO, COD_TIPO_DESARROLLO); return r.Success ? Ok(r) : BadRequest(r); }
}
