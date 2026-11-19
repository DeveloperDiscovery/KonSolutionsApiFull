using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposConceptoRemunerativoRepository : ITiposConceptoRemunerativoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposConceptoRemunerativoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposConceptoRemunerativo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposConceptoRemunerativo>(SP.SCHEMANomina.TiposConceptoRemunerativo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposConceptoRemunerativo?> ShowbyidAsync(string COD_TIPO_REMUNERATIVO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposConceptoRemunerativo>(SP.SCHEMANomina.TiposConceptoRemunerativo.Showbyid, new { COD_TIPO_REMUNERATIVO = COD_TIPO_REMUNERATIVO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.TiposConceptoRemunerativo.Showcombox);
    }

    public async Task<IEnumerable<TiposConceptoRemunerativo>> SearchAsync(TiposConceptoRemunerativoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposConceptoRemunerativo>(SP.SCHEMANomina.TiposConceptoRemunerativo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposConceptoRemunerativoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_REMUNERATIVO", dto.COD_TIPO_REMUNERATIVO);
        p.Add("DES_TIPO_REMUNERATIVO", dto.DES_TIPO_REMUNERATIVO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposConceptoRemunerativo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposConceptoRemunerativo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposConceptoRemunerativo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposConceptoRemunerativoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_REMUNERATIVO", dto.COD_TIPO_REMUNERATIVO);
        p.Add("DES_TIPO_REMUNERATIVO", dto.DES_TIPO_REMUNERATIVO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposConceptoRemunerativo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposConceptoRemunerativo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposConceptoRemunerativo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_REMUNERATIVO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposConceptoRemunerativo.Delete, new { COD_TIPO_REMUNERATIVO = COD_TIPO_REMUNERATIVO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposConceptoRemunerativo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposConceptoRemunerativo.Delete, new { COD_TIPO_REMUNERATIVO = COD_TIPO_REMUNERATIVO })
        };
    }
}

}