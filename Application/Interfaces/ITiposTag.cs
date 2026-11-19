using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposTagRepository
{
    Task<IEnumerable<TiposTag>> ShowallAsync();
    Task<TiposTag?> ShowbyidAsync(string COD_TIPO_TAG);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposTag>> SearchAsync(TiposTagSearchDto dto);
    Task<WriteResult> InsertAsync(TiposTagInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposTagUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_TAG);
}

public interface ITiposTagService
{
    Task<ApiResponse<IEnumerable<TiposTag>>> GetAllAsync();
    Task<ApiResponse<TiposTag>> GetByIdAsync(string COD_TIPO_TAG);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposTag>>> SearchAsync(TiposTagSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposTagInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposTagUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_TAG);
}