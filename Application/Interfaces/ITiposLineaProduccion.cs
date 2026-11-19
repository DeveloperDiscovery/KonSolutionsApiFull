using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposLineaProduccionRepository
{
    Task<IEnumerable<TiposLineaProduccion>> ShowallAsync();
    Task<TiposLineaProduccion?> ShowbyidAsync(string COD_TIPO_LINEA_PRODUCCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposLineaProduccion>> SearchAsync(TiposLineaProduccionSearchDto dto);
    Task<WriteResult> InsertAsync(TiposLineaProduccionInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposLineaProduccionUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_LINEA_PRODUCCION);
}

public interface ITiposLineaProduccionService
{
    Task<ApiResponse<IEnumerable<TiposLineaProduccion>>> GetAllAsync();
    Task<ApiResponse<TiposLineaProduccion>> GetByIdAsync(string COD_TIPO_LINEA_PRODUCCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposLineaProduccion>>> SearchAsync(TiposLineaProduccionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposLineaProduccionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposLineaProduccionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_LINEA_PRODUCCION);
}