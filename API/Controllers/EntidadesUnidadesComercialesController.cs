using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Pestaña "Unidades Comerciales" del diálogo de Entidades, después de Medios de
/// Contacto. PROC_..._INSERT es upsert (autonumera COD_UNIDAD_COMERCIAL si no existe) —
/// siempre POST, igual que EntidadesTiposMedioContactoController. El endpoint PUT queda
/// disponible por si se necesita en el futuro, pero el frontend actual no lo usa.</summary>
[Authorize]
[Route("api/v1/comercial/entidades-unidades-comerciales")]
[ExigePermiso("COMERCIAL.ENTIDADES_UNIDADES_COMERCIALES")]
public class EntidadesUnidadesComercialesController : BaseController
{
    private readonly IEntidadesUnidadesComercialesService _service;
    public EntidadesUnidadesComercialesController(IEntidadesUnidadesComercialesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal codEntidad)
    { var r = await _service.GetAllAsync(codEntidad); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{codEntidad}/{codUnidadComercial}")]
    public async Task<IActionResult> GetById(decimal codEntidad, byte codUnidadComercial)
    { var r = await _service.GetByIdAsync(codEntidad, codUnidadComercial); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesUnidadesComercialesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      dto.COD_USUARIO_REGISTRO = GetCodUsuario();
      dto.COD_ESTACION_REGISTRO = GetEstacion();
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EntidadesUnidadesComercialesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      dto.COD_USUARIO_REGISTRO = GetCodUsuario();
      dto.COD_ESTACION_REGISTRO = GetEstacion();
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{codEntidad}/{codUnidadComercial}")]
    public async Task<IActionResult> Delete(decimal codEntidad, byte codUnidadComercial)
    { var r = await _service.DeleteAsync(codEntidad, codUnidadComercial); return r.Success ? Ok(r) : BadRequest(r); }
}
