using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Soporte;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TareasRepository : ITareasRepository
{
    private readonly IDbConnectionFactory _db;
    public TareasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Tareas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Tareas>(SP.SCHEMASoporte.Tareas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Tareas?> ShowbyidAsync(int COD_TAREA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Tareas>(SP.SCHEMASoporte.Tareas.Showbyid, new { COD_TAREA = COD_TAREA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASoporte.Tareas.Showcombox);
    }

    public async Task<IEnumerable<Tareas>> SearchAsync(TareasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Tareas>(SP.SCHEMASoporte.Tareas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TareasInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TAREA", dto.COD_TAREA);
        p.Add("DES_TAREA", dto.DES_TAREA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASoporte.Tareas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASoporte.Tareas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASoporte.Tareas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo juego de parámetros que el alta: el SP _UPDATE se
    /// genera con el mismo contrato que el _INSERT.</summary>
    public async Task<WriteResult> UpdateAsync(TareasUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TAREA", dto.COD_TAREA);
        p.Add("DES_TAREA", dto.DES_TAREA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASoporte.Tareas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASoporte.Tareas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASoporte.Tareas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_TAREA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASoporte.Tareas.Delete, new { COD_TAREA = COD_TAREA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASoporte.Tareas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASoporte.Tareas.Delete, new { COD_TAREA = COD_TAREA })
        };
    }
}

}