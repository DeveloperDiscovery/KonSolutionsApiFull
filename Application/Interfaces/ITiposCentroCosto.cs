using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposCentroCostoRepository
{
    Task<IEnumerable<TiposCentroCosto>> ShowallAsync();
    Task<TiposCentroCosto?> ShowbyidAsync(string COD_TIPO_CENTRO_COSTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposCentroCosto>> SearchAsync(TiposCentroCostoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposCentroCostoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposCentroCostoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_CENTRO_COSTO);
}

public interface ITiposCentroCostoService
{
    Task<ApiResponse<IEnumerable<TiposCentroCosto>>> GetAllAsync();
    Task<ApiResponse<TiposCentroCosto>> GetByIdAsync(string COD_TIPO_CENTRO_COSTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposCentroCosto>>> SearchAsync(TiposCentroCostoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposCentroCostoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposCentroCostoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CENTRO_COSTO);
}