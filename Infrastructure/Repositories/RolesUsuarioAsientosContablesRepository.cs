using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RolesUsuarioAsientosContablesRepository : IRolesUsuarioAsientosContablesRepository
{
    private readonly IDbConnectionFactory _db;
    public RolesUsuarioAsientosContablesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RolesUsuarioAsientosContables>> ShowallAsync(string COD_ROL_USUARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioAsientosContables>(
            SP.SCHEMASeguridad.RolesUsuarioAsientosContables.Showall,
            new { COD_ROL_USUARIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RolesUsuarioAsientosContables>> DisponiblesAsync(string COD_ROL_USUARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioAsientosContables>(
            SP.SCHEMASeguridad.RolesUsuarioAsientosContables.Disponibles,
            new { COD_ROL_USUARIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<RolesUsuarioAsientosContables?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_ASIENTO_CONTABLE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RolesUsuarioAsientosContables>(
            SP.SCHEMASeguridad.RolesUsuarioAsientosContables.Showbyid,
            new { COD_ROL_USUARIO, COD_ASIENTO_CONTABLE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_ROL_USUARIO)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn,
            SP.SCHEMASeguridad.RolesUsuarioAsientosContables.Showcombox, new { COD_ROL_USUARIO });
    }

    public async Task<IEnumerable<RolesUsuarioAsientosContables>> SearchAsync(RolesUsuarioAsientosContablesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioAsientosContables>(
            SP.SCHEMASeguridad.RolesUsuarioAsientosContables.Search,
            new { COD_ROL_USUARIO = dto.COD_ROL_USUARIO, SEARCH = dto.Search ?? string.Empty },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RolesUsuarioAsientosContablesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_USUARIO", dto.COD_ROL_USUARIO);
        p.Add("COD_ASIENTO_CONTABLE", dto.COD_ASIENTO_CONTABLE);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMASeguridad.RolesUsuarioAsientosContables.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuarioAsientosContables.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuarioAsientosContables.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_ASIENTO_CONTABLE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMASeguridad.RolesUsuarioAsientosContables.Delete,
            new { COD_ROL_USUARIO, COD_ASIENTO_CONTABLE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuarioAsientosContables.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuarioAsientosContables.Delete, new { COD_ROL_USUARIO, COD_ASIENTO_CONTABLE })
        };
    }
}
}
