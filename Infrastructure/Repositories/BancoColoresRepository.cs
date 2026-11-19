using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class BancoColoresRepository : IBancoColoresRepository
{
    private readonly IDbConnectionFactory _db;
    public BancoColoresRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<BancoColores>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<BancoColores>(SP.SCHEMAEntorno.BancoColores.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<BancoColores?> ShowbyidAsync(int COD_BANCO_COLOR)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<BancoColores>(SP.SCHEMAEntorno.BancoColores.Showbyid, new { COD_BANCO_COLOR }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.BancoColores.Showcombox);
    }

    public async Task<IEnumerable<BancoColores>> SearchAsync(BancoColoresSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<BancoColores>(SP.SCHEMAEntorno.BancoColores.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Upsert: el SP intenta UPDATE por COD_BANCO_COLOR; si no encuentra fila (alta nueva,
    /// COD_BANCO_COLOR = 0), hace INSERT sin el código (IDENTITY) y devuelve el nuevo id.</summary>
    public async Task<WriteResult> InsertAsync(BancoColoresSaveDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_BANCO_COLOR",  dto.COD_BANCO_COLOR);
        p.Add("DES_BANCO_COLOR",  dto.DES_BANCO_COLOR);
        p.Add("DES_CODIGO_COLOR", dto.DES_CODIGO_COLOR);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.BancoColores.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.BancoColores.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.BancoColores.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_BANCO_COLOR)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.BancoColores.Delete, new { COD_BANCO_COLOR }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.BancoColores.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.BancoColores.Delete, new { COD_BANCO_COLOR })
        };
    }
}
}
