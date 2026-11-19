using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMovimientosAlmacenReservasStockRepository
{
    Task<IEnumerable<MovimientosAlmacenReservasStock>> ShowallAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<MovimientosAlmacenReservasStock?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MovimientosAlmacenReservasStock>> SearchAsync(MovimientosAlmacenReservasStockSearchDto dto);
    Task<WriteResult> InsertAsync(MovimientosAlmacenReservasStockInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK);
}

public interface IMovimientosAlmacenReservasStockService
{
    Task<ApiResponse<IEnumerable<MovimientosAlmacenReservasStock>>> GetAllAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<MovimientosAlmacenReservasStock>> GetByIdAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MovimientosAlmacenReservasStock>>> SearchAsync(MovimientosAlmacenReservasStockSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MovimientosAlmacenReservasStockInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK);
}
