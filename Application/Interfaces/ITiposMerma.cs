using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposMermaRepository
{
    Task<IEnumerable<TiposMerma>> ShowallAsync();
    Task<TiposMerma?> ShowbyidAsync(string COD_TIPO_MERMA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposMerma>> SearchAsync(TiposMermaSearchDto dto);
    Task<WriteResult> InsertAsync(TiposMermaInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposMermaUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_MERMA);
}

public interface ITiposMermaService
{
    Task<ApiResponse<IEnumerable<TiposMerma>>> GetAllAsync();
    Task<ApiResponse<TiposMerma>> GetByIdAsync(string COD_TIPO_MERMA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposMerma>>> SearchAsync(TiposMermaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposMermaInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposMermaUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_MERMA);
}