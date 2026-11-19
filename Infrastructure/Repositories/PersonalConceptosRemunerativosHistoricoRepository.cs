using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PersonalConceptosRemunerativosHistoricoRepository : IPersonalConceptosRemunerativosHistoricoRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalConceptosRemunerativosHistoricoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PersonalConceptosRemunerativosHistorico>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalConceptosRemunerativosHistorico>(SP.SCHEMARrhh.PersonalConceptosRemunerativosHistorico.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PersonalConceptosRemunerativosHistorico?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PersonalConceptosRemunerativosHistorico>(SP.SCHEMARrhh.PersonalConceptosRemunerativosHistorico.Showbyid, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.PersonalConceptosRemunerativosHistorico.Showcombox);
    }

    public async Task<IEnumerable<PersonalConceptosRemunerativosHistorico>> SearchAsync(PersonalConceptosRemunerativosHistoricoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalConceptosRemunerativosHistorico>(SP.SCHEMARrhh.PersonalConceptosRemunerativosHistorico.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PersonalConceptosRemunerativosHistoricoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("VAL_CONCEPTO_REMUNERATIVO", dto.VAL_CONCEPTO_REMUNERATIVO);
        p.Add("TAS_CONCEPTO_REMUNERATIVO", dto.TAS_CONCEPTO_REMUNERATIVO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalConceptosRemunerativosHistorico.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalConceptosRemunerativosHistorico.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalConceptosRemunerativosHistorico.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PersonalConceptosRemunerativosHistoricoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("VAL_CONCEPTO_REMUNERATIVO", dto.VAL_CONCEPTO_REMUNERATIVO);
        p.Add("TAS_CONCEPTO_REMUNERATIVO", dto.TAS_CONCEPTO_REMUNERATIVO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalConceptosRemunerativosHistorico.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalConceptosRemunerativosHistorico.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalConceptosRemunerativosHistorico.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalConceptosRemunerativosHistorico.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalConceptosRemunerativosHistorico.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalConceptosRemunerativosHistorico.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}