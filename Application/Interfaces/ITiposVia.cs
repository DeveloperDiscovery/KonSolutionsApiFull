using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposViaRepository
{
    Task<IEnumerable<TiposVia>> ShowallAsync();
    Task<TiposVia?> ShowbyidAsync(string COD_TIPO_VIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposVia>> SearchAsync(TiposViaSearchDto dto);
    Task<WriteResult> InsertAsync(TiposViaInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposViaUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_VIA);
}

public interface ITiposViaService
{
    Task<ApiResponse<IEnumerable<TiposVia>>> GetAllAsync();
    Task<ApiResponse<TiposVia>> GetByIdAsync(string COD_TIPO_VIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposVia>>> SearchAsync(TiposViaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposViaInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposViaUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_VIA);
}