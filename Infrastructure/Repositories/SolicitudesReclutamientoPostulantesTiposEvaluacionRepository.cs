using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SolicitudesReclutamientoPostulantesTiposEvaluacionRepository : ISolicitudesReclutamientoPostulantesTiposEvaluacionRepository
{
    private readonly IDbConnectionFactory _db;
    public SolicitudesReclutamientoPostulantesTiposEvaluacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SolicitudesReclutamientoPostulantesTiposEvaluacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesReclutamientoPostulantesTiposEvaluacion>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesTiposEvaluacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<SolicitudesReclutamientoPostulantesTiposEvaluacion?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SolicitudesReclutamientoPostulantesTiposEvaluacion>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesTiposEvaluacion.Showbyid, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_ENTIDAD_POSTULANTE = COD_ENTIDAD_POSTULANTE, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesTiposEvaluacion.Showcombox, "NUM_SOLICITUD_RECLUTAMIENTO", "DES_EVALUACION");
    }

    public async Task<IEnumerable<SolicitudesReclutamientoPostulantesTiposEvaluacion>> SearchAsync(SolicitudesReclutamientoPostulantesTiposEvaluacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesReclutamientoPostulantesTiposEvaluacion>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesTiposEvaluacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SolicitudesReclutamientoPostulantesTiposEvaluacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_RECLUTAMIENTO", dto.NUM_SOLICITUD_RECLUTAMIENTO);
        p.Add("COD_ENTIDAD_POSTULANTE", dto.COD_ENTIDAD_POSTULANTE);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_EVALUACION", dto.COD_TIPO_EVALUACION);
        p.Add("DES_EVALUACION", dto.DES_EVALUACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesTiposEvaluacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesTiposEvaluacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesTiposEvaluacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SolicitudesReclutamientoPostulantesTiposEvaluacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_RECLUTAMIENTO", dto.NUM_SOLICITUD_RECLUTAMIENTO);
        p.Add("COD_ENTIDAD_POSTULANTE", dto.COD_ENTIDAD_POSTULANTE);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_EVALUACION", dto.COD_TIPO_EVALUACION);
        p.Add("DES_EVALUACION", dto.DES_EVALUACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesTiposEvaluacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesTiposEvaluacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesTiposEvaluacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesTiposEvaluacion.Delete, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_ENTIDAD_POSTULANTE = COD_ENTIDAD_POSTULANTE, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesTiposEvaluacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesTiposEvaluacion.Delete, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_ENTIDAD_POSTULANTE = COD_ENTIDAD_POSTULANTE, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}