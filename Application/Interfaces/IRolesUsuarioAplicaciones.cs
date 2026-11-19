using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRolesUsuarioAplicacionesRepository
{
    Task<IEnumerable<RolesUsuarioAplicaciones>> ShowallAsync();
    Task<RolesUsuarioAplicaciones?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_APLICACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RolesUsuarioAplicaciones>> SearchAsync(RolesUsuarioAplicacionesSearchDto dto);
    Task<WriteResult> InsertAsync(RolesUsuarioAplicacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(RolesUsuarioAplicacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_APLICACION);
}

public interface IRolesUsuarioAplicacionesService
{
    Task<ApiResponse<IEnumerable<RolesUsuarioAplicaciones>>> GetAllAsync();
    Task<ApiResponse<RolesUsuarioAplicaciones>> GetByIdAsync(string COD_ROL_USUARIO, string COD_APLICACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RolesUsuarioAplicaciones>>> SearchAsync(RolesUsuarioAplicacionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuarioAplicacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(RolesUsuarioAplicacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO, string COD_APLICACION);
}