using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposDescuentoRepository : ITiposDescuentoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposDescuentoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposDescuento>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDescuento>(SP.SCHEMAComercial.TiposDescuento.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposDescuento?> ShowbyidAsync(int COD_TIPO_DESCUENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposDescuento>(SP.SCHEMAComercial.TiposDescuento.Showbyid, new { COD_TIPO_DESCUENTO = COD_TIPO_DESCUENTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposDescuento.Showcombox);
    }

    public async Task<IEnumerable<TiposDescuento>> SearchAsync(TiposDescuentoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDescuento>(SP.SCHEMAComercial.TiposDescuento.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposDescuentoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_DESCUENTO", dto.COD_TIPO_DESCUENTO);
        p.Add("DES_TIPO_DESCUENTO", dto.DES_TIPO_DESCUENTO);
        p.Add("TAS_DESCUENTO", dto.TAS_DESCUENTO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposDescuento.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposDescuento.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposDescuento.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposDescuentoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_DESCUENTO", dto.COD_TIPO_DESCUENTO);
        p.Add("DES_TIPO_DESCUENTO", dto.DES_TIPO_DESCUENTO);
        p.Add("TAS_DESCUENTO", dto.TAS_DESCUENTO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposDescuento.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposDescuento.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposDescuento.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_TIPO_DESCUENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposDescuento.Delete, new { COD_TIPO_DESCUENTO = COD_TIPO_DESCUENTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposDescuento.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposDescuento.Delete, new { COD_TIPO_DESCUENTO = COD_TIPO_DESCUENTO })
        };
    }
}

}