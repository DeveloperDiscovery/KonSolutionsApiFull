using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PersonalBeneficiariosRepository : IPersonalBeneficiariosRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalBeneficiariosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PersonalBeneficiarios>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalBeneficiarios>(SP.SCHEMARrhh.PersonalBeneficiarios.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PersonalBeneficiarios?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_BENEFECIARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PersonalBeneficiarios>(SP.SCHEMARrhh.PersonalBeneficiarios.Showbyid, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_ENTIDAD_BENEFECIARIO = COD_ENTIDAD_BENEFECIARIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.PersonalBeneficiarios.Showcombox);
    }

    public async Task<IEnumerable<PersonalBeneficiarios>> SearchAsync(PersonalBeneficiariosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalBeneficiarios>(SP.SCHEMARrhh.PersonalBeneficiarios.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PersonalBeneficiariosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_ENTIDAD_BENEFECIARIO", dto.COD_ENTIDAD_BENEFECIARIO);
        p.Add("COD_TIPO_PARENTESCO", dto.COD_TIPO_PARENTESCO);
        p.Add("TAS_BENEFICIO", dto.TAS_BENEFICIO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalBeneficiarios.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalBeneficiarios.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalBeneficiarios.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PersonalBeneficiariosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_ENTIDAD_BENEFECIARIO", dto.COD_ENTIDAD_BENEFECIARIO);
        p.Add("COD_TIPO_PARENTESCO", dto.COD_TIPO_PARENTESCO);
        p.Add("TAS_BENEFICIO", dto.TAS_BENEFICIO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalBeneficiarios.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalBeneficiarios.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalBeneficiarios.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_BENEFECIARIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalBeneficiarios.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_ENTIDAD_BENEFECIARIO = COD_ENTIDAD_BENEFECIARIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalBeneficiarios.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalBeneficiarios.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_ENTIDAD_BENEFECIARIO = COD_ENTIDAD_BENEFECIARIO })
        };
    }
}

}