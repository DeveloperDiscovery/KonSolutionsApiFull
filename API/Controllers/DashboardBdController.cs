using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;
using KonSolutionsApi.Infrastructure.Realtime;

namespace KonSolutionsApi.API.Controllers;

/// <summary>
/// Dashboard de Monitoreo de Base de Datos. Cada endpoint ejecuta un SP
/// PROC_ENTORNO_DASHBOARD_BD_* (todos de solo lectura vía DMVs de SQL Server).
/// La pestaña "Trace de Procesos" se sirve desde el buffer en memoria SpTraceBuffer.
/// </summary>
[Authorize]
[SinControlPermisos("Diagnóstico del motor: sesiones, bloqueos y espacio. No administra tablas del "
                    + "negocio. ATENCIÓN: hoy queda accesible a cualquier usuario autenticado y "
                    + "expone información del servidor; si se quiere acotar, hay que crear la opción "
                    + "en SEGURIDAD.APLICACIONES_OPCIONES y declarar aquí su objeto.")]
[Route("api/v1/entorno/dashboard-bd")]
public class DashboardBdController : BaseController
{
    private readonly IDbConnectionFactory _db;
    private readonly SpTraceBuffer _trace;
    private readonly ErrorLogService _errorLog;

    public DashboardBdController(IDbConnectionFactory db, SpTraceBuffer trace, ErrorLogService errorLog)
    { _db = db; _trace = trace; _errorLog = errorLog; }

