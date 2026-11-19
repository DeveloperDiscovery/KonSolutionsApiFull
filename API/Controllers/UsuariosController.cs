using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/seguridad/usuarios")]
[ExigePermiso("SEGURIDAD.USUARIOS")]
public class UsuariosController : BaseController
{
    private readonly IUsuariosService _service;
    private readonly IRolesUsuariosAplicacionesOpcionesBotonesRepository _botonesRepo;

    // FIX SEGURIDAD (Vuln 13): los códigos de aplicación/opción se leen desde
    // configuración en lugar de ser constantes hardcodeadas. Si la BD los cambia,
    // solo hay que actualizar appsettings.json sin recompilar. El fallback al
    // nombre semántico del botón ("CAMBIAR PASSWORD") actúa como segunda línea.
    private readonly string _codAplicacionUsuarios;
    private readonly string _codOpcionUsuarios;
    private readonly IConfiguration _config;

    public UsuariosController(IUsuariosService service,
        IRolesUsuariosAplicacionesOpcionesBotonesRepository botonesRepo,
        IConfiguration config)
    {
        _service       = service;
        _botonesRepo   = botonesRepo;
        _config        = config;
        _codAplicacionUsuarios = config["Permisos:CodAplicacionUsuarios"] ?? "00000";
        _codOpcionUsuarios     = config["Permisos:CodOpcionUsuarios"]     ?? "0104";
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_USUARIO}")]
    public async Task<IActionResult> GetById(string COD_USUARIO)
    { var r = await _service.GetByIdAsync(COD_USUARIO); return r.Success ? Ok(r) : NotFound(r); }

    /// <summary>
    /// GET /seguridad/usuarios/{cod}/foto
    /// Devuelve SOLO la foto del usuario como binario (image/png), no JSON base64.
    /// Pensado para que el cliente la pida UNA vez por usuario y la cachee (el polling
    /// de notificaciones ya no transporta la imagen embebida en cada respuesta).
    /// Se marca con Cache-Control para que el navegador también la reutilice sin
    /// volver a pegar al servidor durante 1 día.
    /// </summary>
    [HttpGet("{COD_USUARIO}/foto")]
    public async Task<IActionResult> GetFoto(string COD_USUARIO)
    {
        var r = await _service.GetByIdAsync(COD_USUARIO);
        var bytes = r.Data?.IMG_FOTO;
        // 204 (sin contenido) si el usuario no tiene foto: el cliente lo cachea como
        // "sin foto" y muestra el ícono por defecto, sin reintentar en cada polling.
        if (bytes is null || bytes.Length == 0) return NoContent();

        Response.Headers.CacheControl = "private, max-age=86400";
        return File(bytes, "image/png");
    }

    // Antes [AllowAnonymous]: exponía COD_USUARIO/DES_USUARIO (lista de usuarios,
    // filtrable por "search") sin token — permite enumeración de usuarios válidos
    // para ataques de fuerza bruta contra /login. Ningún consumidor del Web lo
    // llama antes de autenticar, así que se restringe a sesión igual que el resto
    // del controller.
    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string? search = null)
    { var r = await _service.GetComboboxAsync(search); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] UsuariosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_SEGURIDAD_USUARIOS_LOADCOMBOS: Perfil + Dominios de correo + Estados
    /// para UsuarioDialog.razor en un solo viaje. codTipoEstado es opcional (se autorresuelve).</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos([FromQuery] int codTipoEstado = 0)
    { var r = await _service.LoadcombosAsync(codTipoEstado); return r.Success ? Ok(r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] UsuariosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    [HttpDelete("{COD_USUARIO}")]
    public async Task<IActionResult> Delete(string COD_USUARIO)
    { var r = await _service.DeleteAsync(COD_USUARIO); return r.Success ? Ok(r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    [HttpPost("estados")]
    public async Task<IActionResult> Estados([FromBody] UsuariosEstadosDto dto)
    { var r = await _service.EstadosAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    // Sin Policy="Administrador" a propósito: cualquier usuario autenticado debe
    // poder cambiar SU PROPIA contraseña. Para cambiar la de OTRO usuario, se
    // exige tener el botón "Cambiar Password" habilitado en la opción Usuarios
    // (verificado abajo contra ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES).
    [HttpPost("password")]
    public async Task<IActionResult> Password([FromBody] UsuariosPasswordDto dto)
    {
        var codUsuarioLogueado = GetCodUsuario();

        if (!string.Equals(dto.COD_USUARIO, codUsuarioLogueado, StringComparison.OrdinalIgnoreCase))
        {
            // Pide cambiar la contraseña de OTRO usuario: exige el permiso explícito.
            var rolActivo = dto.COD_ROL_USUARIO_ACTIVO ?? GetCodRol();
            if (string.IsNullOrWhiteSpace(rolActivo))
                return Forbid();

            var treeview = await _botonesRepo.TreeviewAsync(rolActivo);
            // Comparación por texto exacto (frágil si se renombra en BD) —
            // provisional hasta tener un identificador estable de botón
            // (ver diseño pendiente de DES_METODO para despacho dinámico).
            // FIX Vuln 13: verificación en dos capas —
            // 1) por código de aplicación+opción (configurable en appsettings.json)
            // 2) fallback semántico por nombre del botón (resistente a recodificación)
            // Ambas capas exigen FLG_HABILITADO = true.
            var botonNombre = "CAMBIAR PASSWORD";
            var puedeCambiarOtros = treeview.Botones.Any(b =>
                b.FLG_HABILITADO == true
                && (b.DES_BOTON_OPCION ?? "").Trim().ToUpperInvariant() == botonNombre
                && (b.COD_APLICACION?.Trim() == _codAplicacionUsuarios
                    || b.COD_OPCION_APLICACION?.Trim() == _codOpcionUsuarios));

            if (!puedeCambiarOtros)
                return Forbid();
        }

        var r = await _service.PasswordAsync(dto, codUsuarioLogueado, GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    /// <summary>
    /// POST /seguridad/usuarios/password-reset
    /// Resetea el password del usuario (lo deja en NULL). En su próximo login
    /// el flag FLG_CHANGE_PASSSWORD lo obligará a definir uno nuevo.
    /// </summary>
    [Authorize(Policy = "Administrador")]
    [HttpPost("password-reset")]
    public async Task<IActionResult> PasswordReset([FromBody] UsuariosPasswordResetDto dto)
    { var r = await _service.PasswordResetAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    [HttpPost("resetintentos")]
    public async Task<IActionResult> Resetintentos([FromBody] UsuariosResetintentosDto dto)
    { var r = await _service.ResetintentosAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }


}