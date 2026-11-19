using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposElementoFormulaRepository : ITiposElementoFormulaRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposElementoFormulaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposElementoFormula>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposElementoFormula>(SP.SCHEMAIngenieria.TiposElementoFormula.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposElementoFormula?> ShowbyidAsync(int COD_CLASE_ELEMENTO_FORMULA, int COD_TIPO_ELEMENTO_FORMULA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposElementoFormula>(SP.SCHEMAIngenieria.TiposElementoFormula.Showbyid, new { COD_CLASE_ELEMENTO_FORMULA = COD_CLASE_ELEMENTO_FORMULA, COD_TIPO_ELEMENTO_FORMULA = COD_TIPO_ELEMENTO_FORMULA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.TiposElementoFormula.Showcombox);
    }

    public async Task<IEnumerable<TiposElementoFormula>> SearchAsync(TiposElementoFormulaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposElementoFormula>(SP.SCHEMAIngenieria.TiposElementoFormula.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposElementoFormulaInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_ELEMENTO_FORMULA", dto.COD_CLASE_ELEMENTO_FORMULA);
        p.Add("COD_TIPO_ELEMENTO_FORMULA", dto.COD_TIPO_ELEMENTO_FORMULA);
        p.Add("DES_TIPO_ELEMENTO_FORMULA", dto.DES_TIPO_ELEMENTO_FORMULA);
        p.Add("DES_TIPO_MASCARA_FORMULA", dto.DES_TIPO_MASCARA_FORMULA);
        p.Add("COD_TIPO_ESPECIFICACION_TECNICA", dto.COD_TIPO_ESPECIFICACION_TECNICA);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("FLG_INPUT", dto.FLG_INPUT ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposElementoFormula.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposElementoFormula.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposElementoFormula.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposElementoFormulaUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_ELEMENTO_FORMULA", dto.COD_CLASE_ELEMENTO_FORMULA);
        p.Add("COD_TIPO_ELEMENTO_FORMULA", dto.COD_TIPO_ELEMENTO_FORMULA);
        p.Add("DES_TIPO_ELEMENTO_FORMULA", dto.DES_TIPO_ELEMENTO_FORMULA);
        p.Add("DES_TIPO_MASCARA_FORMULA", dto.DES_TIPO_MASCARA_FORMULA);
        p.Add("COD_TIPO_ESPECIFICACION_TECNICA", dto.COD_TIPO_ESPECIFICACION_TECNICA);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("FLG_INPUT", dto.FLG_INPUT ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposElementoFormula.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposElementoFormula.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposElementoFormula.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_CLASE_ELEMENTO_FORMULA, int COD_TIPO_ELEMENTO_FORMULA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposElementoFormula.Delete, new { COD_CLASE_ELEMENTO_FORMULA = COD_CLASE_ELEMENTO_FORMULA, COD_TIPO_ELEMENTO_FORMULA = COD_TIPO_ELEMENTO_FORMULA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposElementoFormula.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposElementoFormula.Delete, new { COD_CLASE_ELEMENTO_FORMULA = COD_CLASE_ELEMENTO_FORMULA, COD_TIPO_ELEMENTO_FORMULA = COD_TIPO_ELEMENTO_FORMULA })
        };
    }
}

}