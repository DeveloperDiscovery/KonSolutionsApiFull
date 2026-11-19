using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRolesUsuarioTiposOperacionRepository
{
    /// <summary>Asignados del rol DENTRO de una clase de operación: así lo pide el SP _SHOWALL.</summary>
    Task<IEnumerable<RolesUsuarioTiposOperacion>> ShowallAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION);
    /// <summary>Disponibles de TODAS las clases a la vez — el SP _DISPONIBLES sólo recibe el
    /// rol. El recorte por clase lo hace la pantalla, no la base.</summary>
    Task<IEnumerable<RolesUsuarioTiposOperacion>> DisponiblesAsync(string COD_ROL_USUARIO);
    Task<RolesUsuarioTiposOperacion?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION);
    Task<IEnumerable<RolesUsuarioTiposOperacion>> SearchAsync(RolesUsuarioTiposOperacionSearchDto dto);
    Task<WriteResult> InsertAsync(RolesUsuarioTiposOperacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION);
}

public interface IRolesUsuarioTiposOperacionService
{
    Task<ApiResponse<IEnumerable<RolesUsuarioTiposOperacion>>> GetAllAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION);
    Task<ApiResponse<IEnumerable<RolesUsuarioTiposOperacion>>> GetDisponiblesAsync(string COD_ROL_USUARIO);
    Task<ApiResponse<RolesUsuarioTiposOperacion>> GetByIdAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION);
    Task<ApiResponse<IEnumerable<RolesUsuarioTiposOperacion>>> SearchAsync(RolesUsuarioTiposOperacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuarioTiposOperacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION);

    Task<ApiResponse<RolesUsuarioTiposOperacionLoteResult>> AsignarAsync(RolesUsuarioTiposOperacionLoteDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<RolesUsuarioTiposOperacionLoteResult>> DesasignarAsync(RolesUsuarioTiposOperacionLoteDto dto);
}
