using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SolicitudesReclutamientoPostulantesFormacionAcademicaRepository : ISolicitudesReclutamientoPostulantesFormacionAcademicaRepository
{
    private readonly IDbConnectionFactory _db;
    public SolicitudesReclutamientoPostulantesFormacionAcademicaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SolicitudesReclutamientoPostulantesFormacionAcademica>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesReclutamientoPostulantesFormacionAcademica>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesFormacionAcademica.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<SolicitudesReclutamientoPostulantesFormacionAcademica?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, string COD_CARRERA_ACADEMICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SolicitudesReclutamientoPostulantesFormacionAcademica>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesFormacionAcademica.Showbyid, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_ENTIDAD_POSTULANTE = COD_ENTIDAD_POSTULANTE, COD_CARRERA_ACADEMICA = COD_CARRERA_ACADEMICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesFormacionAcademica.Showcombox, "NUM_SOLICITUD_RECLUTAMIENTO", "DES_CARRERA_ACADEMICA");
    }

    public async Task<IEnumerable<SolicitudesReclutamientoPostulantesFormacionAcademica>> SearchAsync(SolicitudesReclutamientoPostulantesFormacionAcademicaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesReclutamientoPostulantesFormacionAcademica>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesFormacionAcademica.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SolicitudesReclutamientoPostulantesFormacionAcademicaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_RECLUTAMIENTO", dto.NUM_SOLICITUD_RECLUTAMIENTO);
        p.Add("COD_ENTIDAD_POSTULANTE", dto.COD_ENTIDAD_POSTULANTE);
        p.Add("COD_CARRERA_ACADEMICA", dto.COD_CARRERA_ACADEMICA);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_TERMINO", dto.FEC_TERMINO);
        p.Add("COD_TITULO_ACADEMICO", dto.COD_TITULO_ACADEMICO);
        p.Add("COD_RANKING_ACADEMICO", dto.COD_RANKING_ACADEMICO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesFormacionAcademica.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesFormacionAcademica.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesFormacionAcademica.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SolicitudesReclutamientoPostulantesFormacionAcademicaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_RECLUTAMIENTO", dto.NUM_SOLICITUD_RECLUTAMIENTO);
        p.Add("COD_ENTIDAD_POSTULANTE", dto.COD_ENTIDAD_POSTULANTE);
        p.Add("COD_CARRERA_ACADEMICA", dto.COD_CARRERA_ACADEMICA);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_TERMINO", dto.FEC_TERMINO);
        p.Add("COD_TITULO_ACADEMICO", dto.COD_TITULO_ACADEMICO);
        p.Add("COD_RANKING_ACADEMICO", dto.COD_RANKING_ACADEMICO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesFormacionAcademica.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesFormacionAcademica.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesFormacionAcademica.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, string COD_CARRERA_ACADEMICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesFormacionAcademica.Delete, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_ENTIDAD_POSTULANTE = COD_ENTIDAD_POSTULANTE, COD_CARRERA_ACADEMICA = COD_CARRERA_ACADEMICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesFormacionAcademica.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoPostulantesFormacionAcademica.Delete, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_ENTIDAD_POSTULANTE = COD_ENTIDAD_POSTULANTE, COD_CARRERA_ACADEMICA = COD_CARRERA_ACADEMICA })
        };
    }
}

}