using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Common;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.API.Controllers;

// Endpoints para los tabs de asignación en la pantalla de Usuarios.
// Usan los SP _SHOWALL existentes.
[Authorize]
[ExigePermiso("SEGURIDAD.ROLES_USUARIO_USUARIOS")]
[Route("api/v1/seguridad/usuario-asignaciones")]
public class UsuarioAsignacionesController : BaseController
{
    private readonly IDbConnectionFactory _db;
    private readonly ErrorLogService _errorLog;
    public UsuarioAsignacionesController(IDbConnectionFactory db, ErrorLogService errorLog)
    { _db = db; _errorLog = errorLog; }

    // ── Roles de un USUARIO ──
    // La relación rol↔usuario está en ROLES_USUARIO_USUARIOS, cuyos SP filtran por rol.
    // Para obtener los roles de un usuario, recorremos todos los roles (combo) y nos quedamos
    // con aquellos cuyo SHOWALL incluye al usuario.
    [HttpGet("roles-de-usuario/{COD_USUARIO}")]
    public async Task<IActionResult> GetRolesDeUsuario(string COD_USUARIO)
    {
        try
        {
            using var conn = _db.CreateConnection();

            // 1. Todos los roles disponibles
            var roles = (await conn.QueryAsync(SP.SCHEMASeguridad.RolesUsuario.Showall,
                commandType: CommandType.StoredProcedure)).ToList();

            var asignados = new List<dynamic>();
            foreach (var rol in roles)
            {
                var dict = (IDictionary<string, object>)rol;
                if (!dict.TryGetValue("COD_ROL_USUARIO", out var codRolObj) || codRolObj is null) continue;
                var codRol = codRolObj.ToString();

                // 2. Usuarios de ese rol
                var usuariosRol = await conn.QueryAsync(SP.SCHEMASeguridad.RolesUsuarioUsuarios.Showall,
                    new { COD_ROL_USUARIO = codRol }, commandType: CommandType.StoredProcedure);

                // 3. ¿Está el usuario buscado?
                var contiene = usuariosRol.Any(u =>
                {
                    var ud = (IDictionary<string, object>)u;
                    return ud.TryGetValue("COD_USUARIO", out var cu) && cu?.ToString() == COD_USUARIO;
                });

                if (contiene)
                {
                    dict.TryGetValue("DES_ROL_USUARIO", out var desRol);
                    asignados.Add(new { COD_ROL_USUARIO = codRol, DES_ROL_USUARIO = desRol?.ToString() });
                }
            }
            return Ok(ApiResponse<IEnumerable<dynamic>>.Ok(asignados, total: asignados.Count));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(UsuarioAsignacionesController), nameof(GetRolesDeUsuario), $"COD_USUARIO={COD_USUARIO}");
            return BadRequest(ApiResponse<string>.Fail(ex.Message));
        }
    }

    // Opciones asignadas a un rol + aplicación
    [HttpGet("opciones/{COD_ROL_USUARIO}/{COD_APLICACION}")]
    public async Task<IActionResult> GetOpciones(string COD_ROL_USUARIO, string COD_APLICACION)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var data = await conn.QueryAsync(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Showall,
                new { COD_ROL_USUARIO, COD_APLICACION }, commandType: CommandType.StoredProcedure);
            return Ok(ApiResponse<IEnumerable<dynamic>>.Ok(data));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(UsuarioAsignacionesController), nameof(GetOpciones), $"COD_ROL_USUARIO={COD_ROL_USUARIO}, COD_APLICACION={COD_APLICACION}");
            return BadRequest(ApiResponse<string>.Fail(ex.Message));
        }
    }

    // Botones de una opción (rol + aplicación + opción)
    [HttpGet("botones/{COD_ROL_USUARIO}/{COD_APLICACION}/{COD_OPCION_APLICACION}")]
    public async Task<IActionResult> GetBotones(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var data = await conn.QueryAsync(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Showall,
                new { COD_ROL_USUARIO, COD_APLICACION, COD_OPCION_APLICACION }, commandType: CommandType.StoredProcedure);
            return Ok(ApiResponse<IEnumerable<dynamic>>.Ok(data));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(UsuarioAsignacionesController), nameof(GetBotones), $"COD_ROL_USUARIO={COD_ROL_USUARIO}, COD_APLICACION={COD_APLICACION}, COD_OPCION_APLICACION={COD_OPCION_APLICACION}");
            return BadRequest(ApiResponse<string>.Fail(ex.Message));
        }
    }
}
