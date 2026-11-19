using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RolesUsuariosAplicacionesOpcionesRepository : IRolesUsuariosAplicacionesOpcionesRepository
{
    private readonly IDbConnectionFactory _db;
    public RolesUsuariosAplicacionesOpcionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RolesUsuariosAplicacionesOpciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuariosAplicacionesOpciones>(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RolesUsuariosAplicacionesOpciones>> ShowByRolAplicacionAsync(string codRol, string codAplicacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_USUARIO", codRol, DbType.String);
        p.Add("COD_APLICACION",  codAplicacion, DbType.String);
        // Reutiliza el SP de SHOWALL filtrando en memoria si no hay SP específico,
        // o ejecuta el SP de SHOWBYID con solo rol+app si existe en la BD.
        var todos = await conn.QueryAsync<RolesUsuariosAplicacionesOpciones>(
            SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Showall,
            commandType: CommandType.StoredProcedure);
        return todos.Where(r => r.COD_ROL_USUARIO == codRol && r.COD_APLICACION == codAplicacion);
    }

    public async Task<RolesUsuariosAplicacionesOpciones?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RolesUsuariosAplicacionesOpciones>(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Showbyid, new { COD_ROL_USUARIO = COD_ROL_USUARIO, COD_APLICACION = COD_APLICACION, COD_OPCION_APLICACION = COD_OPCION_APLICACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Showcombox);
    }

    public async Task<IEnumerable<RolesUsuariosAplicacionesOpciones>> SearchAsync(RolesUsuariosAplicacionesOpcionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuariosAplicacionesOpciones>(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RolesUsuariosAplicacionesOpcionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_USUARIO", dto.COD_ROL_USUARIO);
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION);
        p.Add("FLG_ACCESO_PERMITIDO", dto.FLG_ACCESO_PERMITIDO ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(RolesUsuariosAplicacionesOpcionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_USUARIO", dto.COD_ROL_USUARIO);
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION);
        p.Add("FLG_ACCESO_PERMITIDO", dto.FLG_ACCESO_PERMITIDO ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Delete, new { COD_ROL_USUARIO = COD_ROL_USUARIO, COD_APLICACION = COD_APLICACION, COD_OPCION_APLICACION = COD_OPCION_APLICACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.Delete, new { COD_ROL_USUARIO = COD_ROL_USUARIO, COD_APLICACION = COD_APLICACION, COD_OPCION_APLICACION = COD_OPCION_APLICACION })
        };
    }
}

    public async Task<IEnumerable<NodoOpcion>> TreeviewAsignadasAsync(string codRolUsuario)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<NodoOpcion>(
            SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.TreeviewAsignadas,
            new { COD_ROL_USUARIO = codRolUsuario },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<NodoOpcion>> TreeviewDisponiblesAsync(string codRolUsuario)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<NodoOpcion>(
            SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpciones.TreeviewDisponibles,
            new { COD_ROL_USUARIO = codRolUsuario },
            commandType: CommandType.StoredProcedure);
    }

}