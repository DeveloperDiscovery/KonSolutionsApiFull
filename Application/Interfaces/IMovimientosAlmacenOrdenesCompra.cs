using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMovimientosAlmacenOrdenesCompraRepository
{
    Task<IEnumerable<MovimientoAlmacenOrdenCompra>> DisponiblesAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<IEnumerable<MovimientoAlmacenOrdenCompraDetalle>> DisponiblesDetalleAsync(
        decimal NUM_MOVIMIENTO_ALMACEN, string codClaseOperacion, string codTipoOperacion, string lstPedidos);
    Task<IEnumerable<MovimientoAlmacenOrdenCompra>> AsignadosAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<IEnumerable<MovimientoAlmacenOrdenCompraDetalle>> AsignadosDetalleAsync(decimal NUM_MOVIMIENTO_ALMACEN);

    // ── Escritura ────────────────────────────────────────────────────────────
    Task<WriteResult> AsignarAsync(decimal numMovimiento, string codClaseOperacion, string codTipoOperacion, string lineas, string codUsuario, string codEstacion);
    Task<WriteResult> QuitarAsync(decimal numMovimiento, string codClaseOperacion, string codTipoOperacion, string lineas, string codUsuario, string codEstacion);

    /// <summary>Graba lote y ubicación en la línea del PEDIDO, que es de donde ASIGNA los
    /// lee. No pasa por el _UPDATE de la línea a propósito: ese recalcula importes y rechaza
    /// las líneas que vienen de una proforma.</summary>
    Task<WriteResult> GuardarLoteUbicacionAsync(string codClaseOperacion, string codTipoOperacion,
        decimal numPedidoComercial, int numSecuencia, string? numLoteControl, string? codUbicacionAlmacen);
}

public interface IMovimientosAlmacenOrdenesCompraService
{
    Task<ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompra>>> DisponiblesAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompraDetalle>>> DisponiblesDetalleAsync(
        decimal NUM_MOVIMIENTO_ALMACEN, string codClaseOperacion, string codTipoOperacion, string lstPedidos);
    Task<ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompra>>> AsignadosAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompraDetalle>>> AsignadosDetalleAsync(decimal NUM_MOVIMIENTO_ALMACEN);

    Task<ApiResponse<WriteResult>> AsignarAsync(decimal numMovimiento, string codClaseOperacion, string codTipoOperacion, string lineas, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> QuitarAsync(decimal numMovimiento, string codClaseOperacion, string codTipoOperacion, string lineas, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> GuardarLoteUbicacionAsync(LoteUbicacionLineaDto dto);
}
