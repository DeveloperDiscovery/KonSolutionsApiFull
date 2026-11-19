using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/rrhh/solicitudes-reclutamiento-postulantes-tipos-evaluacion")]
[ExigePermiso("RRHH.SOLICITUDES_RECLUTAMIENTO_POSTULANTES_TIPOS_EVALUACION")]
public class SolicitudesReclutamientoPostulantesTiposEvaluacionController : BaseController
{
    private readonly ISolicitudesReclutamientoPostulantesTiposEvaluacionService _service;
    public SolicitudesReclutamientoPostulantesTiposEvaluacionController(ISolicitudesReclutamientoPostulantesTiposEvaluacionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_SOLICITUD_RECLUTAMIENTO}/{COD_ENTIDAD_POSTULANTE}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, int NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(NUM_SOLICITUD_RECLUTAMIENTO, COD_ENTIDAD_POSTULANTE, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SolicitudesReclutamientoPostulantesTiposEvaluacionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] SolicitudesReclutamientoPostulantesTiposEvaluacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] SolicitudesReclutamientoPostulantesTiposEvaluacionUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_SOLICITUD_RECLUTAMIENTO}/{COD_ENTIDAD_POSTULANTE}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, int NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(NUM_SOLICITUD_RECLUTAMIENTO, COD_ENTIDAD_POSTULANTE, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

}