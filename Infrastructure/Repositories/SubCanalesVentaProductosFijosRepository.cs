using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SubCanalesVentaProductosFijosRepository : ISubCanalesVentaProductosFijosRepository
{
    private readonly IDbConnectionFactory _db;
    public SubCanalesVentaProductosFijosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SubCanalesVentaProductosFijos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SubCanalesVentaProductosFijos>(SP.SCHEMAComercial.SubCanalesVentaProductosFijos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<SubCanalesVentaProductosFijos?> ShowbyidAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SubCanalesVentaProductosFijos>(SP.SCHEMAComercial.SubCanalesVentaProductosFijos.Showbyid, new { NUM_UNIDAD_NEGOCIO = NUM_UNIDAD_NEGOCIO, NUM_CANAL_VENTA = NUM_CANAL_VENTA, NUM_SUB_CANAL_VENTA = NUM_SUB_CANAL_VENTA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.SubCanalesVentaProductosFijos.Showcombox);
    }

    public async Task<IEnumerable<SubCanalesVentaProductosFijos>> SearchAsync(SubCanalesVentaProductosFijosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SubCanalesVentaProductosFijos>(SP.SCHEMAComercial.SubCanalesVentaProductosFijos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SubCanalesVentaProductosFijosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_UNIDAD_NEGOCIO", dto.NUM_UNIDAD_NEGOCIO);
        p.Add("NUM_CANAL_VENTA", dto.NUM_CANAL_VENTA);
        p.Add("NUM_SUB_CANAL_VENTA", dto.NUM_SUB_CANAL_VENTA);
        p.Add("COD_PRODUCTO_FIJO", dto.COD_PRODUCTO_FIJO);
        p.Add("DES_PRODUCTO_ALIAS", dto.DES_PRODUCTO_ALIAS);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.SubCanalesVentaProductosFijos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.SubCanalesVentaProductosFijos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.SubCanalesVentaProductosFijos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SubCanalesVentaProductosFijosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_UNIDAD_NEGOCIO", dto.NUM_UNIDAD_NEGOCIO);
        p.Add("NUM_CANAL_VENTA", dto.NUM_CANAL_VENTA);
        p.Add("NUM_SUB_CANAL_VENTA", dto.NUM_SUB_CANAL_VENTA);
        p.Add("COD_PRODUCTO_FIJO", dto.COD_PRODUCTO_FIJO);
        p.Add("DES_PRODUCTO_ALIAS", dto.DES_PRODUCTO_ALIAS);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.SubCanalesVentaProductosFijos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.SubCanalesVentaProductosFijos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.SubCanalesVentaProductosFijos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.SubCanalesVentaProductosFijos.Delete, new { NUM_UNIDAD_NEGOCIO = NUM_UNIDAD_NEGOCIO, NUM_CANAL_VENTA = NUM_CANAL_VENTA, NUM_SUB_CANAL_VENTA = NUM_SUB_CANAL_VENTA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.SubCanalesVentaProductosFijos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.SubCanalesVentaProductosFijos.Delete, new { NUM_UNIDAD_NEGOCIO = NUM_UNIDAD_NEGOCIO, NUM_CANAL_VENTA = NUM_CANAL_VENTA, NUM_SUB_CANAL_VENTA = NUM_SUB_CANAL_VENTA })
        };
    }
}

}