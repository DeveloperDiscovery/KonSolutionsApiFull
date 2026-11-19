using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPerfilesUsuarioRepository
{
    Task<IEnumerable<PerfilesUsuario>> ShowallAsync();
    Task<PerfilesUsuario?> ShowbyidAsync(string COD_PERFIL_USUARIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PerfilesUsuario>> SearchAsync(PerfilesUsuarioSearchDto dto);
    Task<WriteResult> InsertAsync(PerfilesUsuarioInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PerfilesUsuarioUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PERFIL_USUARIO);
    Task<PerfilesUsuarioLoadCombosResult> LoadcombosAsync(int codTipoEstado);
}

public interface IPerfilesUsuarioService
{
    Task<ApiResponse<IEnumerable<PerfilesUsuario>>> GetAllAsync();
    Task<ApiResponse<PerfilesUsuario>> GetByIdAsync(string COD_PERFIL_USUARIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PerfilesUsuario>>> SearchAsync(PerfilesUsuarioSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PerfilesUsuarioInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PerfilesUsuarioUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PERFIL_USUARIO);
    Task<ApiResponse<PerfilesUsuarioLoadCombosResult>> LoadcombosAsync(int codTipoEstado);
}