using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ClasesProcesoRepository : IClasesProcesoRepository
{
    private readonly IDbConnectionFactory _db;
    public ClasesProcesoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ClasesProceso>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesProceso>(SP.SCHEMAProduccion.ClasesProceso.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ClasesProceso?> ShowbyidAsync(string COD_CLASE_PROCESO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ClasesProceso>(SP.SCHEMAProduccion.ClasesProceso.Showbyid, new { COD_CLASE_PROCESO = COD_CLASE_PROCESO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProduccion.ClasesProceso.Showcombox);
    }

    public async Task<IEnumerable<ClasesProceso>> SearchAsync(ClasesProcesoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesProceso>(SP.SCHEMAProduccion.ClasesProceso.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ClasesProcesoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_PROCESO", dto.COD_CLASE_PROCESO);
        p.Add("DES_CLASE_PROCESO", dto.DES_CLASE_PROCESO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("FLG_GENERA_GRUPO_REQUERIMIENTO", dto.FLG_GENERA_GRUPO_REQUERIMIENTO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ClasesProceso.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ClasesProceso.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ClasesProceso.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ClasesProcesoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_PROCESO", dto.COD_CLASE_PROCESO);
        p.Add("DES_CLASE_PROCESO", dto.DES_CLASE_PROCESO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("FLG_GENERA_GRUPO_REQUERIMIENTO", dto.FLG_GENERA_GRUPO_REQUERIMIENTO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ClasesProceso.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ClasesProceso.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ClasesProceso.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_PROCESO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ClasesProceso.Delete, new { COD_CLASE_PROCESO = COD_CLASE_PROCESO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ClasesProceso.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ClasesProceso.Delete, new { COD_CLASE_PROCESO = COD_CLASE_PROCESO })
        };
    }
}

}