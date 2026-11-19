using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/solicitudes-comerciales")]
[ExigePermiso("COMERCIAL.SOLICITUDES_COMERCIALES")]
public class SolicitudesComercialesController : BaseController
{
    private readonly ISolicitudesComercialesService _service;
    public SolicitudesComercialesController(ISolicitudesComercialesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{numSolicitudComercial}")]
    public async Task<IActionResult> GetById(int numSolicitudComercial)
    { var r = await _service.GetByIdAsync(numSolicitudComercial); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] decimal? codEntidadCliente)
    { var r = await _service.GetComboboxAsync(codEntidadCliente); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SolicitudesComercialesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] SolicitudesComercialesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{numSolicitudComercial}")]
    public async Task<IActionResult> Delete(int numSolicitudComercial)
    { var r = await _service.DeleteAsync(numSolicitudComercial, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("asignar-responsable")]
    public async Task<IActionResult> AsignarResponsable([FromBody] SolicitudesComercialesAsignarResponsableDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.AsignarResponsableAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("reasigna-asesor")]
    public async Task<IActionResult> ReasignaAsesor([FromBody] SolicitudesComercialesReasignaAsesorDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.ReasignaAsesorAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos()
    { var r = await _service.GetLoadcombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }
}
