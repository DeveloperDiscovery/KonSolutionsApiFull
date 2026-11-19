using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Interfaces;

public interface IControlCalidadProduccionRepository
{
    Task<IEnumerable<ControlCalidadProduccion>> ShowallAsync();
    Task<ControlCalidadProduccion?> ShowbyidAsync(int NUM_CONTROL_CALIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ControlCalidadProduccion>> SearchAsync(ControlCalidadProduccionSearchDto dto);
    Task<WriteResult> InsertAsync(ControlCalidadProduccionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ControlCalidadProduccionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_CONTROL_CALIDAD);
}

public interface IControlCalidadProduccionService
{
    Task<ApiResponse<IEnumerable<ControlCalidadProduccion>>> GetAllAsync();
    Task<ApiResponse<ControlCalidadProduccion>> GetByIdAsync(int NUM_CONTROL_CALIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ControlCalidadProduccion>>> SearchAsync(ControlCalidadProduccionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ControlCalidadProduccionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ControlCalidadProduccionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_CONTROL_CALIDAD);
}