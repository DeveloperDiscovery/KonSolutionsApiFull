using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Tabla puente COMERCIAL.ENTIDADES_ASESORES_COMERCIALES: asesores comerciales
/// (personas) asignados a un cliente. Solo aplica al Catálogo de Clientes
/// (COD_TIPO_ENTIDAD = 'CLI') — ver tab "Asesores Comerciales" en EntidadesDialog.razor.</summary>
[Authorize]
[Route("api/v1/comercial/entidades-asesores-comerciales")]
[ExigePermiso("COMERCIAL.ENTIDADES_ASESORES_COMERCIALES")]
public class EntidadesAsesoresComercialesController : BaseController
{
    private readonly IEntidadesAsesoresComercialesService _service;
    public EntidadesAsesoresComercialesController(IEntidadesAsesoresComercialesService service) => _service = service;

    [HttpGet("{codEntidadCliente}")]
    public async Task<IActionResult> GetAll(decimal codEntidadCliente)
    { var r = await _service.GetAllAsync(codEntidadCliente); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{codEntidadCliente}/{codEntidadAsesorComercial}")]
    public async Task<IActionResult> GetById(decimal codEntidadCliente, decimal codEntidadAsesorComercial)
    { var r = await _service.GetByIdAsync(codEntidadCliente, codEntidadAsesorComercial); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesAsesoresComercialesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesAsesoresComercialesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{codEntidadCliente}/{codEntidadAsesorComercial}")]
    public async Task<IActionResult> Delete(decimal codEntidadCliente, decimal codEntidadAsesorComercial)
    { var r = await _service.DeleteAsync(codEntidadCliente, codEntidadAsesorComercial); return r.Success ? Ok(r) : BadRequest(r); }
}
