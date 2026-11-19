using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class FlujosProcesoMemoriaService : IFlujosProcesoMemoriaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(FlujosProcesoMemoriaService);
    private readonly IFlujosProcesoMemoriaRepository _repo;
    private readonly ErrorLogService _log;
    public FlujosProcesoMemoriaService(IFlujosProcesoMemoriaRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<FlujosProcesoMemoria>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<FlujosProcesoMemoria>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<FlujosProcesoMemoria>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<FlujosProcesoMemoria>> GetByIdAsync(int NUM_FLUJO_PROCESO, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_FLUJO_PROCESO, NUM_SECUENCIA);
              return d is null ? ApiResponse<FlujosProcesoMemoria>.Fail("No encontrado.") : ApiResponse<FlujosProcesoMemoria>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<FlujosProcesoMemoria>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<FlujosProcesoMemoria>>> SearchAsync(FlujosProcesoMemoriaSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<FlujosProcesoMemoria>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<FlujosProcesoMemoria>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(FlujosProcesoMemoriaInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(FlujosProcesoMemoriaUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(NUM_FLUJO_PROCESO, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}