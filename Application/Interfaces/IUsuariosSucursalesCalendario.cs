using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUsuariosSucursalesCalendarioRepository
{
    Task<IEnumerable<UsuariosSucursalesCalendario>> ShowallAsync();
    Task<UsuariosSucursalesCalendario?> ShowbyidAsync(string COD_USUARIO, string COD_SUCURSAL, DateTime FEC_INICIAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<UsuariosSucursalesCalendario>> SearchAsync(UsuariosSucursalesCalendarioSearchDto dto);
    Task<WriteResult> InsertAsync(UsuariosSucursalesCalendarioInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(UsuariosSucursalesCalendarioUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_USUARIO, string COD_SUCURSAL, DateTime FEC_INICIAL);
}

public interface IUsuariosSucursalesCalendarioService
{
    Task<ApiResponse<IEnumerable<UsuariosSucursalesCalendario>>> GetAllAsync();
    Task<ApiResponse<UsuariosSucursalesCalendario>> GetByIdAsync(string COD_USUARIO, string COD_SUCURSAL, DateTime FEC_INICIAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<UsuariosSucursalesCalendario>>> SearchAsync(UsuariosSucursalesCalendarioSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(UsuariosSucursalesCalendarioInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(UsuariosSucursalesCalendarioUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_USUARIO, string COD_SUCURSAL, DateTime FEC_INICIAL);
}