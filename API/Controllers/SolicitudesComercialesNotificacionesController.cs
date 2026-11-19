using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

// Solo GET: las notificaciones de una solicitud las genera el flujo interno
// (PROC_COMERCIAL_SOLICITUDES_COMERCIALES_NOTIFICACION), la UI únicamente las consulta.
[Authorize]
[Route("api/v1/comercial/solicitudes-comerciales-notificaciones")]
[ExigePermiso("COMERCIAL.SOLICITUDES_COMERCIALES_NOTIFICACIONES")]
public class SolicitudesComercialesNotificacionesController : BaseController
{
    private readonly ISolicitudesComercialesNotificacionesService _service;
    public SolicitudesComercialesNotificacionesController(ISolicitudesComercialesNotificacionesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int numSolicitudComercial)
    { var r = await _service.GetAllAsync(numSolicitudComercial); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_SOLICITUD_COMERCIAL}/{NUM_NOTIFICACION}")]
    public async Task<IActionResult> GetById(int NUM_SOLICITUD_COMERCIAL, int NUM_NOTIFICACION)
    { var r = await _service.GetByIdAsync(NUM_SOLICITUD_COMERCIAL, NUM_NOTIFICACION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SolicitudesComercialesNotificacionesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }
}
