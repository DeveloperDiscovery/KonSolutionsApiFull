using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesConfigurarRepository : IEntidadesConfigurarRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesConfigurarRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadesConfigurar>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesConfigurar>(SP.SCHEMAEntorno.EntidadesConfigurar.Showall,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesConfigurar?> ShowbyidAsync(string desTable, string desColumn)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesConfigurar>(SP.SCHEMAEntorno.EntidadesConfigurar.Showbyid,
            new { DES_TABLE = desTable, DES_COLUMN = desColumn }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string desTable)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.EntidadesConfigurar.Showcombox,
            new { DES_TABLE = desTable });
    }

    public async Task<IEnumerable<EntidadesConfigurar>> SearchAsync(EntidadesConfigurarSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesConfigurar>(SP.SCHEMAEntorno.EntidadesConfigurar.Search,
            new { DES_TABLE = dto.DES_TABLE, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesConfigurarInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("DES_TABLE", dto.DES_TABLE);
        p.Add("DES_COLUMN", dto.DES_COLUMN);
        p.Add("DES_COMMANDSQL", dto.DES_COMMANDSQL);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.EntidadesConfigurar.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEntorno.EntidadesConfigurar.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.EntidadesConfigurar.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(EntidadesConfigurarUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("DES_TABLE", dto.DES_TABLE);
        p.Add("DES_COLUMN", dto.DES_COLUMN);
        p.Add("DES_COMMANDSQL", dto.DES_COMMANDSQL);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.EntidadesConfigurar.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEntorno.EntidadesConfigurar.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.EntidadesConfigurar.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string desTable, string desColumn)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("DES_TABLE", desTable);
        p.Add("DES_COLUMN", desColumn);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.EntidadesConfigurar.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEntorno.EntidadesConfigurar.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.EntidadesConfigurar.Delete, p)
            };
        }
    }
}
