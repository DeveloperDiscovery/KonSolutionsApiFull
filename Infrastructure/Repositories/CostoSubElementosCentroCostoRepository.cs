using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CostoSubElementosCentroCostoRepository : ICostoSubElementosCentroCostoRepository
{
    private readonly IDbConnectionFactory _db;
    public CostoSubElementosCentroCostoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CostoSubElementosCentroCosto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CostoSubElementosCentroCosto>(SP.SCHEMACostos.CostoSubElementosCentroCosto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CostoSubElementosCentroCosto?> ShowbyidAsync(int NUM_PERIODO_CONTABLE, string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CostoSubElementosCentroCosto>(SP.SCHEMACostos.CostoSubElementosCentroCosto.Showbyid, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO, COD_SUB_ELEMENTO_COSTO = COD_SUB_ELEMENTO_COSTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMACostos.CostoSubElementosCentroCosto.Showcombox);
    }

    public async Task<IEnumerable<CostoSubElementosCentroCosto>> SearchAsync(CostoSubElementosCentroCostoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CostoSubElementosCentroCosto>(SP.SCHEMACostos.CostoSubElementosCentroCosto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CostoSubElementosCentroCostoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ELEMENTO_COSTO", dto.COD_ELEMENTO_COSTO);
        p.Add("COD_SUB_ELEMENTO_COSTO", dto.COD_SUB_ELEMENTO_COSTO);
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("VAL_COSTO", dto.VAL_COSTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostoSubElementosCentroCosto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostoSubElementosCentroCosto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostoSubElementosCentroCosto.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CostoSubElementosCentroCostoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ELEMENTO_COSTO", dto.COD_ELEMENTO_COSTO);
        p.Add("COD_SUB_ELEMENTO_COSTO", dto.COD_SUB_ELEMENTO_COSTO);
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("VAL_COSTO", dto.VAL_COSTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostoSubElementosCentroCosto.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostoSubElementosCentroCosto.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostoSubElementosCentroCosto.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostoSubElementosCentroCosto.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO, COD_SUB_ELEMENTO_COSTO = COD_SUB_ELEMENTO_COSTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostoSubElementosCentroCosto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostoSubElementosCentroCosto.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO, COD_SUB_ELEMENTO_COSTO = COD_SUB_ELEMENTO_COSTO })
        };
    }
}

}