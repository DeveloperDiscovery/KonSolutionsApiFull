using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class RequerimientosStockService : IRequerimientosStockService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(RequerimientosStockService);
    private readonly IRequerimientosStockRepository _repo;
    private readonly ErrorLogService _log;
    public RequerimientosStockService(IRequerimientosStockRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<RequerimientosStock>>> GetAllAsync(RequerimientosStockSearchDto? filtros = null)
    {
        try { var d = (await _repo.ShowallAsync(filtros)).ToList();
              return ApiResponse<IEnumerable<RequerimientosStock>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<RequerimientosStock>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RequerimientosStock>> GetByIdAsync(int NUM_REQUERIMIENTO_STOCK)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_REQUERIMIENTO_STOCK);
              return d is null ? ApiResponse<RequerimientosStock>.Fail("No encontrado.") : ApiResponse<RequerimientosStock>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<RequerimientosStock>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RequerimientosStock>>> SearchAsync(RequerimientosStockSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<RequerimientosStock>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<RequerimientosStock>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(RequerimientosStockInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(RequerimientosStockUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_REQUERIMIENTO_STOCK)
    {
        try { var r = await _repo.DeleteAsync(NUM_REQUERIMIENTO_STOCK);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RequerimientosStockLoadCombosResult>> LoadcombosAsync()
    {
        try { return ApiResponse<RequerimientosStockLoadCombosResult>.Ok(await _repo.LoadcombosAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<RequerimientosStockLoadCombosResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> MakereservaAsync(int numRequerimientoStock, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.MakereservaAsync(numRequerimientoStock, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Reservas y requerimiento de compra generados."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(MakereservaAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> ReversereservaAsync(int numRequerimientoStock, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.ReversereservaAsync(numRequerimientoStock, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Reservas revertidas."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ReversereservaAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}