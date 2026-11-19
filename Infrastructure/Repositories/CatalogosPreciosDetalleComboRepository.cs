using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CatalogosPreciosDetalleComboRepository : ICatalogosPreciosDetalleComboRepository
{
    private readonly IDbConnectionFactory _db;
    public CatalogosPreciosDetalleComboRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CatalogosPreciosDetalleCombo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CatalogosPreciosDetalleCombo>(SP.SCHEMAComercial.CatalogosPreciosDetalleCombo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CatalogosPreciosDetalleCombo?> ShowbyidAsync(int NUM_CATALOGO_PRECIOS, string COD_PRODUCTO, string COD_PRODUCTO_COMBO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CatalogosPreciosDetalleCombo>(SP.SCHEMAComercial.CatalogosPreciosDetalleCombo.Showbyid, new { NUM_CATALOGO_PRECIOS = NUM_CATALOGO_PRECIOS, COD_PRODUCTO = COD_PRODUCTO, COD_PRODUCTO_COMBO = COD_PRODUCTO_COMBO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.CatalogosPreciosDetalleCombo.Showcombox);
    }

    public async Task<IEnumerable<CatalogosPreciosDetalleCombo>> SearchAsync(CatalogosPreciosDetalleComboSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CatalogosPreciosDetalleCombo>(SP.SCHEMAComercial.CatalogosPreciosDetalleCombo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CatalogosPreciosDetalleComboInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CATALOGO_PRECIOS", dto.NUM_CATALOGO_PRECIOS);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_PRODUCTO_COMBO", dto.COD_PRODUCTO_COMBO);
        p.Add("COD_SKU_INTERMEDIARIO", dto.COD_SKU_INTERMEDIARIO);
        p.Add("DES_SKU_INTERMEDIARIO", dto.DES_SKU_INTERMEDIARIO);
        p.Add("PRE_UNITARIO", dto.PRE_UNITARIO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CatalogosPreciosDetalleCombo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CatalogosPreciosDetalleCombo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CatalogosPreciosDetalleCombo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CatalogosPreciosDetalleComboUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CATALOGO_PRECIOS", dto.NUM_CATALOGO_PRECIOS);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_PRODUCTO_COMBO", dto.COD_PRODUCTO_COMBO);
        p.Add("COD_SKU_INTERMEDIARIO", dto.COD_SKU_INTERMEDIARIO);
        p.Add("DES_SKU_INTERMEDIARIO", dto.DES_SKU_INTERMEDIARIO);
        p.Add("PRE_UNITARIO", dto.PRE_UNITARIO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CatalogosPreciosDetalleCombo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CatalogosPreciosDetalleCombo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CatalogosPreciosDetalleCombo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_CATALOGO_PRECIOS, string COD_PRODUCTO, string COD_PRODUCTO_COMBO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CatalogosPreciosDetalleCombo.Delete, new { NUM_CATALOGO_PRECIOS = NUM_CATALOGO_PRECIOS, COD_PRODUCTO = COD_PRODUCTO, COD_PRODUCTO_COMBO = COD_PRODUCTO_COMBO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CatalogosPreciosDetalleCombo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CatalogosPreciosDetalleCombo.Delete, new { NUM_CATALOGO_PRECIOS = NUM_CATALOGO_PRECIOS, COD_PRODUCTO = COD_PRODUCTO, COD_PRODUCTO_COMBO = COD_PRODUCTO_COMBO })
        };
    }
}

}