using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISubElementosCostoRepository
{
    Task<IEnumerable<SubElementosCosto>> ShowallAsync();
    Task<SubElementosCosto?> ShowbyidAsync(string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SubElementosCosto>> SearchAsync(SubElementosCostoSearchDto dto);
    Task<WriteResult> InsertAsync(SubElementosCostoInsertDto dto);
    Task<WriteResult> UpdateAsync(SubElementosCostoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO);
}

public interface ISubElementosCostoService
{
    Task<ApiResponse<IEnumerable<SubElementosCosto>>> GetAllAsync();
    Task<ApiResponse<SubElementosCosto>> GetByIdAsync(string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SubElementosCosto>>> SearchAsync(SubElementosCostoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SubElementosCostoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(SubElementosCostoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO);
}