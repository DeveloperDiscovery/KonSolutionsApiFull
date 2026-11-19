using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Controlcalidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IControlCalidadProduccionEspecificacionesRepository
{
    Task<IEnumerable<ControlCalidadProduccionEspecificaciones>> ShowallAsync();
    Task<ControlCalidadProduccionEspecificaciones?> ShowbyidAsync(int NUM_CONTROL_CALIDAD, string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ControlCalidadProduccionEspecificaciones>> SearchAsync(ControlCalidadProduccionEspecificacionesSearchDto dto);
    Task<WriteResult> InsertAsync(ControlCalidadProduccionEspecificacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ControlCalidadProduccionEspecificacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_CONTROL_CALIDAD, string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO);
}

public interface IControlCalidadProduccionEspecificacionesService
{
    Task<ApiResponse<IEnumerable<ControlCalidadProduccionEspecificaciones>>> GetAllAsync();
    Task<ApiResponse<ControlCalidadProduccionEspecificaciones>> GetByIdAsync(int NUM_CONTROL_CALIDAD, string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ControlCalidadProduccionEspecificaciones>>> SearchAsync(ControlCalidadProduccionEspecificacionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ControlCalidadProduccionEspecificacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ControlCalidadProduccionEspecificacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_CONTROL_CALIDAD, string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO);
}