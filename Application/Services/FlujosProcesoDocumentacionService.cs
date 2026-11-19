using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class FlujosProcesoDocumentacionService : IFlujosProcesoDocumentacionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(FlujosProcesoDocumentacionService);
    private readonly IFlujosProcesoDocumentacionRepository _repo;
    private readonly ErrorLogService _log;
    public FlujosProcesoDocumentacionService(IFlujosProcesoDocumentacionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<FlujosProcesoDocumentacion>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<FlujosProcesoDocumentacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<FlujosProcesoDocumentacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<FlujosProcesoDocumentacion>> GetByIdAsync(int NUM_FLUJO_PROCESO, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_FLUJO_PROCESO, NUM_SECUENCIA);
              return d is null ? ApiResponse<FlujosProcesoDocumentacion>.Fail("No encontrado.") : ApiResponse<FlujosProcesoDocumentacion>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<FlujosProcesoDocumentacion>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<FlujosProcesoDocumentacion>>> SearchAsync(FlujosProcesoDocumentacionSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<FlujosProcesoDocumentacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<FlujosProcesoDocumentacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(FlujosProcesoDocumentacionInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(FlujosProcesoDocumentacionUpdateDto dto, string codUsuario, string codEstacion)
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