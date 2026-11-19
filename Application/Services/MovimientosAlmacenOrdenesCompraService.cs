using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class MovimientosAlmacenOrdenesCompraService : IMovimientosAlmacenOrdenesCompraService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(MovimientosAlmacenOrdenesCompraService);
    private readonly IMovimientosAlmacenOrdenesCompraRepository _repo;
    private readonly ErrorLogService _log;
    public MovimientosAlmacenOrdenesCompraService(IMovimientosAlmacenOrdenesCompraRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompra>>> DisponiblesAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var d = (await _repo.DisponiblesAsync(NUM_MOVIMIENTO_ALMACEN)).ToList();
              return ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompra>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DisponiblesAsync));
              return ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompra>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompraDetalle>>> DisponiblesDetalleAsync(
        decimal NUM_MOVIMIENTO_ALMACEN, string codClaseOperacion, string codTipoOperacion, string lstPedidos)
    {
        try { var d = (await _repo.DisponiblesDetalleAsync(NUM_MOVIMIENTO_ALMACEN, codClaseOperacion, codTipoOperacion, lstPedidos)).ToList();
              return ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompraDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DisponiblesDetalleAsync));
              return ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompraDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompra>>> AsignadosAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var d = (await _repo.AsignadosAsync(NUM_MOVIMIENTO_ALMACEN)).ToList();
              return ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompra>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(AsignadosAsync));
              return ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompra>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompraDetalle>>> AsignadosDetalleAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var d = (await _repo.AsignadosDetalleAsync(NUM_MOVIMIENTO_ALMACEN)).ToList();
              return ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompraDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(AsignadosDetalleAsync));
              return ApiResponse<IEnumerable<MovimientoAlmacenOrdenCompraDetalle>>.Fail(ex.Message); }
    }

    // ── Escritura ────────────────────────────────────────────────────────────────

    /// <summary>Asignar. Antes de llegar a la base se comprueba que la cadena diga algo:
    /// ListaPedidoSecuencia.Leer la vuelve a interpretar igual que FNCN_SEPARAR_ID_SECUENCIA.
    ///
    /// Sin esta comprobación, una cadena mal formada no da error —la función de la base usa
    /// TRY_CONVERT— y el SP recorre una lista vacía: no asigna nada y devuelve éxito. El
    /// usuario ve el botón responder y nada moverse.</summary>
    public async Task<ApiResponse<WriteResult>> AsignarAsync(decimal numMovimiento, string codClaseOperacion,
        string codTipoOperacion, string lineas, string codUsuario, string codEstacion)
        => await EscribirAsync(nameof(AsignarAsync), codClaseOperacion, codTipoOperacion, lineas,
               () => _repo.AsignarAsync(numMovimiento, codClaseOperacion, codTipoOperacion, lineas, codUsuario, codEstacion));

    public async Task<ApiResponse<WriteResult>> QuitarAsync(decimal numMovimiento, string codClaseOperacion,
        string codTipoOperacion, string lineas, string codUsuario, string codEstacion)
        => await EscribirAsync(nameof(QuitarAsync), codClaseOperacion, codTipoOperacion, lineas,
               () => _repo.QuitarAsync(numMovimiento, codClaseOperacion, codTipoOperacion, lineas, codUsuario, codEstacion));

    public async Task<ApiResponse<WriteResult>> GuardarLoteUbicacionAsync(LoteUbicacionLineaDto dto)
    {
        try
        {
            var r = await _repo.GuardarLoteUbicacionAsync(dto.COD_CLASE_OPERACION, dto.COD_TIPO_OPERACION,
                        dto.NUM_PEDIDO_COMERCIAL, dto.NUM_SECUENCIA, dto.NUM_LOTE_CONTROL, dto.COD_UBICACION_ALMACEN);
            // FromWriteResult y no un if propio: además de traducir, dispara OnSpError, que
            // es lo que deja el error del SP en el log. Armándolo a mano se perdía.
            return ApiResponse<WriteResult>.FromWriteResult(r, "Lote y ubicación grabados.");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(GuardarLoteUbicacionAsync));
            return ApiResponse<WriteResult>.Fail(ex.Message);
        }
    }

    /// <summary>Lo común a asignar y quitar: validar la cadena, ejecutar, y traducir.</summary>
    private async Task<ApiResponse<WriteResult>> EscribirAsync(string metodo, string codClaseOperacion,
        string codTipoOperacion, string lineas, Func<Task<WriteResult>> accion)
    {
        try
        {
            // Sin clase o sin tipo el SP buscaría la línea del pedido en TODAS las clases de
            // operación y podría traer la de otro documento con el mismo número. Se corta
            // acá, con el motivo dicho, en vez de dejar que grabe algo equivocado.
            if (string.IsNullOrWhiteSpace(codClaseOperacion) || string.IsNullOrWhiteSpace(codTipoOperacion))
                return ApiResponse<WriteResult>.Fail(
                    "Falta la clase o el tipo de operación. Sin ellos no se puede saber a qué " +
                    "documento pertenecen las líneas: la numeración se repite entre clases.");

            var leidas = ListaPedidoSecuencia.Leer(lineas);
            if (leidas.Count == 0)
                return ApiResponse<WriteResult>.Fail(
                    "La lista de líneas llegó vacía o con un formato que la base no entiende. " +
                    "Se esperaba P<pedido>S<secuencia>S<secuencia>_P<pedido>S<secuencia>.");

            var r = await accion();
            return ApiResponse<WriteResult>.FromWriteResult(r);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, metodo);
            return ApiResponse<WriteResult>.Fail(ex.Message);
        }
    }
}
