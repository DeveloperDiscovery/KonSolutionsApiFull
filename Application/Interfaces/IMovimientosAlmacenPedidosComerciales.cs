using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMovimientosAlmacenPedidosComercialesRepository
{
    Task<IEnumerable<MovimientosAlmacenPedidosComerciales>> ShowallAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<MovimientosAlmacenPedidosComerciales?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MovimientosAlmacenPedidosComerciales>> SearchAsync(MovimientosAlmacenPedidosComercialesSearchDto dto);
    Task<WriteResult> InsertAsync(MovimientosAlmacenPedidosComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL);
}

public interface IMovimientosAlmacenPedidosComercialesService
{
    Task<ApiResponse<IEnumerable<MovimientosAlmacenPedidosComerciales>>> GetAllAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<MovimientosAlmacenPedidosComerciales>> GetByIdAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MovimientosAlmacenPedidosComerciales>>> SearchAsync(MovimientosAlmacenPedidosComercialesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MovimientosAlmacenPedidosComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL);
}
