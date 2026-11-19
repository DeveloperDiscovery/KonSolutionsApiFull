using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Controlcalidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IControlCalidadMovimientosAlmacenRepository
{
    Task<IEnumerable<ControlCalidadMovimientosAlmacen>> ShowallAsync();
    Task<ControlCalidadMovimientosAlmacen?> ShowbyidAsync(int NUM_CONTROL_CALIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ControlCalidadMovimientosAlmacen>> SearchAsync(ControlCalidadMovimientosAlmacenSearchDto dto);
    Task<WriteResult> InsertAsync(ControlCalidadMovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ControlCalidadMovimientosAlmacenUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_CONTROL_CALIDAD);
}

public interface IControlCalidadMovimientosAlmacenService
{
    Task<ApiResponse<IEnumerable<ControlCalidadMovimientosAlmacen>>> GetAllAsync();
    Task<ApiResponse<ControlCalidadMovimientosAlmacen>> GetByIdAsync(int NUM_CONTROL_CALIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ControlCalidadMovimientosAlmacen>>> SearchAsync(ControlCalidadMovimientosAlmacenSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ControlCalidadMovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ControlCalidadMovimientosAlmacenUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_CONTROL_CALIDAD);
}