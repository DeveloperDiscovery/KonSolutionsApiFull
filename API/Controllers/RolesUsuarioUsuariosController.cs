using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/seguridad/roles-usuario-usuarios")]
[ExigePermiso("SEGURIDAD.ROLES_USUARIO_USUARIOS")]
public class RolesUsuarioUsuariosController : BaseController
{
    private readonly IRolesUsuarioUsuariosService _service;
    public RolesUsuarioUsuariosController(IRolesUsuarioUsuariosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("por-rol/{COD_ROL_USUARIO}")]
    public async Task<IActionResult> GetByRol(string COD_ROL_USUARIO)
    { var r = await _service.GetByRolAsync(COD_ROL_USUARIO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>
    /// GET /seguridad/roles-usuario-usuarios/mis-roles
    /// Trae todos los roles del usuario AUTENTICADO (resuelto del token JWT, nunca
    /// de un parámetro libre). Usado al login para construir el selector de rol en
    /// la barra superior — solo se muestra si el usuario tiene 2 o más roles.
    /// </summary>
    [HttpGet("mis-roles")]
    public async Task<IActionResult> MisRoles()
    { var r = await _service.GetByUsuarioAsync(GetCodUsuario()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ROL_USUARIO}/{COD_USUARIO}")]
    public async Task<IActionResult> GetById(string COD_ROL_USUARIO, string COD_USUARIO)
    { var r = await _service.GetByIdAsync(COD_ROL_USUARIO, COD_USUARIO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] RolesUsuarioUsuariosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] RolesUsuarioUsuariosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    [HttpDelete("{COD_ROL_USUARIO}/{COD_USUARIO}")]
    public async Task<IActionResult> Delete(string COD_ROL_USUARIO, string COD_USUARIO)
    { var r = await _service.DeleteAsync(COD_ROL_USUARIO, COD_USUARIO); return r.Success ? Ok(r) : BadRequest(r); }

}