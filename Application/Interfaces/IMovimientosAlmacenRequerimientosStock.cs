using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMovimientosAlmacenRequerimientosStockRepository
{
    Task<IEnumerable<MovimientosAlmacenRequerimientosStock>> ShowallAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<MovimientosAlmacenRequerimientosStock?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_REQUERIMIENTO_STOCK);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MovimientosAlmacenRequerimientosStock>> SearchAsync(MovimientosAlmacenRequerimientosStockSearchDto dto);
    Task<WriteResult> InsertAsync(MovimientosAlmacenRequerimientosStockInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_REQUERIMIENTO_STOCK);
}

public interface IMovimientosAlmacenRequerimientosStockService
{
    Task<ApiResponse<IEnumerable<MovimientosAlmacenRequerimientosStock>>> GetAllAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<MovimientosAlmacenRequerimientosStock>> GetByIdAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_REQUERIMIENTO_STOCK);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MovimientosAlmacenRequerimientosStock>>> SearchAsync(MovimientosAlmacenRequerimientosStockSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MovimientosAlmacenRequerimientosStockInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_REQUERIMIENTO_STOCK);
}
