using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class UsuariosAsientosContablesRepository : IUsuariosAsientosContablesRepository
{
    private readonly IDbConnectionFactory _db;
    public UsuariosAsientosContablesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<UsuariosAsientosContables>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosAsientosContables>(SP.SCHEMASeguridad.UsuariosAsientosContables.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<UsuariosAsientosContables?> ShowbyidAsync(string COD_USUARIO, string COD_ASIENTO_CONTABLE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<UsuariosAsientosContables>(SP.SCHEMASeguridad.UsuariosAsientosContables.Showbyid, new { COD_USUARIO = COD_USUARIO, COD_ASIENTO_CONTABLE = COD_ASIENTO_CONTABLE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.UsuariosAsientosContables.Showcombox);
    }

    public async Task<IEnumerable<UsuariosAsientosContables>> SearchAsync(UsuariosAsientosContablesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosAsientosContables>(SP.SCHEMASeguridad.UsuariosAsientosContables.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(UsuariosAsientosContablesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("COD_ASIENTO_CONTABLE", dto.COD_ASIENTO_CONTABLE);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.UsuariosAsientosContables.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.UsuariosAsientosContables.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.UsuariosAsientosContables.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_USUARIO, string COD_ASIENTO_CONTABLE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.UsuariosAsientosContables.Delete, new { COD_USUARIO = COD_USUARIO, COD_ASIENTO_CONTABLE = COD_ASIENTO_CONTABLE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.UsuariosAsientosContables.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.UsuariosAsientosContables.Delete, new { COD_USUARIO = COD_USUARIO, COD_ASIENTO_CONTABLE = COD_ASIENTO_CONTABLE })
        };
    }
}

}