using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ReservasStockDetalleRepository : IReservasStockDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public ReservasStockDetalleRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>El SHOWALL de esta tabla pide el documento: el detalle siempre se lee
    /// dentro de su cabecera, nunca entero.</summary>
    public async Task<IEnumerable<ReservasStockDetalle>> ShowallAsync(int NUM_RESERVA_STOCK)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ReservasStockDetalle>(SP.SCHEMALogistica.ReservasStockDetalle.Showall, new { NUM_RESERVA_STOCK }, commandType: CommandType.StoredProcedure);
    }

    public async Task<ReservasStockDetalle?> ShowbyidAsync(int NUM_RESERVA_STOCK, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ReservasStockDetalle>(SP.SCHEMALogistica.ReservasStockDetalle.Showbyid, new { NUM_RESERVA_STOCK = NUM_RESERVA_STOCK, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.ReservasStockDetalle.Showcombox);
    }

    public async Task<IEnumerable<ReservasStockDetalle>> SearchAsync(ReservasStockDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ReservasStockDetalle>(SP.SCHEMALogistica.ReservasStockDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>"" y NULL no son lo mismo para una clave foránea: NULL nunca la viola, ""
    /// jamás existe en el catálogo referenciado. Ver el mismo resguardo en
    /// RequerimientosStockDetalleRepository — acá aplica igual, mismas columnas opcionales
    /// contra los mismos catálogos (LOTES_CONTROL, UNIDADES_MEDIDA ×2).</summary>
    private static string? Nulo(string? v) => string.IsNullOrWhiteSpace(v) ? null : v;

    public async Task<WriteResult> InsertAsync(ReservasStockDetalleInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_RESERVA_STOCK", dto.NUM_RESERVA_STOCK);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_UBICACION_ALMACEN", dto.COD_UBICACION_ALMACEN);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_PROCESO", Nulo(dto.COD_PROCESO));
        p.Add("NUM_LOTE_CONTROL", Nulo(dto.NUM_LOTE_CONTROL));
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("COD_UNIDAD_MEDIDA", Nulo(dto.COD_UNIDAD_MEDIDA));
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", Nulo(dto.COD_UNIDAD_MEDIDA_SECUNDARIA));
        p.Add("CAN_PRODUCTO_SECUNDARIA", dto.CAN_PRODUCTO_SECUNDARIA);
        p.Add("CAN_PRODUCTO_ATENDIDA", dto.CAN_PRODUCTO_ATENDIDA);
        p.Add("CAN_PRODUCTO_ATENDIDA_SECUNDARIA", dto.CAN_PRODUCTO_ATENDIDA_SECUNDARIA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ReservasStockDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ReservasStockDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ReservasStockDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ReservasStockDetalleUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_RESERVA_STOCK", dto.NUM_RESERVA_STOCK);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_UBICACION_ALMACEN", dto.COD_UBICACION_ALMACEN);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_PROCESO", Nulo(dto.COD_PROCESO));
        p.Add("NUM_LOTE_CONTROL", Nulo(dto.NUM_LOTE_CONTROL));
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("COD_UNIDAD_MEDIDA", Nulo(dto.COD_UNIDAD_MEDIDA));
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", Nulo(dto.COD_UNIDAD_MEDIDA_SECUNDARIA));
        p.Add("CAN_PRODUCTO_SECUNDARIA", dto.CAN_PRODUCTO_SECUNDARIA);
        p.Add("CAN_PRODUCTO_ATENDIDA", dto.CAN_PRODUCTO_ATENDIDA);
        p.Add("CAN_PRODUCTO_ATENDIDA_SECUNDARIA", dto.CAN_PRODUCTO_ATENDIDA_SECUNDARIA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ReservasStockDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ReservasStockDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ReservasStockDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_RESERVA_STOCK, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ReservasStockDetalle.Delete, new { NUM_RESERVA_STOCK = NUM_RESERVA_STOCK, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ReservasStockDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ReservasStockDetalle.Delete, new { NUM_RESERVA_STOCK = NUM_RESERVA_STOCK, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}