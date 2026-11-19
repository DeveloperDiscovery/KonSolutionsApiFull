using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class MovimientosAlmacenDetalleService : IMovimientosAlmacenDetalleService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(MovimientosAlmacenDetalleService);
    private readonly IMovimientosAlmacenDetalleRepository _repo;
    private readonly ErrorLogService _log;
    public MovimientosAlmacenDetalleService(IMovimientosAlmacenDetalleRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<MovimientosAlmacenDetalle>>> GetAllAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var d = (await _repo.ShowallAsync(NUM_MOVIMIENTO_ALMACEN)).ToList();
              return ApiResponse<IEnumerable<MovimientosAlmacenDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<MovimientosAlmacenDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MovimientosAlmacenDetalle>> GetByIdAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_MOVIMIENTO_ALMACEN, NUM_SECUENCIA);
              return d is null ? ApiResponse<MovimientosAlmacenDetalle>.Fail("No encontrado.") : ApiResponse<MovimientosAlmacenDetalle>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<MovimientosAlmacenDetalle>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientosAlmacenDetalle>>> SearchAsync(MovimientosAlmacenDetalleSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<MovimientosAlmacenDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<MovimientosAlmacenDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(MovimientosAlmacenDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(MovimientosAlmacenDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientosAlmacenDetalleStockDisponible>>> StockDisponibleAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var d = (await _repo.StockDisponibleAsync(NUM_MOVIMIENTO_ALMACEN)).ToList();
              return ApiResponse<IEnumerable<MovimientosAlmacenDetalleStockDisponible>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(StockDisponibleAsync));
              return ApiResponse<IEnumerable<MovimientosAlmacenDetalleStockDisponible>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MovimientosAlmacenDetalleCombosLinea>> CombosLineaAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { return ApiResponse<MovimientosAlmacenDetalleCombosLinea>.Ok(await _repo.CombosLineaAsync(NUM_MOVIMIENTO_ALMACEN)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CombosLineaAsync));
              return ApiResponse<MovimientosAlmacenDetalleCombosLinea>.Fail(ex.Message); }
    }

    // ── Lecturas de etiquetas (RFID / codigo de barras) ───────────────────────────────────────

    public async Task<ApiResponse<IEnumerable<MovimientosAlmacenEtiqueta>>> EtiquetasAsync(decimal NUM_MOVIMIENTO_ALMACEN, int? NUM_SECUENCIA, string? search)
    {
        try { var d = (await _repo.EtiquetasAsync(NUM_MOVIMIENTO_ALMACEN, NUM_SECUENCIA, search)).ToList();
              return ApiResponse<IEnumerable<MovimientosAlmacenEtiqueta>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(EtiquetasAsync));
              return ApiResponse<IEnumerable<MovimientosAlmacenEtiqueta>>.Fail(ex.Message); }
    }

    /// <summary>Una lectura. Un rechazo del SP (ErrorNumber) vuelve como respuesta fallida CON el numero en Data:
    /// la pantalla lo necesita para pedir el peso (50708) o preguntar si se fuerza (50706 / 50707).</summary>
    public async Task<ApiResponse<MovimientosAlmacenLecturaResultado>> LeerAsync(MovimientosAlmacenLecturaDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var r = await _repo.LeerAsync(dto, codUsuario, codEstacion);
            if (r.ErrorNumber is null) return ApiResponse<MovimientosAlmacenLecturaResultado>.Ok(r, "Lectura registrada.");
            var mensaje = string.IsNullOrWhiteSpace(r.DES_ADVERTENCIA) ? "No se pudo registrar la lectura." : r.DES_ADVERTENCIA!;
            return new ApiResponse<MovimientosAlmacenLecturaResultado>
            {
                Success = false, Message = mensaje,
                Data = new MovimientosAlmacenLecturaResultado { ErrorNumber = r.ErrorNumber }
            };
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LeerAsync));
              return ApiResponse<MovimientosAlmacenLecturaResultado>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> EtiquetaUpdateAsync(MovimientosAlmacenEtiquetaUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.EtiquetaUpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Etiqueta modificada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(EtiquetaUpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> EtiquetaDeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_SECUENCIA, string COD_STICKER_RFID, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.EtiquetaDeleteAsync(NUM_MOVIMIENTO_ALMACEN, NUM_SECUENCIA, COD_STICKER_RFID, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Etiqueta quitada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(EtiquetaDeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientosAlmacenLecturaPendiente>>> LecturasPendientesAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var d = (await _repo.LecturasPendientesAsync(NUM_MOVIMIENTO_ALMACEN)).ToList();
              return ApiResponse<IEnumerable<MovimientosAlmacenLecturaPendiente>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LecturasPendientesAsync));
              return ApiResponse<IEnumerable<MovimientosAlmacenLecturaPendiente>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(NUM_MOVIMIENTO_ALMACEN, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}