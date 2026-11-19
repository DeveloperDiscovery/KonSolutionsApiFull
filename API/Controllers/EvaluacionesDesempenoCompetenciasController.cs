using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/rrhh/evaluaciones-desempeno-competencias")]
[ExigePermiso("RRHH.EVALUACIONES_DESEMPENO_COMPETENCIAS")]
public class EvaluacionesDesempenoCompetenciasController : BaseController
{
    private readonly IEvaluacionesDesempenoCompetenciasService _service;
    public EvaluacionesDesempenoCompetenciasController(IEvaluacionesDesempenoCompetenciasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_EVALUACION_DESEMPENO}/{COD_TIPO_COMPETENCIA}/{COD_COMPETENCIA}")]
    public async Task<IActionResult> GetById(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA, string COD_COMPETENCIA)
    { var r = await _service.GetByIdAsync(NUM_EVALUACION_DESEMPENO, COD_TIPO_COMPETENCIA, COD_COMPETENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EvaluacionesDesempenoCompetenciasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EvaluacionesDesempenoCompetenciasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EvaluacionesDesempenoCompetenciasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_EVALUACION_DESEMPENO}/{COD_TIPO_COMPETENCIA}/{COD_COMPETENCIA}")]
    public async Task<IActionResult> Delete(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA, string COD_COMPETENCIA)
    { var r = await _service.DeleteAsync(NUM_EVALUACION_DESEMPENO, COD_TIPO_COMPETENCIA, COD_COMPETENCIA); return r.Success ? Ok(r) : BadRequest(r); }

}