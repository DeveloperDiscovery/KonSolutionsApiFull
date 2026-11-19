using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Services;

public class SaldosContablesService : ISaldosContablesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SaldosContablesService);
    private readonly ISaldosContablesRepository _repo;
    private readonly ErrorLogService _log;
    public SaldosContablesService(ISaldosContablesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SaldosContables>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<SaldosContables>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SaldosContables>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SaldosContables>> GetByIdAsync(int NUM_PERIODO_CONTABLE, string COD_CUENTA_CONTABLE, string COD_MONEDA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PERIODO_CONTABLE, COD_CUENTA_CONTABLE, COD_MONEDA);
              return d is null ? ApiResponse<SaldosContables>.Fail("No encontrado.") : ApiResponse<SaldosContables>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SaldosContables>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SaldosContables>>> SearchAsync(SaldosContablesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<SaldosContables>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SaldosContables>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(SaldosContablesInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(SaldosContablesUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_CUENTA_CONTABLE, string COD_MONEDA)
    {
        try { var r = await _repo.DeleteAsync(NUM_PERIODO_CONTABLE, COD_CUENTA_CONTABLE, COD_MONEDA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}