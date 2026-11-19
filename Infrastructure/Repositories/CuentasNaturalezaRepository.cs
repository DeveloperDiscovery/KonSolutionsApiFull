using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CuentasNaturalezaRepository : ICuentasNaturalezaRepository
{
    private readonly IDbConnectionFactory _db;
    public CuentasNaturalezaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CuentasNaturaleza>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CuentasNaturaleza>(SP.SCHEMAContabilidad.CuentasNaturaleza.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CuentasNaturaleza?> ShowbyidAsync(string COD_CUENTA_NATURALEZA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CuentasNaturaleza>(SP.SCHEMAContabilidad.CuentasNaturaleza.Showbyid, new { COD_CUENTA_NATURALEZA = COD_CUENTA_NATURALEZA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAContabilidad.CuentasNaturaleza.Showcombox);
    }

    public async Task<IEnumerable<CuentasNaturaleza>> SearchAsync(CuentasNaturalezaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CuentasNaturaleza>(SP.SCHEMAContabilidad.CuentasNaturaleza.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CuentasNaturalezaInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CUENTA_NATURALEZA", dto.COD_CUENTA_NATURALEZA);
        p.Add("DES_CUENTA_NATURALEZA", dto.DES_CUENTA_NATURALEZA);
        p.Add("DES_CUENTA_NATURALEZA_ALTERNATIVA", dto.DES_CUENTA_NATURALEZA_ALTERNATIVA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.CuentasNaturaleza.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.CuentasNaturaleza.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.CuentasNaturaleza.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CuentasNaturalezaUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CUENTA_NATURALEZA", dto.COD_CUENTA_NATURALEZA);
        p.Add("DES_CUENTA_NATURALEZA", dto.DES_CUENTA_NATURALEZA);
        p.Add("DES_CUENTA_NATURALEZA_ALTERNATIVA", dto.DES_CUENTA_NATURALEZA_ALTERNATIVA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.CuentasNaturaleza.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.CuentasNaturaleza.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.CuentasNaturaleza.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CUENTA_NATURALEZA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.CuentasNaturaleza.Delete, new { COD_CUENTA_NATURALEZA = COD_CUENTA_NATURALEZA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.CuentasNaturaleza.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.CuentasNaturaleza.Delete, new { COD_CUENTA_NATURALEZA = COD_CUENTA_NATURALEZA })
        };
    }
}

}