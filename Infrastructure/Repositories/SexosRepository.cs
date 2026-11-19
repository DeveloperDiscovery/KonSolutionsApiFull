using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SexosRepository : ISexosRepository
{
    private readonly IDbConnectionFactory _db;
    public SexosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Sexos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Sexos>(SP.SCHEMAEstandar.Sexos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Sexos?> ShowbyidAsync(string COD_SEXO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Sexos>(SP.SCHEMAEstandar.Sexos.Showbyid, new { COD_SEXO = COD_SEXO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.Sexos.Showcombox);
    }

    public async Task<IEnumerable<Sexos>> SearchAsync(SexosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Sexos>(SP.SCHEMAEstandar.Sexos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SexosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SEXO", dto.COD_SEXO);
        p.Add("DES_SEXO", dto.DES_SEXO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Sexos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Sexos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Sexos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SexosUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SEXO", dto.COD_SEXO);
        p.Add("DES_SEXO", dto.DES_SEXO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Sexos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Sexos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Sexos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_SEXO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Sexos.Delete, new { COD_SEXO = COD_SEXO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Sexos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Sexos.Delete, new { COD_SEXO = COD_SEXO })
        };
    }
}

}