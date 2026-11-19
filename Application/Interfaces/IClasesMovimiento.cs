using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IClasesMovimientoRepository
{
    Task<IEnumerable<ClasesMovimiento>> ShowallAsync();
    Task<ClasesMovimiento?> ShowbyidAsync(string COD_CLASE_MOVIMIENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ClasesMovimiento>> SearchAsync(ClasesMovimientoSearchDto dto);
    Task<WriteResult> InsertAsync(ClasesMovimientoInsertDto dto);
    Task<WriteResult> UpdateAsync(ClasesMovimientoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_CLASE_MOVIMIENTO);
}

public interface IClasesMovimientoService
{
    Task<ApiResponse<IEnumerable<ClasesMovimiento>>> GetAllAsync();
    Task<ApiResponse<ClasesMovimiento>> GetByIdAsync(string COD_CLASE_MOVIMIENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ClasesMovimiento>>> SearchAsync(ClasesMovimientoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ClasesMovimientoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ClasesMovimientoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_MOVIMIENTO);
}