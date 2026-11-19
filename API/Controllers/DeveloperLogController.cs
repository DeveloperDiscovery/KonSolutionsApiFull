using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.API.Filters;
using KonSolutionsApi.Common;

namespace KonSolutionsApi.API.Controllers;

/// <summary>
/// Controlador de logs del sistema (errores del frontend y del backend).
/// Permite recibir logs del cliente Blazor y listar/leer archivos de log del servidor.
/// </summary>
[Authorize]
[SinControlPermisos("Diagnóstico. No administra ninguna tabla: recibe el log del frontend y " +
                    "lee archivos de log del servidor. Exigirle permiso de tabla no tendría " +
                    "contra qué contrastar, y el guardián lo auditaría como (SIN DECLARAR) en " +
                    "cada request — que en este endpoint son muchos.")]
[Route("api/v1/developer")]
public class DeveloperLogController : BaseController
{
    private readonly ILogger<DeveloperLogController> _logger;
    private readonly IConfiguration _config;
    private readonly ErrorLogService _errorLog;

    public DeveloperLogController(ILogger<DeveloperLogController> logger, IConfiguration config, ErrorLogService errorLog)
    {
        _logger   = logger;
        _config   = config;
        _errorLog = errorLog;
    }

    public class ErrorEntry
    {
        public string Programa   { get; set; } = "";
        public string Clase      { get; set; } = "";
        public string Metodo     { get; set; } = "";
        public string TipoError  { get; set; } = "";
        public string Mensaje    { get; set; } = "";
        public string Detalle    { get; set; } = "";
        public string Parametros { get; set; } = "";
        public DateTime Fecha    { get; set; }
    }

    public class AccessEntry
    {
        public string COD_USUARIO { get; set; } = "";
        public string Modulo      { get; set; } = "";
        public string Pagina      { get; set; } = "";
        public string Evento      { get; set; } = "";
        public string Parametros  { get; set; } = "";
        public DateTime Fecha     { get; set; }
    }

    public class LogFileInfo
    {
        public string Nombre   { get; set; } = "";
        public long   Tamaño   { get; set; }
        public DateTime Fecha  { get; set; }
        public int    Lineas   { get; set; }
    }

    /// <summary>Recibe errores y traces del frontend Blazor y los persiste en archivo.</summary>
    [HttpPost("error-log")]
    public IActionResult ErrorLog([FromBody] ErrorEntry e)
    {
        if (e.TipoError == "TRACE")
            _errorLog.LogTrace("WEB", e.Mensaje, e.Detalle);
        else
            _errorLog.LogErrorFromWeb(e.Programa, e.Clase, e.Metodo, e.TipoError, e.Mensaje, e.Detalle, e.Parametros);

        return Ok(new ApiResponse<object> { Success = true, Message = "Registrado." });
    }

    /// <summary>Recibe el log de accesos/eventos del frontend.</summary>
    [HttpPost("access-log")]
    public IActionResult AccessLog([FromBody] AccessEntry e)
    {
        _logger.LogInformation("[WEB-ACCESO] {Usuario} · {Modulo}/{Pagina} · {Evento} | {Params}",
            e.COD_USUARIO, e.Modulo, e.Pagina, e.Evento, e.Parametros);
        return Ok(new ApiResponse<object> { Success = true, Message = "Registrado." });
    }

    /// <summary>Diagnóstico: de dónde salió la ruta de logs que se está usando realmente
    /// (ENTORNO.PATHFILES o el fallback de appsettings), y por qué si cayó al fallback.
    /// Útil para no adivinar cuando "ya cargué la ruta pero no la toma" — el proceso
    /// cachea la resolución una sola vez (Singleton), así que un cambio en PATHFILES
    /// recién se refleja tras reiniciar la API.</summary>
    [Authorize(Policy = "Administrador")]
    [HttpGet("logs/diagnostico-ruta")]
    public IActionResult DiagnosticoRutaLogs()
        => Ok(new ApiResponse<object> { Success = true, Data = _errorLog.DiagnosticoRutaLogs() });

    /// <summary>Retorna la lista de archivos de log disponibles en el servidor.</summary>
    [Authorize(Policy = "Administrador")]
    [HttpGet("logs")]
    public IActionResult ListarLogs()
    {
        var dir = _errorLog.ObtenerDirectorioLogs();
        if (!Directory.Exists(dir))
            return Ok(new ApiResponse<List<LogFileInfo>> { Success = true, Data = new() });

        var archivos = Directory.GetFiles(dir, "*.log")
            .Select(f =>
            {
                var info = new FileInfo(f);
                int lineas = 0;
                try { lineas = System.IO.File.ReadAllLines(f).Length; } catch { }
                return new LogFileInfo
                {
                    Nombre  = info.Name,
                    Tamaño  = info.Length,
                    Fecha   = info.LastWriteTime,
                    Lineas  = lineas
                };
            })
            .OrderByDescending(f => f.Fecha)
            .ToList();

        return Ok(new ApiResponse<List<LogFileInfo>> { Success = true, Data = archivos });
    }

    /// <summary>Retorna el contenido de un archivo de log específico, paginado por líneas.</summary>
    [Authorize(Policy = "Administrador")]
    [HttpGet("logs/{nombre}")]
    public IActionResult LeerLog(string nombre, [FromQuery] int pagina = 1, [FromQuery] int porPagina = 100, [FromQuery] string? filtro = null)
    {
        // Seguridad: solo archivos .log, sin rutas relativas
        if (nombre.Contains('/') || nombre.Contains('\\') || !nombre.EndsWith(".log"))
            return BadRequest(new ApiResponse<object> { Success = false, Message = "Nombre de archivo inválido." });

        var dir = _errorLog.ObtenerDirectorioLogs();
        var ruta = Path.Combine(dir, nombre);
        if (!System.IO.File.Exists(ruta))
            return NotFound(new ApiResponse<object> { Success = false, Message = "Archivo no encontrado." });

        string[] todasLineas;
        try { todasLineas = System.IO.File.ReadAllLines(ruta, System.Text.Encoding.UTF8); }
        catch { return StatusCode(500, new ApiResponse<object> { Success = false, Message = "No se pudo leer el archivo." }); }

        // Filtro por texto
        if (!string.IsNullOrWhiteSpace(filtro))
        {
            var f = filtro.ToLower();
            todasLineas = todasLineas.Where(l => l.ToLower().Contains(f)).ToArray();
        }

        var total     = todasLineas.Length;
        var lineas    = todasLineas
                            .Skip((pagina - 1) * porPagina)
                            .Take(porPagina)
                            .ToList();

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Data = new { Total = total, Pagina = pagina, PorPagina = porPagina, Lineas = lineas }
        });
    }
}
