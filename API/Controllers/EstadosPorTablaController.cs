using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Common;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.API.Controllers;

// Endpoint genérico: dado el nombre de una tabla, obtiene su COD_TIPO_ESTADO
// (vía ESTANDAR.TABLAS_TIPOS_ESTADO_SHOWBYID) y devuelve los estados (SHOWCOMBOX) de ese tipo.
[Authorize]
[SinControlPermisos("Combo de apoyo: lista los estados válidos de una tabla. Lo necesitan todas "
                    + "las pantallas con flujo de estados, sin importar el rol.")]
[Route("api/v1/seguridad/estados-por-tabla")]
public class EstadosPorTablaController : BaseController
{
    private readonly IDbConnectionFactory _db;
    public EstadosPorTablaController(IDbConnectionFactory db) => _db = db;

    // Respuesta de diagnóstico: incluye el COD_TIPO_ESTADO y un detalle del proceso.
    public class EstadosDiag
    {
        public string DesTabla { get; set; } = "";
        public int? CodTipoEstado { get; set; }
        public int Cantidad { get; set; }
        public string Paso { get; set; } = "";
        public List<ComboboxItem> Estados { get; set; } = new();
        public List<EstadoItem> EstadosDetalle { get; set; } = new();
    }

    [HttpGet]
    public async Task<IActionResult> GetEstados([FromQuery] string desTabla)
    {
        var diag = new EstadosDiag { DesTabla = desTabla };
        try
        {
            using var conn = _db.CreateConnection();

            // PASO 1: COD_TIPO_ESTADO de la tabla
            var tabla = await conn.QueryFirstOrDefaultAsync(
                "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWBYID",
                new { DES_TABLA = desTabla }, commandType: CommandType.StoredProcedure);

            if (tabla is null)
            {
                diag.Paso = "PASO1: SHOWBYID no devolvió fila para esta tabla";
                return Ok(ApiResponse<EstadosDiag>.Ok(diag, message: diag.Paso));
            }

            var dict = (IDictionary<string, object>)tabla;
            // Listar las claves que devolvió, por si la columna se llama distinto
            var claves = string.Join(",", dict.Keys);

            if (!dict.TryGetValue("COD_TIPO_ESTADO", out var codTipoObj) || codTipoObj is null)
            {
                diag.Paso = $"PASO1: la fila no tiene COD_TIPO_ESTADO. Columnas devueltas: [{claves}]";
                return Ok(ApiResponse<EstadosDiag>.Ok(diag, message: diag.Paso));
            }

            var codTipoEstado = Convert.ToInt32(codTipoObj);
            diag.CodTipoEstado = codTipoEstado;

            // PASO 2: estados de ese tipo (SHOWCOMBOX)
            var filas = await conn.QueryAsync(
                "PROC_ESTANDAR_ESTADOS_SHOWCOMBOX",
                new { COD_TIPO_ESTADO = codTipoEstado }, commandType: CommandType.StoredProcedure);

            var estadosList = filas.Select(f =>
            {
                var d = (IDictionary<string, object>)f;
                var codStr   = d.TryGetValue("COD_ESTADO",   out var id) ? id?.ToString() ?? "" : "";
                var des      = d.TryGetValue("DES_ESTADO",   out var tx) ? tx?.ToString() ?? "" : "";
                var back     = d.TryGetValue("DES_BACKCOLOR", out var bc) ? bc?.ToString() ?? "" : "";
                var fore     = d.TryGetValue("DES_FORECOLOR", out var fc) ? fc?.ToString() ?? "" : "";
                var esDef    = d.TryGetValue("FLG_DEFAULT",  out var df) && df is not null && Convert.ToBoolean(df);
                return new EstadoItem
                {
                    CodEstado = int.TryParse(codStr, out var n) ? n : 0,
                    DesEstado = des,
                    BackColor = back,
                    ForeColor = fore,
                    EsDefault = esDef
                };
            }).ToList();

            diag.EstadosDetalle = estadosList;
            diag.Estados = estadosList.Select(e => new ComboboxItem
            {
                Id = e.CodEstado.ToString(),
                Texto = e.DesEstado
            }).ToList();
            diag.Cantidad = estadosList.Count;
            diag.Paso = estadosList.Count > 0
                ? $"OK: tipo={codTipoEstado}, {estadosList.Count} estados"
                : $"PASO2: SHOWCOMBOX devolvió 0 estados para COD_TIPO_ESTADO={codTipoEstado}";

            return Ok(ApiResponse<EstadosDiag>.Ok(diag, message: diag.Paso));
        }
        catch (Exception ex)
        {
            diag.Paso = $"ERROR SQL: {ex.Message}";
            return Ok(ApiResponse<EstadosDiag>.Ok(diag, message: diag.Paso));
        }
    }
}
