using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Services;

public class ProyectosTiposEntidadService : IProyectosTiposEntidadService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProyectosTiposEntidadService);
    private readonly IProyectosTiposEntidadRepository _repo;
    private readonly ErrorLogService _log;
    public ProyectosTiposEntidadService(IProyectosTiposEntidadRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ProyectosTiposEntidad>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ProyectosTiposEntidad>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ProyectosTiposEntidad>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ProyectosTiposEntidad>> GetByIdAsync(int NUM_PROYECTO, string COD_TIPO_ENTIDAD)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PROYECTO, COD_TIPO_ENTIDAD);
              return d is null ? ApiResponse<ProyectosTiposEntidad>.Fail("No encontrado.") : ApiResponse<ProyectosTiposEntidad>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ProyectosTiposEntidad>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ProyectosTiposEntidad>>> SearchAsync(ProyectosTiposEntidadSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ProyectosTiposEntidad>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ProyectosTiposEntidad>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ProyectosTiposEntidadInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, string COD_TIPO_ENTIDAD)
    {
        try { var r = await _repo.DeleteAsync(NUM_PROYECTO, COD_TIPO_ENTIDAD);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}