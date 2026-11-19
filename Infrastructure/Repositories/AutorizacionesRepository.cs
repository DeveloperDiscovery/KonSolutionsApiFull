using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AutorizacionesRepository : IAutorizacionesRepository
{
    private readonly IDbConnectionFactory _db;
    public AutorizacionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Autorizaciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Autorizaciones>(SP.SCHEMAEntorno.Autorizaciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Autorizaciones?> ShowbyidAsync(int NUM_AUTORIZACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Autorizaciones>(SP.SCHEMAEntorno.Autorizaciones.Showbyid, new { NUM_AUTORIZACION = NUM_AUTORIZACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.Autorizaciones.Showcombox);
    }

    public async Task<IEnumerable<Autorizaciones>> SearchAsync(AutorizacionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Autorizaciones>(SP.SCHEMAEntorno.Autorizaciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AutorizacionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_AUTORIZACION", dto.NUM_AUTORIZACION);
        p.Add("DES_AUTORIZACION", dto.DES_AUTORIZACION);
        p.Add("COD_ENTIDAD_SOLICITANTE", dto.COD_ENTIDAD_SOLICITANTE);
        p.Add("FEC_SOLICITUD", dto.FEC_SOLICITUD);
        p.Add("COD_TIPO_AUTORIZACION", dto.COD_TIPO_AUTORIZACION);
        p.Add("COD_ENTIDAD_AUTORIZADOR", dto.COD_ENTIDAD_AUTORIZADOR);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("DES_PATH_FILE", dto.DES_PATH_FILE);
        p.Add("DES_QUERY_EXECUTE_SI", dto.DES_QUERY_EXECUTE_SI);
        p.Add("DES_QUERY_EXECUTE_NO", dto.DES_QUERY_EXECUTE_NO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.Autorizaciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.Autorizaciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.Autorizaciones.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(AutorizacionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_AUTORIZACION", dto.NUM_AUTORIZACION);
        p.Add("DES_AUTORIZACION", dto.DES_AUTORIZACION);
        p.Add("COD_ENTIDAD_SOLICITANTE", dto.COD_ENTIDAD_SOLICITANTE);
        p.Add("FEC_SOLICITUD", dto.FEC_SOLICITUD);
        p.Add("COD_TIPO_AUTORIZACION", dto.COD_TIPO_AUTORIZACION);
        p.Add("COD_ENTIDAD_AUTORIZADOR", dto.COD_ENTIDAD_AUTORIZADOR);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("DES_PATH_FILE", dto.DES_PATH_FILE);
        p.Add("DES_QUERY_EXECUTE_SI", dto.DES_QUERY_EXECUTE_SI);
        p.Add("DES_QUERY_EXECUTE_NO", dto.DES_QUERY_EXECUTE_NO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.Autorizaciones.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.Autorizaciones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.Autorizaciones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_AUTORIZACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.Autorizaciones.Delete, new { NUM_AUTORIZACION = NUM_AUTORIZACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.Autorizaciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.Autorizaciones.Delete, new { NUM_AUTORIZACION = NUM_AUTORIZACION })
        };
    }
}

}