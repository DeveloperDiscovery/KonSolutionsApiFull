using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/tipos-solicitud-comercial")]
[ExigePermiso("COMERCIAL.TIPOS_SOLICITUD_COMERCIAL")]
public class TiposSolicitudComercialController : BaseController
{
    private readonly ITiposSolicitudComercialService _service;
    public TiposSolicitudComercialController(ITiposSolicitudComercialService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{codTipoSolicitudComercial}")]
    public async Task<IActionResult> GetById(string codTipoSolicitudComercial)
    { var r = await _service.GetByIdAsync(codTipoSolicitudComercial); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposSolicitudComercialInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{codTipoSolicitudComercial}")]
    public async Task<IActionResult> Delete(string codTipoSolicitudComercial)
    { var r = await _service.DeleteAsync(codTipoSolicitudComercial); return r.Success ? Ok(r) : BadRequest(r); }
}
