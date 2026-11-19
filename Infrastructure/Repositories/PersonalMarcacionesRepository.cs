using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PersonalMarcacionesRepository : IPersonalMarcacionesRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalMarcacionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PersonalMarcaciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalMarcaciones>(SP.SCHEMANomina.PersonalMarcaciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PersonalMarcaciones?> ShowbyidAsync(decimal NUM_MARCACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PersonalMarcaciones>(SP.SCHEMANomina.PersonalMarcaciones.Showbyid, new { NUM_MARCACION = NUM_MARCACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.PersonalMarcaciones.Showcombox);
    }

    public async Task<IEnumerable<PersonalMarcaciones>> SearchAsync(PersonalMarcacionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalMarcaciones>(SP.SCHEMANomina.PersonalMarcaciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PersonalMarcacionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MARCACION", dto.NUM_MARCACION);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("FEC_MARCACION", dto.FEC_MARCACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PersonalMarcaciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PersonalMarcaciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PersonalMarcaciones.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PersonalMarcacionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MARCACION", dto.NUM_MARCACION);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("FEC_MARCACION", dto.FEC_MARCACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PersonalMarcaciones.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PersonalMarcaciones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PersonalMarcaciones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_MARCACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PersonalMarcaciones.Delete, new { NUM_MARCACION = NUM_MARCACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PersonalMarcaciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PersonalMarcaciones.Delete, new { NUM_MARCACION = NUM_MARCACION })
        };
    }
}

}