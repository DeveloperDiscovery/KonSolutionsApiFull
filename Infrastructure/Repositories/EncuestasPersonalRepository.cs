using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EncuestasPersonalRepository : IEncuestasPersonalRepository
{
    private readonly IDbConnectionFactory _db;
    public EncuestasPersonalRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EncuestasPersonal>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EncuestasPersonal>(SP.SCHEMARrhh.EncuestasPersonal.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EncuestasPersonal?> ShowbyidAsync(int NUM_ENCUESTA, decimal COD_ENTIDAD_PERSONAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EncuestasPersonal>(SP.SCHEMARrhh.EncuestasPersonal.Showbyid, new { NUM_ENCUESTA = NUM_ENCUESTA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.EncuestasPersonal.Showcombox, "NUM_ENCUESTA", "DES_OBSERVACION");
    }

    public async Task<IEnumerable<EncuestasPersonal>> SearchAsync(EncuestasPersonalSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EncuestasPersonal>(SP.SCHEMARrhh.EncuestasPersonal.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EncuestasPersonalInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENCUESTA", dto.NUM_ENCUESTA);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EncuestasPersonal.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EncuestasPersonal.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EncuestasPersonal.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EncuestasPersonalUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENCUESTA", dto.NUM_ENCUESTA);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EncuestasPersonal.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EncuestasPersonal.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EncuestasPersonal.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_ENCUESTA, decimal COD_ENTIDAD_PERSONAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EncuestasPersonal.Delete, new { NUM_ENCUESTA = NUM_ENCUESTA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EncuestasPersonal.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EncuestasPersonal.Delete, new { NUM_ENCUESTA = NUM_ENCUESTA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL })
        };
    }
}

}