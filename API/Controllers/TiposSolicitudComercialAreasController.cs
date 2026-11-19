using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Sub-módulo hijo de Tipos de Solicitud Comercial: qué áreas administrativas
/// participan de cada tipo de solicitud (tabla puente COD_TIPO_SOLICITUD_COMERICAL+COD_AREA).</summary>
[Authorize]
[Route("api/v1/comercial/tipos-solicitud-comercial-areas")]
[ExigePermiso("COMERCIAL.TIPOS_SOLICITUD_COMERCIAL_AREAS")]
public class TiposSolicitudComercialAreasController : BaseController
{
    private readonly ITiposSolicitudComercialAreasService _service;
    public TiposSolicitudComercialAreasController(ITiposSolicitudComercialAreasService service) => _service = service;

    [HttpGet("{codTipoSolicitudComercial}")]
    public async Task<IActionResult> GetAll(string codTipoSolicitudComercial)
    { var r = await _service.GetAllAsync(codTipoSolicitudComercial); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{codTipoSolicitudComercial}/{codArea}")]
    public async Task<IActionResult> GetById(string codTipoSolicitudComercial, int codArea)
    { var r = await _service.GetByIdAsync(codTipoSolicitudComercial, codArea); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("{codTipoSolicitudComercial}/combobox")]
    public async Task<IActionResult> GetCombobox(string codTipoSolicitudComercial)
    { var r = await _service.GetComboboxAsync(codTipoSolicitudComercial); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposSolicitudComercialAreasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposSolicitudComercialAreasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{codTipoSolicitudComercial}/{codArea}")]
    public async Task<IActionResult> Delete(string codTipoSolicitudComercial, int codArea)
    { var r = await _service.DeleteAsync(codTipoSolicitudComercial, codArea); return r.Success ? Ok(r) : BadRequest(r); }
}
