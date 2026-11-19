using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CatalogosPreciosRepository : ICatalogosPreciosRepository
{
    private readonly IDbConnectionFactory _db;
    public CatalogosPreciosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CatalogosPrecios>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CatalogosPrecios>(SP.SCHEMAComercial.CatalogosPrecios.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CatalogosPrecios?> ShowbyidAsync(int NUM_CATALOGO_PRECIOS)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CatalogosPrecios>(SP.SCHEMAComercial.CatalogosPrecios.Showbyid, new { NUM_CATALOGO_PRECIOS = NUM_CATALOGO_PRECIOS }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.CatalogosPrecios.Showcombox, "NUM_CATALOGO_PRECIOS", "DES_CATALOGO_PRECIOS");
    }

    public async Task<IEnumerable<CatalogosPrecios>> SearchAsync(CatalogosPreciosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CatalogosPrecios>(SP.SCHEMAComercial.CatalogosPrecios.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CatalogosPreciosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CATALOGO_PRECIOS", dto.NUM_CATALOGO_PRECIOS);
        p.Add("DES_CATALOGO_PRECIOS", dto.DES_CATALOGO_PRECIOS);
        p.Add("NUM_PERIODO_VENTAS", dto.NUM_PERIODO_VENTAS);
        p.Add("COD_TIPO_CATALOGO_PRECIO", dto.COD_TIPO_CATALOGO_PRECIO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("NUM_UNIDAD_COMERCIAL", dto.NUM_UNIDAD_COMERCIAL);
        p.Add("NUM_CANAL_VENTA", dto.NUM_CANAL_VENTA);
        p.Add("NUM_SUB_CANAL_VENTA", dto.NUM_SUB_CANAL_VENTA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CatalogosPrecios.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CatalogosPrecios.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CatalogosPrecios.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CatalogosPreciosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CATALOGO_PRECIOS", dto.NUM_CATALOGO_PRECIOS);
        p.Add("DES_CATALOGO_PRECIOS", dto.DES_CATALOGO_PRECIOS);
        p.Add("NUM_PERIODO_VENTAS", dto.NUM_PERIODO_VENTAS);
        p.Add("COD_TIPO_CATALOGO_PRECIO", dto.COD_TIPO_CATALOGO_PRECIO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("NUM_UNIDAD_COMERCIAL", dto.NUM_UNIDAD_COMERCIAL);
        p.Add("NUM_CANAL_VENTA", dto.NUM_CANAL_VENTA);
        p.Add("NUM_SUB_CANAL_VENTA", dto.NUM_SUB_CANAL_VENTA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CatalogosPrecios.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CatalogosPrecios.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CatalogosPrecios.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_CATALOGO_PRECIOS)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CatalogosPrecios.Delete, new { NUM_CATALOGO_PRECIOS = NUM_CATALOGO_PRECIOS }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CatalogosPrecios.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CatalogosPrecios.Delete, new { NUM_CATALOGO_PRECIOS = NUM_CATALOGO_PRECIOS })
        };
    }
}

}