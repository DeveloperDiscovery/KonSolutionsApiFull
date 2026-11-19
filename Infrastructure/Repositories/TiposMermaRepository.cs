using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposMermaRepository : ITiposMermaRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposMermaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposMerma>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposMerma>(SP.SCHEMAIngenieria.TiposMerma.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposMerma?> ShowbyidAsync(string COD_TIPO_MERMA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposMerma>(SP.SCHEMAIngenieria.TiposMerma.Showbyid, new { COD_TIPO_MERMA = COD_TIPO_MERMA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.TiposMerma.Showcombox);
    }

    public async Task<IEnumerable<TiposMerma>> SearchAsync(TiposMermaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposMerma>(SP.SCHEMAIngenieria.TiposMerma.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposMermaInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_MERMA", dto.COD_TIPO_MERMA);
        p.Add("DES_TIPO_MERMA", dto.DES_TIPO_MERMA);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("FLG_LOGISTICA", dto.FLG_LOGISTICA ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposMerma.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposMerma.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposMerma.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposMermaUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_MERMA", dto.COD_TIPO_MERMA);
        p.Add("DES_TIPO_MERMA", dto.DES_TIPO_MERMA);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("FLG_LOGISTICA", dto.FLG_LOGISTICA ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposMerma.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposMerma.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposMerma.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_MERMA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposMerma.Delete, new { COD_TIPO_MERMA = COD_TIPO_MERMA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposMerma.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposMerma.Delete, new { COD_TIPO_MERMA = COD_TIPO_MERMA })
        };
    }
}

}