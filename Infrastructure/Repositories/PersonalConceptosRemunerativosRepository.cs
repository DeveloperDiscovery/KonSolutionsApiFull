using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PersonalConceptosRemunerativosRepository : IPersonalConceptosRemunerativosRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalConceptosRemunerativosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PersonalConceptosRemunerativos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalConceptosRemunerativos>(SP.SCHEMARrhh.PersonalConceptosRemunerativos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PersonalConceptosRemunerativos?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PersonalConceptosRemunerativos>(SP.SCHEMARrhh.PersonalConceptosRemunerativos.Showbyid, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.PersonalConceptosRemunerativos.Showcombox);
    }

    public async Task<IEnumerable<PersonalConceptosRemunerativos>> SearchAsync(PersonalConceptosRemunerativosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalConceptosRemunerativos>(SP.SCHEMARrhh.PersonalConceptosRemunerativos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PersonalConceptosRemunerativosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("VAL_CONCEPTO_REMUNERATIVO", dto.VAL_CONCEPTO_REMUNERATIVO);
        p.Add("TAS_CONCEPTO_REMUNERATIVO", dto.TAS_CONCEPTO_REMUNERATIVO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalConceptosRemunerativos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalConceptosRemunerativos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalConceptosRemunerativos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PersonalConceptosRemunerativosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("VAL_CONCEPTO_REMUNERATIVO", dto.VAL_CONCEPTO_REMUNERATIVO);
        p.Add("TAS_CONCEPTO_REMUNERATIVO", dto.TAS_CONCEPTO_REMUNERATIVO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalConceptosRemunerativos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalConceptosRemunerativos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalConceptosRemunerativos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalConceptosRemunerativos.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalConceptosRemunerativos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalConceptosRemunerativos.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO })
        };
    }
}

}