using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Administracion;

namespace KonSolutionsApi.Application.Services;

public class AreasCargosEntidadesService : IAreasCargosEntidadesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase    = nameof(AreasCargosEntidadesService);

    private readonly IAreasCargosEntidadesRepository _repo;
    private readonly ErrorLogService _log;

    public AreasCargosEntidadesService(IAreasCargosEntidadesRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<AreasCargosEntidades>>> GetAllAsync(int codArea, int codCargo)
    {
        try { var d = (await _repo.ShowallAsync(codArea, codCargo)).ToList();
              return ApiResponse<IEnumerable<AreasCargosEntidades>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<AreasCargosEntidades>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxPorCentroCostoAsync(string codCentroCosto)
    {
        try { var d = (await _repo.ShowcomboxPorCentroCostoAsync(codCentroCosto)).ToList();
              return ApiResponse<IEnumerable<ComboboxItem>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxPorCentroCostoAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<AreasCargosEntidades>> GetByIdAsync(int codArea, int codCargo, decimal codEntidad)
    {
        try { var d = await _repo.ShowbyidAsync(codArea, codCargo, codEntidad);
              return d is null ? ApiResponse<AreasCargosEntidades>.Fail("No encontrado.") : ApiResponse<AreasCargosEntidades>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<AreasCargosEntidades>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<AreasCargosEntidades>>> SearchAsync(AreasCargosEntidadesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<AreasCargosEntidades>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<AreasCargosEntidades>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(AreasCargosEntidadesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Entidad asignada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int codArea, int codCargo, decimal codEntidad)
    {
        try { var r = await _repo.DeleteAsync(codArea, codCargo, codEntidad);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Entidad desasignada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<AreasCargosEntidadesNodo>>> GetTreeviewAsync()
    {
        try { var d = (await _repo.GetTreeviewAsync()).ToList();
              return ApiResponse<IEnumerable<AreasCargosEntidadesNodo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetTreeviewAsync));
              return ApiResponse<IEnumerable<AreasCargosEntidadesNodo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadDisponible>>> GetEntidadesDisponiblesAsync()
    {
        try { var d = (await _repo.GetEntidadesDisponiblesAsync()).ToList();
              return ApiResponse<IEnumerable<EntidadDisponible>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetEntidadesDisponiblesAsync));
              return ApiResponse<IEnumerable<EntidadDisponible>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<AreaCargoEntidadPorOpcion>>> GetPorEtiquetaAsync(string codEtiquetaBusqueda)
    {
        try { var d = (await _repo.GetPorEtiquetaAsync(codEtiquetaBusqueda)).ToList();
              return ApiResponse<IEnumerable<AreaCargoEntidadPorOpcion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetPorEtiquetaAsync));
              return ApiResponse<IEnumerable<AreaCargoEntidadPorOpcion>>.Fail(ex.Message); }
    }
}
