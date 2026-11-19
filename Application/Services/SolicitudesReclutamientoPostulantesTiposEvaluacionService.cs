using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class SolicitudesReclutamientoPostulantesTiposEvaluacionService : ISolicitudesReclutamientoPostulantesTiposEvaluacionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SolicitudesReclutamientoPostulantesTiposEvaluacionService);
    private readonly ISolicitudesReclutamientoPostulantesTiposEvaluacionRepository _repo;
    private readonly ErrorLogService _log;
    public SolicitudesReclutamientoPostulantesTiposEvaluacionService(ISolicitudesReclutamientoPostulantesTiposEvaluacionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SolicitudesReclutamientoPostulantesTiposEvaluacion>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<SolicitudesReclutamientoPostulantesTiposEvaluacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SolicitudesReclutamientoPostulantesTiposEvaluacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SolicitudesReclutamientoPostulantesTiposEvaluacion>> GetByIdAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_SOLICITUD_RECLUTAMIENTO, COD_ENTIDAD_POSTULANTE, NUM_SECUENCIA);
              return d is null ? ApiResponse<SolicitudesReclutamientoPostulantesTiposEvaluacion>.Fail("No encontrado.") : ApiResponse<SolicitudesReclutamientoPostulantesTiposEvaluacion>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SolicitudesReclutamientoPostulantesTiposEvaluacion>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SolicitudesReclutamientoPostulantesTiposEvaluacion>>> SearchAsync(SolicitudesReclutamientoPostulantesTiposEvaluacionSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<SolicitudesReclutamientoPostulantesTiposEvaluacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SolicitudesReclutamientoPostulantesTiposEvaluacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesReclutamientoPostulantesTiposEvaluacionInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(SolicitudesReclutamientoPostulantesTiposEvaluacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, int NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(NUM_SOLICITUD_RECLUTAMIENTO, COD_ENTIDAD_POSTULANTE, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}