using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Tabla SIN CRUD manual (la puebla un proceso todavía no implementado) — se
/// implementa la capa completa por consistencia arquitectónica, pero en la práctica solo
/// se usa ShowallAsync desde el frontend.</summary>
public class InventariosAlmacenesImagenRepository : IInventariosAlmacenesImagenRepository
{
    private readonly IDbConnectionFactory _db;
    public InventariosAlmacenesImagenRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>El SP _SHOWALL exige los 8 parámetros de la clave aunque conceptualmente
    /// alcance con NUM_INVENTARIO+COD_ALMACEN para listar todo lo del almacén — se pasan los
    /// demás como null con DbType explícito (Dapper lo exige para que no sea DBNull sin tipo).</summary>
    public async Task<IEnumerable<InventariosAlmacenesImagen>> ShowallAsync(int numInventario, string codAlmacen)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", numInventario);
        p.Add("COD_ALMACEN", codAlmacen);
        p.Add("COD_UBICACION_ALMACEN", dbType: DbType.String, value: null);
        p.Add("COD_PRODUCTO", dbType: DbType.String, value: null);
        p.Add("NUM_LOTE_CONTROL", dbType: DbType.String, value: null);
        p.Add("COD_PROCESO", dbType: DbType.String, value: null);
        p.Add("NUM_ORDEN_PRODUCCION", dbType: DbType.Int32, value: null);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dbType: DbType.Int32, value: null);
        return await conn.QueryAsync<InventariosAlmacenesImagen>(SP.SCHEMALogistica.InventariosAlmacenesImagen.Showall, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<InventariosAlmacenesImagen?> ShowbyidAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen, string codProducto, string numLoteControl, string codProceso, int numOrdenProduccion, int numOrdenProduccionItem, int numOrdenProduccionUnidad)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<InventariosAlmacenesImagen>(SP.SCHEMALogistica.InventariosAlmacenesImagen.Showbyid, new
        {
            NUM_INVENTARIO = numInventario,
            COD_ALMACEN = codAlmacen,
            COD_UBICACION_ALMACEN = codUbicacionAlmacen,
            COD_PRODUCTO = codProducto,
            NUM_LOTE_CONTROL = numLoteControl,
            COD_PROCESO = codProceso,
            NUM_ORDEN_PRODUCCION = numOrdenProduccion,
            NUM_ORDEN_PRODUCCION_ITEM = numOrdenProduccionItem,
            NUM_ORDEN_PRODUCCION_UNIDAD = numOrdenProduccionUnidad
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(int numInventario, string codAlmacen)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.InventariosAlmacenesImagen.Showcombox,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen });
    }

    public async Task<IEnumerable<InventariosAlmacenesImagen>> SearchAsync(InventariosAlmacenesImagenSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenesImagen>(SP.SCHEMALogistica.InventariosAlmacenesImagen.Search,
            new { NUM_INVENTARIO = dto.NUM_INVENTARIO, COD_ALMACEN = dto.COD_ALMACEN, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(InventariosAlmacenesImagenInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_UBICACION_ALMACEN", dto.COD_UBICACION_ALMACEN);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("CAN_PRODUCTO_CONTEO", dto.CAN_PRODUCTO_CONTEO);
        p.Add("CAN_PRODUCTO_CONTEO_DIFERENCIA", dto.CAN_PRODUCTO_CONTEO_DIFERENCIA);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_PRODUCTO_INVENTARIADA", dto.CAN_PRODUCTO_INVENTARIADA);
        p.Add("CAN_PRODUCTO_DIFERENCIA", dto.CAN_PRODUCTO_DIFERENCIA);
        // El SP _INSERT no declara @COD_USUARIO_REGISTRO/@COD_ESTACION_REGISTRO (la tabla
        // no tiene columnas de auditoría) — pasarlos rompe la llamada con "too many arguments".
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesImagen.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesImagen.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesImagen.Insert, p)
        };
    }
}

    public async Task<WriteResult> UpdateAsync(InventariosAlmacenesImagenUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_UBICACION_ALMACEN", dto.COD_UBICACION_ALMACEN);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("CAN_PRODUCTO_CONTEO", dto.CAN_PRODUCTO_CONTEO);
        p.Add("CAN_PRODUCTO_CONTEO_DIFERENCIA", dto.CAN_PRODUCTO_CONTEO_DIFERENCIA);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_PRODUCTO_INVENTARIADA", dto.CAN_PRODUCTO_INVENTARIADA);
        p.Add("CAN_PRODUCTO_DIFERENCIA", dto.CAN_PRODUCTO_DIFERENCIA);
        // Mismo caso que en InsertAsync: el SP _UPDATE tampoco declara estos dos parámetros.
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesImagen.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesImagen.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesImagen.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen, string codProducto, string numLoteControl, string codProceso, int numOrdenProduccion, int numOrdenProduccionItem, int numOrdenProduccionUnidad)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesImagen.Delete, new
        {
            NUM_INVENTARIO = numInventario,
            COD_ALMACEN = codAlmacen,
            COD_UBICACION_ALMACEN = codUbicacionAlmacen,
            COD_PRODUCTO = codProducto,
            NUM_LOTE_CONTROL = numLoteControl,
            COD_PROCESO = codProceso,
            NUM_ORDEN_PRODUCCION = numOrdenProduccion,
            NUM_ORDEN_PRODUCCION_ITEM = numOrdenProduccionItem,
            NUM_ORDEN_PRODUCCION_UNIDAD = numOrdenProduccionUnidad
        }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesImagen.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesImagen.Delete, new
        {
            NUM_INVENTARIO = numInventario,
            COD_ALMACEN = codAlmacen,
            COD_UBICACION_ALMACEN = codUbicacionAlmacen,
            COD_PRODUCTO = codProducto,
            NUM_LOTE_CONTROL = numLoteControl,
            COD_PROCESO = codProceso,
            NUM_ORDEN_PRODUCCION = numOrdenProduccion,
            NUM_ORDEN_PRODUCCION_ITEM = numOrdenProduccionItem,
            NUM_ORDEN_PRODUCCION_UNIDAD = numOrdenProduccionUnidad
        })
        };
    }
}
}
