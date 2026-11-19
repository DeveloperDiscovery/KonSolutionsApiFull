using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosComercialesRequerimientosRepository
{
    // Lo que se puede elegir
    Task<IEnumerable<RequerimientoStockRegistrado>> RegistradosAsync(decimal numPedidoComercial);
    Task<IEnumerable<RequerimientoStockDetalleRegistrado>> RegistradosDetalleAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);

    // Vínculo de cabecera
    Task<IEnumerable<PedidoComercialRequerimientoStock>> ShowallCabeceraAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<WriteResult> InsertCabeceraAsync(PedidoComercialRequerimientoStockDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteCabeceraAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numRequerimientoStock);

    // Vínculo de detalle
    Task<IEnumerable<PedidoComercialDetalleRequerimientoStockDetalle>> ShowallDetalleAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia, int numRequerimientoStock);
    Task<WriteResult> InsertDetalleAsync(PedidoComercialDetalleRequerimientoStockDetalleDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteDetalleAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia, int numRequerimientoStock, int numSecuenciaRequerimientoStock);
}

public interface IPedidosComercialesRequerimientosService
{
    Task<ApiResponse<IEnumerable<RequerimientoStockRegistrado>>> GetRegistradosAsync(decimal numPedidoComercial);
    Task<ApiResponse<IEnumerable<RequerimientoStockDetalleRegistrado>>> GetRegistradosDetalleAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);

    Task<ApiResponse<IEnumerable<PedidoComercialRequerimientoStock>>> GetCabecerasAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<ApiResponse<WriteResult>> InsertCabeceraAsync(PedidoComercialRequerimientoStockDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteCabeceraAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numRequerimientoStock);

    Task<ApiResponse<IEnumerable<PedidoComercialDetalleRequerimientoStockDetalle>>> GetDetallesAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia, int numRequerimientoStock);
    Task<ApiResponse<WriteResult>> InsertDetalleAsync(PedidoComercialDetalleRequerimientoStockDetalleDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteDetalleAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia, int numRequerimientoStock, int numSecuenciaRequerimientoStock);
}
