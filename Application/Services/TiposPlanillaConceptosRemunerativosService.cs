using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Services;

public class TiposPlanillaConceptosRemunerativosService : ITiposPlanillaConceptosRemunerativosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposPlanillaConceptosRemunerativosService);
    private readonly ITiposPlanillaConceptosRemunerativosRepository _repo;
    private readonly ErrorLogService _log;
    public TiposPlanillaConceptosRemunerativosService(ITiposPlanillaConceptosRemunerativosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposPlanillaConceptosRemunerativos>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<TiposPlanillaConceptosRemunerativos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposPlanillaConceptosRemunerativos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposPlanillaConceptosRemunerativos>> GetByIdAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TIPO_PLANILLA, COD_CONCEPTO_REMUNERATIVO);
              return d is null ? ApiResponse<TiposPlanillaConceptosRemunerativos>.Fail("No encontrado.") : ApiResponse<TiposPlanillaConceptosRemunerativos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposPlanillaConceptosRemunerativos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposPlanillaConceptosRemunerativos>>> SearchAsync(TiposPlanillaConceptosRemunerativosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposPlanillaConceptosRemunerativos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposPlanillaConceptosRemunerativos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposPlanillaConceptosRemunerativosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(TiposPlanillaConceptosRemunerativosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO)
    {
        try { var r = await _repo.DeleteAsync(COD_TIPO_PLANILLA, COD_CONCEPTO_REMUNERATIVO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}