using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/seguridad/roles-usuarios-aplicaciones-opciones")]
[ExigePermiso("SEGURIDAD.ROLES_USUARIOS_APLICACIONES_OPCIONES")]
public class RolesUsuariosAplicacionesOpcionesController : BaseController
{
    private readonly IRolesUsuariosAplicacionesOpcionesService _service;
    public RolesUsuariosAplicacionesOpcionesController(IRolesUsuariosAplicacionesOpcionesService service) => _service = service;

    [Authorize(Policy = "Administrador")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>
    /// Retorna las opciones autorizadas para el rol del usuario en sesión.
    /// El rol se extrae del JWT — el usuario solo ve sus propias opciones.
    /// </summary>
    [HttpGet("por-rol")]
    public async Task<IActionResult> GetPorRol([FromQuery] string codAplicacion)
    {
        var codRol = User.FindFirst("rol")?.Value ?? string.Empty;
        if (string.IsNullOrWhiteSpace(codRol))
            return Unauthorized(new { Success = false, Message = "Sin rol en el token." });
        var r = await _service.GetByRolAplicacionAsync(codRol, codAplicacion);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpGet("{COD_ROL_USUARIO}/{COD_APLICACION}/{COD_OPCION_APLICACION}")]
    public async Task<IActionResult> GetById(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION)
    { var r = await _service.GetByIdAsync(COD_ROL_USUARIO, COD_APLICACION, COD_OPCION_APLICACION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] RolesUsuariosAplicacionesOpcionesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] RolesUsuariosAplicacionesOpcionesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RolesUsuariosAplicacionesOpcionesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    [HttpDelete("{COD_ROL_USUARIO}/{COD_APLICACION}/{COD_OPCION_APLICACION}")]
    public async Task<IActionResult> Delete(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION)
    { var r = await _service.DeleteAsync(COD_ROL_USUARIO, COD_APLICACION, COD_OPCION_APLICACION); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("treeview-asignadas")]
    public async Task<IActionResult> GetTreeviewAsignadas([FromQuery] string codRolUsuario)
    { var r = await _service.TreeviewAsignadasAsync(codRolUsuario); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("treeview-disponibles")]
    public async Task<IActionResult> GetTreeviewDisponibles([FromQuery] string codRolUsuario)
    { var r = await _service.TreeviewDisponiblesAsync(codRolUsuario); return r.Success ? Ok(r) : BadRequest(r); }

}