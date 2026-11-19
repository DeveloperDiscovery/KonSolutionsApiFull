using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class UsuariosTiposOperacionRepository : IUsuariosTiposOperacionRepository
{
    private readonly IDbConnectionFactory _db;
    public UsuariosTiposOperacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<UsuariosTiposOperacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosTiposOperacion>(SP.SCHEMASeguridad.UsuariosTiposOperacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<UsuariosTiposOperacion?> ShowbyidAsync(string COD_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<UsuariosTiposOperacion>(SP.SCHEMASeguridad.UsuariosTiposOperacion.Showbyid, new { COD_USUARIO = COD_USUARIO, COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.UsuariosTiposOperacion.Showcombox);
    }

    public async Task<IEnumerable<UsuariosTiposOperacion>> SearchAsync(UsuariosTiposOperacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosTiposOperacion>(SP.SCHEMASeguridad.UsuariosTiposOperacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(UsuariosTiposOperacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.UsuariosTiposOperacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.UsuariosTiposOperacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.UsuariosTiposOperacion.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.UsuariosTiposOperacion.Delete, new { COD_USUARIO = COD_USUARIO, COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.UsuariosTiposOperacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.UsuariosTiposOperacion.Delete, new { COD_USUARIO = COD_USUARIO, COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION })
        };
    }
}

}