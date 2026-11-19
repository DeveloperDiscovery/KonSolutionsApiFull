using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Services;

public class ProyectosService : IProyectosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProyectosService);
    private readonly IProyectosRepository _repo;
    private readonly IProyectosParametrosRepository _parametros;
    private readonly IPathFilesRepository _paths;
    private readonly ErrorLogService _log;
    public ProyectosService(IProyectosRepository repo, IProyectosParametrosRepository parametros, IPathFilesRepository paths, ErrorLogService log) { _repo = repo; _parametros = parametros; _paths = paths; _log = log; }

    public async Task<ApiResponse<IEnumerable<Proyectos>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<Proyectos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<Proyectos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<Proyectos>> GetByIdAsync(int NUM_PROYECTO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PROYECTO);
              return d is null ? ApiResponse<Proyectos>.Fail("No encontrado.") : ApiResponse<Proyectos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<Proyectos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<Proyectos>>> SearchAsync(ProyectosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<Proyectos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<Proyectos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ProyectosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              // al crear el proyecto se crea su carpeta de archivos (número a 10 dígitos); si no se puede, el proyecto igual queda creado y se avisa
              string? aviso = null;
              if (!r.IsError && int.TryParse(r.Codigo, out var num) && num > 0) aviso = await ProyectosCarpetas.CrearAsync(_parametros, _paths, num);
              return ApiResponse<WriteResult>.FromWriteResult(r, aviso is null ? "Registro guardado." : $"Registro guardado. Ojo con la carpeta de archivos: {aviso}"); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO)
    {
        try { var r = await _repo.DeleteAsync(NUM_PROYECTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
