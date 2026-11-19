using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Solicitudes;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PrioridadesRepository : IPrioridadesRepository
{
    private readonly IDbConnectionFactory _db;
    public PrioridadesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Prioridades>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Prioridades>(SP.SCHEMASolicitudes.Prioridades.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Prioridades?> ShowbyidAsync(string COD_PRIORIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Prioridades>(SP.SCHEMASolicitudes.Prioridades.Showbyid, new { COD_PRIORIDAD = COD_PRIORIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASolicitudes.Prioridades.Showcombox);
    }

    public async Task<IEnumerable<Prioridades>> SearchAsync(PrioridadesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Prioridades>(SP.SCHEMASolicitudes.Prioridades.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PrioridadesInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PRIORIDAD", dto.COD_PRIORIDAD);
        p.Add("DES_PRIORIDAD", dto.DES_PRIORIDAD);
        p.Add("VAL_PRIORIDAD", dto.VAL_PRIORIDAD);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASolicitudes.Prioridades.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASolicitudes.Prioridades.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASolicitudes.Prioridades.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PrioridadesUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PRIORIDAD", dto.COD_PRIORIDAD);
        p.Add("DES_PRIORIDAD", dto.DES_PRIORIDAD);
        p.Add("VAL_PRIORIDAD", dto.VAL_PRIORIDAD);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASolicitudes.Prioridades.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASolicitudes.Prioridades.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASolicitudes.Prioridades.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PRIORIDAD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASolicitudes.Prioridades.Delete, new { COD_PRIORIDAD = COD_PRIORIDAD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASolicitudes.Prioridades.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASolicitudes.Prioridades.Delete, new { COD_PRIORIDAD = COD_PRIORIDAD })
        };
    }
}

}