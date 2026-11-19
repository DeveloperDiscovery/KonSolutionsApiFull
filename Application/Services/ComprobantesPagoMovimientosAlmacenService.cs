using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Services;

public class ComprobantesPagoMovimientosAlmacenService : IComprobantesPagoMovimientosAlmacenService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ComprobantesPagoMovimientosAlmacenService);
    private readonly IComprobantesPagoMovimientosAlmacenRepository _repo;
    private readonly ErrorLogService _log;
    public ComprobantesPagoMovimientosAlmacenService(IComprobantesPagoMovimientosAlmacenRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ComprobantesPagoMovimientosAlmacen>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ComprobantesPagoMovimientosAlmacen>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ComprobantesPagoMovimientosAlmacen>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ComprobantesPagoMovimientosAlmacen>> GetByIdAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA_COMPROBANTE, decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_REGISTRO, NUM_SECUENCIA_COMPROBANTE, NUM_MOVIMIENTO_ALMACEN);
              return d is null ? ApiResponse<ComprobantesPagoMovimientosAlmacen>.Fail("No encontrado.") : ApiResponse<ComprobantesPagoMovimientosAlmacen>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ComprobantesPagoMovimientosAlmacen>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComprobantesPagoMovimientosAlmacen>>> SearchAsync(ComprobantesPagoMovimientosAlmacenSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ComprobantesPagoMovimientosAlmacen>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ComprobantesPagoMovimientosAlmacen>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ComprobantesPagoMovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA_COMPROBANTE, decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var r = await _repo.DeleteAsync(NUM_REGISTRO, NUM_SECUENCIA_COMPROBANTE, NUM_MOVIMIENTO_ALMACEN);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}