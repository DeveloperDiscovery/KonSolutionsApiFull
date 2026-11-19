using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IElementosCostoRepository
{
    Task<IEnumerable<ElementosCosto>> ShowallAsync();
    Task<ElementosCosto?> ShowbyidAsync(string COD_ELEMENTO_COSTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ElementosCosto>> SearchAsync(ElementosCostoSearchDto dto);
    Task<WriteResult> InsertAsync(ElementosCostoInsertDto dto);
    Task<WriteResult> UpdateAsync(ElementosCostoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_ELEMENTO_COSTO);
}

public interface IElementosCostoService
{
    Task<ApiResponse<IEnumerable<ElementosCosto>>> GetAllAsync();
    Task<ApiResponse<ElementosCosto>> GetByIdAsync(string COD_ELEMENTO_COSTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ElementosCosto>>> SearchAsync(ElementosCostoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ElementosCostoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ElementosCostoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ELEMENTO_COSTO);
}