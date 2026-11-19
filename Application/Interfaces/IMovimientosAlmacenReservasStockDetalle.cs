using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMovimientosAlmacenReservasStockDetalleRepository
{
    Task<IEnumerable<MovimientosAlmacenReservasStockDetalle>> ShowallAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK);
    Task<MovimientosAlmacenReservasStockDetalle?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MovimientosAlmacenReservasStockDetalle>> SearchAsync(MovimientosAlmacenReservasStockDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(MovimientosAlmacenReservasStockDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK, int NUM_SECUENCIA);
}

public interface IMovimientosAlmacenReservasStockDetalleService
{
    Task<ApiResponse<IEnumerable<MovimientosAlmacenReservasStockDetalle>>> GetAllAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK);
    Task<ApiResponse<MovimientosAlmacenReservasStockDetalle>> GetByIdAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MovimientosAlmacenReservasStockDetalle>>> SearchAsync(MovimientosAlmacenReservasStockDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MovimientosAlmacenReservasStockDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK, int NUM_SECUENCIA);
}
