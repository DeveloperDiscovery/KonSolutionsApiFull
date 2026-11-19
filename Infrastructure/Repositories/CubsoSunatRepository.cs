using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Sunat;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CubsoSunatRepository : ICubsoSunatRepository
{
    private readonly IDbConnectionFactory _db;
    public CubsoSunatRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CubsoSunat>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CubsoSunat>(SP.SCHEMASunat.CubsoSunat.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CubsoSunat?> ShowbyidAsync(string COD_CUBSO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CubsoSunat>(SP.SCHEMASunat.CubsoSunat.Showbyid, new { COD_CUBSO = COD_CUBSO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASunat.CubsoSunat.Showcombox, "COD_CUBSO", "");
    }

    public async Task<IEnumerable<CubsoSunat>> SearchAsync(CubsoSunatSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CubsoSunat>(SP.SCHEMASunat.CubsoSunat.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CubsoSunatInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CUBSO", dto.COD_CUBSO);
        p.Add("DES_CUBSO", dto.DES_CUBSO);
        p.Add("DES_CUBSO_ES", dto.DES_CUBSO_ES);
        p.Add("COD_CUBSO_PARENT", dto.COD_CUBSO_PARENT);
        p.Add("COD_UNSPS", dto.COD_UNSPS);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASunat.CubsoSunat.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASunat.CubsoSunat.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASunat.CubsoSunat.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CubsoSunatUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CUBSO", dto.COD_CUBSO);
        p.Add("DES_CUBSO", dto.DES_CUBSO);
        p.Add("DES_CUBSO_ES", dto.DES_CUBSO_ES);
        p.Add("COD_CUBSO_PARENT", dto.COD_CUBSO_PARENT);
        p.Add("COD_UNSPS", dto.COD_UNSPS);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASunat.CubsoSunat.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASunat.CubsoSunat.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASunat.CubsoSunat.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CUBSO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASunat.CubsoSunat.Delete, new { COD_CUBSO = COD_CUBSO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASunat.CubsoSunat.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASunat.CubsoSunat.Delete, new { COD_CUBSO = COD_CUBSO })
        };
    }
}

    public async Task<WriteResult> UploadJsonAsync(string jsonArray)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMASunat.CubsoSunat.UploadJson,
            new { JSON_ARRAY = jsonArray },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASunat.CubsoSunat.UploadJson,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASunat.CubsoSunat.UploadJson, new { JSON_ARRAY = jsonArray })
        };
    }
}

}