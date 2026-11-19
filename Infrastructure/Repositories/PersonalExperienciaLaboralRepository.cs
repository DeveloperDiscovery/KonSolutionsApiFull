using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PersonalExperienciaLaboralRepository : IPersonalExperienciaLaboralRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalExperienciaLaboralRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PersonalExperienciaLaboral>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalExperienciaLaboral>(SP.SCHEMARrhh.PersonalExperienciaLaboral.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PersonalExperienciaLaboral?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PersonalExperienciaLaboral>(SP.SCHEMARrhh.PersonalExperienciaLaboral.Showbyid, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.PersonalExperienciaLaboral.Showcombox);
    }

    public async Task<IEnumerable<PersonalExperienciaLaboral>> SearchAsync(PersonalExperienciaLaboralSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalExperienciaLaboral>(SP.SCHEMARrhh.PersonalExperienciaLaboral.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PersonalExperienciaLaboralInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ENTIDAD_EMPRESA", dto.COD_ENTIDAD_EMPRESA);
        p.Add("DES_ENTIDAD_EMPRESA", dto.DES_ENTIDAD_EMPRESA);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("DES_AREA", dto.DES_AREA);
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("DES_CARGO", dto.DES_CARGO);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_TERMINO", dto.FEC_TERMINO);
        p.Add("DES_EXPERIENCIA", dto.DES_EXPERIENCIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalExperienciaLaboral.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalExperienciaLaboral.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalExperienciaLaboral.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PersonalExperienciaLaboralUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ENTIDAD_EMPRESA", dto.COD_ENTIDAD_EMPRESA);
        p.Add("DES_ENTIDAD_EMPRESA", dto.DES_ENTIDAD_EMPRESA);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("DES_AREA", dto.DES_AREA);
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("DES_CARGO", dto.DES_CARGO);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_TERMINO", dto.FEC_TERMINO);
        p.Add("DES_EXPERIENCIA", dto.DES_EXPERIENCIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalExperienciaLaboral.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalExperienciaLaboral.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalExperienciaLaboral.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalExperienciaLaboral.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalExperienciaLaboral.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalExperienciaLaboral.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}