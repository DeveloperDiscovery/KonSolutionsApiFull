using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class TiposSolicitudComercialService : ITiposSolicitudComercialService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposSolicitudComercialService);
    private readonly ITiposSolicitudComercialRepository _repo;
    private readonly ErrorLogService _log;
    public TiposSolicitudComercialService(ITiposSolicitudComercialRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposSolicitudComercial>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<TiposSolicitudComercial>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposSolicitudComercial>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposSolicitudComercial>> GetByIdAsync(string codTipoSolicitudComercial)
    {
        try { var d = await _repo.ShowbyidAsync(codTipoSolicitudComercial);
              return d is null ? ApiResponse<TiposSolicitudComercial>.Fail("No encontrado.") : ApiResponse<TiposSolicitudComercial>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposSolicitudComercial>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposSolicitudComercialInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codTipoSolicitudComercial)
    {
        try { var r = await _repo.DeleteAsync(codTipoSolicitudComercial);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
