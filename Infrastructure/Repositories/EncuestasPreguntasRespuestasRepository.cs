using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EncuestasPreguntasRespuestasRepository : IEncuestasPreguntasRespuestasRepository
{
    private readonly IDbConnectionFactory _db;
    public EncuestasPreguntasRespuestasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EncuestasPreguntasRespuestas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EncuestasPreguntasRespuestas>(SP.SCHEMARrhh.EncuestasPreguntasRespuestas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EncuestasPreguntasRespuestas?> ShowbyidAsync(int NUM_ENCUESTA, int NUM_PREGUNTA, int NUM_RESPUESTA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EncuestasPreguntasRespuestas>(SP.SCHEMARrhh.EncuestasPreguntasRespuestas.Showbyid, new { NUM_ENCUESTA = NUM_ENCUESTA, NUM_PREGUNTA = NUM_PREGUNTA, NUM_RESPUESTA = NUM_RESPUESTA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.EncuestasPreguntasRespuestas.Showcombox);
    }

    public async Task<IEnumerable<EncuestasPreguntasRespuestas>> SearchAsync(EncuestasPreguntasRespuestasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EncuestasPreguntasRespuestas>(SP.SCHEMARrhh.EncuestasPreguntasRespuestas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EncuestasPreguntasRespuestasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENCUESTA", dto.NUM_ENCUESTA);
        p.Add("NUM_PREGUNTA", dto.NUM_PREGUNTA);
        p.Add("NUM_RESPUESTA", dto.NUM_RESPUESTA);
        p.Add("DES_RESPUESTA", dto.DES_RESPUESTA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EncuestasPreguntasRespuestas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EncuestasPreguntasRespuestas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EncuestasPreguntasRespuestas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EncuestasPreguntasRespuestasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENCUESTA", dto.NUM_ENCUESTA);
        p.Add("NUM_PREGUNTA", dto.NUM_PREGUNTA);
        p.Add("NUM_RESPUESTA", dto.NUM_RESPUESTA);
        p.Add("DES_RESPUESTA", dto.DES_RESPUESTA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EncuestasPreguntasRespuestas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EncuestasPreguntasRespuestas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EncuestasPreguntasRespuestas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_ENCUESTA, int NUM_PREGUNTA, int NUM_RESPUESTA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EncuestasPreguntasRespuestas.Delete, new { NUM_ENCUESTA = NUM_ENCUESTA, NUM_PREGUNTA = NUM_PREGUNTA, NUM_RESPUESTA = NUM_RESPUESTA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EncuestasPreguntasRespuestas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EncuestasPreguntasRespuestas.Delete, new { NUM_ENCUESTA = NUM_ENCUESTA, NUM_PREGUNTA = NUM_PREGUNTA, NUM_RESPUESTA = NUM_RESPUESTA })
        };
    }
}

}