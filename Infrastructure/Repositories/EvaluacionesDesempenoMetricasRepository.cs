using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EvaluacionesDesempenoMetricasRepository : IEvaluacionesDesempenoMetricasRepository
{
    private readonly IDbConnectionFactory _db;
    public EvaluacionesDesempenoMetricasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EvaluacionesDesempenoMetricas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EvaluacionesDesempenoMetricas>(SP.SCHEMARrhh.EvaluacionesDesempenoMetricas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EvaluacionesDesempenoMetricas?> ShowbyidAsync(int NUM_EVALUACION_DESEMPENO, int COD_AREA, decimal COD_ENTIDAD_PERSONAL_EVALUADO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EvaluacionesDesempenoMetricas>(SP.SCHEMARrhh.EvaluacionesDesempenoMetricas.Showbyid, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_AREA = COD_AREA, COD_ENTIDAD_PERSONAL_EVALUADO = COD_ENTIDAD_PERSONAL_EVALUADO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.EvaluacionesDesempenoMetricas.Showcombox);
    }

    public async Task<IEnumerable<EvaluacionesDesempenoMetricas>> SearchAsync(EvaluacionesDesempenoMetricasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EvaluacionesDesempenoMetricas>(SP.SCHEMARrhh.EvaluacionesDesempenoMetricas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EvaluacionesDesempenoMetricasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EVALUACION_DESEMPENO", dto.NUM_EVALUACION_DESEMPENO);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("COD_ENTIDAD_PERSONAL_EVALUADO", dto.COD_ENTIDAD_PERSONAL_EVALUADO);
        p.Add("COD_TIPO_COMPETENCIA", dto.COD_TIPO_COMPETENCIA);
        p.Add("COD_COMPETENCIA", dto.COD_COMPETENCIA);
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("VAL_POTENCIAL", dto.VAL_POTENCIAL);
        p.Add("VAL_EVALUADO", dto.VAL_EVALUADO);
        p.Add("TAS_EVALUACION", dto.TAS_EVALUACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoMetricas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoMetricas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoMetricas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EvaluacionesDesempenoMetricasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EVALUACION_DESEMPENO", dto.NUM_EVALUACION_DESEMPENO);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("COD_ENTIDAD_PERSONAL_EVALUADO", dto.COD_ENTIDAD_PERSONAL_EVALUADO);
        p.Add("COD_TIPO_COMPETENCIA", dto.COD_TIPO_COMPETENCIA);
        p.Add("COD_COMPETENCIA", dto.COD_COMPETENCIA);
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("VAL_POTENCIAL", dto.VAL_POTENCIAL);
        p.Add("VAL_EVALUADO", dto.VAL_EVALUADO);
        p.Add("TAS_EVALUACION", dto.TAS_EVALUACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoMetricas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoMetricas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoMetricas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_EVALUACION_DESEMPENO, int COD_AREA, decimal COD_ENTIDAD_PERSONAL_EVALUADO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoMetricas.Delete, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_AREA = COD_AREA, COD_ENTIDAD_PERSONAL_EVALUADO = COD_ENTIDAD_PERSONAL_EVALUADO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoMetricas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoMetricas.Delete, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_AREA = COD_AREA, COD_ENTIDAD_PERSONAL_EVALUADO = COD_ENTIDAD_PERSONAL_EVALUADO })
        };
    }
}

}