using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SaldosContablesRepository : ISaldosContablesRepository
{
    private readonly IDbConnectionFactory _db;
    public SaldosContablesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SaldosContables>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SaldosContables>(SP.SCHEMAContabilidad.SaldosContables.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<SaldosContables?> ShowbyidAsync(int NUM_PERIODO_CONTABLE, string COD_CUENTA_CONTABLE, string COD_MONEDA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SaldosContables>(SP.SCHEMAContabilidad.SaldosContables.Showbyid, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_CUENTA_CONTABLE = COD_CUENTA_CONTABLE, COD_MONEDA = COD_MONEDA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAContabilidad.SaldosContables.Showcombox);
    }

    public async Task<IEnumerable<SaldosContables>> SearchAsync(SaldosContablesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SaldosContables>(SP.SCHEMAContabilidad.SaldosContables.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SaldosContablesInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_INICIAL_DEBE", dto.VAL_INICIAL_DEBE);
        p.Add("VAL_INICIAL_HABER", dto.VAL_INICIAL_HABER);
        p.Add("VAL_MOVIMIENTO_DEBE", dto.VAL_MOVIMIENTO_DEBE);
        p.Add("VAL_MOVIMIENTO_HABER", dto.VAL_MOVIMIENTO_HABER);
        p.Add("VAL_AJUSTE_DEBE", dto.VAL_AJUSTE_DEBE);
        p.Add("VAL_AJUSTE_HABER", dto.VAL_AJUSTE_HABER);
        p.Add("VAL_CIERRE_DEBE", dto.VAL_CIERRE_DEBE);
        p.Add("VAL_CIERRE_HABER", dto.VAL_CIERRE_HABER);
        p.Add("VAL_SALDO_DEBE", dto.VAL_SALDO_DEBE);
        p.Add("VAL_SALDO_HABER", dto.VAL_SALDO_HABER);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.SaldosContables.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.SaldosContables.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.SaldosContables.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SaldosContablesUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_INICIAL_DEBE", dto.VAL_INICIAL_DEBE);
        p.Add("VAL_INICIAL_HABER", dto.VAL_INICIAL_HABER);
        p.Add("VAL_MOVIMIENTO_DEBE", dto.VAL_MOVIMIENTO_DEBE);
        p.Add("VAL_MOVIMIENTO_HABER", dto.VAL_MOVIMIENTO_HABER);
        p.Add("VAL_AJUSTE_DEBE", dto.VAL_AJUSTE_DEBE);
        p.Add("VAL_AJUSTE_HABER", dto.VAL_AJUSTE_HABER);
        p.Add("VAL_CIERRE_DEBE", dto.VAL_CIERRE_DEBE);
        p.Add("VAL_CIERRE_HABER", dto.VAL_CIERRE_HABER);
        p.Add("VAL_SALDO_DEBE", dto.VAL_SALDO_DEBE);
        p.Add("VAL_SALDO_HABER", dto.VAL_SALDO_HABER);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.SaldosContables.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.SaldosContables.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.SaldosContables.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_CUENTA_CONTABLE, string COD_MONEDA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.SaldosContables.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_CUENTA_CONTABLE = COD_CUENTA_CONTABLE, COD_MONEDA = COD_MONEDA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.SaldosContables.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.SaldosContables.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_CUENTA_CONTABLE = COD_CUENTA_CONTABLE, COD_MONEDA = COD_MONEDA })
        };
    }
}

}