    // Los 17 endpoints del dashboard pasan por estos dos helpers, así que arreglar el
    // registro de errores acá alcanza para todos — no hace falta tocar cada uno.
    private async Task<IActionResult> Uno<T>(string sp, object? p = null, int timeoutSeg = 30) where T : class
    {
        try
        {
            using var conn = _db.CreateConnection();
            var data = await conn.QueryFirstOrDefaultAsync<T>(sp, p,
                commandType: CommandType.StoredProcedure, commandTimeout: timeoutSeg);
            return Ok(ApiResponse<T>.Ok(data!));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(DashboardBdController), sp);
            return BadRequest(ApiResponse<string>.Fail(ex.Message));
        }
    }

    private async Task<IActionResult> Lista<T>(string sp, object? p = null)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var data = (await conn.QueryAsync<T>(sp, p, commandType: CommandType.StoredProcedure)).ToList();
            return Ok(ApiResponse<List<T>>.Ok(data));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(DashboardBdController), sp);
            return BadRequest(ApiResponse<string>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Bundle del refresco automático (PROC_ENTORNO_DASHBOARD_BD_REFRESCO_COMPLETO): reemplaza
    /// las 10 llamadas secuenciales que hacía el frontend por un solo viaje con 10 resultsets,
    /// mismo criterio que los SPs "LOADCOMBOS" del resto del proyecto. El orden de lectura acá
    /// DEBE coincidir exactamente con el orden de los EXEC dentro del SP.
    /// </summary>
    [HttpGet("refresco-completo")]
    public async Task<IActionResult> RefrescoCompleto(
        [FromQuery] int topConsultas = 20, [FromQuery] int topWaits = 15, [FromQuery] int topTablas = 30,
        [FromQuery] decimal minFrag = 10, [FromQuery] int minPaginas = 100, [FromQuery] int topIndicesFaltantes = 20)
    {
        try
        {
            using var conn = _db.CreateConnection();
            using var multi = await conn.QueryMultipleAsync("PROC_ENTORNO_DASHBOARD_BD_REFRESCO_COMPLETO",
                new
                {
                    TOP_CONSULTAS = topConsultas, TOP_WAITS = topWaits, TOP_TABLAS = topTablas,
                    MIN_FRAGMENTACION = minFrag, MIN_PAGINAS = minPaginas, TOP_INDICES_FALTANTES = topIndicesFaltantes
                },
                commandType: CommandType.StoredProcedure);

            var data = new DashboardBdRefrescoCompleto
            {
                Resumen          = await multi.ReadFirstOrDefaultAsync<DashboardBdResumen>(),
                Memoria          = await multi.ReadFirstOrDefaultAsync<DashboardBdMemoria>(),
                IoLatencia       = (await multi.ReadAsync<DashboardBdIoLatencia>()).ToList(),
                Sesiones         = (await multi.ReadAsync<DashboardBdSesion>()).ToList(),
                Waits            = (await multi.ReadAsync<DashboardBdWait>()).ToList(),
                Consultas        = (await multi.ReadAsync<DashboardBdConsulta>()).ToList(),
                Archivos         = (await multi.ReadAsync<DashboardBdArchivo>()).ToList(),
                Backups          = (await multi.ReadAsync<DashboardBdBackup>()).ToList(),
                Fragmentacion    = (await multi.ReadAsync<DashboardBdFragmentacion>()).ToList(),
                IndicesFaltantes = (await multi.ReadAsync<DashboardBdIndiceFaltante>()).ToList(),
                Tablas           = (await multi.ReadAsync<DashboardBdTabla>()).ToList(),
            };
            return Ok(ApiResponse<DashboardBdRefrescoCompleto>.Ok(data));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(DashboardBdController), "PROC_ENTORNO_DASHBOARD_BD_REFRESCO_COMPLETO");
            return BadRequest(ApiResponse<string>.Fail(ex.Message));
        }
    }

    [HttpGet("resumen")]
    public Task<IActionResult> Resumen()
        => Uno<DashboardBdResumen>("PROC_ENTORNO_DASHBOARD_BD_RESUMEN");

    [HttpGet("sesiones")]
    public Task<IActionResult> Sesiones()
        => Lista<DashboardBdSesion>("PROC_ENTORNO_DASHBOARD_BD_SESIONES_ACTIVAS");

    [HttpGet("bloqueos")]
    public Task<IActionResult> Bloqueos()
        => Lista<DashboardBdBloqueo>("PROC_ENTORNO_DASHBOARD_BD_BLOQUEOS");

    [HttpGet("consultas")]
    public Task<IActionResult> Consultas([FromQuery] int top = 20)
        => Lista<DashboardBdConsulta>("PROC_ENTORNO_DASHBOARD_BD_TOP_CONSULTAS", new { TOP = top });

    [HttpGet("waits")]
    public Task<IActionResult> Waits([FromQuery] int top = 15)
        => Lista<DashboardBdWait>("PROC_ENTORNO_DASHBOARD_BD_WAIT_STATS", new { TOP = top });

    [HttpGet("tablas")]
    public Task<IActionResult> Tablas([FromQuery] int top = 30)
        => Lista<DashboardBdTabla>("PROC_ENTORNO_DASHBOARD_BD_TAMANO_TABLAS", new { TOP = top });

    [HttpGet("fragmentacion")]
    public Task<IActionResult> Fragmentacion([FromQuery] decimal minFrag = 10, [FromQuery] int minPaginas = 100)
        => Lista<DashboardBdFragmentacion>("PROC_ENTORNO_DASHBOARD_BD_INDICES_FRAGMENTACION",
               new { MIN_FRAGMENTACION = minFrag, MIN_PAGINAS = minPaginas });

    [HttpGet("indices-faltantes")]
    public Task<IActionResult> IndicesFaltantes([FromQuery] int top = 20)
        => Lista<DashboardBdIndiceFaltante>("PROC_ENTORNO_DASHBOARD_BD_INDICES_FALTANTES", new { TOP = top });

    [HttpGet("archivos")]
    public Task<IActionResult> Archivos()
        => Lista<DashboardBdArchivo>("PROC_ENTORNO_DASHBOARD_BD_ARCHIVOS");

    [HttpGet("backups")]
    public Task<IActionResult> Backups()
        => Lista<DashboardBdBackup>("PROC_ENTORNO_DASHBOARD_BD_BACKUPS");

    [HttpGet("memoria")]
    public Task<IActionResult> Memoria()
        => Uno<DashboardBdMemoria>("PROC_ENTORNO_DASHBOARD_BD_MEMORIA");

    [HttpGet("io-latencia")]
    public Task<IActionResult> IoLatencia()
        => Lista<DashboardBdIoLatencia>("PROC_ENTORNO_DASHBOARD_BD_IO_LATENCIA");

    [HttpGet("procedimientos")]
    public Task<IActionResult> Procedimientos([FromQuery] int top = 20)
        => Lista<DashboardBdProcedimiento>("PROC_ENTORNO_DASHBOARD_BD_TOP_PROCEDIMIENTOS", new { TOP = top });

    /// <summary>
    /// Trace en vivo: últimos SPs ejecutados por la API (nombre, EXEC completo y
    /// duración real), desde el buffer en memoria. No pega a la BD.
    /// </summary>
    [HttpGet("trace")]
    public IActionResult Trace([FromQuery] int top = 200)
        => Ok(ApiResponse<List<SpTraceEntry>>.Ok(_trace.GetRecent(top)));

    /// <summary>
    /// POST: CHECKPOINT + DBCC SHRINKFILE sobre los archivos de log de la BD.
    /// Único endpoint del dashboard que modifica estado — devuelve MB antes/después.
    /// </summary>
    [HttpPost("limpiar-log")]
    public Task<IActionResult> LimpiarLog([FromQuery] int objetivoMb = 512)
        => Uno<DashboardBdLimpiezaLog>("PROC_ENTORNO_DASHBOARD_BD_LIMPIAR_LOG",
               new { TAMANO_OBJETIVO_MB = objetivoMb });

    /// <summary>POST: genera un backup (FULL/DIF/LOG). Timeout amplio: un backup
    /// comprimido hacia una ruta UNC puede exceder los 30s por defecto de Dapper.</summary>
    [HttpPost("backup")]
    public Task<IActionResult> Backup([FromQuery] string tipo = "FULL")
        => Uno<DashboardBdBackupResultado>("PROC_ENTORNO_DASHBOARD_BD_BACKUP", new { TIPO = tipo }, timeoutSeg: 1800);

    /// <summary>POST: elimina (KILL) una sesión bloqueada. El SP protege sesiones de sistema.</summary>
    [HttpPost("kill-sesion")]
    public Task<IActionResult> KillSesion([FromQuery] int numSesion)
        => Uno<DashboardBdKillResultado>("PROC_ENTORNO_DASHBOARD_BD_KILL_SESION", new { NUM_SESION = numSesion });
}
