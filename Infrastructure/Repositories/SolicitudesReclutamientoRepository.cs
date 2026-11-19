using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SolicitudesReclutamientoRepository : ISolicitudesReclutamientoRepository
{
    private readonly IDbConnectionFactory _db;
    public SolicitudesReclutamientoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SolicitudesReclutamiento>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesReclutamiento>(SP.SCHEMARrhh.SolicitudesReclutamiento.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<SolicitudesReclutamiento?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SolicitudesReclutamiento>(SP.SCHEMARrhh.SolicitudesReclutamiento.Showbyid, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.SolicitudesReclutamiento.Showcombox, "NUM_SOLICITUD_RECLUTAMIENTO", "DES_DETALLE");
    }

    public async Task<IEnumerable<SolicitudesReclutamiento>> SearchAsync(SolicitudesReclutamientoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesReclutamiento>(SP.SCHEMARrhh.SolicitudesReclutamiento.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SolicitudesReclutamientoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_RECLUTAMIENTO", dto.NUM_SOLICITUD_RECLUTAMIENTO);
        p.Add("COD_ENTIDAD_SOLICITANTE", dto.COD_ENTIDAD_SOLICITANTE);
        p.Add("FEC_SOLICITUD", dto.FEC_SOLICITUD);
        p.Add("FEC_REQUERIDA", dto.FEC_REQUERIDA);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("CAN_PUESTOS", dto.CAN_PUESTOS);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_REMUNERACION_PROPUESTA", dto.VAL_REMUNERACION_PROPUESTA);
        p.Add("DES_DETALLE", dto.DES_DETALLE);
        p.Add("NUM_AUTORIZACION_SOLICTUD", dto.NUM_AUTORIZACION_SOLICTUD);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamiento.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamiento.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamiento.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SolicitudesReclutamientoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_RECLUTAMIENTO", dto.NUM_SOLICITUD_RECLUTAMIENTO);
        p.Add("COD_ENTIDAD_SOLICITANTE", dto.COD_ENTIDAD_SOLICITANTE);
        p.Add("FEC_SOLICITUD", dto.FEC_SOLICITUD);
        p.Add("FEC_REQUERIDA", dto.FEC_REQUERIDA);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("CAN_PUESTOS", dto.CAN_PUESTOS);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_REMUNERACION_PROPUESTA", dto.VAL_REMUNERACION_PROPUESTA);
        p.Add("DES_DETALLE", dto.DES_DETALLE);
        p.Add("NUM_AUTORIZACION_SOLICTUD", dto.NUM_AUTORIZACION_SOLICTUD);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamiento.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamiento.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamiento.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.SolicitudesReclutamiento.Delete, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.SolicitudesReclutamiento.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.SolicitudesReclutamiento.Delete, new { NUM_SOLICITUD_RECLUTAMIENTO = NUM_SOLICITUD_RECLUTAMIENTO })
        };
    }
}

}