using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ClasesCostoRepository : IClasesCostoRepository
{
    private readonly IDbConnectionFactory _db;
    public ClasesCostoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ClasesCosto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesCosto>(SP.SCHEMACostos.ClasesCosto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ClasesCosto?> ShowbyidAsync(string COD_CLASE_COSTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ClasesCosto>(SP.SCHEMACostos.ClasesCosto.Showbyid, new { COD_CLASE_COSTO = COD_CLASE_COSTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMACostos.ClasesCosto.Showcombox);
    }

    public async Task<IEnumerable<ClasesCosto>> SearchAsync(ClasesCostoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesCosto>(SP.SCHEMACostos.ClasesCosto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ClasesCostoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_COSTO", dto.COD_CLASE_COSTO);
        p.Add("DES_CLASE_COSTO", dto.DES_CLASE_COSTO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.ClasesCosto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.ClasesCosto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.ClasesCosto.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ClasesCostoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_COSTO", dto.COD_CLASE_COSTO);
        p.Add("DES_CLASE_COSTO", dto.DES_CLASE_COSTO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.ClasesCosto.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.ClasesCosto.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.ClasesCosto.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_COSTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.ClasesCosto.Delete, new { COD_CLASE_COSTO = COD_CLASE_COSTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.ClasesCosto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.ClasesCosto.Delete, new { COD_CLASE_COSTO = COD_CLASE_COSTO })
        };
    }
}

}