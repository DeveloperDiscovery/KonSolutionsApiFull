using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EvaluacionesDesempenoRepository : IEvaluacionesDesempenoRepository
{
    private readonly IDbConnectionFactory _db;
    public EvaluacionesDesempenoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EvaluacionesDesempeno>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EvaluacionesDesempeno>(SP.SCHEMARrhh.EvaluacionesDesempeno.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EvaluacionesDesempeno?> ShowbyidAsync(int NUM_EVALUACION_DESEMPENO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EvaluacionesDesempeno>(SP.SCHEMARrhh.EvaluacionesDesempeno.Showbyid, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.EvaluacionesDesempeno.Showcombox, "NUM_EVALUACION_DESEMPENO", "DES_EVALUACION_DESEMPENO");
    }

    public async Task<IEnumerable<EvaluacionesDesempeno>> SearchAsync(EvaluacionesDesempenoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EvaluacionesDesempeno>(SP.SCHEMARrhh.EvaluacionesDesempeno.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EvaluacionesDesempenoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EVALUACION_DESEMPENO", dto.NUM_EVALUACION_DESEMPENO);
        p.Add("DES_EVALUACION_DESEMPENO", dto.DES_EVALUACION_DESEMPENO);
        p.Add("COD_TIPO_EVALUACION", dto.COD_TIPO_EVALUACION);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("FLG_EVALUA_GERENCIA", dto.FLG_EVALUA_GERENCIA ?? false);
        p.Add("FLG_EVALUA_JEFATURA", dto.FLG_EVALUA_JEFATURA ?? false);
        p.Add("FLG_EVALUA_SUBORDINADO", dto.FLG_EVALUA_SUBORDINADO ?? false);
        p.Add("DES_DETALLE", dto.DES_DETALLE);
        p.Add("NUM_AUTORIZACION", dto.NUM_AUTORIZACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempeno.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempeno.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempeno.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EvaluacionesDesempenoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EVALUACION_DESEMPENO", dto.NUM_EVALUACION_DESEMPENO);
        p.Add("DES_EVALUACION_DESEMPENO", dto.DES_EVALUACION_DESEMPENO);
        p.Add("COD_TIPO_EVALUACION", dto.COD_TIPO_EVALUACION);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("FLG_EVALUA_GERENCIA", dto.FLG_EVALUA_GERENCIA ?? false);
        p.Add("FLG_EVALUA_JEFATURA", dto.FLG_EVALUA_JEFATURA ?? false);
        p.Add("FLG_EVALUA_SUBORDINADO", dto.FLG_EVALUA_SUBORDINADO ?? false);
        p.Add("DES_DETALLE", dto.DES_DETALLE);
        p.Add("NUM_AUTORIZACION", dto.NUM_AUTORIZACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempeno.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempeno.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempeno.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_EVALUACION_DESEMPENO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EvaluacionesDesempeno.Delete, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EvaluacionesDesempeno.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EvaluacionesDesempeno.Delete, new { NUM_EVALUACION_DESEMPENO = NUM_EVALUACION_DESEMPENO })
        };
    }
}

}