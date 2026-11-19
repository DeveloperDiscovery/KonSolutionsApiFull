using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

// Las lecturas piden rol Y almacén: el permiso es "este tipo de movimiento, en este almacén".
public interface IRolesUsuarioAlmacenesTiposMovimientoRepository
{
    Task<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>> ShowallAsync(string COD_ROL_USUARIO, string COD_ALMACEN);
    /// <summary>Tipos habilitados en el almacén que el rol todavía no tiene. El universo lo
    /// define LOGISTICA.ALMACENES_TIPOS_MOVIMIENTO, no el catálogo general de tipos.</summary>
    Task<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>> DisponiblesAsync(string COD_ROL_USUARIO, string COD_ALMACEN);
    Task<RolesUsuarioAlmacenesTiposMovimiento?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_ROL_USUARIO, string COD_ALMACEN);
    Task<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>> SearchAsync(RolesUsuarioAlmacenesTiposMovimientoSearchDto dto);
    Task<WriteResult> InsertAsync(RolesUsuarioAlmacenesTiposMovimientoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO);
}

public interface IRolesUsuarioAlmacenesTiposMovimientoService
{
    Task<ApiResponse<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>>> GetAllAsync(string COD_ROL_USUARIO, string COD_ALMACEN);
    Task<ApiResponse<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>>> GetDisponiblesAsync(string COD_ROL_USUARIO, string COD_ALMACEN);
    Task<ApiResponse<RolesUsuarioAlmacenesTiposMovimiento>> GetByIdAsync(string COD_ROL_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_ROL_USUARIO, string COD_ALMACEN);
    Task<ApiResponse<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>>> SearchAsync(RolesUsuarioAlmacenesTiposMovimientoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuarioAlmacenesTiposMovimientoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO);

    Task<ApiResponse<RolesUsuarioAlmacenesTiposMovimientoLoteResult>> AsignarAsync(RolesUsuarioAlmacenesTiposMovimientoLoteDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<RolesUsuarioAlmacenesTiposMovimientoLoteResult>> DesasignarAsync(RolesUsuarioAlmacenesTiposMovimientoLoteDto dto);
}
