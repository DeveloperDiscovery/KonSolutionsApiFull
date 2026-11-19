using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Developer;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITablasCodigosRepository
{
    Task<IEnumerable<TablasCodigos>> ShowallAsync();
    Task<TablasCodigos?> ShowbyidAsync(string NAME_SCHEMA, string NAME_TABLA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TablasCodigos>> SearchAsync(TablasCodigosSearchDto dto);
    Task<WriteResult> InsertAsync(TablasCodigosInsertDto dto);
    Task<WriteResult> UpdateAsync(TablasCodigosUpdateDto dto);
    Task<WriteResult> DeleteAsync(string NAME_SCHEMA, string NAME_TABLA);
}

public interface ITablasCodigosService
{
    Task<ApiResponse<IEnumerable<TablasCodigos>>> GetAllAsync();
    Task<ApiResponse<TablasCodigos>> GetByIdAsync(string NAME_SCHEMA, string NAME_TABLA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TablasCodigos>>> SearchAsync(TablasCodigosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TablasCodigosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TablasCodigosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string NAME_SCHEMA, string NAME_TABLA);
}