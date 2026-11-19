using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUsuariosAlmacenesTiposMovimientoRepository
{
    Task<IEnumerable<UsuariosAlmacenesTiposMovimiento>> ShowallAsync();
    Task<UsuariosAlmacenesTiposMovimiento?> ShowbyidAsync(string COD_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<UsuariosAlmacenesTiposMovimiento>> SearchAsync(UsuariosAlmacenesTiposMovimientoSearchDto dto);
    Task<WriteResult> InsertAsync(UsuariosAlmacenesTiposMovimientoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO);
}

public interface IUsuariosAlmacenesTiposMovimientoService
{
    Task<ApiResponse<IEnumerable<UsuariosAlmacenesTiposMovimiento>>> GetAllAsync();
    Task<ApiResponse<UsuariosAlmacenesTiposMovimiento>> GetByIdAsync(string COD_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<UsuariosAlmacenesTiposMovimiento>>> SearchAsync(UsuariosAlmacenesTiposMovimientoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(UsuariosAlmacenesTiposMovimientoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO);
}