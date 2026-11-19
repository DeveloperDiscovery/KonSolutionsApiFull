using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class TiposMovimientoService : ITiposMovimientoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposMovimientoService);
    private readonly ITiposMovimientoRepository _repo;
    private readonly ErrorLogService _log;
    public TiposMovimientoService(ITiposMovimientoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposMovimiento>>> GetAllAsync(string codClaseMovimiento)
    {
        try { var d = (await _repo.ShowallAsync(codClaseMovimiento)).ToList();
              return ApiResponse<IEnumerable<TiposMovimiento>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposMovimiento>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposMovimiento>> GetByIdAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO);
              return d is null ? ApiResponse<TiposMovimiento>.Fail("No encontrado.") : ApiResponse<TiposMovimiento>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposMovimiento>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codClaseMovimiento)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(codClaseMovimiento)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposMovimiento>>> SearchAsync(TiposMovimientoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposMovimiento>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposMovimiento>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposMovimientoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(TiposMovimientoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    {
        try { var r = await _repo.DeleteAsync(COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposMovimientoLoadCombosResult>> LoadcombosAsync(int codTipoEstado)
    {
        try { return ApiResponse<TiposMovimientoLoadCombosResult>.Ok(await _repo.LoadcombosAsync(codTipoEstado)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<TiposMovimientoLoadCombosResult>.Fail(ex.Message); }
    }

}