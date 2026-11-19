using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class FlujosProcesoEtapasNotificadosService : IFlujosProcesoEtapasNotificadosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(FlujosProcesoEtapasNotificadosService);
    private readonly IFlujosProcesoEtapasNotificadosRepository _repo;
    private readonly ErrorLogService _log;
    public FlujosProcesoEtapasNotificadosService(IFlujosProcesoEtapasNotificadosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<FlujosProcesoEtapasNotificados>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<FlujosProcesoEtapasNotificados>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<FlujosProcesoEtapasNotificados>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<FlujosProcesoEtapasNotificados>> GetByIdAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA, decimal COD_ENTIDAD_NOTIFICAR)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_FLUJO_PROCESO, NUM_ETAPA, COD_ENTIDAD_NOTIFICAR);
              return d is null ? ApiResponse<FlujosProcesoEtapasNotificados>.Fail("No encontrado.") : ApiResponse<FlujosProcesoEtapasNotificados>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<FlujosProcesoEtapasNotificados>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<FlujosProcesoEtapasNotificados>>> SearchAsync(FlujosProcesoEtapasNotificadosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<FlujosProcesoEtapasNotificados>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<FlujosProcesoEtapasNotificados>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(FlujosProcesoEtapasNotificadosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(FlujosProcesoEtapasNotificadosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA, decimal COD_ENTIDAD_NOTIFICAR)
    {
        try { var r = await _repo.DeleteAsync(NUM_FLUJO_PROCESO, NUM_ETAPA, COD_ENTIDAD_NOTIFICAR);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}