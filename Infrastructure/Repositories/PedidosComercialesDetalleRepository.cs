using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PedidosComercialesDetalleRepository : IPedidosComercialesDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public PedidosComercialesDetalleRepository(IDbConnectionFactory db) => _db = db;

    private static string? Nulo(string? v) => string.IsNullOrWhiteSpace(v) ? null : v;

    public async Task<IEnumerable<PedidosComercialesDetalle>> ShowallAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComercialesDetalle>(SP.SCHEMAComercial.PedidosComercialesDetalle.Showall,
            new
            {
                COD_CLASE_OPERACION  = codClaseOperacion,
                COD_TIPO_OPERACION   = codTipoOperacion,
                NUM_PEDIDO_COMERCIAL = numPedidoComercial
            },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>PROC_..._DETALLE_SEARCH ya no recibe clase/tipo de operación —el número de
    /// pedido solo, junto con la búsqueda libre, le alcanza para el WHERE.</summary>
    public async Task<IEnumerable<PedidosComercialesDetalle>> SearchAsync(PedidosComercialesDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComercialesDetalle>(SP.SCHEMAComercial.PedidosComercialesDetalle.Search,
            new
            {
                COD_CLASE_OPERACION  = dto.CodClaseOperacion,
                COD_TIPO_OPERACION   = dto.CodTipoOperacion,
                NUM_PEDIDO_COMERCIAL = dto.NumPedidoComercial,
                SEARCH               = dto.Search
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<PedidosComercialesDetalle?> ShowbyidAsync(decimal numPedidoComercial, int numSecuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PedidosComercialesDetalle>(SP.SCHEMAComercial.PedidosComercialesDetalle.Showbyid,
            new { NUM_PEDIDO_COMERCIAL = numPedidoComercial, NUM_SECUENCIA = numSecuencia }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PedidosComercialesDetalleInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_DISENO", Nulo(dto.COD_DISENO));
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_LOTE_CONTROL", Nulo(dto.NUM_LOTE_CONTROL));
        // Proceso y orden de producción. Van con Nulo()/tal cual: NULL es un valor legítimo
        // —la línea que no viene de producción no tiene orden—, y el cero SÍ es un número de
        // orden real en esta base, así que no se puede usar cero como "sin dato".
        p.Add("COD_PROCESO", Nulo(dto.COD_PROCESO));
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO ?? 0);
        p.Add("COD_UNIDAD_MEDIDA", Nulo(dto.COD_UNIDAD_MEDIDA));
        p.Add("CAN_PRODUCTO_SECUDARIO", dto.CAN_PRODUCTO_SECUDARIO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIO", Nulo(dto.COD_UNIDAD_MEDIDA_SECUNDARIO));
        p.Add("COD_TIPO_DESCUENTO", Nulo(dto.COD_TIPO_DESCUENTO));
        // Sin ISNULL en la propia SP: @VAL_TOTAL * @TAS_DESCUENTO / 100 da NULL si
        // @TAS_DESCUENTO llega NULL (no eligió tipo de descuento), y ese NULL se graba en
        // VAL_TOTAL. No se nota en el momento — recién revienta después, cuando
        // PROC_COMERCIAL_PEDIDOS_COMERCIALES_CALCULA suma VAL_TOTAL de todas las líneas del
        // pedido: SUM() con un NULL adentro da NULL, y esa suma se intenta grabar en una
        // columna NOT NULL, error 515. Y como CALCULA se ejecuta en cada alta o baja de
        // línea, una sola línea así deja el pedido ENTERO sin poder tocarse.
        p.Add("TAS_DESCUENTO", dto.TAS_DESCUENTO ?? 0);
        // Mismo motivo que TAS_DESCUENTO arriba: la SP hace PRE_UNITARIO * CAN_PRODUCTO_ENTIDAD
        // sin ISNULL. Si el producto se incorpora desde un requerimiento sin precio configurado
        // (PRE_UNITARIO llega null), esa multiplicación da NULL, VAL_TOTAL queda NULL, y
        // PROC_COMERCIAL_PEDIDOS_COMERCIALES_CALCULA vuelve a reventar en el mismo SUM().
        p.Add("PRE_UNITARIO", dto.PRE_UNITARIO ?? 0);
        p.Add("VAL_DESCUENTO", dto.VAL_DESCUENTO);
        p.Add("VAL_TOTAL", dto.VAL_TOTAL);
        // El SP recalcula VAL_TOTAL a partir de esto (PRE_UNITARIO * CAN_PRODUCTO_ENTIDAD),
        // así que el VAL_TOTAL de arriba queda como valor de entrada nomás. Mientras la
        // pantalla no traiga la conversión real, se manda igual a CAN_PRODUCTO (asume 1 a 1).
        p.Add("CAN_PRODUCTO_ENTIDAD", dto.CAN_PRODUCTO_ENTIDAD ?? dto.CAN_PRODUCTO ?? 0);
        p.Add("CAN_PRODUCTO_ATENDIDO", dto.CAN_PRODUCTO_ATENDIDO);
        p.Add("CAN_PRODUCTO_SECUNDARIO_ATENDIDO", dto.CAN_PRODUCTO_SECUNDARIO_ATENDIDO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", dto.COD_USUARIO_REGISTRO);
        p.Add("COD_ESTACION_REGISTRO", dto.COD_ESTACION_REGISTRO);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComercialesDetalle.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesDetalle.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesDetalle.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(PedidosComercialesDetalleUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_DISENO", Nulo(dto.COD_DISENO));
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_LOTE_CONTROL", Nulo(dto.NUM_LOTE_CONTROL));
        // Proceso y orden de producción. Van con Nulo()/tal cual: NULL es un valor legítimo
        // —la línea que no viene de producción no tiene orden—, y el cero SÍ es un número de
        // orden real en esta base, así que no se puede usar cero como "sin dato".
        p.Add("COD_PROCESO", Nulo(dto.COD_PROCESO));
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO ?? 0);
        p.Add("COD_UNIDAD_MEDIDA", Nulo(dto.COD_UNIDAD_MEDIDA));
        p.Add("CAN_PRODUCTO_SECUDARIO", dto.CAN_PRODUCTO_SECUDARIO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIO", Nulo(dto.COD_UNIDAD_MEDIDA_SECUNDARIO));
        p.Add("COD_TIPO_DESCUENTO", Nulo(dto.COD_TIPO_DESCUENTO));
        // Sin ISNULL en la propia SP: @VAL_TOTAL * @TAS_DESCUENTO / 100 da NULL si
        // @TAS_DESCUENTO llega NULL (no eligió tipo de descuento), y ese NULL se graba en
        // VAL_TOTAL. No se nota en el momento — recién revienta después, cuando
        // PROC_COMERCIAL_PEDIDOS_COMERCIALES_CALCULA suma VAL_TOTAL de todas las líneas del
        // pedido: SUM() con un NULL adentro da NULL, y esa suma se intenta grabar en una
        // columna NOT NULL, error 515. Y como CALCULA se ejecuta en cada alta o baja de
        // línea, una sola línea así deja el pedido ENTERO sin poder tocarse.
        p.Add("TAS_DESCUENTO", dto.TAS_DESCUENTO ?? 0);
        // Mismo motivo que TAS_DESCUENTO arriba: la SP hace PRE_UNITARIO * CAN_PRODUCTO_ENTIDAD
        // sin ISNULL. Si el producto se incorpora desde un requerimiento sin precio configurado
        // (PRE_UNITARIO llega null), esa multiplicación da NULL, VAL_TOTAL queda NULL, y
        // PROC_COMERCIAL_PEDIDOS_COMERCIALES_CALCULA vuelve a reventar en el mismo SUM().
        p.Add("PRE_UNITARIO", dto.PRE_UNITARIO ?? 0);
        p.Add("VAL_DESCUENTO", dto.VAL_DESCUENTO);
        p.Add("VAL_TOTAL", dto.VAL_TOTAL);
        // El SP recalcula VAL_TOTAL a partir de esto (PRE_UNITARIO * CAN_PRODUCTO_ENTIDAD),
        // así que el VAL_TOTAL de arriba queda como valor de entrada nomás. Mientras la
        // pantalla no traiga la conversión real, se manda igual a CAN_PRODUCTO (asume 1 a 1).
        p.Add("CAN_PRODUCTO_ENTIDAD", dto.CAN_PRODUCTO_ENTIDAD ?? dto.CAN_PRODUCTO ?? 0);
        p.Add("CAN_PRODUCTO_ATENDIDO", dto.CAN_PRODUCTO_ATENDIDO);
        p.Add("CAN_PRODUCTO_SECUNDARIO_ATENDIDO", dto.CAN_PRODUCTO_SECUNDARIO_ATENDIDO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", dto.COD_USUARIO_REGISTRO);
        p.Add("COD_ESTACION_REGISTRO", dto.COD_ESTACION_REGISTRO);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComercialesDetalle.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesDetalle.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesDetalle.Update, p)
            };
        }
    }

    /// <summary>La clave del detalle son cuatro columnas, no dos: el SP las exige todas.
    /// Borra en cascada los vínculos con requerimientos de stock de esa línea, y de paso el
    /// vínculo de cabecera del requerimiento si ya no le queda ninguna línea en el pedido.</summary>
    public async Task<WriteResult> DeleteAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia)
    {
        using var conn = _db.CreateConnection();
        var p = new
        {
            COD_CLASE_OPERACION = codClaseOperacion,
            COD_TIPO_OPERACION = codTipoOperacion,
            NUM_PEDIDO_COMERCIAL = numPedidoComercial,
            NUM_SECUENCIA = numSecuencia
        };
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComercialesDetalle.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesDetalle.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesDetalle.Delete, p)
            };
        }
    }
}
