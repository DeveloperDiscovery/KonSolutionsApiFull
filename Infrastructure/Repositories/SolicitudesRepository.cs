using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Solicitudes;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SolicitudesRepository : ISolicitudesRepository
{
    private readonly IDbConnectionFactory _db;
    public SolicitudesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Solicitudes>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Solicitudes>(SP.SCHEMASolicitudes.Solicitudes.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Solicitudes?> ShowbyidAsync(int NUM_SOLICITUD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Solicitudes>(SP.SCHEMASolicitudes.Solicitudes.Showbyid, new { NUM_SOLICITUD = NUM_SOLICITUD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASolicitudes.Solicitudes.Showcombox, "NUM_SOLICITUD", "DES_SOLICITUD");
    }

    public async Task<IEnumerable<Solicitudes>> SearchAsync(SolicitudesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Solicitudes>(SP.SCHEMASolicitudes.Solicitudes.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SolicitudesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD", dto.NUM_SOLICITUD);
        p.Add("DES_SOLICITUD", dto.DES_SOLICITUD);
        p.Add("FEC_SOLICITUD", dto.FEC_SOLICITUD);
        p.Add("COD_ENTIDAD_SOLICITANTE", dto.COD_ENTIDAD_SOLICITANTE);
        p.Add("COD_ENTIDAD_ATENCION", dto.COD_ENTIDAD_ATENCION);
        p.Add("COD_PRIORIDAD", dto.COD_PRIORIDAD);
        p.Add("COD_TIPO_SOLICITUD", dto.COD_TIPO_SOLICITUD);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASolicitudes.Solicitudes.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASolicitudes.Solicitudes.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASolicitudes.Solicitudes.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SolicitudesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD", dto.NUM_SOLICITUD);
        p.Add("DES_SOLICITUD", dto.DES_SOLICITUD);
        p.Add("FEC_SOLICITUD", dto.FEC_SOLICITUD);
        p.Add("COD_ENTIDAD_SOLICITANTE", dto.COD_ENTIDAD_SOLICITANTE);
        p.Add("COD_ENTIDAD_ATENCION", dto.COD_ENTIDAD_ATENCION);
        p.Add("COD_PRIORIDAD", dto.COD_PRIORIDAD);
        p.Add("COD_TIPO_SOLICITUD", dto.COD_TIPO_SOLICITUD);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASolicitudes.Solicitudes.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASolicitudes.Solicitudes.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASolicitudes.Solicitudes.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_SOLICITUD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASolicitudes.Solicitudes.Delete, new { NUM_SOLICITUD = NUM_SOLICITUD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASolicitudes.Solicitudes.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASolicitudes.Solicitudes.Delete, new { NUM_SOLICITUD = NUM_SOLICITUD })
        };
    }
}

}