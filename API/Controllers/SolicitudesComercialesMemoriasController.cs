using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/solicitudes-comerciales-memorias")]
[ExigePermiso("COMERCIAL.SOLICITUDES_COMERCIALES_MEMORIAS")]
public class SolicitudesComercialesMemoriasController : BaseController
{
    private readonly ISolicitudesComercialesMemoriasService _service;
    public SolicitudesComercialesMemoriasController(ISolicitudesComercialesMemoriasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int numSolicitudComercial)
    { var r = await _service.GetAllAsync(numSolicitudComercial); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_SOLICITUD_COMERCIAL}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(int NUM_SOLICITUD_COMERCIAL, short NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(NUM_SOLICITUD_COMERCIAL, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SolicitudesComercialesMemoriasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] SolicitudesComercialesMemoriasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_SOLICITUD_COMERCIAL}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(int NUM_SOLICITUD_COMERCIAL, short NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(NUM_SOLICITUD_COMERCIAL, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }
}
