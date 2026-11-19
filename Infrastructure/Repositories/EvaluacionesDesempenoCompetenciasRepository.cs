using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EvaluacionesDesempenoCompetenciasRepository : IEvaluacionesDesempenoCompetenciasRepository
{
    private readonly IDbConnectionFactory _db;
    public EvaluacionesDesempenoCompetenciasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EvaluacionesDesempenoCompetencias>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EvaluacionesDesempenoCompetencias>(SP.SCHEMARrhh.EvaluacionesDesempenoCompetencias.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EvaluacionesDesempenoCompetencias?> ShowbyidAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA, string COD_COMPETENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EvaluacionesDesempenoCompetencias>(SP.SCHEMARrhh.EvaluacionesDesempenoCompetencias.Showbyid, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_TIPO_COMPETENCIA = COD_TIPO_COMPETENCIA, COD_COMPETENCIA = COD_COMPETENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.EvaluacionesDesempenoCompetencias.Showcombox);
    }

    public async Task<IEnumerable<EvaluacionesDesempenoCompetencias>> SearchAsync(EvaluacionesDesempenoCompetenciasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EvaluacionesDesempenoCompetencias>(SP.SCHEMARrhh.EvaluacionesDesempenoCompetencias.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EvaluacionesDesempenoCompetenciasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EVALUACION_DESEMPENO", dto.NUM_EVALUACION_DESEMPENO);
        p.Add("COD_TIPO_COMPETENCIA", dto.COD_TIPO_COMPETENCIA);
        p.Add("COD_COMPETENCIA", dto.COD_COMPETENCIA);
        p.Add("NUM_NIVEL_EVALUACION", dto.NUM_NIVEL_EVALUACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoCompetencias.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoCompetencias.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoCompetencias.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EvaluacionesDesempenoCompetenciasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EVALUACION_DESEMPENO", dto.NUM_EVALUACION_DESEMPENO);
        p.Add("COD_TIPO_COMPETENCIA", dto.COD_TIPO_COMPETENCIA);
        p.Add("COD_COMPETENCIA", dto.COD_COMPETENCIA);
        p.Add("NUM_NIVEL_EVALUACION", dto.NUM_NIVEL_EVALUACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoCompetencias.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoCompetencias.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoCompetencias.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA, string COD_COMPETENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoCompetencias.Delete, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_TIPO_COMPETENCIA = COD_TIPO_COMPETENCIA, COD_COMPETENCIA = COD_COMPETENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoCompetencias.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoCompetencias.Delete, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_TIPO_COMPETENCIA = COD_TIPO_COMPETENCIA, COD_COMPETENCIA = COD_COMPETENCIA })
        };
    }
}

}