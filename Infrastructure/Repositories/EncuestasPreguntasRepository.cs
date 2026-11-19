using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EncuestasPreguntasRepository : IEncuestasPreguntasRepository
{
    private readonly IDbConnectionFactory _db;
    public EncuestasPreguntasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EncuestasPreguntas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EncuestasPreguntas>(SP.SCHEMARrhh.EncuestasPreguntas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EncuestasPreguntas?> ShowbyidAsync(int NUM_ENCUESTA, int NUM_PREGUNTA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EncuestasPreguntas>(SP.SCHEMARrhh.EncuestasPreguntas.Showbyid, new { NUM_ENCUESTA = NUM_ENCUESTA, NUM_PREGUNTA = NUM_PREGUNTA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.EncuestasPreguntas.Showcombox);
    }

    public async Task<IEnumerable<EncuestasPreguntas>> SearchAsync(EncuestasPreguntasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EncuestasPreguntas>(SP.SCHEMARrhh.EncuestasPreguntas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EncuestasPreguntasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENCUESTA", dto.NUM_ENCUESTA);
        p.Add("NUM_PREGUNTA", dto.NUM_PREGUNTA);
        p.Add("DES_PREGUNTA", dto.DES_PREGUNTA);
        p.Add("FLG_PARRAFO", dto.FLG_PARRAFO ?? false);
        p.Add("FLG_SINO", dto.FLG_SINO ?? false);
        p.Add("FLG_NUMERO", dto.FLG_NUMERO ?? false);
        p.Add("FLG_OPCION_SIMPLE", dto.FLG_OPCION_SIMPLE ?? false);
        p.Add("FLG_OPCION_MULTIPLE", dto.FLG_OPCION_MULTIPLE ?? false);
        p.Add("FLG_CALIFICACION", dto.FLG_CALIFICACION ?? false);
        p.Add("FLG_FECHA", dto.FLG_FECHA ?? false);
        p.Add("FLG_HORA", dto.FLG_HORA ?? false);
        p.Add("NUM_CALIFICACION_MINIMO", dto.NUM_CALIFICACION_MINIMO);
        p.Add("NUM_CALIFICACION_MAXIMO", dto.NUM_CALIFICACION_MAXIMO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EncuestasPreguntas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EncuestasPreguntas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EncuestasPreguntas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EncuestasPreguntasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENCUESTA", dto.NUM_ENCUESTA);
        p.Add("NUM_PREGUNTA", dto.NUM_PREGUNTA);
        p.Add("DES_PREGUNTA", dto.DES_PREGUNTA);
        p.Add("FLG_PARRAFO", dto.FLG_PARRAFO ?? false);
        p.Add("FLG_SINO", dto.FLG_SINO ?? false);
        p.Add("FLG_NUMERO", dto.FLG_NUMERO ?? false);
        p.Add("FLG_OPCION_SIMPLE", dto.FLG_OPCION_SIMPLE ?? false);
        p.Add("FLG_OPCION_MULTIPLE", dto.FLG_OPCION_MULTIPLE ?? false);
        p.Add("FLG_CALIFICACION", dto.FLG_CALIFICACION ?? false);
        p.Add("FLG_FECHA", dto.FLG_FECHA ?? false);
        p.Add("FLG_HORA", dto.FLG_HORA ?? false);
        p.Add("NUM_CALIFICACION_MINIMO", dto.NUM_CALIFICACION_MINIMO);
        p.Add("NUM_CALIFICACION_MAXIMO", dto.NUM_CALIFICACION_MAXIMO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EncuestasPreguntas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EncuestasPreguntas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EncuestasPreguntas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_ENCUESTA, int NUM_PREGUNTA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EncuestasPreguntas.Delete, new { NUM_ENCUESTA = NUM_ENCUESTA, NUM_PREGUNTA = NUM_PREGUNTA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EncuestasPreguntas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EncuestasPreguntas.Delete, new { NUM_ENCUESTA = NUM_ENCUESTA, NUM_PREGUNTA = NUM_PREGUNTA })
        };
    }
}

}