using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/proyectos/proyectos-unidades-medida-tareas")]
[ExigePermiso("PROYECTOS.PROYECTOS_UNIDADES_MEDIDA_TAREAS")]
public class ProyectosUnidadesMedidaTareasController : BaseController
{
    private readonly IProyectosUnidadesMedidaTareasService _service;
    public ProyectosUnidadesMedidaTareasController(IProyectosUnidadesMedidaTareasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_UNIDAD_MEDIDA_TAREAS}")]
    public async Task<IActionResult> GetById(string COD_UNIDAD_MEDIDA_TAREAS)
    { var r = await _service.GetByIdAsync(COD_UNIDAD_MEDIDA_TAREAS); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ProyectosUnidadesMedidaTareasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ProyectosUnidadesMedidaTareasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProyectosUnidadesMedidaTareasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_UNIDAD_MEDIDA_TAREAS}")]
    public async Task<IActionResult> Delete(string COD_UNIDAD_MEDIDA_TAREAS)
    { var r = await _service.DeleteAsync(COD_UNIDAD_MEDIDA_TAREAS); return r.Success ? Ok(r) : BadRequest(r); }

}