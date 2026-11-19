using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class FormasPagoRepository : IFormasPagoRepository
{
    private readonly IDbConnectionFactory _db;
    public FormasPagoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<FormasPago>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FormasPago>(SP.SCHEMAComercial.FormasPago.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<FormasPago?> ShowbyidAsync(int COD_FORMA_PAGO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FormasPago>(SP.SCHEMAComercial.FormasPago.Showbyid, new { COD_FORMA_PAGO = COD_FORMA_PAGO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.FormasPago.Showcombox);
    }

    public async Task<IEnumerable<FormasPago>> SearchAsync(FormasPagoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FormasPago>(SP.SCHEMAComercial.FormasPago.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(FormasPagoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_FORMA_PAGO", dto.COD_FORMA_PAGO);
        p.Add("DES_FORMA_PAGO", dto.DES_FORMA_PAGO);
        p.Add("FLG_EFECTIVO", dto.FLG_EFECTIVO ?? false);
        p.Add("COD_FORMA_PAGO_SUNAT", dto.COD_FORMA_PAGO_SUNAT);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.FormasPago.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.FormasPago.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.FormasPago.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(FormasPagoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_FORMA_PAGO", dto.COD_FORMA_PAGO);
        p.Add("DES_FORMA_PAGO", dto.DES_FORMA_PAGO);
        p.Add("FLG_EFECTIVO", dto.FLG_EFECTIVO ?? false);
        p.Add("COD_FORMA_PAGO_SUNAT", dto.COD_FORMA_PAGO_SUNAT);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.FormasPago.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.FormasPago.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.FormasPago.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_FORMA_PAGO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.FormasPago.Delete, new { COD_FORMA_PAGO = COD_FORMA_PAGO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.FormasPago.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.FormasPago.Delete, new { COD_FORMA_PAGO = COD_FORMA_PAGO })
        };
    }
}

}