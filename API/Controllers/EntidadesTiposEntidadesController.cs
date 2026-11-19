using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/entidades-tipos-entidades")]
[ExigePermiso("ESTANDAR.ENTIDADES_TIPOS_ENTIDADES")]
public class EntidadesTiposEntidadesController : BaseController
{
    private readonly IEntidadesTiposEntidadesService _service;
    public EntidadesTiposEntidadesController(IEntidadesTiposEntidadesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ENTIDAD}/{COD_TIPO_ENTIDAD}")]
    public async Task<IActionResult> GetById(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD)
    { var r = await _service.GetByIdAsync(COD_ENTIDAD, COD_TIPO_ENTIDAD); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesTiposEntidadesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesTiposEntidadesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_ENTIDAD}/{COD_TIPO_ENTIDAD}")]
    public async Task<IActionResult> Delete(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD)
    { var r = await _service.DeleteAsync(COD_ENTIDAD, COD_TIPO_ENTIDAD); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("showselect/{COD_ENTIDAD}")]
    public async Task<IActionResult> Showselect(decimal COD_ENTIDAD)
    { var r = await _service.GetShowselectAsync(COD_ENTIDAD); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("showcomboxid/{COD_TIPO_ENTIDAD}")]
    public async Task<IActionResult> Showcomboxid(string COD_TIPO_ENTIDAD)
    { var r = await _service.GetComboboxidAsync(COD_TIPO_ENTIDAD); return r.Success ? Ok(r) : BadRequest(r); }

}