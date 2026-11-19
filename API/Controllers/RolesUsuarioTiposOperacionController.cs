using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/seguridad/roles-usuario-tipos-operacion")]
[ExigePermiso("SEGURIDAD.ROLES_USUARIO_TIPOS_OPERACION")]
public class RolesUsuarioTiposOperacionController : BaseController
{
    private readonly IRolesUsuarioTiposOperacionService _service;
    public RolesUsuarioTiposOperacionController(IRolesUsuarioTiposOperacionService service) => _service = service;

    [HttpPost("asignar")]
    public async Task<IActionResult> Asignar([FromBody] RolesUsuarioTiposOperacionLoteDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.AsignarAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("desasignar")]
    public async Task<IActionResult> Desasignar([FromBody] RolesUsuarioTiposOperacionLoteDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.DesasignarAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] RolesUsuarioTiposOperacionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // Sin clase de operación: el SP _DISPONIBLES devuelve las de todas las clases juntas.
    [HttpGet("disponibles/{COD_ROL_USUARIO}")]
    public async Task<IActionResult> GetDisponibles(string COD_ROL_USUARIO)
    { var r = await _service.GetDisponiblesAsync(COD_ROL_USUARIO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("combobox/{COD_ROL_USUARIO}/{COD_CLASE_OPERACION}")]
    public async Task<IActionResult> GetCombobox(string COD_ROL_USUARIO, string COD_CLASE_OPERACION)
    { var r = await _service.GetComboboxAsync(COD_ROL_USUARIO, COD_CLASE_OPERACION); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ROL_USUARIO}/{COD_CLASE_OPERACION}")]
    public async Task<IActionResult> GetAll(string COD_ROL_USUARIO, string COD_CLASE_OPERACION)
    { var r = await _service.GetAllAsync(COD_ROL_USUARIO, COD_CLASE_OPERACION); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ROL_USUARIO}/{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}")]
    public async Task<IActionResult> GetById(string COD_ROL_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    { var r = await _service.GetByIdAsync(COD_ROL_USUARIO, COD_CLASE_OPERACION, COD_TIPO_OPERACION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] RolesUsuarioTiposOperacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_ROL_USUARIO}/{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}")]
    public async Task<IActionResult> Delete(string COD_ROL_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    { var r = await _service.DeleteAsync(COD_ROL_USUARIO, COD_CLASE_OPERACION, COD_TIPO_OPERACION); return r.Success ? Ok(r) : BadRequest(r); }
}
