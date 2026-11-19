using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ComponentesProductoRepository : IComponentesProductoRepository
{
    private readonly IDbConnectionFactory _db;
    public ComponentesProductoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ComponentesProducto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComponentesProducto>(SP.SCHEMAIngenieria.ComponentesProducto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ComponentesProducto?> ShowbyidAsync(string COD_COMPONENTE_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ComponentesProducto>(SP.SCHEMAIngenieria.ComponentesProducto.Showbyid, new { COD_COMPONENTE_PRODUCTO = COD_COMPONENTE_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.ComponentesProducto.Showcombox);
    }

    public async Task<IEnumerable<ComponentesProducto>> SearchAsync(ComponentesProductoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComponentesProducto>(SP.SCHEMAIngenieria.ComponentesProducto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ComponentesProductoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_COMPONENTE_PRODUCTO", dto.COD_COMPONENTE_PRODUCTO);
        p.Add("DES_COMPONENTE_PRODUCTO", dto.DES_COMPONENTE_PRODUCTO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.ComponentesProducto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.ComponentesProducto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.ComponentesProducto.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ComponentesProductoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_COMPONENTE_PRODUCTO", dto.COD_COMPONENTE_PRODUCTO);
        p.Add("DES_COMPONENTE_PRODUCTO", dto.DES_COMPONENTE_PRODUCTO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.ComponentesProducto.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.ComponentesProducto.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.ComponentesProducto.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_COMPONENTE_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.ComponentesProducto.Delete, new { COD_COMPONENTE_PRODUCTO = COD_COMPONENTE_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.ComponentesProducto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.ComponentesProducto.Delete, new { COD_COMPONENTE_PRODUCTO = COD_COMPONENTE_PRODUCTO })
        };
    }
}

}