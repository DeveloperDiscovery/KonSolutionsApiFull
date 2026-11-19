using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SolicitudesReclutamientoPostulantesRepository : ISolicitudesReclutamientoPostulantesRepository
{
    private readonly IDbConnectionFactory _db;
    public SolicitudesReclutamientoPostulantesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SolicitudesReclutamientoPostulantes>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesReclutamientoPostulantes>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantes.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<SolicitudesReclutamientoPostulantes?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SolicitudesReclutamientoPostulantes>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantes.Showbyid, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_ENTIDAD_POSTULANTE = COD_ENTIDAD_POSTULANTE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.SolicitudesReclutamientoPostulantes.Showcombox, "NUM_SOLICITUD_RECLUTAMIENTO", "DES_EVALUACION");
    }

    public async Task<IEnumerable<SolicitudesReclutamientoPostulantes>> SearchAsync(SolicitudesReclutamientoPostulantesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesReclutamientoPostulantes>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantes.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SolicitudesReclutamientoPostulantesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_RECLUTAMIENTO", dto.NUM_SOLICITUD_RECLUTAMIENTO);
        p.Add("COD_ENTIDAD_POSTULANTE", dto.COD_ENTIDAD_POSTULANTE);
        p.Add("DES_EVALUACION", dto.DES_EVALUACION);
        p.Add("NUM_AUTORIZACION_APROBACION", dto.NUM_AUTORIZACION_APROBACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantes.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoPostulantes.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantes.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SolicitudesReclutamientoPostulantesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_RECLUTAMIENTO", dto.NUM_SOLICITUD_RECLUTAMIENTO);
        p.Add("COD_ENTIDAD_POSTULANTE", dto.COD_ENTIDAD_POSTULANTE);
        p.Add("DES_EVALUACION", dto.DES_EVALUACION);
        p.Add("NUM_AUTORIZACION_APROBACION", dto.NUM_AUTORIZACION_APROBACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantes.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoPostulantes.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantes.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantes.Delete, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_ENTIDAD_POSTULANTE = COD_ENTIDAD_POSTULANTE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoPostulantes.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantes.Delete, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_ENTIDAD_POSTULANTE = COD_ENTIDAD_POSTULANTE })
        };
    }
}

}