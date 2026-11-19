using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class EvaluacionesDesempenoPersonalService : IEvaluacionesDesempenoPersonalService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EvaluacionesDesempenoPersonalService);
    private readonly IEvaluacionesDesempenoPersonalRepository _repo;
    private readonly ErrorLogService _log;
    public EvaluacionesDesempenoPersonalService(IEvaluacionesDesempenoPersonalRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EvaluacionesDesempenoPersonal>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<EvaluacionesDesempenoPersonal>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EvaluacionesDesempenoPersonal>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EvaluacionesDesempenoPersonal>> GetByIdAsync(int NUM_EVALUACION_DESEMPENO, decimal COD_ENTIDAD_PERSONAL_EVALUADOR, decimal COD_ENTIDAD_PERSONAL_EVALUADO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_EVALUACION_DESEMPENO, COD_ENTIDAD_PERSONAL_EVALUADOR, COD_ENTIDAD_PERSONAL_EVALUADO);
              return d is null ? ApiResponse<EvaluacionesDesempenoPersonal>.Fail("No encontrado.") : ApiResponse<EvaluacionesDesempenoPersonal>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EvaluacionesDesempenoPersonal>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EvaluacionesDesempenoPersonal>>> SearchAsync(EvaluacionesDesempenoPersonalSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EvaluacionesDesempenoPersonal>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EvaluacionesDesempenoPersonal>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EvaluacionesDesempenoPersonalInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EvaluacionesDesempenoPersonalUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_EVALUACION_DESEMPENO, decimal COD_ENTIDAD_PERSONAL_EVALUADOR, decimal COD_ENTIDAD_PERSONAL_EVALUADO)
    {
        try { var r = await _repo.DeleteAsync(NUM_EVALUACION_DESEMPENO, COD_ENTIDAD_PERSONAL_EVALUADOR, COD_ENTIDAD_PERSONAL_EVALUADO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}