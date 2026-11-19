using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/rrhh/solicitudes-reclutamiento-funciones")]
[ExigePermiso("RRHH.SOLICITUDES_RECLUTAMIENTO_FUNCIONES")]
public class SolicitudesReclutamientoFuncionesController : BaseController
{
    private readonly ISolicitudesReclutamientoFuncionesService _service;
    public SolicitudesReclutamientoFuncionesController(ISolicitudesReclutamientoFuncionesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_SOLICITUD_RECLUTAMIENTO}/{COD_FUNCION}")]
    public async Task<IActionResult> GetById(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_FUNCION)
    { var r = await _service.GetByIdAsync(NUM_SOLICITUD_RECLUTAMIENTO, COD_FUNCION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SolicitudesReclutamientoFuncionesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] SolicitudesReclutamientoFuncionesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_SOLICITUD_RECLUTAMIENTO}/{COD_FUNCION}")]
    public async Task<IActionResult> Delete(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_FUNCION)
    { var r = await _service.DeleteAsync(NUM_SOLICITUD_RECLUTAMIENTO, COD_FUNCION); return r.Success ? Ok(r) : BadRequest(r); }

}