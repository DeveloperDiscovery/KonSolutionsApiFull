using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EvaluacionesDesempenoPersonalEvaluacionRepository : IEvaluacionesDesempenoPersonalEvaluacionRepository
{
    private readonly IDbConnectionFactory _db;
    public EvaluacionesDesempenoPersonalEvaluacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EvaluacionesDesempenoPersonalEvaluacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EvaluacionesDesempenoPersonalEvaluacion>(SP.SCHEMARrhh.EvaluacionesDesempenoPersonalEvaluacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EvaluacionesDesempenoPersonalEvaluacion?> ShowbyidAsync(int NUM_EVALUACION_DESEMPENO, decimal COD_ENTIDAD_PERSONAL_EVALUADOR, decimal COD_ENTIDAD_PERSONAL_EVALUADO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EvaluacionesDesempenoPersonalEvaluacion>(SP.SCHEMARrhh.EvaluacionesDesempenoPersonalEvaluacion.Showbyid, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_ENTIDAD_PERSONAL_EVALUADOR = COD_ENTIDAD_PERSONAL_EVALUADOR, COD_ENTIDAD_PERSONAL_EVALUADO = COD_ENTIDAD_PERSONAL_EVALUADO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.EvaluacionesDesempenoPersonalEvaluacion.Showcombox);
    }

    public async Task<IEnumerable<EvaluacionesDesempenoPersonalEvaluacion>> SearchAsync(EvaluacionesDesempenoPersonalEvaluacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EvaluacionesDesempenoPersonalEvaluacion>(SP.SCHEMARrhh.EvaluacionesDesempenoPersonalEvaluacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EvaluacionesDesempenoPersonalEvaluacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EVALUACION_DESEMPENO", dto.NUM_EVALUACION_DESEMPENO);
        p.Add("COD_ENTIDAD_PERSONAL_EVALUADOR", dto.COD_ENTIDAD_PERSONAL_EVALUADOR);
        p.Add("COD_ENTIDAD_PERSONAL_EVALUADO", dto.COD_ENTIDAD_PERSONAL_EVALUADO);
        p.Add("COD_TIPO_COMPETENCIA", dto.COD_TIPO_COMPETENCIA);
        p.Add("COD_COMPETENCIA", dto.COD_COMPETENCIA);
        p.Add("NUM_NIVEL_EVALUACION", dto.NUM_NIVEL_EVALUACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoPersonalEvaluacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoPersonalEvaluacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoPersonalEvaluacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EvaluacionesDesempenoPersonalEvaluacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EVALUACION_DESEMPENO", dto.NUM_EVALUACION_DESEMPENO);
        p.Add("COD_ENTIDAD_PERSONAL_EVALUADOR", dto.COD_ENTIDAD_PERSONAL_EVALUADOR);
        p.Add("COD_ENTIDAD_PERSONAL_EVALUADO", dto.COD_ENTIDAD_PERSONAL_EVALUADO);
        p.Add("COD_TIPO_COMPETENCIA", dto.COD_TIPO_COMPETENCIA);
        p.Add("COD_COMPETENCIA", dto.COD_COMPETENCIA);
        p.Add("NUM_NIVEL_EVALUACION", dto.NUM_NIVEL_EVALUACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoPersonalEvaluacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoPersonalEvaluacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoPersonalEvaluacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_EVALUACION_DESEMPENO, decimal COD_ENTIDAD_PERSONAL_EVALUADOR, decimal COD_ENTIDAD_PERSONAL_EVALUADO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoPersonalEvaluacion.Delete, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_ENTIDAD_PERSONAL_EVALUADOR = COD_ENTIDAD_PERSONAL_EVALUADOR, COD_ENTIDAD_PERSONAL_EVALUADO = COD_ENTIDAD_PERSONAL_EVALUADO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoPersonalEvaluacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoPersonalEvaluacion.Delete, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_ENTIDAD_PERSONAL_EVALUADOR = COD_ENTIDAD_PERSONAL_EVALUADOR, COD_ENTIDAD_PERSONAL_EVALUADO = COD_ENTIDAD_PERSONAL_EVALUADO })
        };
    }
}

}