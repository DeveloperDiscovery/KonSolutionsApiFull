using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Sin colisión de nombres de columna en estos SPs (ver comentario en el Domain
/// Model) — Dapper automático (QueryAsync&lt;T&gt;) alcanza, no hace falta mapeo manual por
/// ordinal como en el módulo padre InventariosAlmacenesPuntoTomaRepository.</summary>
public class InventariosAlmacenesPuntoTomaConteoRepository : IInventariosAlmacenesPuntoTomaConteoRepository
{
    private readonly IDbConnectionFactory _db;
    public InventariosAlmacenesPuntoTomaConteoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<InventariosAlmacenesPuntoTomaConteo>> ShowallAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenesPuntoTomaConteo>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaConteo.Showall,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<InventariosAlmacenesPuntoTomaConteo?> ShowbyidAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, int numSecuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<InventariosAlmacenesPuntoTomaConteo>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaConteo.Showbyid,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario, NUM_SECUENCIA = numSecuencia },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Pese al nombre "SHOWCOMBOX", este SP devuelve todas las columnas de la tabla
    /// (igual que SHOWALL), no Id/Texto.</summary>
    public async Task<IEnumerable<InventariosAlmacenesPuntoTomaConteo>> ShowcomboxAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenesPuntoTomaConteo>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaConteo.Showcombox,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<InventariosAlmacenesPuntoTomaConteo>> SearchAsync(InventariosAlmacenesPuntoTomaConteoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenesPuntoTomaConteo>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaConteo.Search,
            new { NUM_INVENTARIO = dto.NUM_INVENTARIO, COD_ALMACEN = dto.COD_ALMACEN, NUM_PUNTO_TOMA_INVENTARIO = dto.NUM_PUNTO_TOMA_INVENTARIO, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Alta simple: NUM_SECUENCIA se envía tal cual llegue (normalmente null) — el SP
    /// lo recalcula internamente (MAX+1), nunca choca la clave.</summary>
    public async Task<WriteResult> InsertAsync(InventariosAlmacenesPuntoTomaConteoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("NUM_PUNTO_TOMA_INVENTARIO", dto.NUM_PUNTO_TOMA_INVENTARIO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_UBICACION_ALMACEN", dto.COD_UBICACION_ALMACEN);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("COD_STICKER_RFID", dto.COD_STICKER_RFID);
        p.Add("COD_STICKER_UBICACION", dto.COD_STICKER_UBICACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaConteo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaConteo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaConteo.Insert, p)
        };
    }
}

    /// <summary>PROC_..._READRFIDCODEBAR: lee un sticker de ubicación + uno RFID/código de
    /// barras y resuelve Producto/Lote/Proceso/Orden/Almacén-Ubicación/UM — sin escritura en
    /// BD, solo lectura para autocompletar el formulario de alta rápida.</summary>
    public async Task<InventariosAlmacenesPuntoTomaReadRfidCodebarResult?> ReadRfidCodebarAsync(string? codStickerUbicacion, string? codStickerRfid)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<InventariosAlmacenesPuntoTomaReadRfidCodebarResult>(
            SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaConteo.Readrfidcodebar,
            new { COD_STICKER_UBICACION = codStickerUbicacion, COD_STICKER_RFID = codStickerRfid },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Modificación. La clave (incluido NUM_SECUENCIA, ya asignado) no se toca.</summary>
    public async Task<WriteResult> UpdateAsync(InventariosAlmacenesPuntoTomaConteoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("NUM_PUNTO_TOMA_INVENTARIO", dto.NUM_PUNTO_TOMA_INVENTARIO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_UBICACION_ALMACEN", dto.COD_UBICACION_ALMACEN);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaConteo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaConteo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaConteo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, int numSecuencia)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaConteo.Delete,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario, NUM_SECUENCIA = numSecuencia },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaConteo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaConteo.Delete, new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario, NUM_SECUENCIA = numSecuencia })
        };
    }
}
}
