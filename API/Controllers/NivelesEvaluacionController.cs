using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/rrhh/niveles-evaluacion")]
[ExigePermiso("RRHH.NIVELES_EVALUACION")]
public class NivelesEvaluacionController : BaseController
{
    private readonly INivelesEvaluacionService _service;
    public NivelesEvaluacionController(INivelesEvaluacionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_NIVEL_EVALUACION}")]
    public async Task<IActionResult> GetById(int NUM_NIVEL_EVALUACION)
    { var r = await _service.GetByIdAsync(NUM_NIVEL_EVALUACION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] NivelesEvaluacionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] NivelesEvaluacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] NivelesEvaluacionUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_NIVEL_EVALUACION}")]
    public async Task<IActionResult> Delete(int NUM_NIVEL_EVALUACION)
    { var r = await _service.DeleteAsync(NUM_NIVEL_EVALUACION); return r.Success ? Ok(r) : BadRequest(r); }

}