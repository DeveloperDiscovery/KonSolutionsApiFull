using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RequerimientosStockDetalleRepository : IRequerimientosStockDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public RequerimientosStockDetalleRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>El SHOWALL de esta tabla pide el documento: el detalle siempre se lee
    /// dentro de su cabecera, nunca entero.</summary>
    public async Task<IEnumerable<RequerimientosStockDetalle>> ShowallAsync(int NUM_REQUERIMIENTO_STOCK)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RequerimientosStockDetalle>(SP.SCHEMALogistica.RequerimientosStockDetalle.Showall, new { NUM_REQUERIMIENTO_STOCK }, commandType: CommandType.StoredProcedure);
    }

    public async Task<RequerimientosStockDetalle?> ShowbyidAsync(int NUM_REQUERIMIENTO_STOCK, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RequerimientosStockDetalle>(SP.SCHEMALogistica.RequerimientosStockDetalle.Showbyid, new { NUM_REQUERIMIENTO_STOCK = NUM_REQUERIMIENTO_STOCK, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.RequerimientosStockDetalle.Showcombox);
    }

    public async Task<IEnumerable<RequerimientosStockDetalle>> SearchAsync(RequerimientosStockDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RequerimientosStockDetalle>(SP.SCHEMALogistica.RequerimientosStockDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>"" y NULL no son lo mismo para una clave foránea: NULL nunca la viola,
    /// "" jamás existe en el catálogo referenciado. NUM_LOTE_CONTROL y las dos unidades de
    /// medida son opcionales y viajan a FK (LOTES_CONTROL, UNIDADES_MEDIDA ×2) — cualquier
    /// "" que llegue hasta acá, sea cual sea su origen del lado del cliente, se vuelve NULL
    /// antes de tocar el procedimiento. Es la última posta antes de la base: no depende de
    /// que cada pantalla lo haga bien.</summary>
    private static string? Nulo(string? v) => string.IsNullOrWhiteSpace(v) ? null : v;

    public async Task<WriteResult> InsertAsync(RequerimientosStockDetalleInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REQUERIMIENTO_STOCK", dto.NUM_REQUERIMIENTO_STOCK);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        // Columnas nuevas del detalle. En el SP van con default NULL, pero se mandan igual:
        // si no viajaran, una modificación las dejaría en NULL sin que nadie lo pidiera.
        p.Add("COD_PROCESO", Nulo(dto.COD_PROCESO));
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("NUM_LOTE_CONTROL", Nulo(dto.NUM_LOTE_CONTROL));
        p.Add("COD_UNIDAD_MEDIDA", Nulo(dto.COD_UNIDAD_MEDIDA));
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", Nulo(dto.COD_UNIDAD_MEDIDA_SECUNDARIA));
        p.Add("CAN_PRODUCTO_SECUNDARIA", dto.CAN_PRODUCTO_SECUNDARIA);
        p.Add("CAN_PRODUCTO_ATENDIDA", dto.CAN_PRODUCTO_ATENDIDA);
        p.Add("CAN_PRODUCTO_ATENDIDA_SECUNDARIA", dto.CAN_PRODUCTO_ATENDIDA_SECUNDARIA);
        p.Add("CAN_PRODUCTO_RESERVADA", dto.CAN_PRODUCTO_RESERVADA);
        p.Add("CAN_PRODUCTO_RESERVADA_SECUNDARIA", dto.CAN_PRODUCTO_RESERVADA_SECUNDARIA);
        p.Add("CAN_PRODUCTO_REQUERIMIENTO_COMPRA", dto.CAN_PRODUCTO_REQUERIMIENTO_COMPRA);
        p.Add("CAN_PRODUCTO_REQUERIMIENTO_COMPRA_SECUNDARIA", dto.CAN_PRODUCTO_REQUERIMIENTO_COMPRA_SECUNDARIA);
        // Alta: siempre NULL. No es un dato que se calcule ni se elija al crear el ítem —
        // solo se completa al modificar, y ahí sale del propio registro (ver UpdateAsync).
        p.Add("NUM_SECUENCIA_ORIGINAL", (short?)null);
        p.Add("COD_ALMACEN", Nulo(dto.COD_ALMACEN));
        p.Add("COD_UBICACION_ALMACEN", Nulo(dto.COD_UBICACION_ALMACEN));
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.RequerimientosStockDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.RequerimientosStockDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.RequerimientosStockDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(RequerimientosStockDetalleUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REQUERIMIENTO_STOCK", dto.NUM_REQUERIMIENTO_STOCK);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        // Columnas nuevas del detalle. En el SP van con default NULL, pero se mandan igual:
        // si no viajaran, una modificación las dejaría en NULL sin que nadie lo pidiera.
        p.Add("COD_PROCESO", Nulo(dto.COD_PROCESO));
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("NUM_LOTE_CONTROL", Nulo(dto.NUM_LOTE_CONTROL));
        p.Add("COD_UNIDAD_MEDIDA", Nulo(dto.COD_UNIDAD_MEDIDA));
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", Nulo(dto.COD_UNIDAD_MEDIDA_SECUNDARIA));
        p.Add("CAN_PRODUCTO_SECUNDARIA", dto.CAN_PRODUCTO_SECUNDARIA);
        p.Add("CAN_PRODUCTO_ATENDIDA", dto.CAN_PRODUCTO_ATENDIDA);
        p.Add("CAN_PRODUCTO_ATENDIDA_SECUNDARIA", dto.CAN_PRODUCTO_ATENDIDA_SECUNDARIA);
        p.Add("CAN_PRODUCTO_RESERVADA", dto.CAN_PRODUCTO_RESERVADA);
        p.Add("CAN_PRODUCTO_RESERVADA_SECUNDARIA", dto.CAN_PRODUCTO_RESERVADA_SECUNDARIA);
        p.Add("CAN_PRODUCTO_REQUERIMIENTO_COMPRA", dto.CAN_PRODUCTO_REQUERIMIENTO_COMPRA);
        p.Add("CAN_PRODUCTO_REQUERIMIENTO_COMPRA_SECUNDARIA", dto.CAN_PRODUCTO_REQUERIMIENTO_COMPRA_SECUNDARIA);
        // Modificación: el valor que ya trae el dto (el que cargó el SHOWALL al abrir el
        // documento) — la pantalla no lo edita, solo lo lleva de ida y vuelta.
        p.Add("NUM_SECUENCIA_ORIGINAL", dto.NUM_SECUENCIA_ORIGINAL);
        p.Add("COD_ALMACEN", Nulo(dto.COD_ALMACEN));
        p.Add("COD_UBICACION_ALMACEN", Nulo(dto.COD_UBICACION_ALMACEN));
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.RequerimientosStockDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.RequerimientosStockDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.RequerimientosStockDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_REQUERIMIENTO_STOCK, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.RequerimientosStockDetalle.Delete, new { NUM_REQUERIMIENTO_STOCK = NUM_REQUERIMIENTO_STOCK, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.RequerimientosStockDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.RequerimientosStockDetalle.Delete, new { NUM_REQUERIMIENTO_STOCK = NUM_REQUERIMIENTO_STOCK, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}