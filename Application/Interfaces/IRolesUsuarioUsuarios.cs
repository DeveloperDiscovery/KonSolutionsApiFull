using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRolesUsuarioUsuariosRepository
{
    Task<IEnumerable<RolesUsuarioUsuarios>> ShowallAsync();
    Task<IEnumerable<RolesUsuarioUsuarios>> ShowByRolAsync(string codRol);
    Task<IEnumerable<RolesUsuarioUsuarios>> ShowbyusuarioAsync(string codUsuario);
    Task<RolesUsuarioUsuarios?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_USUARIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RolesUsuarioUsuarios>> SearchAsync(RolesUsuarioUsuariosSearchDto dto);
    Task<WriteResult> InsertAsync(RolesUsuarioUsuariosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_USUARIO);
}

public interface IRolesUsuarioUsuariosService
{
    Task<ApiResponse<IEnumerable<RolesUsuarioUsuarios>>> GetAllAsync();
    Task<ApiResponse<IEnumerable<RolesUsuarioUsuarios>>> GetByRolAsync(string codRol);
    Task<ApiResponse<IEnumerable<RolesUsuarioUsuarios>>> GetByUsuarioAsync(string codUsuario);
    Task<ApiResponse<RolesUsuarioUsuarios>> GetByIdAsync(string COD_ROL_USUARIO, string COD_USUARIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RolesUsuarioUsuarios>>> SearchAsync(RolesUsuarioUsuariosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuarioUsuariosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO, string COD_USUARIO);
}