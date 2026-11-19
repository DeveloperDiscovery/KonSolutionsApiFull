using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PersonalAfpConceptosRemunerativosRepository : IPersonalAfpConceptosRemunerativosRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalAfpConceptosRemunerativosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PersonalAfpConceptosRemunerativos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalAfpConceptosRemunerativos>(SP.SCHEMARrhh.PersonalAfpConceptosRemunerativos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PersonalAfpConceptosRemunerativos?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA, int COD_CONCEPTO_REMUNERATIVO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PersonalAfpConceptosRemunerativos>(SP.SCHEMARrhh.PersonalAfpConceptosRemunerativos.Showbyid, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.PersonalAfpConceptosRemunerativos.Showcombox);
    }

    public async Task<IEnumerable<PersonalAfpConceptosRemunerativos>> SearchAsync(PersonalAfpConceptosRemunerativosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalAfpConceptosRemunerativos>(SP.SCHEMARrhh.PersonalAfpConceptosRemunerativos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PersonalAfpConceptosRemunerativosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("VAL_CONCEPTO_REMUNERATIVO", dto.VAL_CONCEPTO_REMUNERATIVO);
        p.Add("TAS_CONCEPTO_REMUNERATIVO", dto.TAS_CONCEPTO_REMUNERATIVO);
        p.Add("COD_CUENTA_CONTABLE_DEBE", dto.COD_CUENTA_CONTABLE_DEBE);
        p.Add("COD_CUENTA_CONTABLE_HABER", dto.COD_CUENTA_CONTABLE_HABER);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalAfpConceptosRemunerativos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalAfpConceptosRemunerativos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalAfpConceptosRemunerativos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PersonalAfpConceptosRemunerativosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("VAL_CONCEPTO_REMUNERATIVO", dto.VAL_CONCEPTO_REMUNERATIVO);
        p.Add("TAS_CONCEPTO_REMUNERATIVO", dto.TAS_CONCEPTO_REMUNERATIVO);
        p.Add("COD_CUENTA_CONTABLE_DEBE", dto.COD_CUENTA_CONTABLE_DEBE);
        p.Add("COD_CUENTA_CONTABLE_HABER", dto.COD_CUENTA_CONTABLE_HABER);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalAfpConceptosRemunerativos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalAfpConceptosRemunerativos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalAfpConceptosRemunerativos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA, int COD_CONCEPTO_REMUNERATIVO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalAfpConceptosRemunerativos.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalAfpConceptosRemunerativos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalAfpConceptosRemunerativos.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO })
        };
    }
}

}