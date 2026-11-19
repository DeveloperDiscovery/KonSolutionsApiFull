using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RolesUsuarioTiposOperacionRepository : IRolesUsuarioTiposOperacionRepository
{
    private readonly IDbConnectionFactory _db;
    public RolesUsuarioTiposOperacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RolesUsuarioTiposOperacion>> ShowallAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioTiposOperacion>(
            SP.SCHEMASeguridad.RolesUsuarioTiposOperacion.Showall,
            new { COD_ROL_USUARIO, COD_CLASE_OPERACION }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Devuelve los disponibles de TODAS las clases: el SP no recibe la clase. Se deja
    /// así en vez de filtrar acá para no hacer una llamada por cada clase que el usuario mire.</summary>
    public async Task<IEnumerable<RolesUsuarioTiposOperacion>> DisponiblesAsync(string COD_ROL_USUARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioTiposOperacion>(
            SP.SCHEMASeguridad.RolesUsuarioTiposOperacion.Disponibles,
            new { COD_ROL_USUARIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<RolesUsuarioTiposOperacion?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RolesUsuarioTiposOperacion>(
            SP.SCHEMASeguridad.RolesUsuarioTiposOperacion.Showbyid,
            new { COD_ROL_USUARIO, COD_CLASE_OPERACION, COD_TIPO_OPERACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn,
            SP.SCHEMASeguridad.RolesUsuarioTiposOperacion.Showcombox, new { COD_ROL_USUARIO, COD_CLASE_OPERACION });
    }

    public async Task<IEnumerable<RolesUsuarioTiposOperacion>> SearchAsync(RolesUsuarioTiposOperacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioTiposOperacion>(
            SP.SCHEMASeguridad.RolesUsuarioTiposOperacion.Search,
            new { COD_ROL_USUARIO = dto.COD_ROL_USUARIO, COD_CLASE_OPERACION = dto.COD_CLASE_OPERACION, SEARCH = dto.Search ?? string.Empty },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RolesUsuarioTiposOperacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_USUARIO", dto.COD_ROL_USUARIO);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMASeguridad.RolesUsuarioTiposOperacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuarioTiposOperacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuarioTiposOperacion.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMASeguridad.RolesUsuarioTiposOperacion.Delete,
            new { COD_ROL_USUARIO, COD_CLASE_OPERACION, COD_TIPO_OPERACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuarioTiposOperacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuarioTiposOperacion.Delete, new { COD_ROL_USUARIO, COD_CLASE_OPERACION, COD_TIPO_OPERACION })
        };
    }
}
}
