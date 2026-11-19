using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICategoriasOperacionProduccionRepository
{
    Task<IEnumerable<CategoriasOperacionProduccion>> ShowallAsync();
    Task<CategoriasOperacionProduccion?> ShowbyidAsync(string COD_CATEGORIA_OPERACION_PRODUCCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CategoriasOperacionProduccion>> SearchAsync(CategoriasOperacionProduccionSearchDto dto);
    Task<WriteResult> InsertAsync(CategoriasOperacionProduccionInsertDto dto);
    Task<WriteResult> UpdateAsync(CategoriasOperacionProduccionUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_CATEGORIA_OPERACION_PRODUCCION);
}

public interface ICategoriasOperacionProduccionService
{
    Task<ApiResponse<IEnumerable<CategoriasOperacionProduccion>>> GetAllAsync();
    Task<ApiResponse<CategoriasOperacionProduccion>> GetByIdAsync(string COD_CATEGORIA_OPERACION_PRODUCCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CategoriasOperacionProduccion>>> SearchAsync(CategoriasOperacionProduccionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CategoriasOperacionProduccionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(CategoriasOperacionProduccionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CATEGORIA_OPERACION_PRODUCCION);
}