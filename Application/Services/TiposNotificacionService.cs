using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class TiposNotificacionService : ITiposNotificacionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase    = nameof(TiposNotificacionService);
    private readonly ITiposNotificacionRepository _repo;
    private readonly ErrorLogService _log;
    public TiposNotificacionService(ITiposNotificacionRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposNotificacion>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<TiposNotificacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposNotificacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposNotificacion>> GetByIdAsync(string COD_TIPO_NOTIFICACION)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TIPO_NOTIFICACION);
              return d is null ? ApiResponse<TiposNotificacion>.Fail("No encontrado.")
                               : ApiResponse<TiposNotificacion>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposNotificacion>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposNotificacion>>> SearchAsync(TiposNotificacionSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposNotificacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposNotificacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposNotificacionInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Tipo de notificación guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_NOTIFICACION)
    {
        try { var r = await _repo.DeleteAsync(COD_TIPO_NOTIFICACION);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
