using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUsuariosAlmacenesRepository
{
    Task<IEnumerable<UsuariosAlmacenes>> ShowallAsync();
    Task<UsuariosAlmacenes?> ShowbyidAsync(string COD_USUARIO, string COD_ALMACEN);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<UsuariosAlmacenes>> SearchAsync(UsuariosAlmacenesSearchDto dto);
    Task<WriteResult> InsertAsync(UsuariosAlmacenesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_USUARIO, string COD_ALMACEN);
}

public interface IUsuariosAlmacenesService
{
    Task<ApiResponse<IEnumerable<UsuariosAlmacenes>>> GetAllAsync();
    Task<ApiResponse<UsuariosAlmacenes>> GetByIdAsync(string COD_USUARIO, string COD_ALMACEN);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<UsuariosAlmacenes>>> SearchAsync(UsuariosAlmacenesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(UsuariosAlmacenesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_USUARIO, string COD_ALMACEN);
}