using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PostItsRepository : IPostItsRepository
{
    private readonly IDbConnectionFactory _db;
    public PostItsRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PostIts>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PostIts>(SP.SCHEMAEntorno.PostIts.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PostIts?> ShowbyidAsync(long NUM_POSTIT)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PostIts>(SP.SCHEMAEntorno.PostIts.Showbyid, new { NUM_POSTIT = NUM_POSTIT }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PostItsInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_POSTIT", dto.NUM_POSTIT);
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("DES_TITULO", dto.DES_TITULO);
        p.Add("DES_POSTIT", dto.DES_POSTIT);
        p.Add("POS_LEFT", dto.POS_LEFT);
        p.Add("POS_TOP", dto.POS_TOP);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.PostIts.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.PostIts.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.PostIts.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(long NUM_POSTIT)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.PostIts.Delete, new { NUM_POSTIT = NUM_POSTIT }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.PostIts.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.PostIts.Delete, new { NUM_POSTIT = NUM_POSTIT })
        };
    }
}
}
