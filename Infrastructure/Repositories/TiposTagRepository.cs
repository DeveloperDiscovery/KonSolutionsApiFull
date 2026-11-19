using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposTagRepository : ITiposTagRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposTagRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposTag>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposTag>(SP.SCHEMASafety.TiposTag.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposTag?> ShowbyidAsync(string COD_TIPO_TAG)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposTag>(SP.SCHEMASafety.TiposTag.Showbyid, new { COD_TIPO_TAG = COD_TIPO_TAG }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASafety.TiposTag.Showcombox);
    }

    public async Task<IEnumerable<TiposTag>> SearchAsync(TiposTagSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposTag>(SP.SCHEMASafety.TiposTag.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposTagInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_TAG", dto.COD_TIPO_TAG);
        p.Add("DES_TIPO_TAG", dto.DES_TIPO_TAG);
        p.Add("FLG_PRODUCTO", dto.FLG_PRODUCTO ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.TiposTag.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.TiposTag.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.TiposTag.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposTagUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_TAG", dto.COD_TIPO_TAG);
        p.Add("DES_TIPO_TAG", dto.DES_TIPO_TAG);
        p.Add("FLG_PRODUCTO", dto.FLG_PRODUCTO ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.TiposTag.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.TiposTag.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.TiposTag.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_TAG)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.TiposTag.Delete, new { COD_TIPO_TAG = COD_TIPO_TAG }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.TiposTag.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.TiposTag.Delete, new { COD_TIPO_TAG = COD_TIPO_TAG })
        };
    }
}

}