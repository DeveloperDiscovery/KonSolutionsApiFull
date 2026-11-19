using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposOperacionProduccionRepository
{
    Task<IEnumerable<TiposOperacionProduccion>> ShowallAsync();
    Task<TiposOperacionProduccion?> ShowbyidAsync(string COD_TIPO_OPERACION_PRODUCCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposOperacionProduccion>> SearchAsync(TiposOperacionProduccionSearchDto dto);
    Task<WriteResult> InsertAsync(TiposOperacionProduccionInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposOperacionProduccionUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_OPERACION_PRODUCCION);
}

public interface ITiposOperacionProduccionService
{
    Task<ApiResponse<IEnumerable<TiposOperacionProduccion>>> GetAllAsync();
    Task<ApiResponse<TiposOperacionProduccion>> GetByIdAsync(string COD_TIPO_OPERACION_PRODUCCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposOperacionProduccion>>> SearchAsync(TiposOperacionProduccionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposOperacionProduccionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposOperacionProduccionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_OPERACION_PRODUCCION);
}