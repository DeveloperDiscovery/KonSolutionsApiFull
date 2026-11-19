using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IClasesCostoRepository
{
    Task<IEnumerable<ClasesCosto>> ShowallAsync();
    Task<ClasesCosto?> ShowbyidAsync(string COD_CLASE_COSTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ClasesCosto>> SearchAsync(ClasesCostoSearchDto dto);
    Task<WriteResult> InsertAsync(ClasesCostoInsertDto dto);
    Task<WriteResult> UpdateAsync(ClasesCostoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_CLASE_COSTO);
}

public interface IClasesCostoService
{
    Task<ApiResponse<IEnumerable<ClasesCosto>>> GetAllAsync();
    Task<ApiResponse<ClasesCosto>> GetByIdAsync(string COD_CLASE_COSTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ClasesCosto>>> SearchAsync(ClasesCostoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ClasesCostoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ClasesCostoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_COSTO);
}