using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposReservaStockRepository
{
    Task<IEnumerable<TiposReservaStock>> ShowallAsync();
    Task<TiposReservaStock?> ShowbyidAsync(string COD_TIPO_RESERVA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposReservaStock>> SearchAsync(TiposReservaStockSearchDto dto);
    Task<WriteResult> InsertAsync(TiposReservaStockInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposReservaStockUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_RESERVA);
}

public interface ITiposReservaStockService
{
    Task<ApiResponse<IEnumerable<TiposReservaStock>>> GetAllAsync();
    Task<ApiResponse<TiposReservaStock>> GetByIdAsync(string COD_TIPO_RESERVA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposReservaStock>>> SearchAsync(TiposReservaStockSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposReservaStockInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposReservaStockUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_RESERVA);
}