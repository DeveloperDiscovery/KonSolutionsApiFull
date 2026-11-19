using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SaldosCuentaCorrienteRepository : ISaldosCuentaCorrienteRepository
{
    private readonly IDbConnectionFactory _db;
    public SaldosCuentaCorrienteRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SaldosCuentaCorriente>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SaldosCuentaCorriente>(SP.SCHEMAContabilidad.SaldosCuentaCorriente.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<SaldosCuentaCorriente?> ShowbyidAsync(int NUM_PERIODO_CONTABLE, decimal COD_ENTIDAD_CONTABLE, string COD_CUENTA_CONTABLE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SaldosCuentaCorriente>(SP.SCHEMAContabilidad.SaldosCuentaCorriente.Showbyid, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ENTIDAD_CONTABLE = COD_ENTIDAD_CONTABLE, COD_CUENTA_CONTABLE = COD_CUENTA_CONTABLE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAContabilidad.SaldosCuentaCorriente.Showcombox);
    }

    public async Task<IEnumerable<SaldosCuentaCorriente>> SearchAsync(SaldosCuentaCorrienteSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SaldosCuentaCorriente>(SP.SCHEMAContabilidad.SaldosCuentaCorriente.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SaldosCuentaCorrienteInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ENTIDAD_CONTABLE", dto.COD_ENTIDAD_CONTABLE);
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_IMPORTE_DEBE", dto.VAL_IMPORTE_DEBE);
        p.Add("VAL_IMPORTE_HABER", dto.VAL_IMPORTE_HABER);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.SaldosCuentaCorriente.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.SaldosCuentaCorriente.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.SaldosCuentaCorriente.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SaldosCuentaCorrienteUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ENTIDAD_CONTABLE", dto.COD_ENTIDAD_CONTABLE);
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_IMPORTE_DEBE", dto.VAL_IMPORTE_DEBE);
        p.Add("VAL_IMPORTE_HABER", dto.VAL_IMPORTE_HABER);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.SaldosCuentaCorriente.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.SaldosCuentaCorriente.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.SaldosCuentaCorriente.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE, decimal COD_ENTIDAD_CONTABLE, string COD_CUENTA_CONTABLE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.SaldosCuentaCorriente.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ENTIDAD_CONTABLE = COD_ENTIDAD_CONTABLE, COD_CUENTA_CONTABLE = COD_CUENTA_CONTABLE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.SaldosCuentaCorriente.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.SaldosCuentaCorriente.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ENTIDAD_CONTABLE = COD_ENTIDAD_CONTABLE, COD_CUENTA_CONTABLE = COD_CUENTA_CONTABLE })
        };
    }
}

}