using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AnosRepository : IAnosRepository
{
    private readonly IDbConnectionFactory _db;
    public AnosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Anos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Anos>(SP.SCHEMAEstandar.Anos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Anos?> ShowbyidAsync(int NUM_ANO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Anos>(SP.SCHEMAEstandar.Anos.Showbyid, new { NUM_ANO }, commandType: CommandType.StoredProcedure);
    }

    // PROC_ESTANDAR_ANOS_SHOWCOMBOX no alias sus columnas como Id/Texto (selecciona
    // NUM_ANO/DES_ANO tal cual) — se mapea a mano en vez de dejar que Dapper intente
    // el bind automático por nombre.
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.Anos.Showcombox);
    }

    public async Task<WriteResult> InsertAsync(AnosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ANO", dto.NUM_ANO);
        p.Add("DES_ANO", dto.DES_ANO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Anos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Anos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Anos.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_ANO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Anos.Delete, new { NUM_ANO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Anos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Anos.Delete, new { NUM_ANO })
        };
    }
}
}
