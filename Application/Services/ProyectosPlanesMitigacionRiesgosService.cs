using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Services;

public class ProyectosPlanesMitigacionRiesgosService : IProyectosPlanesMitigacionRiesgosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProyectosPlanesMitigacionRiesgosService);
    private readonly IProyectosPlanesMitigacionRiesgosRepository _repo;
    private readonly ErrorLogService _log;
    public ProyectosPlanesMitigacionRiesgosService(IProyectosPlanesMitigacionRiesgosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ProyectosPlanesMitigacionRiesgos>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ProyectosPlanesMitigacionRiesgos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ProyectosPlanesMitigacionRiesgos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ProyectosPlanesMitigacionRiesgos>> GetByIdAsync(int NUM_PROYECTO, string COD_PLAN_MITIGACION)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PROYECTO, COD_PLAN_MITIGACION);
              return d is null ? ApiResponse<ProyectosPlanesMitigacionRiesgos>.Fail("No encontrado.") : ApiResponse<ProyectosPlanesMitigacionRiesgos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ProyectosPlanesMitigacionRiesgos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ProyectosPlanesMitigacionRiesgos>>> SearchAsync(ProyectosPlanesMitigacionRiesgosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ProyectosPlanesMitigacionRiesgos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ProyectosPlanesMitigacionRiesgos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ProyectosPlanesMitigacionRiesgosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosPlanesMitigacionRiesgosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, string COD_PLAN_MITIGACION)
    {
        try { var r = await _repo.DeleteAsync(NUM_PROYECTO, COD_PLAN_MITIGACION);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}