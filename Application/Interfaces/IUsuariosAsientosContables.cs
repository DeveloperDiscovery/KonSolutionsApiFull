using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUsuariosAsientosContablesRepository
{
    Task<IEnumerable<UsuariosAsientosContables>> ShowallAsync();
    Task<UsuariosAsientosContables?> ShowbyidAsync(string COD_USUARIO, string COD_ASIENTO_CONTABLE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<UsuariosAsientosContables>> SearchAsync(UsuariosAsientosContablesSearchDto dto);
    Task<WriteResult> InsertAsync(UsuariosAsientosContablesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_USUARIO, string COD_ASIENTO_CONTABLE);
}

public interface IUsuariosAsientosContablesService
{
    Task<ApiResponse<IEnumerable<UsuariosAsientosContables>>> GetAllAsync();
    Task<ApiResponse<UsuariosAsientosContables>> GetByIdAsync(string COD_USUARIO, string COD_ASIENTO_CONTABLE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<UsuariosAsientosContables>>> SearchAsync(UsuariosAsientosContablesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(UsuariosAsientosContablesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_USUARIO, string COD_ASIENTO_CONTABLE);
}