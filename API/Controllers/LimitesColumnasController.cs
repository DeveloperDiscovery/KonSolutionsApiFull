using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Common;
using KonSolutionsApi.Infrastructure.Data;
using KonSolutionsApi.Infrastructure.Realtime;

namespace KonSolutionsApi.API.Controllers;

/// <summary>
/// Expone los límites de tipo/tamaño de las columnas de la BD.
/// El frontend los usa para limitar los textbox (longitud de texto)
/// y los numéricos (rango de enteros, precisión y escala de decimales).
/// Ejecuta PROC_ENTORNO_DATABASE_TIPOS_DATO_SWHOWALL una sola vez y cachea.
/// </summary>
[Authorize]
[SinControlPermisos("Apoyo de interfaz: devuelve las longitudes de columna que los formularios "
                    + "usan para limitar la escritura. Lo consulta cualquier pantalla, de cualquier rol.")]
[Route("api/v1/entorno/limites-columnas")]
public class LimitesColumnasController : BaseController
{
    private readonly IDbConnectionFactory _db;
    private readonly LimitesColumnasStore _store;
    private readonly ErrorLogService _errorLog;

    public LimitesColumnasController(IDbConnectionFactory db, LimitesColumnasStore store, ErrorLogService errorLog)
    {
        _db = db;
        _store = store;
        _errorLog = errorLog;
    }

    /// <summary>
    /// GET /entorno/limites-columnas
    /// Devuelve todos los límites de columnas. La primera vez ejecuta el SP
    /// y guarda en memoria; las siguientes responde desde la caché.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetLimites()
    {
        try
        {
            // Si ya está cacheado, responder desde memoria (rápido)
            if (_store.Cargado)
                return Ok(ApiResponse<List<LimiteColumna>>.Ok(_store.Get()));

            // Primera vez: ejecutar el SP y cachear.
            // commandTimeout alto a propósito: el SP recorre el catálogo COMPLETO de la base
            // (todas las columnas de todas las tablas) y crece con cada tabla nueva — con el
            // default de Dapper (30s) empezó a cortar por timeout, y el catch de abajo lo
            // convertía en un 400 genérico. Solo se paga una vez por arranque del API: el
            // resultado queda en LimitesColumnasStore y las siguientes llamadas salen de ahí.
            using var conn = _db.CreateConnection();
            var datos = (await conn.QueryAsync<LimiteColumna>(
                "PROC_ENTORNO_DATABASE_TIPOS_DATO_SWHOWALL",
                commandType: CommandType.StoredProcedure,
                commandTimeout: 180)).ToList();

            _store.Set(datos);
            return Ok(ApiResponse<List<LimiteColumna>>.Ok(datos));
        }
        catch (Exception ex)
        {
            // Este catch estaba mudo: si el SP fallaba (típicamente por timeout, dado lo que
            // recorre), el error no quedaba registrado en ningún lado — el único rastro era el
            // 400 que veía el usuario. LogError clasifica solo con mirar el tipo de excepción:
            // SqlException -> "SQL" (Base de Datos), cualquier otra -> "DOTNET" (API).
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(LimitesColumnasController), nameof(GetLimites));
            return BadRequest(ApiResponse<string>.Fail(ex.Message));
        }
    }
}
