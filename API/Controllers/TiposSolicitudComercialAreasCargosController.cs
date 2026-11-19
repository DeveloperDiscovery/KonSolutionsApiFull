using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Nieto de Tipos de Solicitud Comercial (vía Áreas): qué cargos de cada área
/// participan en el flujo de aprobación (PK compuesta COD_TIPO_SOLICITUD_COMERICAL+COD_AREA+COD_CARGO).</summary>
[Authorize]
[Route("api/v1/comercial/tipos-solicitud-comercial-areas-cargos")]
[ExigePermiso("COMERCIAL.TIPOS_SOLICITUD_COMERCIAL_AREAS_CARGOS")]
public class TiposSolicitudComercialAreasCargosController : BaseController
{
    private readonly ITiposSolicitudComercialAreasCargosService _service;
    public TiposSolicitudComercialAreasCargosController(ITiposSolicitudComercialAreasCargosService service) => _service = service;

    [HttpGet("{codTipoSolicitudComercial}/{codArea}")]
    public async Task<IActionResult> GetAll(string codTipoSolicitudComercial, int codArea)
    { var r = await _service.GetAllAsync(codTipoSolicitudComercial, codArea); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{codTipoSolicitudComercial}/{codArea}/{codCargo}")]
    public async Task<IActionResult> GetById(string codTipoSolicitudComercial, int codArea, int codCargo)
    { var r = await _service.GetByIdAsync(codTipoSolicitudComercial, codArea, codCargo); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposSolicitudComercialAreasCargosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_CARGOS_LOADCOMBOS: Tipos de
    /// Solicitud Comercial + Cargos (universo completo) para
    /// TiposSolicitudComercialAreasCargos.razor en un solo viaje.</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos()
    { var r = await _service.LoadcombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposSolicitudComercialAreasCargosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{codTipoSolicitudComercial}/{codArea}/{codCargo}")]
    public async Task<IActionResult> Delete(string codTipoSolicitudComercial, int codArea, int codCargo)
    { var r = await _service.DeleteAsync(codTipoSolicitudComercial, codArea, codCargo); return r.Success ? Ok(r) : BadRequest(r); }
}
