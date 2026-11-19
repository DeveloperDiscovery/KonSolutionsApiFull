using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

/// <summary>COMERCIAL.ENTIDADES_INFORMACION_COMERCIAL: condiciones comerciales pactadas con
/// una entidad por tipo de operación (moneda, condición y forma de pago). Alimenta la ficha
/// "Información Comercial" de EntidadesDialog.razor.</summary>
[Authorize]
[Route("api/v1/comercial/entidades-informacion-comercial")]
[ExigePermiso("COMERCIAL.ENTIDADES_INFORMACION_COMERCIAL")]
public class EntidadesInformacionComercialController : BaseController
{
    private readonly IEntidadesInformacionComercialService _service;
    public EntidadesInformacionComercialController(IEntidadesInformacionComercialService service) => _service = service;

    [HttpGet("{codEntidad}/{codClaseOperacion}")]
    public async Task<IActionResult> GetAll(decimal codEntidad, string codClaseOperacion)
    { var r = await _service.GetAllAsync(codEntidad, codClaseOperacion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{codEntidad}/{codClaseOperacion}/{codTipoOperacion}")]
    public async Task<IActionResult> GetById(decimal codEntidad, string codClaseOperacion, string codTipoOperacion)
    { var r = await _service.GetByIdAsync(codEntidad, codClaseOperacion, codTipoOperacion); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesInformacionComercialSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesInformacionComercialInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EntidadesInformacionComercialUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{codEntidad}/{codClaseOperacion}/{codTipoOperacion}")]
    public async Task<IActionResult> Delete(decimal codEntidad, string codClaseOperacion, string codTipoOperacion)
    { var r = await _service.DeleteAsync(codEntidad, codClaseOperacion, codTipoOperacion); return r.Success ? Ok(r) : BadRequest(r); }
}
