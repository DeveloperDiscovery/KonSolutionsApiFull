using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace KonSolutionsApi.Common;

public class ErrorLogService
{
    private readonly IConfiguration _config;
    private readonly ILogger<ErrorLogService> _logger;
    private readonly string _dir;
    private readonly bool _saveErrorToFile;
    private readonly bool _saveTraceToFile;
    private readonly Infrastructure.Realtime.SpTraceBuffer? _spTrace;

    // Ruta de logs resuelta dinámicamente contra ENTORNO.PATHFILES/SERVIDORES por la llave
    // KEY_PATH_FILE_DEVELOPER_LOGERRORES (igual que "Generar TXT de SPs"), en vez de un
    // directorio fijo en appsettings — así se puede mover el destino de los logs sin tocar
    // configuración de despliegue. Se resuelve una sola vez (Singleton, vive todo el proceso)
    // y si la llave no existe o la BD no responde, cae al directorio de appsettings.
    private const string KeyPathLogErrores = "KEY_PATH_FILE_DEVELOPER_LOGERRORES";
    private string? _dirResuelto;
    private readonly object _dirLock = new();

    public ErrorLogService(IConfiguration config, ILogger<ErrorLogService> logger,
        Infrastructure.Realtime.SpTraceBuffer? spTrace = null)
    {
        _config           = config;
        _logger           = logger;
        _spTrace          = spTrace;
        _dir              = config["ErrorLog:LogDirectory"] ?? "/app/logs";
        _saveErrorToFile  = config.GetValue<bool>("ErrorLog:SaveToFile");
        _saveTraceToFile  = config.GetValue<bool>("TraceLog:SaveToFile");
        Directory.CreateDirectory(_dir);
        WriteResult.OnSpError = msg => LogSpError("KonSolutionsApi", "SP", "StoredProcedure", msg);
    }

