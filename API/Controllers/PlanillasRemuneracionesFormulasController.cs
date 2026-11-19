using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/nomina/planillas-remuneraciones-formulas")]
[ExigePermiso("NOMINA.PLANILLAS_REMUNERACIONES_FORMULAS")]
public class PlanillasRemuneracionesFormulasController : BaseController
{
    private readonly IPlanillasRemuneracionesFormulasService _service;
    public PlanillasRemuneracionesFormulasController(IPlanillasRemuneracionesFormulasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_PLANILLA}/{COD_CONCEPTO_REMUNERATIVO}/{NUM_SECUENCIA_FORMULA}")]
    public async Task<IActionResult> GetById(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA_FORMULA)
    { var r = await _service.GetByIdAsync(COD_TIPO_PLANILLA, COD_CONCEPTO_REMUNERATIVO, NUM_SECUENCIA_FORMULA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PlanillasRemuneracionesFormulasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PlanillasRemuneracionesFormulasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PlanillasRemuneracionesFormulasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_PLANILLA}/{COD_CONCEPTO_REMUNERATIVO}/{NUM_SECUENCIA_FORMULA}")]
    public async Task<IActionResult> Delete(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA_FORMULA)
    { var r = await _service.DeleteAsync(COD_TIPO_PLANILLA, COD_CONCEPTO_REMUNERATIVO, NUM_SECUENCIA_FORMULA); return r.Success ? Ok(r) : BadRequest(r); }

}