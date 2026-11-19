using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRolesUsuarioRepository
{
    Task<IEnumerable<RolesUsuario>> ShowallAsync();
    Task<RolesUsuario?> ShowbyidAsync(string COD_ROL_USUARIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RolesUsuario>> SearchAsync(RolesUsuarioSearchDto dto);
    Task<WriteResult> InsertAsync(RolesUsuarioInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(RolesUsuarioUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO);
}

public interface IRolesUsuarioService
{
    Task<ApiResponse<IEnumerable<RolesUsuario>>> GetAllAsync();
    Task<ApiResponse<RolesUsuario>> GetByIdAsync(string COD_ROL_USUARIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RolesUsuario>>> SearchAsync(RolesUsuarioSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuarioInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(RolesUsuarioUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO);
}