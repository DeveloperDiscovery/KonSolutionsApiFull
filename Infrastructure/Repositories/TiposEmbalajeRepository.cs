using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposEmbalajeRepository : ITiposEmbalajeRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposEmbalajeRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposEmbalaje>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposEmbalaje>(SP.SCHEMALogistica.TiposEmbalaje.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposEmbalaje?> ShowbyidAsync(string COD_TIPO_EMBALAJE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposEmbalaje>(SP.SCHEMALogistica.TiposEmbalaje.Showbyid, new { COD_TIPO_EMBALAJE = COD_TIPO_EMBALAJE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.TiposEmbalaje.Showcombox);
    }

    public async Task<IEnumerable<TiposEmbalaje>> SearchAsync(TiposEmbalajeSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposEmbalaje>(SP.SCHEMALogistica.TiposEmbalaje.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposEmbalajeInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_EMBALAJE", dto.COD_TIPO_EMBALAJE);
        p.Add("DES_TIPO_EMBALAJE", dto.DES_TIPO_EMBALAJE);
        p.Add("FLG_INNER_PACK", dto.FLG_INNER_PACK ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposEmbalaje.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposEmbalaje.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposEmbalaje.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposEmbalajeUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_EMBALAJE", dto.COD_TIPO_EMBALAJE);
        p.Add("DES_TIPO_EMBALAJE", dto.DES_TIPO_EMBALAJE);
        p.Add("FLG_INNER_PACK", dto.FLG_INNER_PACK ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposEmbalaje.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposEmbalaje.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposEmbalaje.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_EMBALAJE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposEmbalaje.Delete, new { COD_TIPO_EMBALAJE = COD_TIPO_EMBALAJE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposEmbalaje.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposEmbalaje.Delete, new { COD_TIPO_EMBALAJE = COD_TIPO_EMBALAJE })
        };
    }
}

}