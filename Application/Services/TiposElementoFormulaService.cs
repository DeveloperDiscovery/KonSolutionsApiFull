using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class TiposElementoFormulaService : ITiposElementoFormulaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposElementoFormulaService);
    private readonly ITiposElementoFormulaRepository _repo;
    private readonly ErrorLogService _log;
    public TiposElementoFormulaService(ITiposElementoFormulaRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposElementoFormula>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<TiposElementoFormula>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposElementoFormula>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposElementoFormula>> GetByIdAsync(int COD_CLASE_ELEMENTO_FORMULA, int COD_TIPO_ELEMENTO_FORMULA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CLASE_ELEMENTO_FORMULA, COD_TIPO_ELEMENTO_FORMULA);
              return d is null ? ApiResponse<TiposElementoFormula>.Fail("No encontrado.") : ApiResponse<TiposElementoFormula>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposElementoFormula>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposElementoFormula>>> SearchAsync(TiposElementoFormulaSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposElementoFormula>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposElementoFormula>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposElementoFormulaInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(TiposElementoFormulaUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int COD_CLASE_ELEMENTO_FORMULA, int COD_TIPO_ELEMENTO_FORMULA)
    {
        try { var r = await _repo.DeleteAsync(COD_CLASE_ELEMENTO_FORMULA, COD_TIPO_ELEMENTO_FORMULA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}