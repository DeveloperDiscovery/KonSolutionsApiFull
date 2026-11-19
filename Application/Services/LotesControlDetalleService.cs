using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class LotesControlDetalleService : ILotesControlDetalleService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(LotesControlDetalleService);
    private readonly ILotesControlDetalleRepository _repo;
    private readonly ErrorLogService _log;
    public LotesControlDetalleService(ILotesControlDetalleRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<LotesControlDetalle>>> GetAllAsync(string NUM_LOTE_CONTROL)
    {
        try { var d = (await _repo.ShowallAsync(NUM_LOTE_CONTROL)).ToList();
              return ApiResponse<IEnumerable<LotesControlDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<LotesControlDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<LotesControlDetalle>> GetByIdAsync(string NUM_LOTE_CONTROL, string COD_PRODUCTO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_LOTE_CONTROL, COD_PRODUCTO);
              return d is null ? ApiResponse<LotesControlDetalle>.Fail("No encontrado.") : ApiResponse<LotesControlDetalle>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<LotesControlDetalle>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<LotesControlDetalle>>> SearchAsync(LotesControlDetalleSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<LotesControlDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<LotesControlDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(LotesControlDetalleInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(LotesControlDetalleUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string NUM_LOTE_CONTROL, string COD_PRODUCTO)
    {
        try { var r = await _repo.DeleteAsync(NUM_LOTE_CONTROL, COD_PRODUCTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}