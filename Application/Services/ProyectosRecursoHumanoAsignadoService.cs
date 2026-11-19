using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Services;

public class ProyectosRecursoHumanoAsignadoService : IProyectosRecursoHumanoAsignadoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProyectosRecursoHumanoAsignadoService);
    private readonly IProyectosRecursoHumanoAsignadoRepository _repo;
    private readonly ErrorLogService _log;
    public ProyectosRecursoHumanoAsignadoService(IProyectosRecursoHumanoAsignadoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ProyectosRecursoHumanoAsignado>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ProyectosRecursoHumanoAsignado>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ProyectosRecursoHumanoAsignado>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ProyectosRecursoHumanoAsignado>> GetByIdAsync(int NUM_PROYECTO, int COD_TAREA, decimal COD_ENTIDAD_ASIGNADA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PROYECTO, COD_TAREA, COD_ENTIDAD_ASIGNADA);
              return d is null ? ApiResponse<ProyectosRecursoHumanoAsignado>.Fail("No encontrado.") : ApiResponse<ProyectosRecursoHumanoAsignado>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ProyectosRecursoHumanoAsignado>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ProyectosRecursoHumanoAsignado>>> SearchAsync(ProyectosRecursoHumanoAsignadoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ProyectosRecursoHumanoAsignado>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ProyectosRecursoHumanoAsignado>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ProyectosRecursoHumanoAsignadoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosRecursoHumanoAsignadoUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, int COD_TAREA, decimal COD_ENTIDAD_ASIGNADA)
    {
        try { var r = await _repo.DeleteAsync(NUM_PROYECTO, COD_TAREA, COD_ENTIDAD_ASIGNADA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}