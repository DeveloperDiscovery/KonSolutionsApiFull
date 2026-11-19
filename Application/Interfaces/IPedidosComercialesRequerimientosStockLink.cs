using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosComercialesRequerimientosStockLinkRepository
{
    Task<IEnumerable<PedidoComercialRequerimientoStockLink>> ShowallCabeceraAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<IEnumerable<PedidoComercialDetalleRequerimientoStockDetalleLinea>> ShowallDetalleAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia, int? numRequerimientoStock);
}

public interface IPedidosComercialesRequerimientosStockLinkService
{
    Task<ApiResponse<IEnumerable<PedidoComercialRequerimientoStockLink>>> GetCabeceraAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<ApiResponse<IEnumerable<PedidoComercialDetalleRequerimientoStockDetalleLinea>>> GetDetalleAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia, int? numRequerimientoStock);
}
