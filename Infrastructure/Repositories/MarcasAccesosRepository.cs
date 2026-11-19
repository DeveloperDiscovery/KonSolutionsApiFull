using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MarcasAccesosRepository : IMarcasAccesosRepository
{
    private readonly IDbConnectionFactory _db;
    public MarcasAccesosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<MarcasAccesos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MarcasAccesos>(SP.SCHEMASafety.MarcasAccesos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<MarcasAccesos?> ShowbyidAsync(int NUM_READ_TAG)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MarcasAccesos>(SP.SCHEMASafety.MarcasAccesos.Showbyid, new { NUM_READ_TAG = NUM_READ_TAG }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASafety.MarcasAccesos.Showcombox);
    }

    public async Task<IEnumerable<MarcasAccesos>> SearchAsync(MarcasAccesosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MarcasAccesos>(SP.SCHEMASafety.MarcasAccesos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MarcasAccesosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_READ_TAG", dto.NUM_READ_TAG);
        p.Add("DES_READ_TAG", dto.DES_READ_TAG);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.MarcasAccesos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.MarcasAccesos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.MarcasAccesos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(MarcasAccesosUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_READ_TAG", dto.NUM_READ_TAG);
        p.Add("DES_READ_TAG", dto.DES_READ_TAG);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.MarcasAccesos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.MarcasAccesos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.MarcasAccesos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_READ_TAG)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.MarcasAccesos.Delete, new { NUM_READ_TAG = NUM_READ_TAG }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.MarcasAccesos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.MarcasAccesos.Delete, new { NUM_READ_TAG = NUM_READ_TAG })
        };
    }
}

}