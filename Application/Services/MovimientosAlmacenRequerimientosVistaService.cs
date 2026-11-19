using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class MovimientosAlmacenRequerimientosVistaService : IMovimientosAlmacenRequerimientosVistaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(MovimientosAlmacenRequerimientosVistaService);
    private readonly IMovimientosAlmacenRequerimientosVistaRepository _repo;
    private readonly ErrorLogService _log;
    public MovimientosAlmacenRequerimientosVistaService(IMovimientosAlmacenRequerimientosVistaRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<MovimientoAlmacenRequerimiento>>> DisponiblesAsync()
    {
        try { var d = (await _repo.DisponiblesAsync()).ToList();
              return ApiResponse<IEnumerable<MovimientoAlmacenRequerimiento>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DisponiblesAsync));
              return ApiResponse<IEnumerable<MovimientoAlmacenRequerimiento>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientoAlmacenRequerimiento>>> AsignadosAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var d = (await _repo.AsignadosAsync(NUM_MOVIMIENTO_ALMACEN)).ToList();
              return ApiResponse<IEnumerable<MovimientoAlmacenRequerimiento>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(AsignadosAsync));
              return ApiResponse<IEnumerable<MovimientoAlmacenRequerimiento>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientoAlmacenRequerimientoDetalle>>> AsignadosDetalleAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var d = (await _repo.AsignadosDetalleAsync(NUM_MOVIMIENTO_ALMACEN)).ToList();
              return ApiResponse<IEnumerable<MovimientoAlmacenRequerimientoDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(AsignadosDetalleAsync));
              return ApiResponse<IEnumerable<MovimientoAlmacenRequerimientoDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MovimientoAlmacenRequerimientoCobertura>> CoberturaAsync(
        decimal numMovimiento, int numRequerimiento, string? criterio)
    {
        try { var d = await _repo.CoberturaAsync(numMovimiento, numRequerimiento, criterio);
              return ApiResponse<MovimientoAlmacenRequerimientoCobertura>.Ok(d, total: d.Lineas.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CoberturaAsync));
              return ApiResponse<MovimientoAlmacenRequerimientoCobertura>.Fail(ex.Message); }
    }

    // ── Escritura ────────────────────────────────────────────────────────────────

    public async Task<ApiResponse<WriteResult>> AsignarAsync(decimal numMovimiento, string lineas,
        string criterio, string codUsuario, string codEstacion)
        => await EscribirAsync(nameof(AsignarAsync), lineas,
               () => _repo.AsignarAsync(numMovimiento, lineas, criterio, codUsuario, codEstacion));

    public async Task<ApiResponse<WriteResult>> QuitarAsync(decimal numMovimiento, string lineas,
        string codUsuario, string codEstacion)
        => await EscribirAsync(nameof(QuitarAsync), lineas,
               () => _repo.QuitarAsync(numMovimiento, lineas, codUsuario, codEstacion));

    /// <summary>Lo común a asignar y quitar: validar la cadena, ejecutar, traducir.
    ///
    /// La validación NO es de adorno. La función de la base usa TRY_CONVERT, así que una
    /// cadena mal formada no da error: devuelve las secuencias en NULL, el SP recorre una
    /// lista vacía, no asigna nada y responde éxito. El usuario ve el botón responder y nada
    /// moverse. Es exactamente lo que pasó con el DESASIGNA de comerciales.
    ///
    /// Se lee con la MISMA clase que la armó, que es la única forma de que las dos
    /// interpretaciones no se separen con el tiempo.</summary>
    private async Task<ApiResponse<WriteResult>> EscribirAsync(string metodo, string lineas, Func<Task<WriteResult>> accion)
    {
        try
        {
            var leidas = ListaPedidoSecuencia.Leer(lineas);
            if (leidas.Count == 0)
                return ApiResponse<WriteResult>.Fail(
                    "La lista de líneas llegó vacía o con un formato que la base no entiende. " +
                    "Se esperaba P<requerimiento>S<secuencia>C<cantidad>, separando documentos con _.");

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
