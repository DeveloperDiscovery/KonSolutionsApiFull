using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

// Tabla de asignación: las lecturas piden almacén Y clase de movimiento, porque un tipo de
// movimiento sólo existe dentro de su clase.
public interface IAlmacenesTiposMovimientoRepository
{
    Task<IEnumerable<AlmacenesTiposMovimiento>> ShowallAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO);
    Task<AlmacenesTiposMovimiento?> ShowbyidAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO);
    Task<IEnumerable<AlmacenesTiposMovimiento>> SearchAsync(AlmacenesTiposMovimientoSearchDto dto);
    Task<WriteResult> InsertAsync(AlmacenesTiposMovimientoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO);
}

public interface IAlmacenesTiposMovimientoService
{
    Task<ApiResponse<IEnumerable<AlmacenesTiposMovimiento>>> GetAllAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO);
    Task<ApiResponse<AlmacenesTiposMovimiento>> GetByIdAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO);
    Task<ApiResponse<IEnumerable<AlmacenesTiposMovimiento>>> SearchAsync(AlmacenesTiposMovimientoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AlmacenesTiposMovimientoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO);

    /// <summary>Asigna varios tipos de una vez. Sigue con los demás si uno falla y devuelve
    /// el detalle: en un lote, abortar al primer error deja el trabajo a medio hacer.</summary>
    Task<ApiResponse<AlmacenesTiposMovimientoLoteResult>> AsignarAsync(AlmacenesTiposMovimientoLoteDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<AlmacenesTiposMovimientoLoteResult>> DesasignarAsync(AlmacenesTiposMovimientoLoteDto dto);
}
