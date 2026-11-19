using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposCostoRepository
{
    Task<IEnumerable<TiposCosto>> ShowallAsync();
    Task<TiposCosto?> ShowbyidAsync(string COD_TIPO_COSTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposCosto>> SearchAsync(TiposCostoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposCostoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposCostoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_COSTO);
}

public interface ITiposCostoService
{
    Task<ApiResponse<IEnumerable<TiposCosto>>> GetAllAsync();
    Task<ApiResponse<TiposCosto>> GetByIdAsync(string COD_TIPO_COSTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposCosto>>> SearchAsync(TiposCostoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposCostoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposCostoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_COSTO);
}