using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CostosProductosRepository : ICostosProductosRepository
{
    private readonly IDbConnectionFactory _db;
    public CostosProductosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CostosProductos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CostosProductos>(SP.SCHEMACostos.CostosProductos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CostosProductos?> ShowbyidAsync(string COD_ALMACEN, string COD_PROCESO, int NUM_ORDEN_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CostosProductos>(SP.SCHEMACostos.CostosProductos.Showbyid, new { COD_ALMACEN = COD_ALMACEN, COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMACostos.CostosProductos.Showcombox);
    }

    public async Task<IEnumerable<CostosProductos>> SearchAsync(CostosProductosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CostosProductos>(SP.SCHEMACostos.CostosProductos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CostosProductosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", dto.COD_UNIDAD_MEDIDA_SECUNDARIA);
        p.Add("CAN_PRODUCTO_SECUNDARIA", dto.CAN_PRODUCTO_SECUNDARIA);
        p.Add("VAL_UNITARIO_COSTO", dto.VAL_UNITARIO_COSTO);
        p.Add("VAL_TOTAL_COSTO", dto.VAL_TOTAL_COSTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosProductos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosProductos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosProductos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CostosProductosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", dto.COD_UNIDAD_MEDIDA_SECUNDARIA);
        p.Add("CAN_PRODUCTO_SECUNDARIA", dto.CAN_PRODUCTO_SECUNDARIA);
        p.Add("VAL_UNITARIO_COSTO", dto.VAL_UNITARIO_COSTO);
        p.Add("VAL_TOTAL_COSTO", dto.VAL_TOTAL_COSTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosProductos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosProductos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosProductos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ALMACEN, string COD_PROCESO, int NUM_ORDEN_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosProductos.Delete, new { COD_ALMACEN = COD_ALMACEN, COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosProductos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosProductos.Delete, new { COD_ALMACEN = COD_ALMACEN, COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION })
        };
    }
}

}