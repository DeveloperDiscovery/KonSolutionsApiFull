using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SolicitudesComercialesMemoriasRepository : ISolicitudesComercialesMemoriasRepository
{
    private readonly IDbConnectionFactory _db;
    public SolicitudesComercialesMemoriasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SolicitudesComercialesMemorias>> ShowallAsync(int numSolicitudComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesComercialesMemorias>(
            SP.SCHEMAComercial.SolicitudesComercialesMemorias.Showall,
            new { NUM_SOLICITUD_COMERCIAL = numSolicitudComercial }, commandType: CommandType.StoredProcedure);
    }

    public async Task<SolicitudesComercialesMemorias?> ShowbyidAsync(int numSolicitudComercial, short numSecuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SolicitudesComercialesMemorias>(
            SP.SCHEMAComercial.SolicitudesComercialesMemorias.Showbyid,
            new { NUM_SOLICITUD_COMERCIAL = numSolicitudComercial, NUM_SECUENCIA = numSecuencia }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<SolicitudesComercialesMemorias>> SearchAsync(SolicitudesComercialesMemoriasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesComercialesMemorias>(
            SP.SCHEMAComercial.SolicitudesComercialesMemorias.Search,
            new { NUM_SOLICITUD_COMERCIAL = dto.NUM_SOLICITUD_COMERCIAL, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SolicitudesComercialesMemoriasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_COMERCIAL", dto.NUM_SOLICITUD_COMERCIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("DES_MEMORIA", dto.DES_MEMORIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAComercial.SolicitudesComercialesMemorias.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.SolicitudesComercialesMemorias.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.SolicitudesComercialesMemorias.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int numSolicitudComercial, short numSecuencia)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAComercial.SolicitudesComercialesMemorias.Delete,
            new { NUM_SOLICITUD_COMERCIAL = numSolicitudComercial, NUM_SECUENCIA = numSecuencia }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.SolicitudesComercialesMemorias.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.SolicitudesComercialesMemorias.Delete, new { NUM_SOLICITUD_COMERCIAL = numSolicitudComercial, NUM_SECUENCIA = numSecuencia })
        };
    }
}
}
