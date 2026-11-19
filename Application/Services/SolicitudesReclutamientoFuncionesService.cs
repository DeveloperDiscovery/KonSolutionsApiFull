using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class SolicitudesReclutamientoFuncionesService : ISolicitudesReclutamientoFuncionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SolicitudesReclutamientoFuncionesService);
    private readonly ISolicitudesReclutamientoFuncionesRepository _repo;
    private readonly ErrorLogService _log;
    public SolicitudesReclutamientoFuncionesService(ISolicitudesReclutamientoFuncionesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SolicitudesReclutamientoFunciones>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<SolicitudesReclutamientoFunciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SolicitudesReclutamientoFunciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SolicitudesReclutamientoFunciones>> GetByIdAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_FUNCION)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_SOLICITUD_RECLUTAMIENTO, COD_FUNCION);
              return d is null ? ApiResponse<SolicitudesReclutamientoFunciones>.Fail("No encontrado.") : ApiResponse<SolicitudesReclutamientoFunciones>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SolicitudesReclutamientoFunciones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SolicitudesReclutamientoFunciones>>> SearchAsync(SolicitudesReclutamientoFuncionesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<SolicitudesReclutamientoFunciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SolicitudesReclutamientoFunciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesReclutamientoFuncionesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_FUNCION)
    {
        try { var r = await _repo.DeleteAsync(NUM_SOLICITUD_RECLUTAMIENTO, COD_FUNCION);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}