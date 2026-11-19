using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;

namespace KonSolutionsApi.Application.Interfaces;

public interface IActivosTagRepository
{
    Task<IEnumerable<ActivosTag>> ShowallAsync();
    Task<ActivosTag?> ShowbyidAsync(string COD_ACTIVO_TAG);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ActivosTag>> SearchAsync(ActivosTagSearchDto dto);
    Task<WriteResult> InsertAsync(ActivosTagInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ActivosTagUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ACTIVO_TAG);
}

public interface IActivosTagService
{
    Task<ApiResponse<IEnumerable<ActivosTag>>> GetAllAsync();
    Task<ApiResponse<ActivosTag>> GetByIdAsync(string COD_ACTIVO_TAG);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ActivosTag>>> SearchAsync(ActivosTagSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ActivosTagInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ActivosTagUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ACTIVO_TAG);
}