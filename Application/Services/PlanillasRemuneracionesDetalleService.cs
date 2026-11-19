using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Services;

public class PlanillasRemuneracionesDetalleService : IPlanillasRemuneracionesDetalleService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PlanillasRemuneracionesDetalleService);
    private readonly IPlanillasRemuneracionesDetalleRepository _repo;
    private readonly ErrorLogService _log;
    public PlanillasRemuneracionesDetalleService(IPlanillasRemuneracionesDetalleRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PlanillasRemuneracionesDetalle>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<PlanillasRemuneracionesDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PlanillasRemuneracionesDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PlanillasRemuneracionesDetalle>> GetByIdAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PLANILLA_REMUNERATIVA, COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO);
              return d is null ? ApiResponse<PlanillasRemuneracionesDetalle>.Fail("No encontrado.") : ApiResponse<PlanillasRemuneracionesDetalle>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PlanillasRemuneracionesDetalle>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PlanillasRemuneracionesDetalle>>> SearchAsync(PlanillasRemuneracionesDetalleSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<PlanillasRemuneracionesDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<PlanillasRemuneracionesDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PlanillasRemuneracionesDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(PlanillasRemuneracionesDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO)
    {
        try { var r = await _repo.DeleteAsync(NUM_PLANILLA_REMUNERATIVA, COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}