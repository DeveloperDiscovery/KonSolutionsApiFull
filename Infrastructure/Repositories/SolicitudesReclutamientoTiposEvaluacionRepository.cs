using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SolicitudesReclutamientoTiposEvaluacionRepository : ISolicitudesReclutamientoTiposEvaluacionRepository
{
    private readonly IDbConnectionFactory _db;
    public SolicitudesReclutamientoTiposEvaluacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SolicitudesReclutamientoTiposEvaluacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesReclutamientoTiposEvaluacion>(SP.SCHEMARrhh.SolicitudesReclutamientoTiposEvaluacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<SolicitudesReclutamientoTiposEvaluacion?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_TIPO_EVALUACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SolicitudesReclutamientoTiposEvaluacion>(SP.SCHEMARrhh.SolicitudesReclutamientoTiposEvaluacion.Showbyid, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_TIPO_EVALUACION = COD_TIPO_EVALUACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.SolicitudesReclutamientoTiposEvaluacion.Showcombox);
    }

    public async Task<IEnumerable<SolicitudesReclutamientoTiposEvaluacion>> SearchAsync(SolicitudesReclutamientoTiposEvaluacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesReclutamientoTiposEvaluacion>(SP.SCHEMARrhh.SolicitudesReclutamientoTiposEvaluacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SolicitudesReclutamientoTiposEvaluacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_RECLUTAMIENTO", dto.NUM_SOLICITUD_RECLUTAMIENTO);
        p.Add("COD_TIPO_EVALUACION", dto.COD_TIPO_EVALUACION);
        p.Add("TAS_PESO_COMPETENCIA", dto.TAS_PESO_COMPETENCIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoTiposEvaluacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoTiposEvaluacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoTiposEvaluacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SolicitudesReclutamientoTiposEvaluacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_RECLUTAMIENTO", dto.NUM_SOLICITUD_RECLUTAMIENTO);
        p.Add("COD_TIPO_EVALUACION", dto.COD_TIPO_EVALUACION);
        p.Add("TAS_PESO_COMPETENCIA", dto.TAS_PESO_COMPETENCIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoTiposEvaluacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoTiposEvaluacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoTiposEvaluacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_TIPO_EVALUACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoTiposEvaluacion.Delete, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_TIPO_EVALUACION = COD_TIPO_EVALUACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoTiposEvaluacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoTiposEvaluacion.Delete, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_TIPO_EVALUACION = COD_TIPO_EVALUACION })
        };
    }
}

}