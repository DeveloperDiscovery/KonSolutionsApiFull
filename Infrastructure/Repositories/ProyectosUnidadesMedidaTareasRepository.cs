using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosUnidadesMedidaTareasRepository : IProyectosUnidadesMedidaTareasRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosUnidadesMedidaTareasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProyectosUnidadesMedidaTareas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosUnidadesMedidaTareas>(SP.SCHEMAProyectos.ProyectosUnidadesMedidaTareas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProyectosUnidadesMedidaTareas?> ShowbyidAsync(string COD_UNIDAD_MEDIDA_TAREAS)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosUnidadesMedidaTareas>(SP.SCHEMAProyectos.ProyectosUnidadesMedidaTareas.Showbyid, new { COD_UNIDAD_MEDIDA_TAREAS = COD_UNIDAD_MEDIDA_TAREAS }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.ProyectosUnidadesMedidaTareas.Showcombox);
    }

    public async Task<IEnumerable<ProyectosUnidadesMedidaTareas>> SearchAsync(ProyectosUnidadesMedidaTareasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosUnidadesMedidaTareas>(SP.SCHEMAProyectos.ProyectosUnidadesMedidaTareas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProyectosUnidadesMedidaTareasInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_UNIDAD_MEDIDA_TAREAS", dto.COD_UNIDAD_MEDIDA_TAREAS);
        p.Add("DES_UNIDAD_MEDIDA_TAREAS", dto.DES_UNIDAD_MEDIDA_TAREAS);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosUnidadesMedidaTareas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosUnidadesMedidaTareas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosUnidadesMedidaTareas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProyectosUnidadesMedidaTareasUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_UNIDAD_MEDIDA_TAREAS", dto.COD_UNIDAD_MEDIDA_TAREAS);
        p.Add("DES_UNIDAD_MEDIDA_TAREAS", dto.DES_UNIDAD_MEDIDA_TAREAS);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosUnidadesMedidaTareas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosUnidadesMedidaTareas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosUnidadesMedidaTareas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_UNIDAD_MEDIDA_TAREAS)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosUnidadesMedidaTareas.Delete, new { COD_UNIDAD_MEDIDA_TAREAS = COD_UNIDAD_MEDIDA_TAREAS }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosUnidadesMedidaTareas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosUnidadesMedidaTareas.Delete, new { COD_UNIDAD_MEDIDA_TAREAS = COD_UNIDAD_MEDIDA_TAREAS })
        };
    }
}

}