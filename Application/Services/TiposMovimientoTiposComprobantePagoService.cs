using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class TiposMovimientoTiposComprobantePagoService : ITiposMovimientoTiposComprobantePagoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposMovimientoTiposComprobantePagoService);
    private readonly ITiposMovimientoTiposComprobantePagoRepository _repo;
    private readonly ErrorLogService _log;
    public TiposMovimientoTiposComprobantePagoService(ITiposMovimientoTiposComprobantePagoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposMovimientoTiposComprobantePago>>> GetAllAsync(string codClaseMovimiento, string codTipoMovimiento)
    {
        try { var d = (await _repo.ShowallAsync(codClaseMovimiento, codTipoMovimiento)).ToList();
              return ApiResponse<IEnumerable<TiposMovimientoTiposComprobantePago>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposMovimientoTiposComprobantePago>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposMovimientoTiposComprobantePago>> GetByIdAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO, string COD_TIPO_COMPROBANTE_PAGO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO, COD_TIPO_COMPROBANTE_PAGO);
              return d is null ? ApiResponse<TiposMovimientoTiposComprobantePago>.Fail("No encontrado.") : ApiResponse<TiposMovimientoTiposComprobantePago>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposMovimientoTiposComprobantePago>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codClaseMovimiento, string codTipoMovimiento)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(codClaseMovimiento, codTipoMovimiento)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposMovimientoTiposComprobantePago>>> SearchAsync(string codClaseMovimiento, string codTipoMovimiento, TiposMovimientoTiposComprobantePagoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(codClaseMovimiento, codTipoMovimiento, dto)).ToList();
              return ApiResponse<IEnumerable<TiposMovimientoTiposComprobantePago>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposMovimientoTiposComprobantePago>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposMovimientoTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(TiposMovimientoTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO, string COD_TIPO_COMPROBANTE_PAGO)
    {
        try { var r = await _repo.DeleteAsync(COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO, COD_TIPO_COMPROBANTE_PAGO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
