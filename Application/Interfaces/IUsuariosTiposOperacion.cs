using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUsuariosTiposOperacionRepository
{
    Task<IEnumerable<UsuariosTiposOperacion>> ShowallAsync();
    Task<UsuariosTiposOperacion?> ShowbyidAsync(string COD_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<UsuariosTiposOperacion>> SearchAsync(UsuariosTiposOperacionSearchDto dto);
    Task<WriteResult> InsertAsync(UsuariosTiposOperacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION);
}

public interface IUsuariosTiposOperacionService
{
    Task<ApiResponse<IEnumerable<UsuariosTiposOperacion>>> GetAllAsync();
    Task<ApiResponse<UsuariosTiposOperacion>> GetByIdAsync(string COD_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<UsuariosTiposOperacion>>> SearchAsync(UsuariosTiposOperacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(UsuariosTiposOperacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION);
}