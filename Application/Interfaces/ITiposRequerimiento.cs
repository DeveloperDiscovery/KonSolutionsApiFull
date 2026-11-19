using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposRequerimientoRepository
{
    Task<IEnumerable<TiposRequerimiento>> ShowallAsync();
    Task<TiposRequerimiento?> ShowbyidAsync(string COD_TIPO_REQUERIMIENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposRequerimiento>> SearchAsync(TiposRequerimientoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposRequerimientoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposRequerimientoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_REQUERIMIENTO);
}

public interface ITiposRequerimientoService
{
    Task<ApiResponse<IEnumerable<TiposRequerimiento>>> GetAllAsync();
    Task<ApiResponse<TiposRequerimiento>> GetByIdAsync(string COD_TIPO_REQUERIMIENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposRequerimiento>>> SearchAsync(TiposRequerimientoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposRequerimientoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposRequerimientoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_REQUERIMIENTO);
}