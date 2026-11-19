using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposPersonalRepository
{
    Task<IEnumerable<TiposPersonal>> ShowallAsync();
    Task<TiposPersonal?> ShowbyidAsync(string COD_TIPO_PERSONAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposPersonal>> SearchAsync(TiposPersonalSearchDto dto);
    Task<WriteResult> InsertAsync(TiposPersonalInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposPersonalUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_PERSONAL);
}

public interface ITiposPersonalService
{
    Task<ApiResponse<IEnumerable<TiposPersonal>>> GetAllAsync();
    Task<ApiResponse<TiposPersonal>> GetByIdAsync(string COD_TIPO_PERSONAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposPersonal>>> SearchAsync(TiposPersonalSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposPersonalInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposPersonalUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_PERSONAL);
}