using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/seguridad/roles-usuario-asientos-contables")]
[ExigePermiso("SEGURIDAD.ROLES_USUARIO_ASIENTOS_CONTABLES")]
public class RolesUsuarioAsientosContablesController : BaseController
{
    private readonly IRolesUsuarioAsientosContablesService _service;
    public RolesUsuarioAsientosContablesController(IRolesUsuarioAsientosContablesService service) => _service = service;

    [HttpPost("asignar")]
    public async Task<IActionResult> Asignar([FromBody] RolesUsuarioAsientosContablesLoteDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.AsignarAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("desasignar")]
    public async Task<IActionResult> Desasignar([FromBody] RolesUsuarioAsientosContablesLoteDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.DesasignarAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] RolesUsuarioAsientosContablesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("disponibles/{COD_ROL_USUARIO}")]
    public async Task<IActionResult> GetDisponibles(string COD_ROL_USUARIO)
    { var r = await _service.GetDisponiblesAsync(COD_ROL_USUARIO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("combobox/{COD_ROL_USUARIO}")]
    public async Task<IActionResult> GetCombobox(string COD_ROL_USUARIO)
    { var r = await _service.GetComboboxAsync(COD_ROL_USUARIO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ROL_USUARIO}")]
    public async Task<IActionResult> GetAll(string COD_ROL_USUARIO)
    { var r = await _service.GetAllAsync(COD_ROL_USUARIO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ROL_USUARIO}/{COD_ASIENTO_CONTABLE}")]
    public async Task<IActionResult> GetById(string COD_ROL_USUARIO, string COD_ASIENTO_CONTABLE)
    { var r = await _service.GetByIdAsync(COD_ROL_USUARIO, COD_ASIENTO_CONTABLE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] RolesUsuarioAsientosContablesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_ROL_USUARIO}/{COD_ASIENTO_CONTABLE}")]
    public async Task<IActionResult> Delete(string COD_ROL_USUARIO, string COD_ASIENTO_CONTABLE)
    { var r = await _service.DeleteAsync(COD_ROL_USUARIO, COD_ASIENTO_CONTABLE); return r.Success ? Ok(r) : BadRequest(r); }
}
