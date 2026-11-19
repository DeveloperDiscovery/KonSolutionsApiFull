using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposOrdenProduccionRepository
{
    Task<IEnumerable<TiposOrdenProduccion>> ShowallAsync();
    Task<TiposOrdenProduccion?> ShowbyidAsync(string COD_TIPO_ORDEN_PRODUCCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposOrdenProduccion>> SearchAsync(TiposOrdenProduccionSearchDto dto);
    Task<WriteResult> InsertAsync(TiposOrdenProduccionInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposOrdenProduccionUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_ORDEN_PRODUCCION);
}

public interface ITiposOrdenProduccionService
{
    Task<ApiResponse<IEnumerable<TiposOrdenProduccion>>> GetAllAsync();
    Task<ApiResponse<TiposOrdenProduccion>> GetByIdAsync(string COD_TIPO_ORDEN_PRODUCCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposOrdenProduccion>>> SearchAsync(TiposOrdenProduccionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposOrdenProduccionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposOrdenProduccionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_ORDEN_PRODUCCION);
}