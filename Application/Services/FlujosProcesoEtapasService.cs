using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class FlujosProcesoEtapasService : IFlujosProcesoEtapasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(FlujosProcesoEtapasService);
    private readonly IFlujosProcesoEtapasRepository _repo;
    private readonly ErrorLogService _log;
    public FlujosProcesoEtapasService(IFlujosProcesoEtapasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<FlujosProcesoEtapas>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<FlujosProcesoEtapas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<FlujosProcesoEtapas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<FlujosProcesoEtapas>> GetByIdAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_FLUJO_PROCESO, NUM_ETAPA);
              return d is null ? ApiResponse<FlujosProcesoEtapas>.Fail("No encontrado.") : ApiResponse<FlujosProcesoEtapas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<FlujosProcesoEtapas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<FlujosProcesoEtapas>>> SearchAsync(FlujosProcesoEtapasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<FlujosProcesoEtapas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<FlujosProcesoEtapas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(FlujosProcesoEtapasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(FlujosProcesoEtapasUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA)
    {
        try { var r = await _repo.DeleteAsync(NUM_FLUJO_PROCESO, NUM_ETAPA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}