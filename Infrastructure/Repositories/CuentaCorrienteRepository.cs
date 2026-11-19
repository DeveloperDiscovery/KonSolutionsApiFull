using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CuentaCorrienteRepository : ICuentaCorrienteRepository
{
    private readonly IDbConnectionFactory _db;
    public CuentaCorrienteRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CuentaCorriente>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CuentaCorriente>(SP.SCHEMAFinanzas.CuentaCorriente.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CuentaCorriente?> ShowbyidAsync(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD, string COD_MONEDA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CuentaCorriente>(SP.SCHEMAFinanzas.CuentaCorriente.Showbyid, new { COD_ENTIDAD = COD_ENTIDAD, COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD, COD_MONEDA = COD_MONEDA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.CuentaCorriente.Showcombox);
    }

    public async Task<IEnumerable<CuentaCorriente>> SearchAsync(CuentaCorrienteSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CuentaCorriente>(SP.SCHEMAFinanzas.CuentaCorriente.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CuentaCorrienteInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_IMPORTE_SALDO", dto.VAL_IMPORTE_SALDO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.CuentaCorriente.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.CuentaCorriente.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.CuentaCorriente.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CuentaCorrienteUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_IMPORTE_SALDO", dto.VAL_IMPORTE_SALDO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.CuentaCorriente.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.CuentaCorriente.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.CuentaCorriente.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD, string COD_MONEDA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.CuentaCorriente.Delete, new { COD_ENTIDAD = COD_ENTIDAD, COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD, COD_MONEDA = COD_MONEDA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.CuentaCorriente.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.CuentaCorriente.Delete, new { COD_ENTIDAD = COD_ENTIDAD, COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD, COD_MONEDA = COD_MONEDA })
        };
    }
}

}