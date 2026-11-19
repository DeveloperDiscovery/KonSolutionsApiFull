using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class TiposNotificacionAccionesService : ITiposNotificacionAccionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposNotificacionAccionesService);
    private readonly ITiposNotificacionAccionesRepository _repo;
    private readonly ErrorLogService _log;
    public TiposNotificacionAccionesService(ITiposNotificacionAccionesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposNotificacionAcciones>>> GetAllAsync(string codTipoNotificacion)
    {
        try { var d = (await _repo.ShowallAsync(codTipoNotificacion)).ToList();
              return ApiResponse<IEnumerable<TiposNotificacionAcciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposNotificacionAcciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposNotificacionAcciones>> GetByIdAsync(string codTipoNotificacion, int numSecuencia)
    {
        try { var d = await _repo.ShowbyidAsync(codTipoNotificacion, numSecuencia);
              return d is null ? ApiResponse<TiposNotificacionAcciones>.Fail("No encontrado.") : ApiResponse<TiposNotificacionAcciones>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposNotificacionAcciones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codTipoNotificacion)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(codTipoNotificacion)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposNotificacionAcciones>>> SearchAsync(TiposNotificacionAccionesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposNotificacionAcciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposNotificacionAcciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposNotificacionAccionesInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codTipoNotificacion, int numSecuencia)
    {
        try { var r = await _repo.DeleteAsync(codTipoNotificacion, numSecuencia);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
