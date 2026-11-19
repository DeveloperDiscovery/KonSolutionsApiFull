using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUsuariosRepository
{
    Task<IEnumerable<Usuarios>> ShowallAsync();
    Task<Usuarios?> ShowbyidAsync(string COD_USUARIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string? search = null);
    Task<IEnumerable<Usuarios>> SearchAsync(UsuariosSearchDto dto);
    Task<WriteResult> InsertAsync(UsuariosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_USUARIO);
    Task ActualizarRegistroFacialAsync(string codUsuario, bool valor);
    Task<IEnumerable<dynamic>> EstadosAsync(UsuariosEstadosDto dto, string codUsuario, string codEstacion);
    Task<IEnumerable<dynamic>> PasswordAsync(UsuariosPasswordDto dto, string codUsuario, string codEstacion);
    Task<IEnumerable<dynamic>> PasswordResetAsync(UsuariosPasswordResetDto dto, string codUsuario, string codEstacion);
    Task<IEnumerable<dynamic>> ResetintentosAsync(UsuariosResetintentosDto dto, string codUsuario, string codEstacion);
    Task<UsuariosLoadCombosResult> LoadcombosAsync(int codTipoEstado);
}

public interface IUsuariosService
{
    Task<ApiResponse<IEnumerable<Usuarios>>> GetAllAsync();
    Task<ApiResponse<Usuarios>> GetByIdAsync(string COD_USUARIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string? search = null);
    Task<ApiResponse<IEnumerable<Usuarios>>> SearchAsync(UsuariosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(UsuariosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_USUARIO);
    Task<ApiResponse<IEnumerable<dynamic>>> EstadosAsync(UsuariosEstadosDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<IEnumerable<dynamic>>> PasswordAsync(UsuariosPasswordDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<IEnumerable<dynamic>>> PasswordResetAsync(UsuariosPasswordResetDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<IEnumerable<dynamic>>> ResetintentosAsync(UsuariosResetintentosDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<UsuariosLoadCombosResult>> LoadcombosAsync(int codTipoEstado);
}