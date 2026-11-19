using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMovimientosAlmacenRequerimientosVistaRepository
{
    Task<IEnumerable<MovimientoAlmacenRequerimiento>> DisponiblesAsync();
    Task<IEnumerable<MovimientoAlmacenRequerimiento>> AsignadosAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<IEnumerable<MovimientoAlmacenRequerimientoDetalle>> AsignadosDetalleAsync(decimal NUM_MOVIMIENTO_ALMACEN);

    /// <summary>Cobertura de UN requerimiento contra el stock: los dos resultados del SP de
    /// detalle de disponibles (líneas y stock seleccionado) en una sola llamada.</summary>
    Task<MovimientoAlmacenRequerimientoCobertura> CoberturaAsync(decimal numMovimiento, int numRequerimiento, string? criterio);

    // ── Escritura ────────────────────────────────────────────────────────────
    // Sin clase ni tipo de operación, a diferencia de órdenes de compra: el número de
    // requerimiento es único en toda la base, así que alcanza para identificarlo.
    Task<WriteResult> AsignarAsync(decimal numMovimiento, string lineas, string criterio, string codUsuario, string codEstacion);
    Task<WriteResult> QuitarAsync(decimal numMovimiento, string lineas, string codUsuario, string codEstacion);
}

public interface IMovimientosAlmacenRequerimientosVistaService
{
    Task<ApiResponse<IEnumerable<MovimientoAlmacenRequerimiento>>> DisponiblesAsync();
    Task<ApiResponse<IEnumerable<MovimientoAlmacenRequerimiento>>> AsignadosAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<IEnumerable<MovimientoAlmacenRequerimientoDetalle>>> AsignadosDetalleAsync(decimal NUM_MOVIMIENTO_ALMACEN);

    Task<ApiResponse<MovimientoAlmacenRequerimientoCobertura>> CoberturaAsync(decimal numMovimiento, int numRequerimiento, string? criterio);

    Task<ApiResponse<WriteResult>> AsignarAsync(decimal numMovimiento, string lineas, string criterio, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> QuitarAsync(decimal numMovimiento, string lineas, string codUsuario, string codEstacion);
}
