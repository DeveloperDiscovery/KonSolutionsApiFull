using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CondicionesPagoRepository : ICondicionesPagoRepository
{
    private readonly IDbConnectionFactory _db;
    public CondicionesPagoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CondicionesPago>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CondicionesPago>(SP.SCHEMAComercial.CondicionesPago.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CondicionesPago?> ShowbyidAsync(int COD_CONDICION_PAGO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CondicionesPago>(SP.SCHEMAComercial.CondicionesPago.Showbyid, new { COD_CONDICION_PAGO = COD_CONDICION_PAGO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.CondicionesPago.Showcombox);
    }

    public async Task<IEnumerable<CondicionesPago>> SearchAsync(CondicionesPagoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CondicionesPago>(SP.SCHEMAComercial.CondicionesPago.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CondicionesPagoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CONDICION_PAGO", dto.COD_CONDICION_PAGO);
        p.Add("DES_CONDICION_PAGO", dto.DES_CONDICION_PAGO);
        p.Add("FLG_CREDITO", dto.FLG_CREDITO ?? false);
        p.Add("CAN_DIAS", dto.CAN_DIAS);
        p.Add("FLG_DIAS_CALENDARIO", dto.FLG_DIAS_CALENDARIO ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CondicionesPago.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CondicionesPago.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CondicionesPago.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CondicionesPagoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CONDICION_PAGO", dto.COD_CONDICION_PAGO);
        p.Add("DES_CONDICION_PAGO", dto.DES_CONDICION_PAGO);
        p.Add("FLG_CREDITO", dto.FLG_CREDITO ?? false);
        p.Add("CAN_DIAS", dto.CAN_DIAS);
        p.Add("FLG_DIAS_CALENDARIO", dto.FLG_DIAS_CALENDARIO ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CondicionesPago.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CondicionesPago.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CondicionesPago.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_CONDICION_PAGO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CondicionesPago.Delete, new { COD_CONDICION_PAGO = COD_CONDICION_PAGO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CondicionesPago.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CondicionesPago.Delete, new { COD_CONDICION_PAGO = COD_CONDICION_PAGO })
        };
    }
}

}