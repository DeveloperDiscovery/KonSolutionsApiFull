using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

// Tabla de asignación: todas las lecturas cuelgan del rol.
public interface IRolesUsuarioAlmacenesRepository
{
    Task<IEnumerable<RolesUsuarioAlmacenes>> ShowallAsync(string COD_ROL_USUARIO);
    /// <summary>Almacenes que el rol TODAVÍA no tiene. Lo resuelve el SP _DISPONIBLES y no la
    /// pantalla: la base ya sabe cuáles están anulados, y restar dos listas en el cliente
    /// obligaría a traerse el catálogo completo de almacenes sólo para descartarlo.</summary>
    Task<IEnumerable<RolesUsuarioAlmacenes>> DisponiblesAsync(string COD_ROL_USUARIO);
    Task<RolesUsuarioAlmacenes?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_ALMACEN);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_ROL_USUARIO);
    Task<IEnumerable<RolesUsuarioAlmacenes>> SearchAsync(RolesUsuarioAlmacenesSearchDto dto);
    Task<WriteResult> InsertAsync(RolesUsuarioAlmacenesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_ALMACEN);
}

public interface IRolesUsuarioAlmacenesService
{
    Task<ApiResponse<IEnumerable<RolesUsuarioAlmacenes>>> GetAllAsync(string COD_ROL_USUARIO);
    Task<ApiResponse<IEnumerable<RolesUsuarioAlmacenes>>> GetDisponiblesAsync(string COD_ROL_USUARIO);
    Task<ApiResponse<RolesUsuarioAlmacenes>> GetByIdAsync(string COD_ROL_USUARIO, string COD_ALMACEN);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_ROL_USUARIO);
    Task<ApiResponse<IEnumerable<RolesUsuarioAlmacenes>>> SearchAsync(RolesUsuarioAlmacenesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuarioAlmacenesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO, string COD_ALMACEN);

    /// <summary>Asigna varios almacenes de una vez. Sigue con los demás si uno falla y devuelve
    /// el detalle: en un lote, abortar al primer error deja el trabajo a medio hacer.</summary>
    Task<ApiResponse<RolesUsuarioAlmacenesLoteResult>> AsignarAsync(RolesUsuarioAlmacenesLoteDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<RolesUsuarioAlmacenesLoteResult>> DesasignarAsync(RolesUsuarioAlmacenesLoteDto dto);
}
