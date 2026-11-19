using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposReservaStockRepository : ITiposReservaStockRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposReservaStockRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposReservaStock>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposReservaStock>(SP.SCHEMALogistica.TiposReservaStock.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposReservaStock?> ShowbyidAsync(string COD_TIPO_RESERVA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposReservaStock>(SP.SCHEMALogistica.TiposReservaStock.Showbyid, new { COD_TIPO_RESERVA = COD_TIPO_RESERVA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.TiposReservaStock.Showcombox);
    }

    public async Task<IEnumerable<TiposReservaStock>> SearchAsync(TiposReservaStockSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposReservaStock>(SP.SCHEMALogistica.TiposReservaStock.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposReservaStockInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_RESERVA", dto.COD_TIPO_RESERVA);
        p.Add("DES_TIPO_RESERVA", dto.DES_TIPO_RESERVA);
        p.Add("FLG_VENTA", dto.FLG_VENTA ?? false);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposReservaStock.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposReservaStock.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposReservaStock.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposReservaStockUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_RESERVA", dto.COD_TIPO_RESERVA);
        p.Add("DES_TIPO_RESERVA", dto.DES_TIPO_RESERVA);
        p.Add("FLG_VENTA", dto.FLG_VENTA ?? false);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposReservaStock.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposReservaStock.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposReservaStock.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_RESERVA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposReservaStock.Delete, new { COD_TIPO_RESERVA = COD_TIPO_RESERVA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposReservaStock.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposReservaStock.Delete, new { COD_TIPO_RESERVA = COD_TIPO_RESERVA })
        };
    }
}

}