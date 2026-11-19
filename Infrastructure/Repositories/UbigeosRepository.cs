using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class UbigeosRepository : IUbigeosRepository
{
    private readonly IDbConnectionFactory _db;
    public UbigeosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Ubigeos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Ubigeos>(SP.SCHEMAEstandar.Ubigeos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Ubigeos?> ShowbyidAsync(string COD_UBIGEO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Ubigeos>(SP.SCHEMAEstandar.Ubigeos.Showbyid, new { COD_UBIGEO = COD_UBIGEO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string? search = null)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.Ubigeos.Showcombox, new { SEARCH = search });
    }

    public async Task<IEnumerable<Ubigeos>> SearchAsync(UbigeosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Ubigeos>(SP.SCHEMAEstandar.Ubigeos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(UbigeosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_UBIGEO", dto.COD_UBIGEO);
        p.Add("DES_UBIGEO", dto.DES_UBIGEO);
        p.Add("FLG_DEPARTAMENTO", dto.FLG_DEPARTAMENTO ?? false);
        p.Add("FLG_PROVINCIA", dto.FLG_PROVINCIA ?? false);
        p.Add("FLG_DISTRITO", dto.FLG_DISTRITO ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Ubigeos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Ubigeos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Ubigeos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(UbigeosUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_UBIGEO", dto.COD_UBIGEO);
        p.Add("DES_UBIGEO", dto.DES_UBIGEO);
        p.Add("FLG_DEPARTAMENTO", dto.FLG_DEPARTAMENTO ?? false);
        p.Add("FLG_PROVINCIA", dto.FLG_PROVINCIA ?? false);
        p.Add("FLG_DISTRITO", dto.FLG_DISTRITO ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Ubigeos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Ubigeos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Ubigeos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_UBIGEO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Ubigeos.Delete, new { COD_UBIGEO = COD_UBIGEO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Ubigeos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Ubigeos.Delete, new { COD_UBIGEO = COD_UBIGEO })
        };
    }
}

    public async Task<WriteResult> UploadJsonAsync(string jsonArray)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.Ubigeos.UploadJson,
            new { JSON_ARRAY = jsonArray },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Ubigeos.UploadJson,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Ubigeos.UploadJson, new { JSON_ARRAY = jsonArray })
        };
    }
}

}