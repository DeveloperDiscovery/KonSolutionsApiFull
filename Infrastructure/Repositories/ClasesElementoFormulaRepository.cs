using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ClasesElementoFormulaRepository : IClasesElementoFormulaRepository
{
    private readonly IDbConnectionFactory _db;
    public ClasesElementoFormulaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ClasesElementoFormula>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesElementoFormula>(SP.SCHEMAIngenieria.ClasesElementoFormula.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ClasesElementoFormula?> ShowbyidAsync(int COD_CLASE_ELEMENTO_FORMULA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ClasesElementoFormula>(SP.SCHEMAIngenieria.ClasesElementoFormula.Showbyid, new { COD_CLASE_ELEMENTO_FORMULA = COD_CLASE_ELEMENTO_FORMULA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.ClasesElementoFormula.Showcombox);
    }

    public async Task<IEnumerable<ClasesElementoFormula>> SearchAsync(ClasesElementoFormulaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesElementoFormula>(SP.SCHEMAIngenieria.ClasesElementoFormula.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ClasesElementoFormulaInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_ELEMENTO_FORMULA", dto.COD_CLASE_ELEMENTO_FORMULA);
        p.Add("DES_CLASE_ELEMENTO_FORMULA", dto.DES_CLASE_ELEMENTO_FORMULA);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.ClasesElementoFormula.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.ClasesElementoFormula.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.ClasesElementoFormula.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ClasesElementoFormulaUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_ELEMENTO_FORMULA", dto.COD_CLASE_ELEMENTO_FORMULA);
        p.Add("DES_CLASE_ELEMENTO_FORMULA", dto.DES_CLASE_ELEMENTO_FORMULA);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.ClasesElementoFormula.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.ClasesElementoFormula.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.ClasesElementoFormula.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_CLASE_ELEMENTO_FORMULA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.ClasesElementoFormula.Delete, new { COD_CLASE_ELEMENTO_FORMULA = COD_CLASE_ELEMENTO_FORMULA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.ClasesElementoFormula.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.ClasesElementoFormula.Delete, new { COD_CLASE_ELEMENTO_FORMULA = COD_CLASE_ELEMENTO_FORMULA })
        };
    }
}

}