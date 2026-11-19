using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class EvaluacionesDesempenoMetricasService : IEvaluacionesDesempenoMetricasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EvaluacionesDesempenoMetricasService);
    private readonly IEvaluacionesDesempenoMetricasRepository _repo;
    private readonly ErrorLogService _log;
    public EvaluacionesDesempenoMetricasService(IEvaluacionesDesempenoMetricasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EvaluacionesDesempenoMetricas>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<EvaluacionesDesempenoMetricas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EvaluacionesDesempenoMetricas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EvaluacionesDesempenoMetricas>> GetByIdAsync(int NUM_EVALUACION_DESEMPENO, int COD_AREA, decimal COD_ENTIDAD_PERSONAL_EVALUADO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_EVALUACION_DESEMPENO, COD_AREA, COD_ENTIDAD_PERSONAL_EVALUADO);
              return d is null ? ApiResponse<EvaluacionesDesempenoMetricas>.Fail("No encontrado.") : ApiResponse<EvaluacionesDesempenoMetricas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EvaluacionesDesempenoMetricas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EvaluacionesDesempenoMetricas>>> SearchAsync(EvaluacionesDesempenoMetricasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EvaluacionesDesempenoMetricas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EvaluacionesDesempenoMetricas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EvaluacionesDesempenoMetricasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EvaluacionesDesempenoMetricasUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_EVALUACION_DESEMPENO, int COD_AREA, decimal COD_ENTIDAD_PERSONAL_EVALUADO)
    {
        try { var r = await _repo.DeleteAsync(NUM_EVALUACION_DESEMPENO, COD_AREA, COD_ENTIDAD_PERSONAL_EVALUADO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}