using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EvaluacionesDesempenoTiposCompetenciasRepository : IEvaluacionesDesempenoTiposCompetenciasRepository
{
    private readonly IDbConnectionFactory _db;
    public EvaluacionesDesempenoTiposCompetenciasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EvaluacionesDesempenoTiposCompetencias>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EvaluacionesDesempenoTiposCompetencias>(SP.SCHEMARrhh.EvaluacionesDesempenoTiposCompetencias.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EvaluacionesDesempenoTiposCompetencias?> ShowbyidAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EvaluacionesDesempenoTiposCompetencias>(SP.SCHEMARrhh.EvaluacionesDesempenoTiposCompetencias.Showbyid, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_TIPO_COMPETENCIA = COD_TIPO_COMPETENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.EvaluacionesDesempenoTiposCompetencias.Showcombox);
    }

    public async Task<IEnumerable<EvaluacionesDesempenoTiposCompetencias>> SearchAsync(EvaluacionesDesempenoTiposCompetenciasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EvaluacionesDesempenoTiposCompetencias>(SP.SCHEMARrhh.EvaluacionesDesempenoTiposCompetencias.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EvaluacionesDesempenoTiposCompetenciasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EVALUACION_DESEMPENO", dto.NUM_EVALUACION_DESEMPENO);
        p.Add("COD_TIPO_COMPETENCIA", dto.COD_TIPO_COMPETENCIA);
        p.Add("DES_DETALLE", dto.DES_DETALLE);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoTiposCompetencias.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoTiposCompetencias.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoTiposCompetencias.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EvaluacionesDesempenoTiposCompetenciasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EVALUACION_DESEMPENO", dto.NUM_EVALUACION_DESEMPENO);
        p.Add("COD_TIPO_COMPETENCIA", dto.COD_TIPO_COMPETENCIA);
        p.Add("DES_DETALLE", dto.DES_DETALLE);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoTiposCompetencias.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoTiposCompetencias.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoTiposCompetencias.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoTiposCompetencias.Delete, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_TIPO_COMPETENCIA = COD_TIPO_COMPETENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoTiposCompetencias.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoTiposCompetencias.Delete, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_TIPO_COMPETENCIA = COD_TIPO_COMPETENCIA })
        };
    }
}

}