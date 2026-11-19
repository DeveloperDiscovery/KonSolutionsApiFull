using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProductosAlmacenesRepository : IProductosAlmacenesRepository
{
    private readonly IDbConnectionFactory _db;
    public ProductosAlmacenesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProductosAlmacenes>> ShowallAsync(string codProducto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProductosAlmacenes>(SP.SCHEMALogistica.ProductosAlmacenes.Showall, new { COD_PRODUCTO = codProducto }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>La vuelta: todos los productos configurados en un almacén. Va contra un SP
    /// propio (SHOWALL_ALMACEN) porque el SHOWALL original filtra por @COD_PRODUCTO
    /// obligatorio y no admite entrar por el otro lado de la clave.</summary>
    public async Task<IEnumerable<ProductosAlmacenes>> ShowallPorAlmacenAsync(string codAlmacen)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProductosAlmacenes>(SP.SCHEMALogistica.ProductosAlmacenes.ShowallPorAlmacen, new { COD_ALMACEN = codAlmacen }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Solo las líneas con cantidad pendiente de pedir. Va contra un SP propio y no
    /// se filtra en memoria sobre el Showall: el criterio de "qué hay que pedir" es del
    /// negocio y vive en la BD, así que si mañana cambia (por ejemplo, mirando también el
    /// stock máximo) cambia en un solo lugar y no en cada pantalla que lo muestre.</summary>
    public async Task<IEnumerable<ProductosAlmacenes>> ShowallRequerirAsync()
    {
        using var conn = _db.CreateConnection();
        // Sin parámetros: el SP ya no filtra por producto, devuelve todo lo pendiente.
        return await conn.QueryAsync<ProductosAlmacenes>(SP.SCHEMALogistica.ProductosAlmacenes.Requerir, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProductosAlmacenes?> ShowbyidAsync(string COD_PRODUCTO, string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProductosAlmacenes>(SP.SCHEMALogistica.ProductosAlmacenes.Showbyid, new { COD_PRODUCTO = COD_PRODUCTO, COD_ALMACEN = COD_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codProducto)
    {
        using var conn = _db.CreateConnection();
        // Columnas explícitas y no "la primera COD_*/DES_*": el SP devuelve COD_PRODUCTO
        // antes que COD_ALMACEN, así que el lector genérico usaría el producto como clave
        // del combo — que es la falla que describe el comentario de esa sobrecarga.
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.ProductosAlmacenes.Showcombox,
            "COD_ALMACEN", "DES_ALMACEN", new { COD_PRODUCTO = codProducto });
    }

    public async Task<IEnumerable<ProductosAlmacenes>> SearchAsync(string codProducto, ProductosAlmacenesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProductosAlmacenes>(SP.SCHEMALogistica.ProductosAlmacenes.Search, new { COD_PRODUCTO = codProducto, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProductosAlmacenesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("CAN_STOCK_MINIMO", dto.CAN_STOCK_MINIMO);
        p.Add("CAN_STOCK_MAXIMO", dto.CAN_STOCK_MAXIMO);
        p.Add("CAN_LEAD_TIME_DIAS", dto.CAN_LEAD_TIME_DIAS);
        p.Add("CAN_CONUSMO_DIARIO", dto.CAN_CONUSMO_DIARIO);
        p.Add("CAN_STOCK_REQUERIR", dto.CAN_STOCK_REQUERIR);
        p.Add("NUM_REQUERIMIENTO_STOCK", dto.NUM_REQUERIMIENTO_STOCK);
        p.Add("FLG_REQUERIMIENTO_AUTOMATICO", dto.FLG_REQUERIMIENTO_AUTOMATICO ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ProductosAlmacenes.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ProductosAlmacenes.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ProductosAlmacenes.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProductosAlmacenesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("CAN_STOCK_MINIMO", dto.CAN_STOCK_MINIMO);
        p.Add("CAN_STOCK_MAXIMO", dto.CAN_STOCK_MAXIMO);
        p.Add("CAN_LEAD_TIME_DIAS", dto.CAN_LEAD_TIME_DIAS);
        p.Add("CAN_CONUSMO_DIARIO", dto.CAN_CONUSMO_DIARIO);
        p.Add("CAN_STOCK_REQUERIR", dto.CAN_STOCK_REQUERIR);
        p.Add("NUM_REQUERIMIENTO_STOCK", dto.NUM_REQUERIMIENTO_STOCK);
        p.Add("FLG_REQUERIMIENTO_AUTOMATICO", dto.FLG_REQUERIMIENTO_AUTOMATICO ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ProductosAlmacenes.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ProductosAlmacenes.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ProductosAlmacenes.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PRODUCTO, string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ProductosAlmacenes.Delete, new { COD_PRODUCTO = COD_PRODUCTO, COD_ALMACEN = COD_ALMACEN }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ProductosAlmacenes.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ProductosAlmacenes.Delete, new { COD_PRODUCTO = COD_PRODUCTO, COD_ALMACEN = COD_ALMACEN })
        };
    }
}

    /// <summary>Botones "Borrar requerimiento de producto" (manda almacén y producto) y
    /// "Resetear requerimiento de stock" (manda los dos en null). Pone CAN_STOCK_REQUERIR en
    /// 0 donde todavía no hay requerimiento generado.</summary>
    public async Task<WriteResult> ResetAsync(string? codAlmacen, string? codProducto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        // Cadena vacía y NULL NO son lo mismo para este SP: sus tres IF preguntan por IS NULL,
        // así que un "" haría que ninguna rama entre y el botón no haría nada en silencio.
        p.Add("COD_ALMACEN",  string.IsNullOrWhiteSpace(codAlmacen)  ? null : codAlmacen);
        p.Add("COD_PRODUCTO", string.IsNullOrWhiteSpace(codProducto) ? null : codProducto);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ProductosAlmacenes.Reset, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.ProductosAlmacenes.Reset,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ProductosAlmacenes.Reset, p)
            };
        }
    }

    /// <summary>Botón "Generar requerimiento de compra". Desde la versión del 04/09 el SP ya
    /// NO recibe listas: toma por su cuenta todo lo que tenga CAN_STOCK_REQUERIR > 0, así que
    /// acá solo viajan el usuario y la estación. Ojo con esto en pantalla: el botón es
    /// GLOBAL, no se acota a lo que el usuario tenga seleccionado.</summary>
    public async Task<WriteResult> RequerimientoAsync(string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ProductosAlmacenes.Requerimiento, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.ProductosAlmacenes.Requerimiento,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ProductosAlmacenes.Requerimiento, p)
            };
        }
    }

    /// <summary>Botón "Generar stock de seguridad". Recalcula CAN_STOCK_REQUERIR contra las
    /// existencias reales, para las listas de almacenes y productos seleccionados (códigos
    /// separados por "_", que es lo que el SP parte con STRING_SPLIT). Una lista vacía no
    /// aporta filas: el SP filtra con IN sobre la tabla que arma con el split.</summary>
    public async Task<WriteResult> CantidadesAsync(string lstAlmacenes, string lstProductos)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("LST_ALMACENES", lstAlmacenes ?? "");
        p.Add("LST_PRODUCTOS", lstProductos ?? "");
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ProductosAlmacenes.Cantidades, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.ProductosAlmacenes.Cantidades,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ProductosAlmacenes.Cantidades, p)
            };
        }
    }

    /// <summary>Botón "Revertir": anula el requerimiento generado para esa línea. El SP toma
    /// el número recibido, busca su FEC_ACTUALIZACION y revierte TODOS los requerimientos
    /// generados en esa misma corrida — no solo el de la fila desde la que se disparó.</summary>
    public async Task<WriteResult> ReversionAsync(int numRequerimientoStock, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REQUERIMIENTO_STOCK", numRequerimientoStock);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ProductosAlmacenes.Reversion, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.ProductosAlmacenes.Reversion,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ProductosAlmacenes.Reversion, p)
            };
        }
    }
}
