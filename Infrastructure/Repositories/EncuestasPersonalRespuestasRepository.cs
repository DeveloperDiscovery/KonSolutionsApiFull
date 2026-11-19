using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EncuestasPersonalRespuestasRepository : IEncuestasPersonalRespuestasRepository
{
    private readonly IDbConnectionFactory _db;
    public EncuestasPersonalRespuestasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EncuestasPersonalRespuestas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EncuestasPersonalRespuestas>(SP.SCHEMARrhh.EncuestasPersonalRespuestas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EncuestasPersonalRespuestas?> ShowbyidAsync(int NUM_ENCUESTA, decimal COD_ENTIDAD_PERSONAL, int NUM_PREGUNTA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EncuestasPersonalRespuestas>(SP.SCHEMARrhh.EncuestasPersonalRespuestas.Showbyid, new { NUM_ENCUESTA = NUM_ENCUESTA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, NUM_PREGUNTA = NUM_PREGUNTA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.EncuestasPersonalRespuestas.Showcombox);
    }

    public async Task<IEnumerable<EncuestasPersonalRespuestas>> SearchAsync(EncuestasPersonalRespuestasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EncuestasPersonalRespuestas>(SP.SCHEMARrhh.EncuestasPersonalRespuestas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EncuestasPersonalRespuestasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENCUESTA", dto.NUM_ENCUESTA);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("NUM_PREGUNTA", dto.NUM_PREGUNTA);
        p.Add("NUM_RESPUESTA", dto.NUM_RESPUESTA);
        p.Add("VAL_PARRAFO", dto.VAL_PARRAFO);
        p.Add("VAL_LOGICO", dto.VAL_LOGICO);
        p.Add("VAL_FECHA", dto.VAL_FECHA);
        p.Add("VAL_HORA", dto.VAL_HORA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EncuestasPersonalRespuestas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EncuestasPersonalRespuestas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EncuestasPersonalRespuestas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EncuestasPersonalRespuestasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENCUESTA", dto.NUM_ENCUESTA);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("NUM_PREGUNTA", dto.NUM_PREGUNTA);
        p.Add("NUM_RESPUESTA", dto.NUM_RESPUESTA);
        p.Add("VAL_PARRAFO", dto.VAL_PARRAFO);
        p.Add("VAL_LOGICO", dto.VAL_LOGICO);
        p.Add("VAL_FECHA", dto.VAL_FECHA);
        p.Add("VAL_HORA", dto.VAL_HORA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EncuestasPersonalRespuestas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EncuestasPersonalRespuestas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EncuestasPersonalRespuestas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_ENCUESTA, decimal COD_ENTIDAD_PERSONAL, int NUM_PREGUNTA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EncuestasPersonalRespuestas.Delete, new { NUM_ENCUESTA = NUM_ENCUESTA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, NUM_PREGUNTA = NUM_PREGUNTA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EncuestasPersonalRespuestas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EncuestasPersonalRespuestas.Delete, new { NUM_ENCUESTA = NUM_ENCUESTA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, NUM_PREGUNTA = NUM_PREGUNTA })
        };
    }
}

}