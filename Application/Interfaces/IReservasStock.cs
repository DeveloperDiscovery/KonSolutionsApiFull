using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IReservasStockRepository
{
    // Los filtros son OPCIONALES: una llamada sin nada sigue devolviendo el listado completo.
    Task<IEnumerable<ReservasStock>> ShowallAsync(ReservasStockSearchDto? filtros = null);
    Task<ReservasStock?> ShowbyidAsync(int NUM_RESERVA_STOCK);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ReservasStock>> SearchAsync(ReservasStockSearchDto dto);
    Task<WriteResult> InsertAsync(ReservasStockInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ReservasStockUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_RESERVA_STOCK);
}

public interface IReservasStockService
{
    Task<ApiResponse<IEnumerable<ReservasStock>>> GetAllAsync(ReservasStockSearchDto? filtros = null);
    Task<ApiResponse<ReservasStock>> GetByIdAsync(int NUM_RESERVA_STOCK);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ReservasStock>>> SearchAsync(ReservasStockSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ReservasStockInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ReservasStockUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_RESERVA_STOCK);
}