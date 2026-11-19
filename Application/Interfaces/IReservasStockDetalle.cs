using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IReservasStockDetalleRepository
{
    Task<IEnumerable<ReservasStockDetalle>> ShowallAsync(int NUM_RESERVA_STOCK);
    Task<ReservasStockDetalle?> ShowbyidAsync(int NUM_RESERVA_STOCK, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ReservasStockDetalle>> SearchAsync(ReservasStockDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(ReservasStockDetalleInsertDto dto);
    Task<WriteResult> UpdateAsync(ReservasStockDetalleUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_RESERVA_STOCK, int NUM_SECUENCIA);
}

public interface IReservasStockDetalleService
{
    Task<ApiResponse<IEnumerable<ReservasStockDetalle>>> GetAllAsync(int NUM_RESERVA_STOCK);
    Task<ApiResponse<ReservasStockDetalle>> GetByIdAsync(int NUM_RESERVA_STOCK, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ReservasStockDetalle>>> SearchAsync(ReservasStockDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ReservasStockDetalleInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ReservasStockDetalleUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_RESERVA_STOCK, int NUM_SECUENCIA);
}