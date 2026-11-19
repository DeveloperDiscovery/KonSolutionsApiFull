using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposEvaluacionRepository : ITiposEvaluacionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposEvaluacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposEvaluacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposEvaluacion>(SP.SCHEMARrhh.TiposEvaluacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposEvaluacion?> ShowbyidAsync(string COD_TIPO_EVALUACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposEvaluacion>(SP.SCHEMARrhh.TiposEvaluacion.Showbyid, new { COD_TIPO_EVALUACION = COD_TIPO_EVALUACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.TiposEvaluacion.Showcombox);
    }

    public async Task<IEnumerable<TiposEvaluacion>> SearchAsync(TiposEvaluacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposEvaluacion>(SP.SCHEMARrhh.TiposEvaluacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposEvaluacionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_EVALUACION", dto.COD_TIPO_EVALUACION);
        p.Add("DES_TIPO_EVALUACION", dto.DES_TIPO_EVALUACION);
        p.Add("FLG_DESEMPENO", dto.FLG_DESEMPENO ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposEvaluacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposEvaluacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposEvaluacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposEvaluacionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_EVALUACION", dto.COD_TIPO_EVALUACION);
        p.Add("DES_TIPO_EVALUACION", dto.DES_TIPO_EVALUACION);
        p.Add("FLG_DESEMPENO", dto.FLG_DESEMPENO ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposEvaluacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposEvaluacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposEvaluacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_EVALUACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposEvaluacion.Delete, new { COD_TIPO_EVALUACION = COD_TIPO_EVALUACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposEvaluacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposEvaluacion.Delete, new { COD_TIPO_EVALUACION = COD_TIPO_EVALUACION })
        };
    }
}

}