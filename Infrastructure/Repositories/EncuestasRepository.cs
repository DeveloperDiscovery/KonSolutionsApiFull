using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EncuestasRepository : IEncuestasRepository
{
    private readonly IDbConnectionFactory _db;
    public EncuestasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Encuestas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Encuestas>(SP.SCHEMARrhh.Encuestas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Encuestas?> ShowbyidAsync(int NUM_ENCUESTA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Encuestas>(SP.SCHEMARrhh.Encuestas.Showbyid, new { NUM_ENCUESTA = NUM_ENCUESTA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.Encuestas.Showcombox, "NUM_ENCUESTA", "DES_ECUENSTA");
    }

    public async Task<IEnumerable<Encuestas>> SearchAsync(EncuestasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Encuestas>(SP.SCHEMARrhh.Encuestas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EncuestasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENCUESTA", dto.NUM_ENCUESTA);
        p.Add("DES_ECUENSTA", dto.DES_ECUENSTA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.Encuestas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.Encuestas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.Encuestas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EncuestasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENCUESTA", dto.NUM_ENCUESTA);
        p.Add("DES_ECUENSTA", dto.DES_ECUENSTA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.Encuestas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.Encuestas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.Encuestas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_ENCUESTA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.Encuestas.Delete, new { NUM_ENCUESTA = NUM_ENCUESTA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.Encuestas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.Encuestas.Delete, new { NUM_ENCUESTA = NUM_ENCUESTA })
        };
    }
}

}