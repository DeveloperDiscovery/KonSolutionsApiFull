using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposConceptosFinancierosRepository : ITiposConceptosFinancierosRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposConceptosFinancierosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposConceptosFinancieros>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposConceptosFinancieros>(SP.SCHEMAFinanzas.TiposConceptosFinancieros.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposConceptosFinancieros?> ShowbyidAsync(string COD_TIPO_CONCEPTO_FINANCIERO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposConceptosFinancieros>(SP.SCHEMAFinanzas.TiposConceptosFinancieros.Showbyid, new { COD_TIPO_CONCEPTO_FINANCIERO = COD_TIPO_CONCEPTO_FINANCIERO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.TiposConceptosFinancieros.Showcombox);
    }

    public async Task<IEnumerable<TiposConceptosFinancieros>> SearchAsync(TiposConceptosFinancierosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposConceptosFinancieros>(SP.SCHEMAFinanzas.TiposConceptosFinancieros.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposConceptosFinancierosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CONCEPTO_FINANCIERO", dto.COD_TIPO_CONCEPTO_FINANCIERO);
        p.Add("DES_TIPO_CONCEPTO_FINANCIERO", dto.DES_TIPO_CONCEPTO_FINANCIERO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.TiposConceptosFinancieros.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.TiposConceptosFinancieros.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.TiposConceptosFinancieros.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposConceptosFinancierosUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CONCEPTO_FINANCIERO", dto.COD_TIPO_CONCEPTO_FINANCIERO);
        p.Add("DES_TIPO_CONCEPTO_FINANCIERO", dto.DES_TIPO_CONCEPTO_FINANCIERO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.TiposConceptosFinancieros.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.TiposConceptosFinancieros.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.TiposConceptosFinancieros.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_CONCEPTO_FINANCIERO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.TiposConceptosFinancieros.Delete, new { COD_TIPO_CONCEPTO_FINANCIERO = COD_TIPO_CONCEPTO_FINANCIERO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.TiposConceptosFinancieros.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.TiposConceptosFinancieros.Delete, new { COD_TIPO_CONCEPTO_FINANCIERO = COD_TIPO_CONCEPTO_FINANCIERO })
        };
    }
}

}