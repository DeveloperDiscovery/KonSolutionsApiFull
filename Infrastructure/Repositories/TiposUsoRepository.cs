using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposUsoRepository : ITiposUsoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposUsoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposUso>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposUso>(SP.SCHEMAEstandar.TiposUso.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposUso?> ShowbyidAsync(string COD_TIPO_USO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposUso>(SP.SCHEMAEstandar.TiposUso.Showbyid, new { COD_TIPO_USO = COD_TIPO_USO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TiposUso>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposUso>(SP.SCHEMAEstandar.TiposUso.Showcombox, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposUsoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_USO", dto.COD_TIPO_USO);
        p.Add("DES_TIPO_USO", dto.DES_TIPO_USO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposUso.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposUso.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposUso.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_USO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposUso.Delete, new { COD_TIPO_USO = COD_TIPO_USO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposUso.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposUso.Delete, new { COD_TIPO_USO = COD_TIPO_USO })
        };
    }
}
}
