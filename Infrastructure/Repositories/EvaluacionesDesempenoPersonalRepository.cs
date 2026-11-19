using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EvaluacionesDesempenoPersonalRepository : IEvaluacionesDesempenoPersonalRepository
{
    private readonly IDbConnectionFactory _db;
    public EvaluacionesDesempenoPersonalRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EvaluacionesDesempenoPersonal>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EvaluacionesDesempenoPersonal>(SP.SCHEMARrhh.EvaluacionesDesempenoPersonal.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EvaluacionesDesempenoPersonal?> ShowbyidAsync(int NUM_EVALUACION_DESEMPENO, decimal COD_ENTIDAD_PERSONAL_EVALUADOR, decimal COD_ENTIDAD_PERSONAL_EVALUADO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EvaluacionesDesempenoPersonal>(SP.SCHEMARrhh.EvaluacionesDesempenoPersonal.Showbyid, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_ENTIDAD_PERSONAL_EVALUADOR = COD_ENTIDAD_PERSONAL_EVALUADOR, COD_ENTIDAD_PERSONAL_EVALUADO = COD_ENTIDAD_PERSONAL_EVALUADO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.EvaluacionesDesempenoPersonal.Showcombox, "NUM_EVALUACION_DESEMPENO", "DES_DETALLE");
    }

    public async Task<IEnumerable<EvaluacionesDesempenoPersonal>> SearchAsync(EvaluacionesDesempenoPersonalSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EvaluacionesDesempenoPersonal>(SP.SCHEMARrhh.EvaluacionesDesempenoPersonal.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EvaluacionesDesempenoPersonalInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EVALUACION_DESEMPENO", dto.NUM_EVALUACION_DESEMPENO);
        p.Add("COD_ENTIDAD_PERSONAL_EVALUADOR", dto.COD_ENTIDAD_PERSONAL_EVALUADOR);
        p.Add("COD_ENTIDAD_PERSONAL_EVALUADO", dto.COD_ENTIDAD_PERSONAL_EVALUADO);
        p.Add("DES_DETALLE", dto.DES_DETALLE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoPersonal.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoPersonal.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoPersonal.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EvaluacionesDesempenoPersonalUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EVALUACION_DESEMPENO", dto.NUM_EVALUACION_DESEMPENO);
        p.Add("COD_ENTIDAD_PERSONAL_EVALUADOR", dto.COD_ENTIDAD_PERSONAL_EVALUADOR);
        p.Add("COD_ENTIDAD_PERSONAL_EVALUADO", dto.COD_ENTIDAD_PERSONAL_EVALUADO);
        p.Add("DES_DETALLE", dto.DES_DETALLE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoPersonal.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoPersonal.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoPersonal.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_EVALUACION_DESEMPENO, decimal COD_ENTIDAD_PERSONAL_EVALUADOR, decimal COD_ENTIDAD_PERSONAL_EVALUADO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempenoPersonal.Delete, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_ENTIDAD_PERSONAL_EVALUADOR = COD_ENTIDAD_PERSONAL_EVALUADOR, COD_ENTIDAD_PERSONAL_EVALUADO = COD_ENTIDAD_PERSONAL_EVALUADO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempenoPersonal.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempenoPersonal.Delete, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO, COD_ENTIDAD_PERSONAL_EVALUADOR = COD_ENTIDAD_PERSONAL_EVALUADOR, COD_ENTIDAD_PERSONAL_EVALUADO = COD_ENTIDAD_PERSONAL_EVALUADO })
        };
    }
}

}