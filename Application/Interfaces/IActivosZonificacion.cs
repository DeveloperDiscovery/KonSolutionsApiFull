using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Zonificacion;

namespace KonSolutionsApi.Application.Interfaces;

public interface IActivosZonificacionRepository
{
    Task<IEnumerable<ActivosZonificacion>> ShowallAsync();
    Task<ActivosZonificacion?> ShowbyidAsync(string COD_ACTIVO, string COD_SUCURSAL, string COD_ZONIFICACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ActivosZonificacion>> SearchAsync(ActivosZonificacionSearchDto dto);
    Task<WriteResult> InsertAsync(ActivosZonificacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ActivosZonificacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ACTIVO, string COD_SUCURSAL, string COD_ZONIFICACION);
}

public interface IActivosZonificacionService
{
    Task<ApiResponse<IEnumerable<ActivosZonificacion>>> GetAllAsync();
    Task<ApiResponse<ActivosZonificacion>> GetByIdAsync(string COD_ACTIVO, string COD_SUCURSAL, string COD_ZONIFICACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ActivosZonificacion>>> SearchAsync(ActivosZonificacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ActivosZonificacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ActivosZonificacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ACTIVO, string COD_SUCURSAL, string COD_ZONIFICACION);
}