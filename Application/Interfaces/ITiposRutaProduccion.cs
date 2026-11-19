using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposRutaProduccionRepository
{
    Task<IEnumerable<TiposRutaProduccion>> ShowallAsync();
    Task<TiposRutaProduccion?> ShowbyidAsync(string COD_TIPO_RUTA_PRODUCCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposRutaProduccion>> SearchAsync(TiposRutaProduccionSearchDto dto);
    Task<WriteResult> InsertAsync(TiposRutaProduccionInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposRutaProduccionUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_RUTA_PRODUCCION);
}

public interface ITiposRutaProduccionService
{
    Task<ApiResponse<IEnumerable<TiposRutaProduccion>>> GetAllAsync();
    Task<ApiResponse<TiposRutaProduccion>> GetByIdAsync(string COD_TIPO_RUTA_PRODUCCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposRutaProduccion>>> SearchAsync(TiposRutaProduccionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposRutaProduccionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposRutaProduccionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_RUTA_PRODUCCION);
}