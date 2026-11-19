using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SolicitudesReclutamientoCompetenciasRepository : ISolicitudesReclutamientoCompetenciasRepository
{
    private readonly IDbConnectionFactory _db;
    public SolicitudesReclutamientoCompetenciasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SolicitudesReclutamientoCompetencias>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesReclutamientoCompetencias>(SP.SCHEMARrhh.SolicitudesReclutamientoCompetencias.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<SolicitudesReclutamientoCompetencias?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_COMPETENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SolicitudesReclutamientoCompetencias>(SP.SCHEMARrhh.SolicitudesReclutamientoCompetencias.Showbyid, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_COMPETENCIA = COD_COMPETENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.SolicitudesReclutamientoCompetencias.Showcombox);
    }

    public async Task<IEnumerable<SolicitudesReclutamientoCompetencias>> SearchAsync(SolicitudesReclutamientoCompetenciasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesReclutamientoCompetencias>(SP.SCHEMARrhh.SolicitudesReclutamientoCompetencias.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SolicitudesReclutamientoCompetenciasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_RECLUTAMIENTO", dto.NUM_SOLICITUD_RECLUTAMIENTO);
        p.Add("COD_COMPETENCIA", dto.COD_COMPETENCIA);
        p.Add("TAS_PESO_COMPETENCIA", dto.TAS_PESO_COMPETENCIA);
        p.Add("FLG_OPCIONAL", dto.FLG_OPCIONAL ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoCompetencias.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoCompetencias.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoCompetencias.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SolicitudesReclutamientoCompetenciasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_RECLUTAMIENTO", dto.NUM_SOLICITUD_RECLUTAMIENTO);
        p.Add("COD_COMPETENCIA", dto.COD_COMPETENCIA);
        p.Add("TAS_PESO_COMPETENCIA", dto.TAS_PESO_COMPETENCIA);
        p.Add("FLG_OPCIONAL", dto.FLG_OPCIONAL ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoCompetencias.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoCompetencias.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoCompetencias.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_COMPETENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoCompetencias.Delete, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_COMPETENCIA = COD_COMPETENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoCompetencias.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoCompetencias.Delete, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_COMPETENCIA = COD_COMPETENCIA })
        };
    }
}

}