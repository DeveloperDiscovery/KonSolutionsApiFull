using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PedidosComercialesRequerimientosStockLinkRepository : IPedidosComercialesRequerimientosStockLinkRepository
{
    private readonly IDbConnectionFactory _db;
    public PedidosComercialesRequerimientosStockLinkRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PedidoComercialRequerimientoStockLink>> ShowallCabeceraAsync(
        string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidoComercialRequerimientoStockLink>(
            SP.SCHEMAComercial.PedidosComercialesRequerimientosStock.Showall,
            new { COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion, NUM_PEDIDO_COMERCIAL = numPedidoComercial },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<PedidoComercialDetalleRequerimientoStockDetalleLinea>> ShowallDetalleAsync(
        string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia, int? numRequerimientoStock)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidoComercialDetalleRequerimientoStockDetalleLinea>(
            SP.SCHEMAComercial.PedidosComercialesDetalleRequerimientosStockDetalle.Showall,
            new
            {
                COD_CLASE_OPERACION = codClaseOperacion,
                COD_TIPO_OPERACION = codTipoOperacion,
                NUM_PEDIDO_COMERCIAL = numPedidoComercial,
                NUM_SECUENCIA = numSecuencia,
                // 61: null trae TODOS los requerimientos de la línea. Antes había que
                // pedirlos de a uno, y para saber cuáles eran se consultaba la tabla de
                // cabecera — que nadie escribía, así que nunca se llegaba al detalle.
                NUM_REQUERIMIENTO_STOCK = numRequerimientoStock
            },
            commandType: CommandType.StoredProcedure);
    }
}
