using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Un solo repositorio para las dos tablas puente entre pedido y requerimiento.
/// Van juntas porque nunca se usan por separado: la pantalla lee las dos, graba las dos y
/// las borra las dos.</summary>
public class PedidosComercialesRequerimientosRepository : IPedidosComercialesRequerimientosRepository
{
    private readonly IDbConnectionFactory _db;
    public PedidosComercialesRequerimientosRepository(IDbConnectionFactory db) => _db = db;

    // ── Lo que se puede elegir ───────────────────────────────────────────────

    /// <summary>Requerimientos de compra en estado registrado. El SP recibe el número de
    /// pedido sólo para marcar cuáles ya están vinculados (FLG_SECCION), no para filtrar.</summary>
    public async Task<IEnumerable<RequerimientoStockRegistrado>> RegistradosAsync(decimal numPedidoComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RequerimientoStockRegistrado>(
            SP.SCHEMALogistica.RequerimientosStock.Registrados,
            new { NUM_PEDIDO_COMERCIAL = numPedidoComercial },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RequerimientoStockDetalleRegistrado>> RegistradosDetalleAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RequerimientoStockDetalleRegistrado>(
            SP.SCHEMALogistica.RequerimientosStockDetalle.Registrados,
            new
            {
                COD_CLASE_OPERACION  = codClaseOperacion,
                COD_TIPO_OPERACION   = codTipoOperacion,
                NUM_PEDIDO_COMERCIAL = numPedidoComercial
            },
            commandType: CommandType.StoredProcedure);
    }

    // ── Vínculo de cabecera ──────────────────────────────────────────────────

    public async Task<IEnumerable<PedidoComercialRequerimientoStock>> ShowallCabeceraAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidoComercialRequerimientoStock>(
            SP.SCHEMAComercial.PedidosComercialesRequerimientosStock.Showall,
            new { COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion, NUM_PEDIDO_COMERCIAL = numPedidoComercial },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>El SP hace upsert a propósito: volver a vincular algo ya vinculado no
    /// falla, sólo refresca el registro.</summary>
    public async Task<WriteResult> InsertCabeceraAsync(PedidoComercialRequerimientoStockDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_REQUERIMIENTO_STOCK", dto.NUM_REQUERIMIENTO_STOCK);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return await EjecutarAsync(conn, SP.SCHEMAComercial.PedidosComercialesRequerimientosStock.Insert, p);
    }

    public async Task<WriteResult> DeleteCabeceraAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numRequerimientoStock)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", codClaseOperacion);
        p.Add("COD_TIPO_OPERACION", codTipoOperacion);
        p.Add("NUM_PEDIDO_COMERCIAL", numPedidoComercial);
        p.Add("NUM_REQUERIMIENTO_STOCK", numRequerimientoStock);
        return await EjecutarAsync(conn, SP.SCHEMAComercial.PedidosComercialesRequerimientosStock.Delete, p);
    }

    // ── Vínculo de detalle ───────────────────────────────────────────────────

    public async Task<IEnumerable<PedidoComercialDetalleRequerimientoStockDetalle>> ShowallDetalleAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia, int numRequerimientoStock)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidoComercialDetalleRequerimientoStockDetalle>(
            SP.SCHEMAComercial.PedidosComercialesDetalleRequerimientosStockDetalle.Showall,
            new
            {
                COD_CLASE_OPERACION = codClaseOperacion,
                COD_TIPO_OPERACION = codTipoOperacion,
                NUM_PEDIDO_COMERCIAL = numPedidoComercial,
                NUM_SECUENCIA = numSecuencia,
                NUM_REQUERIMIENTO_STOCK = numRequerimientoStock
            },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>El SP de alta falla si la clave ya existe (no hace upsert), así que la
    /// pantalla no debe reenviar un vínculo ya grabado.</summary>
    public async Task<WriteResult> InsertDetalleAsync(PedidoComercialDetalleRequerimientoStockDetalleDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("NUM_REQUERIMIENTO_STOCK", dto.NUM_REQUERIMIENTO_STOCK);
        p.Add("NUM_SECUENCIA_REQUERIMIENTO_STOCK", dto.NUM_SECUENCIA_REQUERIMIENTO_STOCK);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        // Sí, el parámetro del SP se llama SECUDARIO (sin la N). Está así en la base y en
        // la columna de la tabla; se respeta tal cual para que Dapper lo encuentre.
        p.Add("CAN_PRODUCTO_SECUDARIO", dto.CAN_PRODUCTO_SECUDARIO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIO", dto.COD_UNIDAD_MEDIDA_SECUNDARIO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return await EjecutarAsync(conn, SP.SCHEMAComercial.PedidosComercialesDetalleRequerimientosStockDetalle.Insert, p);
    }

    public async Task<WriteResult> DeleteDetalleAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia, int numRequerimientoStock, int numSecuenciaRequerimientoStock)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", codClaseOperacion);
        p.Add("COD_TIPO_OPERACION", codTipoOperacion);
        p.Add("NUM_PEDIDO_COMERCIAL", numPedidoComercial);
        p.Add("NUM_SECUENCIA", numSecuencia);
        p.Add("NUM_REQUERIMIENTO_STOCK", numRequerimientoStock);
        p.Add("NUM_SECUENCIA_REQUERIMIENTO_STOCK", numSecuenciaRequerimientoStock);
        return await EjecutarAsync(conn, SP.SCHEMAComercial.PedidosComercialesDetalleRequerimientosStockDetalle.Delete, p);
    }

    /// <summary>Los seis métodos de escritura hacen lo mismo con distinto SP: ejecutar y
    /// traducir el SqlException al WriteResult que espera el front.</summary>
    private static async Task<WriteResult> EjecutarAsync(IDbConnection conn, string procedimiento, DynamicParameters p)
    {
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(procedimiento, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR",
                Mensaje = ex.Message,
                ErrorNumber = ex.Number,
                ErrorProcedure = procedimiento,
                SqlSentence = SqlSentenceHelper.Build(procedimiento, p)
            };
        }
    }
}
