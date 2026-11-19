using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class NivelesEvaluacionRepository : INivelesEvaluacionRepository
{
    private readonly IDbConnectionFactory _db;
    public NivelesEvaluacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<NivelesEvaluacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<NivelesEvaluacion>(SP.SCHEMARrhh.NivelesEvaluacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<NivelesEvaluacion?> ShowbyidAsync(int NUM_NIVEL_EVALUACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<NivelesEvaluacion>(SP.SCHEMARrhh.NivelesEvaluacion.Showbyid, new { NUM_NIVEL_EVALUACION = NUM_NIVEL_EVALUACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.NivelesEvaluacion.Showcombox);
    }

    public async Task<IEnumerable<NivelesEvaluacion>> SearchAsync(NivelesEvaluacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<NivelesEvaluacion>(SP.SCHEMARrhh.NivelesEvaluacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(NivelesEvaluacionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_NIVEL_EVALUACION", dto.NUM_NIVEL_EVALUACION);
        p.Add("DES_NIVEL_EVALUACION", dto.DES_NIVEL_EVALUACION);
        p.Add("VAL_EVALUACION", dto.VAL_EVALUACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.NivelesEvaluacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.NivelesEvaluacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.NivelesEvaluacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(NivelesEvaluacionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_NIVEL_EVALUACION", dto.NUM_NIVEL_EVALUACION);
        p.Add("DES_NIVEL_EVALUACION", dto.DES_NIVEL_EVALUACION);
        p.Add("VAL_EVALUACION", dto.VAL_EVALUACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.NivelesEvaluacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.NivelesEvaluacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.NivelesEvaluacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_NIVEL_EVALUACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.NivelesEvaluacion.Delete, new { NUM_NIVEL_EVALUACION = NUM_NIVEL_EVALUACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.NivelesEvaluacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.NivelesEvaluacion.Delete, new { NUM_NIVEL_EVALUACION = NUM_NIVEL_EVALUACION })
        };
    }
}

}