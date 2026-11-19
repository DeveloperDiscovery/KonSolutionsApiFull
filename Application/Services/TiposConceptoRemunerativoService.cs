using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Services;

public class TiposConceptoRemunerativoService : ITiposConceptoRemunerativoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposConceptoRemunerativoService);
    private readonly ITiposConceptoRemunerativoRepository _repo;
    private readonly ErrorLogService _log;
    public TiposConceptoRemunerativoService(ITiposConceptoRemunerativoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposConceptoRemunerativo>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<TiposConceptoRemunerativo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposConceptoRemunerativo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposConceptoRemunerativo>> GetByIdAsync(string COD_TIPO_REMUNERATIVO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TIPO_REMUNERATIVO);
              return d is null ? ApiResponse<TiposConceptoRemunerativo>.Fail("No encontrado.") : ApiResponse<TiposConceptoRemunerativo>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposConceptoRemunerativo>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposConceptoRemunerativo>>> SearchAsync(TiposConceptoRemunerativoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposConceptoRemunerativo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposConceptoRemunerativo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposConceptoRemunerativoInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(TiposConceptoRemunerativoUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_REMUNERATIVO)
    {
        try { var r = await _repo.DeleteAsync(COD_TIPO_REMUNERATIVO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}