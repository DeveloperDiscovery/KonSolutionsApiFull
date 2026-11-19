using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRolesUsuariosAplicacionesOpcionesBotonesRepository
{
    Task<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>> ShowallAsync();
    Task<RolesUsuariosAplicacionesOpcionesBotones?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>> SearchAsync(RolesUsuariosAplicacionesOpcionesBotonesSearchDto dto);
    Task<WriteResult> InsertAsync(RolesUsuariosAplicacionesOpcionesBotonesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(RolesUsuariosAplicacionesOpcionesBotonesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION);
    Task<TreeviewResult> TreeviewAsync(string codRolUsuario);
    Task<TreeviewLogosResult> TreeviewLogosAsync(string codRolUsuario);
    Task<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>> ShowByOpcionAsync(string codRolUsuario, string codAplicacion, string codOpcion);
}

public interface IRolesUsuariosAplicacionesOpcionesBotonesService
{
    Task<ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>>> GetAllAsync();
    Task<ApiResponse<RolesUsuariosAplicacionesOpcionesBotones>> GetByIdAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>>> SearchAsync(RolesUsuariosAplicacionesOpcionesBotonesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuariosAplicacionesOpcionesBotonesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(RolesUsuariosAplicacionesOpcionesBotonesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION);
    Task<ApiResponse<TreeviewResult>> TreeviewAsync(string codRolUsuario);
    Task<ApiResponse<TreeviewLogosResult>> TreeviewLogosAsync(string codRolUsuario);
    Task<ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>>> GetByOpcionAsync(string codRolUsuario, string codAplicacion, string codOpcion);
}