    /// <summary>Misma resolución que SqlConnectionFactory (env var KonSolutions_DB_CONNECTION
    /// primero, appsettings solo como fallback de desarrollo) — antes esta clase leía
    /// directo de appsettings, ignorando la variable de entorno.</summary>
    private string ResolverConnectionString()
    {
        var cs = Environment.GetEnvironmentVariable("KonSolutions_DB_CONNECTION");
        if (string.IsNullOrWhiteSpace(cs)) cs = _config.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(cs))
            throw new InvalidOperationException("Falta configurar la variable de entorno KonSolutions_DB_CONNECTION.");
        return cs;
    }

    /// <summary>Ruta de logs efectiva (resuelta contra ENTORNO.PATHFILES, o el fallback de
    /// appsettings si no hay llave configurada) — expuesta para que el visor de logs
    /// (DeveloperLogController) lea del mismo lugar donde realmente se está escribiendo.</summary>
    public string ObtenerDirectorioLogs() => ObtenerDirectorio();

    // Motivo de diagnóstico: por qué se está usando el fallback (null si se resolvió bien
    // contra la BD, o si ya no aplica porque nunca se intentó). Se llena solo la primera
    // vez que se resuelve el directorio (ver ObtenerDirectorio) — igual que _dirResuelto.
    private string? _motivoFallback;

    /// <summary>Diagnóstico de la resolución de ruta de logs: de dónde salió la ruta
    /// efectiva y, si cayó al fallback de appsettings, por qué (para no tener que adivinar
    /// si el problema es la llave, la fila en PATHFILES/SERVIDORES, o el proceso que no
    /// recogió el cambio por seguir cacheado desde antes de cargar la fila).</summary>
    public object DiagnosticoRutaLogs()
    {
        var rutaEfectiva = ObtenerDirectorio(); // fuerza la resolución si aún no corrió
        return new
        {
            RutaEfectiva      = rutaEfectiva,
            Fuente            = _motivoFallback is null ? "ENTORNO.PATHFILES (BD)" : "Fallback (appsettings.json)",
            MotivoFallback    = _motivoFallback,
            RutaFallbackConfigurada = _dir,
            LlaveBuscada      = KeyPathLogErrores,
            DirectorioExiste  = Directory.Exists(rutaEfectiva),
        };
    }

    private string ObtenerDirectorio()
    {
        if (_dirResuelto is not null) return _dirResuelto;
        lock (_dirLock)
        {
            if (_dirResuelto is not null) return _dirResuelto;
            var desdeBd = ResolverDirectorioDesdeBd();
            _dirResuelto = desdeBd ?? _dir;
            try { Directory.CreateDirectory(_dirResuelto); }
            catch (Exception ex) { _motivoFallback ??= $"No se pudo crear/acceder al directorio '{_dirResuelto}': {ex.Message}"; }
            return _dirResuelto;
        }
    }

    private string? ResolverDirectorioDesdeBd()
    {
        try
        {
            using var conn = new SqlConnection(ResolverConnectionString());
            conn.Open();
            using var cmd = new SqlCommand("""
                SELECT '\\' + LTRIM(RTRIM(c.COD_IP)) + a.DES_KEY_PATHFILE
                FROM   ENTORNO.PATHFILES a
                LEFT JOIN ENTORNO.SERVIDORES c ON a.COD_KEY_SERVIDOR = c.COD_KEY_SERVIDOR
                WHERE  a.COD_KEY_PATHFILE = @key
                """, conn);
            cmd.Parameters.AddWithValue("@key", KeyPathLogErrores);
            var ruta = cmd.ExecuteScalar() as string;
            if (string.IsNullOrWhiteSpace(ruta))
            {
                _motivoFallback = $"La consulta a ENTORNO.PATHFILES no devolvió ninguna fila (o vino vacía) para la llave '{KeyPathLogErrores}'. " +
                                  "Verifica que exista un registro con ese COD_KEY_PATHFILE y que su COD_KEY_SERVIDOR apunte a un servidor válido en ENTORNO.SERVIDORES.";
                return null;
            }
            return ruta;
        }
        catch (Exception ex)
        {
            _motivoFallback = $"Falló la consulta a ENTORNO.PATHFILES/SERVIDORES: {ex.Message}";
            return null;
        }
    }

    // ── Error desde código backend ────────────────────────────────────────
    public void LogError(Exception ex, string programa, string clase, string metodo, string? parametros = null)
    {
        var frame   = new StackTrace(ex, true).GetFrame(0);
        var linea   = frame?.GetFileLineNumber() ?? 0;
        var archivo = frame?.GetFileName() ?? string.Empty;
        var esSql   = ex is SqlException;
        var codSql  = esSql ? ((SqlException)ex).Number.ToString() : string.Empty;
        var tipo    = esSql ? "SQL" : "DOTNET";

        _logger.LogError("[{Tipo}] {Programa}.{Clase}.{Metodo} L{Linea} SQL:{Cod} -> {Msg}",
            tipo, programa, clase, metodo, linea, codSql, ex.Message);

        if (_saveErrorToFile)
            AppendError(tipo, programa, clase, metodo, linea, codSql, ex.Message, ex.ToString(), parametros);

        if (_config.GetValue<bool>("ErrorLog:SaveToDatabase"))
        {
            try
            {
                using var conn = new SqlConnection(ResolverConnectionString());
                conn.Open();
                using var cmd = new SqlCommand("PROC_DEVELOPER_ERROR_LOG_INSERT", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@PROGRAMA", programa);
                cmd.Parameters.AddWithValue("@CLASE", clase);
                cmd.Parameters.AddWithValue("@METODO", metodo);
                cmd.Parameters.AddWithValue("@TIPO_ERROR", tipo);
                cmd.Parameters.AddWithValue("@MENSAJE", ex.Message);
                cmd.Parameters.AddWithValue("@DETALLE", ex.ToString());
                cmd.Parameters.AddWithValue("@LINEA", linea);
                cmd.Parameters.AddWithValue("@ARCHIVO", archivo);
                cmd.Parameters.AddWithValue("@COD_SQL", codSql);
                cmd.Parameters.AddWithValue("@PARAMETROS", parametros ?? string.Empty);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }
    }

    // ── Error recibido desde el frontend Blazor ───────────────────────────
    public void LogErrorFromWeb(string programa, string clase, string metodo, string tipoError,
                                string mensaje, string detalle, string parametros)
    {
        _logger.LogError("[WEB-{Tipo}] {Programa}.{Clase}.{Metodo} -> {Msg}", tipoError, programa, clase, metodo, mensaje);

        if (_saveErrorToFile)
            AppendError(tipoError, programa, clase, metodo, 0, "", mensaje, detalle, parametros);
    }

    // ── Error de negocio: SP retornó Estado='ERROR' (no excepción) ───────────
    public void LogSpError(string programa, string clase, string metodo, string mensaje)
    {
        _logger.LogWarning("[SP-ERROR] {Programa}.{Clase}.{Metodo} -> {Msg}", programa, clase, metodo, mensaje);

        if (_saveErrorToFile)
            AppendError("SP-ERROR", programa, clase, metodo, 0, "", mensaje, "", null);
    }

    // ── Trace recibido desde el frontend Blazor (o backend) ───────────────
    public void LogTrace(string origen, string mensaje, string detalle = "")
    {
        _logger.LogInformation("[TRACE-{Origen}] {Msg}", origen, mensaje);

        if (_saveTraceToFile)
        {
            try
            {
                var ruta = Path.Combine(ObtenerDirectorio(), $"trace_{DateTime.Now:yyyy-MM-dd}.log");
                File.AppendAllText(ruta,
                    $"[{DateTime.Now:HH:mm:ss.fff}] [{origen}] {mensaje}" +
                    (string.IsNullOrWhiteSpace(detalle) ? "" : $"\n   {detalle}") + "\n");
            }
            catch { }
        }
    }

    // ── Trace de SP desde SpTracingCommand (backend) ──────────────────────
    // Serializa las escrituras al archivo de traza entre los múltiples requests
    // concurrentes que ejecutan SPs al mismo tiempo (ej. todas las llamadas en
    // paralelo que se disparan al cargar el menú tras el login).
    private static readonly object _traceLock = new();

    /// <summary>Sobrecarga con la duración real del SP (ms), medida en SpTracingCommand.
    /// Además de trazar, alimenta el buffer en memoria que consume el Dashboard de
    /// Monitoreo (pestaña "Trace de Procesos").</summary>
    public void LogSpTrace(string execLine, long duracionMs)
    {
        _spTrace?.Add(execLine, duracionMs);
        LogSpTrace(execLine);
    }

    public void LogSpTrace(string execLine)
    {
        _logger.LogInformation("[SP] {ExecLine}", execLine);

        if (_saveTraceToFile)
        {
            // Nunca bloquear el hilo que está ejecutando la consulta real esperando a que
            // termine el I/O de disco: antes, File.AppendAllText corría de forma síncrona
            // aquí mismo, así que si dos requests en paralelo escribían al mismo archivo al
            // mismo tiempo, una se quedaba esperando a la otra — con muchos requests
            // concurrentes (como pasa justo al loguear) esto sumaba varios segundos de
            // demora real a la consulta SQL, aunque el SP en sí corriera rápido.
            var linea = $"[{DateTime.Now:HH:mm:ss.fff}] [SP] {execLine}\n";
            var ruta  = Path.Combine(ObtenerDirectorio(), $"trace_{DateTime.Now:yyyy-MM-dd}.log");
            _ = Task.Run(() =>
            {
                try { lock (_traceLock) { File.AppendAllText(ruta, linea); } }
                catch { }
            });
        }
    }

    // ── Helpers ───────────────────────────────────────────────────────────
    private void AppendError(string tipo, string programa, string clase, string metodo,
                             int linea, string codSql, string mensaje, string detalle, string? parametros)
    {
        var ruta  = Path.Combine(ObtenerDirectorio(), $"errors_{DateTime.Now:yyyy-MM-dd}.log");
        var linea2 = $"[{DateTime.Now:HH:mm:ss}] [{tipo}] {programa}.{clase}.{metodo} " +
                     $"L{linea} | SQL:{codSql} | {mensaje}\n   Params: {parametros}\n   {detalle}\n";
        // Antes esto era un Task.Run sin esperar ("fire and forget") — si el request HTTP
        // terminaba y el proceso reciclaba (normal en Debug/IIS Express) justo después,
        // la escritura al archivo podía perderse silenciosamente sin ningún error visible.
        // Ahora es síncrona: si falla, al menos queda en el ILogger (consola/output).
        try { lock (_traceLock) { File.AppendAllText(ruta, linea2); } }
        catch (Exception ex) { _logger.LogError(ex, "No se pudo escribir el log de errores en {Ruta}", ruta); }
    }
}
