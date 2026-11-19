using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUsuariosAutorizadoresRepository
{
    Task<IEnumerable<UsuariosAutorizadores>> ShowallAsync();
    Task<UsuariosAutorizadores?> ShowbyidAsync(string COD_TIPO_AUTORIZACION, string COD_USUARIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<UsuariosAutorizadores>> SearchAsync(UsuariosAutorizadoresSearchDto dto);
    Task<WriteResult> InsertAsync(UsuariosAutorizadoresInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(UsuariosAutorizadoresUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_AUTORIZACION, string COD_USUARIO);
}

public interface IUsuariosAutorizadoresService
{
    Task<ApiResponse<IEnumerable<UsuariosAutorizadores>>> GetAllAsync();
    Task<ApiResponse<UsuariosAutorizadores>> GetByIdAsync(string COD_TIPO_AUTORIZACION, string COD_USUARIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<UsuariosAutorizadores>>> SearchAsync(UsuariosAutorizadoresSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(UsuariosAutorizadoresInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(UsuariosAutorizadoresUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_AUTORIZACION, string COD_USUARIO);
}