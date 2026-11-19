using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposViaRepository : ITiposViaRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposViaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposVia>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposVia>(SP.SCHEMAEstandar.TiposVia.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposVia?> ShowbyidAsync(string COD_TIPO_VIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposVia>(SP.SCHEMAEstandar.TiposVia.Showbyid, new { COD_TIPO_VIA = COD_TIPO_VIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.TiposVia.Showcombox);
    }

    public async Task<IEnumerable<TiposVia>> SearchAsync(TiposViaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposVia>(SP.SCHEMAEstandar.TiposVia.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposViaInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_VIA", dto.COD_TIPO_VIA);
        p.Add("DES_TIPO_VIA", dto.DES_TIPO_VIA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposVia.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposVia.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposVia.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposViaUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_VIA", dto.COD_TIPO_VIA);
        p.Add("DES_TIPO_VIA", dto.DES_TIPO_VIA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposVia.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposVia.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposVia.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_VIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposVia.Delete, new { COD_TIPO_VIA = COD_TIPO_VIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposVia.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposVia.Delete, new { COD_TIPO_VIA = COD_TIPO_VIA })
        };
    }
}

}