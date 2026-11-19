using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class SolicitudesComercialesService : ISolicitudesComercialesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SolicitudesComercialesService);
    private readonly ISolicitudesComercialesRepository _repo;
    private readonly ErrorLogService _log;
    public SolicitudesComercialesService(ISolicitudesComercialesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SolicitudesComerciales>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<SolicitudesComerciales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SolicitudesComerciales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SolicitudesComerciales>> GetByIdAsync(int numSolicitudComercial)
    {
        try { var d = await _repo.ShowbyidAsync(numSolicitudComercial);
              return d is null ? ApiResponse<SolicitudesComerciales>.Fail("No encontrado.") : ApiResponse<SolicitudesComerciales>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SolicitudesComerciales>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(decimal? codEntidadCliente)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(codEntidadCliente)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SolicitudesComerciales>>> SearchAsync(SolicitudesComercialesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<SolicitudesComerciales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SolicitudesComerciales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesComercialesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int numSolicitudComercial, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.DeleteAsync(numSolicitudComercial, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro anulado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> AsignarResponsableAsync(SolicitudesComercialesAsignarResponsableDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.AsignarResponsableAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Responsable asignado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(AsignarResponsableAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> ReasignaAsesorAsync(SolicitudesComercialesReasignaAsesorDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.ReasignaAsesorAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Asesor comercial reasignado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ReasignaAsesorAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SolicitudesComercialesLoadCombosResult>> GetLoadcombosAsync()
    {
        try { return ApiResponse<SolicitudesComercialesLoadCombosResult>.Ok(await _repo.LoadcombosAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetLoadcombosAsync));
              return ApiResponse<SolicitudesComercialesLoadCombosResult>.Fail(ex.Message); }
    }
}
