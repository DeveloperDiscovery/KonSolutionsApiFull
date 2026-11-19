using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosSubTareasRepository : IProyectosSubTareasRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosSubTareasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProyectosSubTareas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosSubTareas>(SP.SCHEMAProyectos.ProyectosSubTareas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProyectosSubTareas?> ShowbyidAsync(int NUM_PROYECTO, int COD_TAREA, int COD_SUB_TAREA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosSubTareas>(SP.SCHEMAProyectos.ProyectosSubTareas.Showbyid, new { NUM_PROYECTO = NUM_PROYECTO, COD_TAREA = COD_TAREA, COD_SUB_TAREA = COD_SUB_TAREA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.ProyectosSubTareas.Showcombox);
    }

    public async Task<IEnumerable<ProyectosSubTareas>> SearchAsync(ProyectosSubTareasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosSubTareas>(SP.SCHEMAProyectos.ProyectosSubTareas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProyectosSubTareasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_TAREA", dto.COD_TAREA);
        p.Add("COD_SUB_TAREA", dto.COD_SUB_TAREA);
        p.Add("DES_SUB_TAREA", dto.DES_SUB_TAREA);
        p.Add("FLG_TERMINADA", dto.FLG_TERMINADA ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosSubTareas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosSubTareas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosSubTareas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProyectosSubTareasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_TAREA", dto.COD_TAREA);
        p.Add("COD_SUB_TAREA", dto.COD_SUB_TAREA);
        p.Add("DES_SUB_TAREA", dto.DES_SUB_TAREA);
        p.Add("FLG_TERMINADA", dto.FLG_TERMINADA ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosSubTareas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosSubTareas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosSubTareas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int COD_TAREA, int COD_SUB_TAREA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosSubTareas.Delete, new { NUM_PROYECTO = NUM_PROYECTO, COD_TAREA = COD_TAREA, COD_SUB_TAREA = COD_SUB_TAREA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosSubTareas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosSubTareas.Delete, new { NUM_PROYECTO = NUM_PROYECTO, COD_TAREA = COD_TAREA, COD_SUB_TAREA = COD_SUB_TAREA })
        };
    }
}

}