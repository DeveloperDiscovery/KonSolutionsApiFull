using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Controlcalidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IControlCalidadMovimientosAlmacenEspecificacionesRepository
{
    Task<IEnumerable<ControlCalidadMovimientosAlmacenEspecificaciones>> ShowallAsync();
    Task<ControlCalidadMovimientosAlmacenEspecificaciones?> ShowbyidAsync(int NUM_CONTROL_CALIDAD, string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ControlCalidadMovimientosAlmacenEspecificaciones>> SearchAsync(ControlCalidadMovimientosAlmacenEspecificacionesSearchDto dto);
    Task<WriteResult> InsertAsync(ControlCalidadMovimientosAlmacenEspecificacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ControlCalidadMovimientosAlmacenEspecificacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_CONTROL_CALIDAD, string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO);
}

public interface IControlCalidadMovimientosAlmacenEspecificacionesService
{
    Task<ApiResponse<IEnumerable<ControlCalidadMovimientosAlmacenEspecificaciones>>> GetAllAsync();
    Task<ApiResponse<ControlCalidadMovimientosAlmacenEspecificaciones>> GetByIdAsync(int NUM_CONTROL_CALIDAD, string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ControlCalidadMovimientosAlmacenEspecificaciones>>> SearchAsync(ControlCalidadMovimientosAlmacenEspecificacionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ControlCalidadMovimientosAlmacenEspecificacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ControlCalidadMovimientosAlmacenEspecificacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_CONTROL_CALIDAD, string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO);
}