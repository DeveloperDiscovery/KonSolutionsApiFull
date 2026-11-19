using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/seguridad/roles-usuarios-aplicaciones-opciones-botones")]
[ExigePermiso("SEGURIDAD.ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES")]
public class RolesUsuariosAplicacionesOpcionesBotonesController : BaseController
{
    private readonly IRolesUsuariosAplicacionesOpcionesBotonesService _service;
    public RolesUsuariosAplicacionesOpcionesBotonesController(IRolesUsuariosAplicacionesOpcionesBotonesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ROL_USUARIO}/{COD_APLICACION}/{COD_OPCION_APLICACION}")]
    public async Task<IActionResult> GetById(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION)
    { var r = await _service.GetByIdAsync(COD_ROL_USUARIO, COD_APLICACION, COD_OPCION_APLICACION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] RolesUsuariosAplicacionesOpcionesBotonesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] RolesUsuariosAplicacionesOpcionesBotonesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RolesUsuariosAplicacionesOpcionesBotonesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    [HttpDelete("{COD_ROL_USUARIO}/{COD_APLICACION}/{COD_OPCION_APLICACION}")]
    public async Task<IActionResult> Delete(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION)
    { var r = await _service.DeleteAsync(COD_ROL_USUARIO, COD_APLICACION, COD_OPCION_APLICACION); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>
    /// GET /seguridad/roles-usuarios-aplicaciones-opciones-botones/por-opcion?codRolUsuario=X&amp;codAplicacion=Y&amp;codOpcion=Z
    /// Botones del rol para una opción específica — usado por la pantalla de asignación.
    /// </summary>
    [HttpGet("por-opcion")]
    public async Task<IActionResult> GetByOpcion(
        [FromQuery] string codRolUsuario,
        [FromQuery] string codAplicacion,
        [FromQuery] string codOpcion)
    {
        var r = await _service.GetByOpcionAsync(codRolUsuario, codAplicacion, codOpcion);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    /// <summary>
    /// GET /seguridad/roles-usuarios-aplicaciones-opciones-botones/treeview?codRolUsuario=XXX
    /// Trae TODOS los botones de TODAS las opciones para el rol indicado. Si no se
    /// especifica codRolUsuario, usa el rol con el que el usuario inició sesión (claim
    /// "rol" del JWT) — el frontend debe mandar el rol elegido explícitamente cuando
    /// el usuario cambia de rol en el selector de la barra superior.
    /// </summary>
    [HttpGet("treeview")]
    public async Task<IActionResult> Treeview([FromQuery] string? codRolUsuario = null)
    {
        var rol = codRolUsuario ?? GetCodRol();
        if (string.IsNullOrWhiteSpace(rol))
            return BadRequest(ApiResponse<object>.Fail("No se pudo determinar el rol activo."));

        var r = await _service.TreeviewAsync(rol);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    /// <summary>
    /// GET /seguridad/roles-usuarios-aplicaciones-opciones-botones/treeview/logos?codRolUsuario=XXX
    /// Experimento: íconos de TODO el árbol (apps/opciones/botones) del rol, en un endpoint
    /// aparte del texto principal (treeview) — se pide en paralelo, sin bloquear el primer
    /// render del menú (tile, lateral y circular).
    /// </summary>
    [HttpGet("treeview/logos")]
    public async Task<IActionResult> TreeviewLogos([FromQuery] string? codRolUsuario = null)
    {
        var rol = codRolUsuario ?? GetCodRol();
        if (string.IsNullOrWhiteSpace(rol))
            return BadRequest(ApiResponse<object>.Fail("No se pudo determinar el rol activo."));

        var r = await _service.TreeviewLogosAsync(rol);
        return r.Success ? Ok(r) : BadRequest(r);
    }

}