using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class LotesControlDetalleRepository : ILotesControlDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public LotesControlDetalleRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>El SHOWALL de esta tabla pide el lote: el detalle se lee dentro de su
    /// cabecera, nunca entero.</summary>
    public async Task<IEnumerable<LotesControlDetalle>> ShowallAsync(string NUM_LOTE_CONTROL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<LotesControlDetalle>(SP.SCHEMALogistica.LotesControlDetalle.Showall, new { NUM_LOTE_CONTROL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<LotesControlDetalle?> ShowbyidAsync(string NUM_LOTE_CONTROL, string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<LotesControlDetalle>(SP.SCHEMALogistica.LotesControlDetalle.Showbyid, new { NUM_LOTE_CONTROL = NUM_LOTE_CONTROL, COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.LotesControlDetalle.Showcombox);
    }

    public async Task<IEnumerable<LotesControlDetalle>> SearchAsync(LotesControlDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<LotesControlDetalle>(SP.SCHEMALogistica.LotesControlDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(LotesControlDetalleInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("VAL_TOTAL", dto.VAL_TOTAL);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.LotesControlDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.LotesControlDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.LotesControlDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(LotesControlDetalleUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("VAL_TOTAL", dto.VAL_TOTAL);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.LotesControlDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.LotesControlDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.LotesControlDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string NUM_LOTE_CONTROL, string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.LotesControlDetalle.Delete, new { NUM_LOTE_CONTROL = NUM_LOTE_CONTROL, COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.LotesControlDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.LotesControlDetalle.Delete, new { NUM_LOTE_CONTROL = NUM_LOTE_CONTROL, COD_PRODUCTO = COD_PRODUCTO })
        };
    }
}

}