using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/proyectos/proyectos-tipos-entidad")]
[ExigePermiso("PROYECTOS.PROYECTOS")]
public class ProyectosTiposEntidadController : BaseController
{
    private readonly IProyectosTiposEntidadService _service;
    public ProyectosTiposEntidadController(IProyectosTiposEntidadService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_PROYECTO}/{COD_TIPO_ENTIDAD}")]
    public async Task<IActionResult> GetById(int NUM_PROYECTO, string COD_TIPO_ENTIDAD)
    { var r = await _service.GetByIdAsync(NUM_PROYECTO, COD_TIPO_ENTIDAD); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ProyectosTiposEntidadSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ProyectosTiposEntidadInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_PROYECTO}/{COD_TIPO_ENTIDAD}")]
    public async Task<IActionResult> Delete(int NUM_PROYECTO, string COD_TIPO_ENTIDAD)
    { var r = await _service.DeleteAsync(NUM_PROYECTO, COD_TIPO_ENTIDAD); return r.Success ? Ok(r) : BadRequest(r); }

}