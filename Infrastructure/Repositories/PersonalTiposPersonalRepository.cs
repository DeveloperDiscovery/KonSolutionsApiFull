using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PersonalTiposPersonalRepository : IPersonalTiposPersonalRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalTiposPersonalRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PersonalTiposPersonal>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalTiposPersonal>(SP.SCHEMANomina.PersonalTiposPersonal.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PersonalTiposPersonal?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PersonalTiposPersonal>(SP.SCHEMANomina.PersonalTiposPersonal.Showbyid, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.PersonalTiposPersonal.Showcombox);
    }

    public async Task<IEnumerable<PersonalTiposPersonal>> SearchAsync(PersonalTiposPersonalSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalTiposPersonal>(SP.SCHEMANomina.PersonalTiposPersonal.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PersonalTiposPersonalInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_PERSONAL", dto.COD_TIPO_PERSONAL);
        p.Add("FEC_INGRESO", dto.FEC_INGRESO);
        p.Add("FEC_SALIDA", dto.FEC_SALIDA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PersonalTiposPersonal.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PersonalTiposPersonal.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PersonalTiposPersonal.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PersonalTiposPersonalUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_PERSONAL", dto.COD_TIPO_PERSONAL);
        p.Add("FEC_INGRESO", dto.FEC_INGRESO);
        p.Add("FEC_SALIDA", dto.FEC_SALIDA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PersonalTiposPersonal.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PersonalTiposPersonal.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PersonalTiposPersonal.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PersonalTiposPersonal.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PersonalTiposPersonal.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PersonalTiposPersonal.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}