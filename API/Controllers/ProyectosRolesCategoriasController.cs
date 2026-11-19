using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/proyectos/proyectos-roles-categorias")]
[ExigePermiso("PROYECTOS.PROYECTOS_ROLES_CATEGORIAS")]
public class ProyectosRolesCategoriasController : BaseController
{
    private readonly IProyectosRolesCategoriasService _service;
    public ProyectosRolesCategoriasController(IProyectosRolesCategoriasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CATEGORIA_ROL}")]
    public async Task<IActionResult> GetById(string COD_CATEGORIA_ROL) { var r = await _service.GetByIdAsync(COD_CATEGORIA_ROL); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ProyectosRolesCategoriasSearchDto dto) { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ProyectosRolesCategoriasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProyectosRolesCategoriasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); var r = await _service.UpdateAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CATEGORIA_ROL}")]
    public async Task<IActionResult> Delete(string COD_CATEGORIA_ROL) { var r = await _service.DeleteAsync(COD_CATEGORIA_ROL); return r.Success ? Ok(r) : BadRequest(r); }
}
