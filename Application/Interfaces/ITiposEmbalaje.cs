using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposEmbalajeRepository
{
    Task<IEnumerable<TiposEmbalaje>> ShowallAsync();
    Task<TiposEmbalaje?> ShowbyidAsync(string COD_TIPO_EMBALAJE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposEmbalaje>> SearchAsync(TiposEmbalajeSearchDto dto);
    Task<WriteResult> InsertAsync(TiposEmbalajeInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposEmbalajeUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_EMBALAJE);
}

public interface ITiposEmbalajeService
{
    Task<ApiResponse<IEnumerable<TiposEmbalaje>>> GetAllAsync();
    Task<ApiResponse<TiposEmbalaje>> GetByIdAsync(string COD_TIPO_EMBALAJE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposEmbalaje>>> SearchAsync(TiposEmbalajeSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposEmbalajeInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposEmbalajeUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_EMBALAJE);
}