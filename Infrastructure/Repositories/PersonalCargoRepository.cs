using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PersonalCargoRepository : IPersonalCargoRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalCargoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PersonalCargo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalCargo>(SP.SCHEMARrhh.PersonalCargo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PersonalCargo?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CARGO, DateTime FEC_INGRESO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PersonalCargo>(SP.SCHEMARrhh.PersonalCargo.Showbyid, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_CARGO = COD_CARGO, FEC_INGRESO = FEC_INGRESO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.PersonalCargo.Showcombox);
    }

    public async Task<IEnumerable<PersonalCargo>> SearchAsync(PersonalCargoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalCargo>(SP.SCHEMARrhh.PersonalCargo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PersonalCargoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("FEC_INGRESO", dto.FEC_INGRESO);
        p.Add("FEC_SALIDA", dto.FEC_SALIDA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalCargo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalCargo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalCargo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PersonalCargoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("FEC_INGRESO", dto.FEC_INGRESO);
        p.Add("FEC_SALIDA", dto.FEC_SALIDA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalCargo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalCargo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalCargo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CARGO, DateTime FEC_INGRESO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalCargo.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_CARGO = COD_CARGO, FEC_INGRESO = FEC_INGRESO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalCargo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalCargo.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_CARGO = COD_CARGO, FEC_INGRESO = FEC_INGRESO })
        };
    }
}

}