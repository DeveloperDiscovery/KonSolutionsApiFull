using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosComercialesMovimientosAlmacenRepository
{
    Task<IEnumerable<PedidosComercialesMovimientosAlmacen>> ShowallAsync();
    Task<PedidosComercialesMovimientosAlmacen?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA_PEDIDO, decimal NUM_MOVIMIENTO_ALMACEN);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PedidosComercialesMovimientosAlmacen>> SearchAsync(PedidosComercialesMovimientosAlmacenSearchDto dto);
    Task<WriteResult> InsertAsync(PedidosComercialesMovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA_PEDIDO, decimal NUM_MOVIMIENTO_ALMACEN);
}

public interface IPedidosComercialesMovimientosAlmacenService
{
    Task<ApiResponse<IEnumerable<PedidosComercialesMovimientosAlmacen>>> GetAllAsync();
    Task<ApiResponse<PedidosComercialesMovimientosAlmacen>> GetByIdAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA_PEDIDO, decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PedidosComercialesMovimientosAlmacen>>> SearchAsync(PedidosComercialesMovimientosAlmacenSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesMovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA_PEDIDO, decimal NUM_MOVIMIENTO_ALMACEN);
}