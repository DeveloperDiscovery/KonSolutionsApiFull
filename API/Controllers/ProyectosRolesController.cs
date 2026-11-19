using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/proyectos/proyectos-roles")]
[ExigePermiso("PROYECTOS.PROYECTOS_ROLES")]
public class ProyectosRolesController : BaseController
{
    private readonly IProyectosRolesService _service;
    public ProyectosRolesController(IProyectosRolesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ROL_TAREA}")]
    public async Task<IActionResult> GetById(string COD_ROL_TAREA)
    { var r = await _service.GetByIdAsync(COD_ROL_TAREA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("categorias/combobox")]
    public async Task<IActionResult> GetCategoriasCombo()
    { var r = await _service.GetCategoriasComboAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ProyectosRolesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ProyectosRolesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProyectosRolesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ROL_TAREA}")]
    public async Task<IActionResult> Delete(string COD_ROL_TAREA)
    { var r = await _service.DeleteAsync(COD_ROL_TAREA); return r.Success ? Ok(r) : BadRequest(r); }

}