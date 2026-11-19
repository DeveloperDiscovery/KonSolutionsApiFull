using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProcesosRepository : IProcesosRepository
{
    private readonly IDbConnectionFactory _db;
    public ProcesosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Procesos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Procesos>(SP.SCHEMAProduccion.Procesos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Procesos?> ShowbyidAsync(string COD_PROCESO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Procesos>(SP.SCHEMAProduccion.Procesos.Showbyid, new { COD_PROCESO = COD_PROCESO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProduccion.Procesos.Showcombox);
    }

    public async Task<IEnumerable<Procesos>> SearchAsync(ProcesosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Procesos>(SP.SCHEMAProduccion.Procesos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProcesosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("DES_PROCESO", dto.DES_PROCESO);
        p.Add("DES_ALIAS", dto.DES_ALIAS);
        p.Add("COD_CLASE_PROCESO", dto.COD_CLASE_PROCESO);
        p.Add("FLG_ORDEN_FABRICACION", dto.FLG_ORDEN_FABRICACION);
        p.Add("DES_NOMBRE_ORDEN_PRODUCCION", dto.DES_NOMBRE_ORDEN_PRODUCCION);
        p.Add("DES_NOMBRE_ORDEN_PRODUCCION_ITEM", dto.DES_NOMBRE_ORDEN_PRODUCCION_ITEM);
        p.Add("DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD", dto.DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD);
        p.Add("DES_EQUIPO_PRODUCCION_PRINCIPAL", dto.DES_EQUIPO_PRODUCCION_PRINCIPAL);
        p.Add("DES_LINEA_PRODUCCION", dto.DES_LINEA_PRODUCCION);
        p.Add("FLG_EMBALAJE_FINAL", dto.FLG_EMBALAJE_FINAL);
        p.Add("FLG_CONTROL_CALIDAD", dto.FLG_CONTROL_CALIDAD);
        p.Add("FLG_PROCESO_MANTENIMIENTO", dto.FLG_PROCESO_MANTENIMIENTO);
        p.Add("COD_TIPO_MERMA", dto.COD_TIPO_MERMA);
        p.Add("VAL_ULTIMO_CORRELATIVO", dto.VAL_ULTIMO_CORRELATIVO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.Procesos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.Procesos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.Procesos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProcesosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("DES_PROCESO", dto.DES_PROCESO);
        p.Add("DES_ALIAS", dto.DES_ALIAS);
        p.Add("COD_CLASE_PROCESO", dto.COD_CLASE_PROCESO);
        p.Add("FLG_ORDEN_FABRICACION", dto.FLG_ORDEN_FABRICACION);
        p.Add("DES_NOMBRE_ORDEN_PRODUCCION", dto.DES_NOMBRE_ORDEN_PRODUCCION);
        p.Add("DES_NOMBRE_ORDEN_PRODUCCION_ITEM", dto.DES_NOMBRE_ORDEN_PRODUCCION_ITEM);
        p.Add("DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD", dto.DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD);
        p.Add("DES_EQUIPO_PRODUCCION_PRINCIPAL", dto.DES_EQUIPO_PRODUCCION_PRINCIPAL);
        p.Add("DES_LINEA_PRODUCCION", dto.DES_LINEA_PRODUCCION);
        p.Add("FLG_EMBALAJE_FINAL", dto.FLG_EMBALAJE_FINAL);
        p.Add("FLG_CONTROL_CALIDAD", dto.FLG_CONTROL_CALIDAD);
        p.Add("FLG_PROCESO_MANTENIMIENTO", dto.FLG_PROCESO_MANTENIMIENTO);
        p.Add("COD_TIPO_MERMA", dto.COD_TIPO_MERMA);
        p.Add("VAL_ULTIMO_CORRELATIVO", dto.VAL_ULTIMO_CORRELATIVO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.Procesos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.Procesos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.Procesos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PROCESO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.Procesos.Delete, new { COD_PROCESO = COD_PROCESO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.Procesos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.Procesos.Delete, new { COD_PROCESO = COD_PROCESO })
        };
    }
}

    /// <summary>PROC_PRODUCCION_PROCESOS_LOADCOMBOS: clases de proceso + tipos de merma en un
    /// solo viaje para ProcesosDialog. Nota: PROC_PRODUCCION_CLASES_PROCESO_SHOWCOMBOX y
    /// PROC_INGENIERIA_TIPOS_MERMA_SHOWCOMBOX alían sus columnas como "Id"/"Texto" (fix histórico
    /// de esos 2 SPs puntuales) — por eso el nuevo LOADCOMBOS selecciona directo las columnas
    /// reales COD_*/DES_* en vez de reejecutar esos SHOWCOMBOX, para poder mapear por convención
    /// COD_/DES_ sin depender de nombres de columna literales "Id"/"Texto".</summary>
    public async Task<ProcesosLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(SP.SCHEMAProduccion.Procesos.Loadcombos, commandType: CommandType.StoredProcedure);

        var clasesProceso = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposMerma = MapCombo(await multi.ReadAsync<dynamic>());

        return new ProcesosLoadCombosResult { ClasesProceso = clasesProceso, TiposMerma = tiposMerma };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem detectando la columna de código (primera que
    /// empiece con COD_) y la de descripción (primera DES_/NOMBRE_) — nunca por nombre literal
    /// "Id"/"Texto".</summary>
    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas)
    {
        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            var colId = d.Keys.FirstOrDefault(k => k.StartsWith("COD_", StringComparison.OrdinalIgnoreCase));
            var colTexto = d.Keys.FirstOrDefault(k => k.StartsWith("DES_", StringComparison.OrdinalIgnoreCase) || k.StartsWith("NOMBRE_", StringComparison.OrdinalIgnoreCase));
            resultado.Add(new ComboboxItem
            {
                Id = colId is not null ? d[colId]?.ToString() ?? "" : "",
                Texto = colTexto is not null ? d[colTexto]?.ToString() ?? "" : ""
            });
        }
        return resultado;
    }

}