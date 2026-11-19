using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Common;
using KonSolutionsApi.API.Filters;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.API.Controllers;

// Favoritos del usuario sobre opciones de aplicación (menú lateral).
[Authorize]
[Route("api/v1/seguridad/favoritos")]
// El mapeo automático lo salteó porque este controlador no tiene repositorio propio —usa
// IDbConnectionFactory directo—, pero sus SP son los de esta tabla.
[ExigePermiso("SEGURIDAD.USUARIOS_APLICACIONES_OPCIONES_FAVORITOS")]
public class FavoritosController : BaseController
{
    private readonly IDbConnectionFactory _db;
    private readonly ErrorLogService _errorLog;
    public FavoritosController(IDbConnectionFactory db, ErrorLogService errorLog)
    { _db = db; _errorLog = errorLog; }

    public class FavoritoWorkspaceDto
    {
        public string  COD_APLICACION           { get; set; } = string.Empty;
        public string? DES_APLICACION           { get; set; }
        public string  COD_OPCION_APLICACION    { get; set; } = string.Empty;
        public string? DES_OPCION_APLICACION    { get; set; }
        public string? DES_FORMULARIO           { get; set; }
        public byte[]? IMG_ICONO_OPCION         { get; set; }
        public byte[]? IMG_ICONO_APLICACION     { get; set; }
        public string  DES_BACKCOLOR_APLICACION { get; set; } = "";
        public string  DES_FORECOLOR_APLICACION { get; set; } = "";
        public string  DES_BACKCOLOR_OPCION     { get; set; } = "";
        public string  DES_FORECOLOR_OPCION     { get; set; } = "";
    }

    public class FavoritoDto
    {
        public string COD_USUARIO { get; set; } = string.Empty;
        public string COD_APLICACION { get; set; } = "1";
        public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    }

    // Experimento: fila de PROC_..._FAVORITOS_SHOWALLLOGOS — solo íconos, identificados por
    // el mismo par COD_APLICACION/COD_OPCION_APLICACION que ya usa FavoritoWorkspaceDto.
    public class FavoritoLogoDto
    {
        public string  COD_USUARIO           { get; set; } = string.Empty;
        public string  COD_APLICACION        { get; set; } = string.Empty;
        public string  COD_OPCION_APLICACION { get; set; } = string.Empty;
        public byte[]? IMG_ICONO_OPCION      { get; set; }
        public byte[]? IMG_ICONO_APLICACION  { get; set; }
    }

