using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class EvaluacionesDesempenoCompetenciasService : IEvaluacionesDesempenoCompetenciasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EvaluacionesDesempenoCompetenciasService);
    private readonly IEvaluacionesDesempenoCompetenciasRepository _repo;
    private readonly ErrorLogService _log;
    public EvaluacionesDesempenoCompetenciasService(IEvaluacionesDesempenoCompetenciasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EvaluacionesDesempenoCompetencias>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<EvaluacionesDesempenoCompetencias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EvaluacionesDesempenoCompetencias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EvaluacionesDesempenoCompetencias>> GetByIdAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA, string COD_COMPETENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_EVALUACION_DESEMPENO, COD_TIPO_COMPETENCIA, COD_COMPETENCIA);
              return d is null ? ApiResponse<EvaluacionesDesempenoCompetencias>.Fail("No encontrado.") : ApiResponse<EvaluacionesDesempenoCompetencias>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EvaluacionesDesempenoCompetencias>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EvaluacionesDesempenoCompetencias>>> SearchAsync(EvaluacionesDesempenoCompetenciasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EvaluacionesDesempenoCompetencias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EvaluacionesDesempenoCompetencias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EvaluacionesDesempenoCompetenciasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EvaluacionesDesempenoCompetenciasUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA, string COD_COMPETENCIA)
    {
        try { var r = await _repo.DeleteAsync(NUM_EVALUACION_DESEMPENO, COD_TIPO_COMPETENCIA, COD_COMPETENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}