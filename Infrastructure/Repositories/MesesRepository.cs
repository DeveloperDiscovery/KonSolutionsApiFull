using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MesesRepository : IMesesRepository
{
    private readonly IDbConnectionFactory _db;
    public MesesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Meses>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Meses>(SP.SCHEMAEstandar.Meses.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Meses?> ShowbyidAsync(int NUM_MES)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Meses>(SP.SCHEMAEstandar.Meses.Showbyid, new { NUM_MES }, commandType: CommandType.StoredProcedure);
    }

    // PROC_ESTANDAR_MESES_SHOWCOMBOX no alias sus columnas como Id/Texto (selecciona
    // NUM_MES/DES_MES tal cual) — se mapea a mano en vez de dejar que Dapper intente
    // el bind automático por nombre.
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.Meses.Showcombox);
    }

    public async Task<WriteResult> InsertAsync(MesesInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MES", dto.NUM_MES);
        p.Add("DES_MES", dto.DES_MES);
        p.Add("NUM_DIAS", dto.NUM_DIAS);
        p.Add("FLG_AFECTA_BISIESTO", dto.FLG_AFECTA_BISIESTO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Meses.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Meses.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Meses.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_MES)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Meses.Delete, new { NUM_MES }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Meses.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Meses.Delete, new { NUM_MES })
        };
    }
}
}
