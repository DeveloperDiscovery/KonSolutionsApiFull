using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMovimientosAlmacenPedidosComercialesDetalleRepository
{
    Task<IEnumerable<MovimientosAlmacenPedidosComercialesDetalle>> ShowallAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL);
    Task<MovimientosAlmacenPedidosComercialesDetalle?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MovimientosAlmacenPedidosComercialesDetalle>> SearchAsync(MovimientosAlmacenPedidosComercialesDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(MovimientosAlmacenPedidosComercialesDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
}

public interface IMovimientosAlmacenPedidosComercialesDetalleService
{
    Task<ApiResponse<IEnumerable<MovimientosAlmacenPedidosComercialesDetalle>>> GetAllAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL);
    Task<ApiResponse<MovimientosAlmacenPedidosComercialesDetalle>> GetByIdAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MovimientosAlmacenPedidosComercialesDetalle>>> SearchAsync(MovimientosAlmacenPedidosComercialesDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MovimientosAlmacenPedidosComercialesDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
}
