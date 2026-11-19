using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CatalogosPreciosDetalleRepository : ICatalogosPreciosDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public CatalogosPreciosDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CatalogosPreciosDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CatalogosPreciosDetalle>(SP.SCHEMAComercial.CatalogosPreciosDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CatalogosPreciosDetalle?> ShowbyidAsync(int NUM_CATALOGO_PRECIOS, string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CatalogosPreciosDetalle>(SP.SCHEMAComercial.CatalogosPreciosDetalle.Showbyid, new { NUM_CATALOGO_PRECIOS = NUM_CATALOGO_PRECIOS, COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.CatalogosPreciosDetalle.Showcombox, "NUM_CATALOGO_PRECIOS", "DES_SKU_INTERMEDIARIO");
    }

    public async Task<IEnumerable<CatalogosPreciosDetalle>> SearchAsync(CatalogosPreciosDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CatalogosPreciosDetalle>(SP.SCHEMAComercial.CatalogosPreciosDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CatalogosPreciosDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CATALOGO_PRECIOS", dto.NUM_CATALOGO_PRECIOS);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_SKU_INTERMEDIARIO", dto.COD_SKU_INTERMEDIARIO);
        p.Add("DES_SKU_INTERMEDIARIO", dto.DES_SKU_INTERMEDIARIO);
        p.Add("PRE_UNITARIO", dto.PRE_UNITARIO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CatalogosPreciosDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CatalogosPreciosDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CatalogosPreciosDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CatalogosPreciosDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CATALOGO_PRECIOS", dto.NUM_CATALOGO_PRECIOS);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_SKU_INTERMEDIARIO", dto.COD_SKU_INTERMEDIARIO);
        p.Add("DES_SKU_INTERMEDIARIO", dto.DES_SKU_INTERMEDIARIO);
        p.Add("PRE_UNITARIO", dto.PRE_UNITARIO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CatalogosPreciosDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CatalogosPreciosDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CatalogosPreciosDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_CATALOGO_PRECIOS, string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CatalogosPreciosDetalle.Delete, new { NUM_CATALOGO_PRECIOS = NUM_CATALOGO_PRECIOS, COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CatalogosPreciosDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CatalogosPreciosDetalle.Delete, new { NUM_CATALOGO_PRECIOS = NUM_CATALOGO_PRECIOS, COD_PRODUCTO = COD_PRODUCTO })
        };
    }
}

}