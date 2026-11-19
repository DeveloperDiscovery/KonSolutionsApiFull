using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/proyectos/proyectos-probabilidad-riesgo")]
[ExigePermiso("PROYECTOS.PROYECTOS_PROBABILIDAD_RIESGO")]
public class ProyectosProbabilidadRiesgoController : BaseController
{
    private readonly IProyectosProbabilidadRiesgoService _service;
    public ProyectosProbabilidadRiesgoController(IProyectosProbabilidadRiesgoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_PROBABILIDAD_RIESGO}")]
    public async Task<IActionResult> GetById(int COD_PROBABILIDAD_RIESGO)
    { var r = await _service.GetByIdAsync(COD_PROBABILIDAD_RIESGO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ProyectosProbabilidadRiesgoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ProyectosProbabilidadRiesgoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProyectosProbabilidadRiesgoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_PROBABILIDAD_RIESGO}")]
    public async Task<IActionResult> Delete(int COD_PROBABILIDAD_RIESGO)
    { var r = await _service.DeleteAsync(COD_PROBABILIDAD_RIESGO); return r.Success ? Ok(r) : BadRequest(r); }

}