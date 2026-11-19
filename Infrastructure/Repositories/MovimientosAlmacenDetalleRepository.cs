using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MovimientosAlmacenDetalleRepository : IMovimientosAlmacenDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public MovimientosAlmacenDetalleRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>Las lineas de UN movimiento. El SP recibe @NUM_MOVIMIENTO_ALMACEN y ese
    /// parametro NO tiene valor por defecto, asi que llamarlo sin nada —como se hacia hasta
    /// ahora— devuelve "expects parameter '@NUM_MOVIMIENTO_ALMACEN', which was not supplied"
    /// y el endpoint del detalle no funcionaba.
    ///
    /// De paso esto es el "por cabecera" que otras pantallas resuelven con un endpoint
    /// aparte: aca el SHOWALL ya viene filtrado de origen.</summary>
    public async Task<IEnumerable<MovimientosAlmacenDetalle>> ShowallAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenDetalle>(
            SP.SCHEMALogistica.MovimientosAlmacenDetalle.Showall,
            new { NUM_MOVIMIENTO_ALMACEN },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<MovimientosAlmacenDetalle?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MovimientosAlmacenDetalle>(SP.SCHEMALogistica.MovimientosAlmacenDetalle.Showbyid, new { NUM_MOVIMIENTO_ALMACEN = NUM_MOVIMIENTO_ALMACEN, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.MovimientosAlmacenDetalle.Showcombox, "NUM_MOVIMIENTO_ALMACEN", "");
    }

    public async Task<IEnumerable<MovimientosAlmacenDetalle>> SearchAsync(MovimientosAlmacenDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenDetalle>(SP.SCHEMALogistica.MovimientosAlmacenDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MovimientosAlmacenDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = ParametrosLinea(dto, codUsuario, codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(MovimientosAlmacenDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = ParametrosLinea(dto, codUsuario, codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenDetalle.Update, p)
        };
    }
}

    /// <summary>Los parámetros de los SP _INSERT y _UPDATE del detalle, TODOS: los dos SP los
    /// declaran sin valor por defecto. Antes faltaban el lote, el proceso, la orden de producción y
    /// las secuencias de los enlaces, y el SP respondía "expects parameter '@NUM_LOTE_CONTROL',
    /// which was not supplied". Los textos vacíos van como NULL: en el SP, NULL es "usa el valor
    /// por defecto" (o "déjalo como estaba" al modificar), y una cadena vacía se grabaría tal cual.</summary>
    private static DynamicParameters ParametrosLinea(MovimientosAlmacenDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        static string? T(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO", T(dto.COD_PRODUCTO));
        p.Add("COD_CLASE_MOVIMIENTO", T(dto.COD_CLASE_MOVIMIENTO));
        p.Add("COD_TIPO_MOVIMIENTO", T(dto.COD_TIPO_MOVIMIENTO));
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", T(dto.COD_UNIDAD_MEDIDA));
        p.Add("CAN_PRODUCTO_SECUNDARIO", dto.CAN_PRODUCTO_SECUNDARIO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIO", T(dto.COD_UNIDAD_MEDIDA_SECUNDARIO));
        p.Add("COD_ALMACEN", T(dto.COD_ALMACEN));
        p.Add("COD_UBICACION_ALMACEN", T(dto.COD_UBICACION_ALMACEN));
        p.Add("NUM_LOTE_CONTROL", T(dto.NUM_LOTE_CONTROL));
        p.Add("COD_PROCESO", T(dto.COD_PROCESO));
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("NUM_MOVIMIENTO_ALMACEN_SINCRONIZADO", dto.NUM_MOVIMIENTO_ALMACEN_SINCRONIZADO);
        p.Add("NUM_SECUENCIA_ALMACEN_SINCRONIZADO", dto.NUM_SECUENCIA_ALMACEN_SINCRONIZADO);
        p.Add("COD_ALMACEN_SINCRONIZADO", T(dto.COD_ALMACEN_SINCRONIZADO));
        p.Add("COD_UBICACION_ALMACEN_SINCRONIZADO", T(dto.COD_UBICACION_ALMACEN_SINCRONIZADO));
        p.Add("NUM_REQUERIMIENTO_STOCK_ATENDIDO", dto.NUM_REQUERIMIENTO_STOCK_ATENDIDO);
        p.Add("NUM_SECUENCIA_REQUERIMIENTO_STOCK", dto.NUM_SECUENCIA_REQUERIMIENTO_STOCK);
        p.Add("NUM_RESERVA_STOCK_ATENDIDO", dto.NUM_RESERVA_STOCK_ATENDIDO);
        p.Add("NUM_SECUENCIA_RESERVA_STOCK", dto.NUM_SECUENCIA_RESERVA_STOCK);
        p.Add("COD_CLASE_OPERACION", T(dto.COD_CLASE_OPERACION));
        p.Add("COD_TIPO_OPERACION", T(dto.COD_TIPO_OPERACION));
        p.Add("MUM_PEDIDO_COMERCIAL", dto.MUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA_PEDIDO_COMERCIAL", dto.NUM_SECUENCIA_PEDIDO_COMERCIAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return p;
    }

    /// <summary>Stock con disponible en el almacén del movimiento: las líneas de una salida.</summary>
    public async Task<IEnumerable<MovimientosAlmacenDetalleStockDisponible>> StockDisponibleAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenDetalleStockDisponible>(
            SP.SCHEMALogistica.MovimientosAlmacenDetalle.StockDisponible,
            new { NUM_MOVIMIENTO_ALMACEN },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Tres resultsets, siempre en este orden: ubicaciones del almacén, ubicaciones del
    /// almacén destino y productos.</summary>
    public async Task<MovimientosAlmacenDetalleCombosLinea> CombosLineaAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMALogistica.MovimientosAlmacenDetalle.LoadCombos,
            new { NUM_MOVIMIENTO_ALMACEN },
            commandType: CommandType.StoredProcedure);
        return new MovimientosAlmacenDetalleCombosLinea
        {
            Ubicaciones        = (await multi.ReadAsync<ComboboxItem>()).ToList(),
            UbicacionesDestino = (await multi.ReadAsync<ComboboxItem>()).ToList(),
            Productos          = (await multi.ReadAsync<MovimientosAlmacenDetalleProducto>()).ToList()
        };
    }

    // ── Lecturas de etiquetas (RFID / codigo de barras) ───────────────────────────────────────

    /// <summary>Las etiquetas RFID leidas de un movimiento; con NUM_SECUENCIA, solo las de esa linea.
    /// Con texto de busqueda usa el SEARCH (etiqueta, producto, cantidad, usuario...). Lo ultimo leido primero.</summary>
    public async Task<IEnumerable<MovimientosAlmacenEtiqueta>> EtiquetasAsync(decimal NUM_MOVIMIENTO_ALMACEN, int? NUM_SECUENCIA, string? search)
    {
        using var conn = _db.CreateConnection();
        if (string.IsNullOrWhiteSpace(search))
            return await conn.QueryAsync<MovimientosAlmacenEtiqueta>(
                SP.SCHEMALogistica.MovimientosAlmacenDetalleConteoRfid.Showall,
                new { NUM_MOVIMIENTO_ALMACEN, NUM_SECUENCIA },
                commandType: CommandType.StoredProcedure);
        return await conn.QueryAsync<MovimientosAlmacenEtiqueta>(
            SP.SCHEMALogistica.MovimientosAlmacenDetalleConteoRfid.Search,
            new { NUM_MOVIMIENTO_ALMACEN, NUM_SECUENCIA, SEARCH = search.Trim() },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Una lectura completa (PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_LEER). El SP devuelve SIEMPRE dos
    /// tablas: la primera es la linea tocada (la deja DETALLE_INSERT o el propio SP) y la segunda, el
    /// resultado. Si la lectura se rechaza, el SP lanza un error con un numero entre 50700 y 50715 (o el 50580
    /// de la guia ya enviada): no se propaga como excepcion sino como un resultado con ErrorNumber, que es lo
    /// que la pantalla necesita para decidir si pide el peso o pregunta si se fuerza.</summary>
    public async Task<MovimientosAlmacenLecturaResultado> LeerAsync(MovimientosAlmacenLecturaDto dto, string codUsuario, string codEstacion)
    {
        static string? T(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("COD_STICKER_RFID", T(dto.COD_STICKER_RFID));
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("COD_STICKER_UBICACION", T(dto.COD_STICKER_UBICACION));
        p.Add("COD_UBICACION_ALMACEN", T(dto.COD_UBICACION_ALMACEN));
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UBICACION_ALMACEN_SINCRONIZADO", T(dto.COD_UBICACION_ALMACEN_SINCRONIZADO));
        p.Add("FLG_FORZAR", dto.FLG_FORZAR);
        p.Add("NUM_SECUENCIA_LINEA", dto.NUM_SECUENCIA_LINEA);
        try
        {
            using var multi = await conn.QueryMultipleAsync(SP.SCHEMALogistica.MovimientosAlmacen.Leer, p, commandType: CommandType.StoredProcedure);
            await multi.ReadAsync();                                   // la linea tocada: la segunda tabla trae su numero
            var r = await multi.ReadFirstOrDefaultAsync<MovimientosAlmacenLecturaResultado>();
            return r ?? new MovimientosAlmacenLecturaResultado { ErrorNumber = -1 };
        }
        catch (SqlException ex)
        {
            return new MovimientosAlmacenLecturaResultado { ErrorNumber = ex.Number, DES_ADVERTENCIA = ex.Message };
        }
    }

    /// <summary>Cambia el peso de una etiqueta ya leida; el SP recalcula la linea y el stock.</summary>
    public async Task<WriteResult> EtiquetaUpdateAsync(MovimientosAlmacenEtiquetaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_STICKER_RFID", dto.COD_STICKER_RFID);
        p.Add("COD_PRODUCTO", (string?)null);                       // el producto de una etiqueta no cambia
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenDetalleConteoRfid.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenDetalleConteoRfid.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenDetalleConteoRfid.Update, p)
            };
        }
    }

    /// <summary>Quita una etiqueta leida. Si era la ultima de su linea, el SP elimina la linea y el stock vuelve.</summary>
    public async Task<WriteResult> EtiquetaDeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_SECUENCIA, string COD_STICKER_RFID, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_SECUENCIA", NUM_SECUENCIA);
        p.Add("COD_STICKER_RFID", COD_STICKER_RFID);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenDetalleConteoRfid.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenDetalleConteoRfid.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenDetalleConteoRfid.Delete, p)
            };
        }
    }

    /// <summary>Lo que falta o no coincide en un movimiento cuyo tipo exige lectura (requisito R). La lista vacia es
    /// que esta todo en orden o que el tipo no exige nada. Siempre con @FLG_LANZAR_ERROR = 0: la pantalla quiere la
    /// lista, no un error.</summary>
    public async Task<IEnumerable<MovimientosAlmacenLecturaPendiente>> LecturasPendientesAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenLecturaPendiente>(
            SP.SCHEMALogistica.MovimientosAlmacen.LecturasValidar,
            new { NUM_MOVIMIENTO_ALMACEN, FLG_LANZAR_ERROR = false },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenDetalle.Delete, new { NUM_MOVIMIENTO_ALMACEN = NUM_MOVIMIENTO_ALMACEN, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenDetalle.Delete, new { NUM_MOVIMIENTO_ALMACEN = NUM_MOVIMIENTO_ALMACEN, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}