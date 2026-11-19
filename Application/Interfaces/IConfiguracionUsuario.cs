using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IConfiguracionUsuarioRepository
{
    Task<IEnumerable<ConfiguracionUsuario>> ShowallAsync();
    Task<ConfiguracionUsuario?> ShowbyidAsync(string COD_USUARIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ConfiguracionUsuario>> SearchAsync(ConfiguracionUsuarioSearchDto dto);
    Task<WriteResult> SaveAsync(ConfiguracionUsuarioSaveDto dto, string codUsuarioSesion, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_USUARIO);
}

public interface IConfiguracionUsuarioService
{
    Task<ApiResponse<IEnumerable<ConfiguracionUsuario>>> GetAllAsync();
    Task<ApiResponse<ConfiguracionUsuario>> GetByIdAsync(string COD_USUARIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ConfiguracionUsuario>>> SearchAsync(ConfiguracionUsuarioSearchDto dto);
    Task<ApiResponse<WriteResult>> SaveAsync(ConfiguracionUsuarioSaveDto dto, string codUsuarioSesion, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_USUARIO);
}
