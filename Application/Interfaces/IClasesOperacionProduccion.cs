using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IClasesOperacionProduccionRepository
{
    Task<IEnumerable<ClasesOperacionProduccion>> ShowallAsync();
    Task<ClasesOperacionProduccion?> ShowbyidAsync(string COD_CLASE_OPERACION_PRODUCCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ClasesOperacionProduccion>> SearchAsync(ClasesOperacionProduccionSearchDto dto);
    Task<WriteResult> InsertAsync(ClasesOperacionProduccionInsertDto dto);
    Task<WriteResult> UpdateAsync(ClasesOperacionProduccionUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_CLASE_OPERACION_PRODUCCION);
}

public interface IClasesOperacionProduccionService
{
    Task<ApiResponse<IEnumerable<ClasesOperacionProduccion>>> GetAllAsync();
    Task<ApiResponse<ClasesOperacionProduccion>> GetByIdAsync(string COD_CLASE_OPERACION_PRODUCCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ClasesOperacionProduccion>>> SearchAsync(ClasesOperacionProduccionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ClasesOperacionProduccionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ClasesOperacionProduccionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_OPERACION_PRODUCCION);
}