    // Todos los favoritos del usuario, de todas sus aplicaciones, en UNA sola llamada.
    // El SP _FAVORITOS_SHOWALL solo recibe @COD_USUARIO y ya devuelve el listado completo:
    // la sobrecarga de abajo (que además pide COD_APLICACION) termina ignorando ese segundo
    // parámetro y devolviendo exactamente lo mismo. El menú lateral pedía esa ruta una vez
    // POR CADA aplicación del usuario, así que hacía N peticiones idénticas —con el tope de
    // ~6 conexiones simultáneas del navegador, ese era el tramo más lento del login.
    [HttpGet("{COD_USUARIO}")]
    public async Task<IActionResult> GetTodos(string COD_USUARIO)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var data = await conn.QueryAsync(SP.SCHEMASeguridad.Favoritos.Showall,
                new { COD_USUARIO }, commandType: CommandType.StoredProcedure);
            return Ok(ApiResponse<IEnumerable<dynamic>>.Ok(data));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(FavoritosController), nameof(GetTodos), $"COD_USUARIO={COD_USUARIO}");
            return BadRequest(ApiResponse<string>.Fail(ex.Message));
        }
    }

    // Lista de favoritos del usuario (para una aplicación).
    // Se conserva por compatibilidad con llamadores existentes; COD_APLICACION no se usa
    // (ver GetTodos arriba) — para cargas nuevas conviene la ruta de un solo segmento.
    [HttpGet("{COD_USUARIO}/{COD_APLICACION}")]
    public async Task<IActionResult> GetAll(string COD_USUARIO, string COD_APLICACION)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var data = await conn.QueryAsync(SP.SCHEMASeguridad.Favoritos.Showall,
                new { COD_USUARIO }, commandType: CommandType.StoredProcedure);
            return Ok(ApiResponse<IEnumerable<dynamic>>.Ok(data));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(FavoritosController), nameof(GetAll), $"COD_USUARIO={COD_USUARIO}");
            return BadRequest(ApiResponse<string>.Fail(ex.Message));
        }
    }

    // Agregar favorito (upsert)
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] FavoritoDto dto)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var p = new DynamicParameters();
            p.Add("COD_USUARIO", dto.COD_USUARIO);
            p.Add("COD_APLICACION", dto.COD_APLICACION);
            p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION);
            p.Add("COD_USUARIO_REGISTRO", GetCodUsuario());
            p.Add("COD_ESTACION_REGISTRO", GetEstacion());
            await conn.ExecuteAsync(SP.SCHEMASeguridad.Favoritos.Insert, p, commandType: CommandType.StoredProcedure);
            return Ok(ApiResponse<string>.Ok("Favorito agregado."));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(FavoritosController), nameof(Add), $"COD_USUARIO={dto.COD_USUARIO}, COD_OPCION_APLICACION={dto.COD_OPCION_APLICACION}");
            return BadRequest(ApiResponse<string>.Fail(ex.Message));
        }
    }

    // Workspace: todos los favoritos del usuario con iconos y datos de app/opción
    [HttpGet("workspace/{COD_USUARIO}")]
    public async Task<IActionResult> GetWorkspace(string COD_USUARIO)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var data = await conn.QueryAsync<FavoritoWorkspaceDto>(
                "PROC_SEGURIDAD_USUARIOS_APLICACIONES_OPCIONES_FAVORITOS_SHOWALL",
                new { COD_USUARIO }, commandType: CommandType.StoredProcedure);
            return Ok(ApiResponse<IEnumerable<FavoritoWorkspaceDto>>.Ok(data));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(FavoritosController), nameof(GetWorkspace), $"COD_USUARIO={COD_USUARIO}");
            return BadRequest(ApiResponse<string>.Fail(ex.Message));
        }
    }

    // Experimento: íconos de los favoritos del Workspace aparte del texto (GetWorkspace) —
    // se pide en paralelo, sin bloquear el primer render del Workspace.
    [HttpGet("workspace/logos/{COD_USUARIO}")]
    public async Task<IActionResult> GetWorkspaceLogos(string COD_USUARIO)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var data = await conn.QueryAsync<FavoritoLogoDto>(
                SP.SCHEMASeguridad.Favoritos.ShowallLogos,
                new { COD_USUARIO }, commandType: CommandType.StoredProcedure);
            return Ok(ApiResponse<IEnumerable<FavoritoLogoDto>>.Ok(data));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(FavoritosController), nameof(GetWorkspaceLogos), $"COD_USUARIO={COD_USUARIO}");
            return BadRequest(ApiResponse<string>.Fail(ex.Message));
        }
    }

    // Quitar favorito
    [HttpDelete("{COD_USUARIO}/{COD_APLICACION}/{COD_OPCION_APLICACION}")]
    public async Task<IActionResult> Remove(string COD_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        try
        {
            using var conn = _db.CreateConnection();
            await conn.ExecuteAsync(SP.SCHEMASeguridad.Favoritos.Delete,
                new { COD_USUARIO, COD_APLICACION, COD_OPCION_APLICACION }, commandType: CommandType.StoredProcedure);
            return Ok(ApiResponse<string>.Ok("Favorito eliminado."));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(FavoritosController), nameof(Remove), $"COD_USUARIO={COD_USUARIO}, COD_OPCION_APLICACION={COD_OPCION_APLICACION}");
            return BadRequest(ApiResponse<string>.Fail(ex.Message));
        }
    }
}
