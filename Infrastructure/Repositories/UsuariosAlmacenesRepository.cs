using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class UsuariosAlmacenesRepository : IUsuariosAlmacenesRepository
{
    private readonly IDbConnectionFactory _db;
    public UsuariosAlmacenesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<UsuariosAlmacenes>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosAlmacenes>(SP.SCHEMASeguridad.UsuariosAlmacenes.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<UsuariosAlmacenes?> ShowbyidAsync(string COD_USUARIO, string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<UsuariosAlmacenes>(SP.SCHEMASeguridad.UsuariosAlmacenes.Showbyid, new { COD_USUARIO = COD_USUARIO, COD_ALMACEN = COD_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.UsuariosAlmacenes.Showcombox);
    }

    public async Task<IEnumerable<UsuariosAlmacenes>> SearchAsync(UsuariosAlmacenesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosAlmacenes>(SP.SCHEMASeguridad.UsuariosAlmacenes.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(UsuariosAlmacenesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.UsuariosAlmacenes.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.UsuariosAlmacenes.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.UsuariosAlmacenes.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_USUARIO, string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.UsuariosAlmacenes.Delete, new { COD_USUARIO = COD_USUARIO, COD_ALMACEN = COD_ALMACEN }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.UsuariosAlmacenes.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.UsuariosAlmacenes.Delete, new { COD_USUARIO = COD_USUARIO, COD_ALMACEN = COD_ALMACEN })
        };
    }
}

}