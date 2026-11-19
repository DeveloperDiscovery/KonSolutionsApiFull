using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CostosProduccionRepository : ICostosProduccionRepository
{
    private readonly IDbConnectionFactory _db;
    public CostosProduccionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CostosProduccion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CostosProduccion>(SP.SCHEMACostos.CostosProduccion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CostosProduccion?> ShowbyidAsync(int NUM_PERIODO_CONTABLE, string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CostosProduccion>(SP.SCHEMACostos.CostosProduccion.Showbyid, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO, COD_SUB_ELEMENTO_COSTO = COD_SUB_ELEMENTO_COSTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMACostos.CostosProduccion.Showcombox);
    }

    public async Task<IEnumerable<CostosProduccion>> SearchAsync(CostosProduccionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CostosProduccion>(SP.SCHEMACostos.CostosProduccion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CostosProduccionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ELEMENTO_COSTO", dto.COD_ELEMENTO_COSTO);
        p.Add("COD_SUB_ELEMENTO_COSTO", dto.COD_SUB_ELEMENTO_COSTO);
        p.Add("VAL_COSTO", dto.VAL_COSTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosProduccion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosProduccion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosProduccion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CostosProduccionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ELEMENTO_COSTO", dto.COD_ELEMENTO_COSTO);
        p.Add("COD_SUB_ELEMENTO_COSTO", dto.COD_SUB_ELEMENTO_COSTO);
        p.Add("VAL_COSTO", dto.VAL_COSTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosProduccion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosProduccion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosProduccion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosProduccion.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO, COD_SUB_ELEMENTO_COSTO = COD_SUB_ELEMENTO_COSTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosProduccion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosProduccion.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO, COD_SUB_ELEMENTO_COSTO = COD_SUB_ELEMENTO_COSTO })
        };
    }
}

}