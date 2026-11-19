using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Services;

public class ProyectosUnidadesMedidaTareasService : IProyectosUnidadesMedidaTareasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProyectosUnidadesMedidaTareasService);
    private readonly IProyectosUnidadesMedidaTareasRepository _repo;
    private readonly ErrorLogService _log;
    public ProyectosUnidadesMedidaTareasService(IProyectosUnidadesMedidaTareasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ProyectosUnidadesMedidaTareas>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ProyectosUnidadesMedidaTareas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ProyectosUnidadesMedidaTareas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ProyectosUnidadesMedidaTareas>> GetByIdAsync(string COD_UNIDAD_MEDIDA_TAREAS)
    {
        try { var d = await _repo.ShowbyidAsync(COD_UNIDAD_MEDIDA_TAREAS);
              return d is null ? ApiResponse<ProyectosUnidadesMedidaTareas>.Fail("No encontrado.") : ApiResponse<ProyectosUnidadesMedidaTareas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ProyectosUnidadesMedidaTareas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ProyectosUnidadesMedidaTareas>>> SearchAsync(ProyectosUnidadesMedidaTareasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ProyectosUnidadesMedidaTareas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ProyectosUnidadesMedidaTareas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ProyectosUnidadesMedidaTareasInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosUnidadesMedidaTareasUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_UNIDAD_MEDIDA_TAREAS)
    {
        try { var r = await _repo.DeleteAsync(COD_UNIDAD_MEDIDA_TAREAS);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}