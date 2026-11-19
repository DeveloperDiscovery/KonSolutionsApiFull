using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RolesUsuarioUsuariosRepository : IRolesUsuarioUsuariosRepository
{
    private readonly IDbConnectionFactory _db;
    public RolesUsuarioUsuariosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RolesUsuarioUsuarios>> ShowByRolAsync(string codRol)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioUsuarios>(SP.SCHEMASeguridad.RolesUsuarioUsuarios.Showall, new { COD_ROL_USUARIO = codRol }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Trae TODOS los roles asignados a un usuario (un usuario puede tener varios).
    /// Usado al login para construir el selector de rol en la barra superior, y
    /// para resolver qué rol activo se usa al pedir el treeview de permisos.
    /// </summary>
    public async Task<IEnumerable<RolesUsuarioUsuarios>> ShowbyusuarioAsync(string codUsuario)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioUsuarios>(
            SP.SCHEMASeguridad.RolesUsuarioUsuarios.Showbyusuario,
            new { COD_USUARIO = codUsuario },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RolesUsuarioUsuarios>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioUsuarios>(SP.SCHEMASeguridad.RolesUsuarioUsuarios.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<RolesUsuarioUsuarios?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_USUARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RolesUsuarioUsuarios>(SP.SCHEMASeguridad.RolesUsuarioUsuarios.Showbyid, new { COD_ROL_USUARIO = COD_ROL_USUARIO, COD_USUARIO = COD_USUARIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.RolesUsuarioUsuarios.Showcombox);
    }

    public async Task<IEnumerable<RolesUsuarioUsuarios>> SearchAsync(RolesUsuarioUsuariosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioUsuarios>(SP.SCHEMASeguridad.RolesUsuarioUsuarios.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RolesUsuarioUsuariosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_USUARIO", dto.COD_ROL_USUARIO);
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.RolesUsuarioUsuarios.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuarioUsuarios.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuarioUsuarios.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_USUARIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.RolesUsuarioUsuarios.Delete, new { COD_ROL_USUARIO = COD_ROL_USUARIO, COD_USUARIO = COD_USUARIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuarioUsuarios.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuarioUsuarios.Delete, new { COD_ROL_USUARIO = COD_ROL_USUARIO, COD_USUARIO = COD_USUARIO })
        };
    }
}

}