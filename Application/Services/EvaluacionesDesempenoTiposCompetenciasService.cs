using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class EvaluacionesDesempenoTiposCompetenciasService : IEvaluacionesDesempenoTiposCompetenciasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EvaluacionesDesempenoTiposCompetenciasService);
    private readonly IEvaluacionesDesempenoTiposCompetenciasRepository _repo;
    private readonly ErrorLogService _log;
    public EvaluacionesDesempenoTiposCompetenciasService(IEvaluacionesDesempenoTiposCompetenciasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EvaluacionesDesempenoTiposCompetencias>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<EvaluacionesDesempenoTiposCompetencias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EvaluacionesDesempenoTiposCompetencias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EvaluacionesDesempenoTiposCompetencias>> GetByIdAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_EVALUACION_DESEMPENO, COD_TIPO_COMPETENCIA);
              return d is null ? ApiResponse<EvaluacionesDesempenoTiposCompetencias>.Fail("No encontrado.") : ApiResponse<EvaluacionesDesempenoTiposCompetencias>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EvaluacionesDesempenoTiposCompetencias>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EvaluacionesDesempenoTiposCompetencias>>> SearchAsync(EvaluacionesDesempenoTiposCompetenciasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EvaluacionesDesempenoTiposCompetencias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EvaluacionesDesempenoTiposCompetencias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EvaluacionesDesempenoTiposCompetenciasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EvaluacionesDesempenoTiposCompetenciasUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA)
    {
        try { var r = await _repo.DeleteAsync(NUM_EVALUACION_DESEMPENO, COD_TIPO_COMPETENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}