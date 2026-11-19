using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/solicitudes-comerciales-cliente")]
[ExigePermiso("COMERCIAL.SOLICITUDES_COMERCIALES_CLIENTE")]
public class SolicitudesComercialesClienteController : BaseController
{
    private readonly ISolicitudesComercialesClienteService _service;
    public SolicitudesComercialesClienteController(ISolicitudesComercialesClienteService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal codEntidadCliente)
    { var r = await _service.GetAllAsync(codEntidadCliente); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] decimal codEntidadCliente, [FromQuery] string? search)
    { var r = await _service.SearchAsync(codEntidadCliente, search); return r.Success ? Ok(r) : BadRequest(r); }
}
