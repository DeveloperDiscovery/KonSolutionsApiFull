using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRequerimientosStockDetalleRepository
{
    Task<IEnumerable<RequerimientosStockDetalle>> ShowallAsync(int NUM_REQUERIMIENTO_STOCK);
    Task<RequerimientosStockDetalle?> ShowbyidAsync(int NUM_REQUERIMIENTO_STOCK, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RequerimientosStockDetalle>> SearchAsync(RequerimientosStockDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(RequerimientosStockDetalleInsertDto dto);
    Task<WriteResult> UpdateAsync(RequerimientosStockDetalleUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_REQUERIMIENTO_STOCK, int NUM_SECUENCIA);
}

public interface IRequerimientosStockDetalleService
{
    Task<ApiResponse<IEnumerable<RequerimientosStockDetalle>>> GetAllAsync(int NUM_REQUERIMIENTO_STOCK);
    Task<ApiResponse<RequerimientosStockDetalle>> GetByIdAsync(int NUM_REQUERIMIENTO_STOCK, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RequerimientosStockDetalle>>> SearchAsync(RequerimientosStockDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RequerimientosStockDetalleInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(RequerimientosStockDetalleUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_REQUERIMIENTO_STOCK, int NUM_SECUENCIA);
}