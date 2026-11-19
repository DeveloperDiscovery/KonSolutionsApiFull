using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Services;

public class PlanillasRemuneracionesFormulasService : IPlanillasRemuneracionesFormulasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PlanillasRemuneracionesFormulasService);
    private readonly IPlanillasRemuneracionesFormulasRepository _repo;
    private readonly ErrorLogService _log;
    public PlanillasRemuneracionesFormulasService(IPlanillasRemuneracionesFormulasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PlanillasRemuneracionesFormulas>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<PlanillasRemuneracionesFormulas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PlanillasRemuneracionesFormulas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PlanillasRemuneracionesFormulas>> GetByIdAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA_FORMULA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TIPO_PLANILLA, COD_CONCEPTO_REMUNERATIVO, NUM_SECUENCIA_FORMULA);
              return d is null ? ApiResponse<PlanillasRemuneracionesFormulas>.Fail("No encontrado.") : ApiResponse<PlanillasRemuneracionesFormulas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PlanillasRemuneracionesFormulas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PlanillasRemuneracionesFormulas>>> SearchAsync(PlanillasRemuneracionesFormulasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<PlanillasRemuneracionesFormulas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<PlanillasRemuneracionesFormulas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PlanillasRemuneracionesFormulasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(PlanillasRemuneracionesFormulasUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA_FORMULA)
    {
        try { var r = await _repo.DeleteAsync(COD_TIPO_PLANILLA, COD_CONCEPTO_REMUNERATIVO, NUM_SECUENCIA_FORMULA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}