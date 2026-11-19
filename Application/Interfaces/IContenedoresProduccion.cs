using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Interfaces;

public interface IContenedoresProduccionRepository
{
    Task<IEnumerable<ContenedoresProduccion>> ShowallAsync();
    Task<ContenedoresProduccion?> ShowbyidAsync(int NUM_CONTENEDOR_PRODUCCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ContenedoresProduccion>> SearchAsync(ContenedoresProduccionSearchDto dto);
    Task<WriteResult> InsertAsync(ContenedoresProduccionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ContenedoresProduccionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_CONTENEDOR_PRODUCCION);
}

public interface IContenedoresProduccionService
{
    Task<ApiResponse<IEnumerable<ContenedoresProduccion>>> GetAllAsync();
    Task<ApiResponse<ContenedoresProduccion>> GetByIdAsync(int NUM_CONTENEDOR_PRODUCCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ContenedoresProduccion>>> SearchAsync(ContenedoresProduccionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ContenedoresProduccionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ContenedoresProduccionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_CONTENEDOR_PRODUCCION);
}