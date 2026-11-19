using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Developer;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class StoresProceduresRepository : IStoresProceduresRepository
{
    private readonly IDbConnectionFactory _db;
    public StoresProceduresRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<StoresProcedures>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<StoresProcedures>(SP.SCHEMADeveloper.StoresProcedures.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMADeveloper.StoresProcedures.Showcombox);
    }

    public async Task<IEnumerable<StoresProcedures>> SearchAsync(StoresProceduresSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<StoresProcedures>(SP.SCHEMADeveloper.StoresProcedures.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

}