using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosRolesRepository : IProyectosRolesRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosRolesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProyectosRoles>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosRoles>(SP.SCHEMAProyectos.ProyectosRoles.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProyectosRoles?> ShowbyidAsync(string COD_ROL_TAREA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosRoles>(SP.SCHEMAProyectos.ProyectosRoles.Showbyid, new { COD_ROL_TAREA = COD_ROL_TAREA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> CategoriasComboAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, "PROC_PROYECTOS_PROYECTOS_ROLES_CATEGORIAS_SHOWCOMBOX");
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.ProyectosRoles.Showcombox);
    }

    public async Task<IEnumerable<ProyectosRoles>> SearchAsync(ProyectosRolesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosRoles>(SP.SCHEMAProyectos.ProyectosRoles.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProyectosRolesInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_TAREA", dto.COD_ROL_TAREA);
        p.Add("DES_ROL_TAREA", dto.DES_ROL_TAREA);
        p.Add("COD_CATEGORIA_ROL", dto.COD_CATEGORIA_ROL);
        p.Add("COD_ROL_PADRE", dto.COD_ROL_PADRE);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosRoles.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosRoles.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosRoles.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProyectosRolesUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_TAREA", dto.COD_ROL_TAREA);
        p.Add("DES_ROL_TAREA", dto.DES_ROL_TAREA);
        p.Add("COD_CATEGORIA_ROL", dto.COD_CATEGORIA_ROL);
        p.Add("COD_ROL_PADRE", dto.COD_ROL_PADRE);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosRoles.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosRoles.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosRoles.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ROL_TAREA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosRoles.Delete, new { COD_ROL_TAREA = COD_ROL_TAREA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosRoles.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosRoles.Delete, new { COD_ROL_TAREA = COD_ROL_TAREA })
        };
    }
}

}