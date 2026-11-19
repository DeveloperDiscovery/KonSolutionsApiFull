using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SolicitudesReclutamientoFuncionesRepository : ISolicitudesReclutamientoFuncionesRepository
{
    private readonly IDbConnectionFactory _db;
    public SolicitudesReclutamientoFuncionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SolicitudesReclutamientoFunciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesReclutamientoFunciones>(SP.SCHEMARrhh.SolicitudesReclutamientoFunciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<SolicitudesReclutamientoFunciones?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_FUNCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SolicitudesReclutamientoFunciones>(SP.SCHEMARrhh.SolicitudesReclutamientoFunciones.Showbyid, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_FUNCION = COD_FUNCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.SolicitudesReclutamientoFunciones.Showcombox);
    }

    public async Task<IEnumerable<SolicitudesReclutamientoFunciones>> SearchAsync(SolicitudesReclutamientoFuncionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesReclutamientoFunciones>(SP.SCHEMARrhh.SolicitudesReclutamientoFunciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SolicitudesReclutamientoFuncionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_RECLUTAMIENTO", dto.NUM_SOLICITUD_RECLUTAMIENTO);
        p.Add("COD_FUNCION", dto.COD_FUNCION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoFunciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoFunciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoFunciones.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_FUNCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamientoFunciones.Delete, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_FUNCION = COD_FUNCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamientoFunciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamientoFunciones.Delete, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO, COD_FUNCION = COD_FUNCION })
        };
    }
}

}