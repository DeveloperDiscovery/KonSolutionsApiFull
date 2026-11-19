using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RolesUsuarioAplicacionesRepository : IRolesUsuarioAplicacionesRepository
{
    private readonly IDbConnectionFactory _db;
    public RolesUsuarioAplicacionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RolesUsuarioAplicaciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioAplicaciones>(SP.SCHEMASeguridad.RolesUsuarioAplicaciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<RolesUsuarioAplicaciones?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_APLICACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RolesUsuarioAplicaciones>(SP.SCHEMASeguridad.RolesUsuarioAplicaciones.Showbyid, new { COD_ROL_USUARIO = COD_ROL_USUARIO, COD_APLICACION = COD_APLICACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.RolesUsuarioAplicaciones.Showcombox);
    }

    public async Task<IEnumerable<RolesUsuarioAplicaciones>> SearchAsync(RolesUsuarioAplicacionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioAplicaciones>(SP.SCHEMASeguridad.RolesUsuarioAplicaciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RolesUsuarioAplicacionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_USUARIO", dto.COD_ROL_USUARIO);
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("FLG_ACCESO_PERMITIDO", dto.FLG_ACCESO_PERMITIDO ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.RolesUsuarioAplicaciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuarioAplicaciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuarioAplicaciones.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(RolesUsuarioAplicacionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_USUARIO", dto.COD_ROL_USUARIO);
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("FLG_ACCESO_PERMITIDO", dto.FLG_ACCESO_PERMITIDO ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.RolesUsuarioAplicaciones.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuarioAplicaciones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuarioAplicaciones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_APLICACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.RolesUsuarioAplicaciones.Delete, new { COD_ROL_USUARIO = COD_ROL_USUARIO, COD_APLICACION = COD_APLICACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuarioAplicaciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuarioAplicaciones.Delete, new { COD_ROL_USUARIO = COD_ROL_USUARIO, COD_APLICACION = COD_APLICACION })
        };
    }
}

}