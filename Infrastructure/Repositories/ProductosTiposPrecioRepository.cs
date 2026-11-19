using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProductosTiposPrecioRepository : IProductosTiposPrecioRepository
{
    private readonly IDbConnectionFactory _db;
    public ProductosTiposPrecioRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProductosTiposPrecio>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProductosTiposPrecio>(SP.SCHEMAComercial.ProductosTiposPrecio.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProductosTiposPrecio?> ShowbyidAsync(string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProductosTiposPrecio>(SP.SCHEMAComercial.ProductosTiposPrecio.Showbyid, new { COD_PRODUCTO = COD_PRODUCTO, COD_TIPO_PRECIO = COD_TIPO_PRECIO, COD_MONEDA = COD_MONEDA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.ProductosTiposPrecio.Showcombox);
    }

    public async Task<IEnumerable<ProductosTiposPrecio>> SearchAsync(ProductosTiposPrecioSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProductosTiposPrecio>(SP.SCHEMAComercial.ProductosTiposPrecio.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProductosTiposPrecioInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_TIPO_PRECIO", dto.COD_TIPO_PRECIO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_PRECIO", dto.VAL_PRECIO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.ProductosTiposPrecio.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.ProductosTiposPrecio.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.ProductosTiposPrecio.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProductosTiposPrecioUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_TIPO_PRECIO", dto.COD_TIPO_PRECIO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_PRECIO", dto.VAL_PRECIO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.ProductosTiposPrecio.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.ProductosTiposPrecio.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.ProductosTiposPrecio.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.ProductosTiposPrecio.Delete, new { COD_PRODUCTO = COD_PRODUCTO, COD_TIPO_PRECIO = COD_TIPO_PRECIO, COD_MONEDA = COD_MONEDA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.ProductosTiposPrecio.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.ProductosTiposPrecio.Delete, new { COD_PRODUCTO = COD_PRODUCTO, COD_TIPO_PRECIO = COD_TIPO_PRECIO, COD_MONEDA = COD_MONEDA })
        };
    }
}

}