using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposLoteControlRepository : ITiposLoteControlRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposLoteControlRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposLoteControl>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposLoteControl>(SP.SCHEMALogistica.TiposLoteControl.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposLoteControl?> ShowbyidAsync(string COD_TIPO_LOTE_CONTROL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposLoteControl>(SP.SCHEMALogistica.TiposLoteControl.Showbyid, new { COD_TIPO_LOTE_CONTROL = COD_TIPO_LOTE_CONTROL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.TiposLoteControl.Showcombox);
    }

    public async Task<IEnumerable<TiposLoteControl>> SearchAsync(TiposLoteControlSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposLoteControl>(SP.SCHEMALogistica.TiposLoteControl.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposLoteControlInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_LOTE_CONTROL", dto.COD_TIPO_LOTE_CONTROL);
        p.Add("DES_TIPO_LOTE_CONTROL", dto.DES_TIPO_LOTE_CONTROL);
        p.Add("FLG_LOCAL", dto.FLG_LOCAL ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposLoteControl.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposLoteControl.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposLoteControl.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposLoteControlUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_LOTE_CONTROL", dto.COD_TIPO_LOTE_CONTROL);
        p.Add("DES_TIPO_LOTE_CONTROL", dto.DES_TIPO_LOTE_CONTROL);
        p.Add("FLG_LOCAL", dto.FLG_LOCAL ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposLoteControl.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposLoteControl.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposLoteControl.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_LOTE_CONTROL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposLoteControl.Delete, new { COD_TIPO_LOTE_CONTROL = COD_TIPO_LOTE_CONTROL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposLoteControl.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposLoteControl.Delete, new { COD_TIPO_LOTE_CONTROL = COD_TIPO_LOTE_CONTROL })
        };
    }
}

}