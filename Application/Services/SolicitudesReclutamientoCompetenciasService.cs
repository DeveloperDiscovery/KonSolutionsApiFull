using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class SolicitudesReclutamientoCompetenciasService : ISolicitudesReclutamientoCompetenciasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SolicitudesReclutamientoCompetenciasService);
    private readonly ISolicitudesReclutamientoCompetenciasRepository _repo;
    private readonly ErrorLogService _log;
    public SolicitudesReclutamientoCompetenciasService(ISolicitudesReclutamientoCompetenciasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SolicitudesReclutamientoCompetencias>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<SolicitudesReclutamientoCompetencias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SolicitudesReclutamientoCompetencias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SolicitudesReclutamientoCompetencias>> GetByIdAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_COMPETENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_SOLICITUD_RECLUTAMIENTO, COD_COMPETENCIA);
              return d is null ? ApiResponse<SolicitudesReclutamientoCompetencias>.Fail("No encontrado.") : ApiResponse<SolicitudesReclutamientoCompetencias>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SolicitudesReclutamientoCompetencias>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SolicitudesReclutamientoCompetencias>>> SearchAsync(SolicitudesReclutamientoCompetenciasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<SolicitudesReclutamientoCompetencias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SolicitudesReclutamientoCompetencias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesReclutamientoCompetenciasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(SolicitudesReclutamientoCompetenciasUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_COMPETENCIA)
    {
        try { var r = await _repo.DeleteAsync(NUM_SOLICITUD_RECLUTAMIENTO, COD_COMPETENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}