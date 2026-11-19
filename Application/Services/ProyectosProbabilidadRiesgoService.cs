using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Services;

public class ProyectosProbabilidadRiesgoService : IProyectosProbabilidadRiesgoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProyectosProbabilidadRiesgoService);
    private readonly IProyectosProbabilidadRiesgoRepository _repo;
    private readonly ErrorLogService _log;
    public ProyectosProbabilidadRiesgoService(IProyectosProbabilidadRiesgoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ProyectosProbabilidadRiesgo>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ProyectosProbabilidadRiesgo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ProyectosProbabilidadRiesgo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ProyectosProbabilidadRiesgo>> GetByIdAsync(int COD_PROBABILIDAD_RIESGO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_PROBABILIDAD_RIESGO);
              return d is null ? ApiResponse<ProyectosProbabilidadRiesgo>.Fail("No encontrado.") : ApiResponse<ProyectosProbabilidadRiesgo>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ProyectosProbabilidadRiesgo>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ProyectosProbabilidadRiesgo>>> SearchAsync(ProyectosProbabilidadRiesgoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ProyectosProbabilidadRiesgo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ProyectosProbabilidadRiesgo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ProyectosProbabilidadRiesgoInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosProbabilidadRiesgoUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int COD_PROBABILIDAD_RIESGO)
    {
        try { var r = await _repo.DeleteAsync(COD_PROBABILIDAD_RIESGO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}