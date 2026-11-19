using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SubElementosCostoRepository : ISubElementosCostoRepository
{
    private readonly IDbConnectionFactory _db;
    public SubElementosCostoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SubElementosCosto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SubElementosCosto>(SP.SCHEMACostos.SubElementosCosto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<SubElementosCosto?> ShowbyidAsync(string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SubElementosCosto>(SP.SCHEMACostos.SubElementosCosto.Showbyid, new { COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO, COD_SUB_ELEMENTO_COSTO = COD_SUB_ELEMENTO_COSTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMACostos.SubElementosCosto.Showcombox);
    }

    public async Task<IEnumerable<SubElementosCosto>> SearchAsync(SubElementosCostoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SubElementosCosto>(SP.SCHEMACostos.SubElementosCosto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SubElementosCostoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ELEMENTO_COSTO", dto.COD_ELEMENTO_COSTO);
        p.Add("COD_SUB_ELEMENTO_COSTO", dto.COD_SUB_ELEMENTO_COSTO);
        p.Add("DES_SUB_ELEMENTO_COSTO", dto.DES_SUB_ELEMENTO_COSTO);
        p.Add("COD_CATEGORIA_COMPORTAMIENTO", dto.COD_CATEGORIA_COMPORTAMIENTO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.SubElementosCosto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.SubElementosCosto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.SubElementosCosto.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SubElementosCostoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ELEMENTO_COSTO", dto.COD_ELEMENTO_COSTO);
        p.Add("COD_SUB_ELEMENTO_COSTO", dto.COD_SUB_ELEMENTO_COSTO);
        p.Add("DES_SUB_ELEMENTO_COSTO", dto.DES_SUB_ELEMENTO_COSTO);
        p.Add("COD_CATEGORIA_COMPORTAMIENTO", dto.COD_CATEGORIA_COMPORTAMIENTO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.SubElementosCosto.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.SubElementosCosto.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.SubElementosCosto.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.SubElementosCosto.Delete, new { COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO, COD_SUB_ELEMENTO_COSTO = COD_SUB_ELEMENTO_COSTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.SubElementosCosto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.SubElementosCosto.Delete, new { COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO, COD_SUB_ELEMENTO_COSTO = COD_SUB_ELEMENTO_COSTO })
        };
    }
}

}