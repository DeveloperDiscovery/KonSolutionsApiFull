using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class EntidadesTemporadasColeccionesService : IEntidadesTemporadasColeccionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesTemporadasColeccionesService);
    private readonly IEntidadesTemporadasColeccionesRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesTemporadasColeccionesService(IEntidadesTemporadasColeccionesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesTemporadasColecciones>>> GetAllAsync(decimal codEntidad, int numTemporada)
    {
        try { var d = (await _repo.ShowallAsync(codEntidad, numTemporada)).ToList();
              return ApiResponse<IEnumerable<EntidadesTemporadasColecciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesTemporadasColecciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesTemporadasColecciones>> GetByIdAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA, int NUM_COLECCION)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ENTIDAD, NUM_TEMPORADA, NUM_COLECCION);
              return d is null ? ApiResponse<EntidadesTemporadasColecciones>.Fail("No encontrado.") : ApiResponse<EntidadesTemporadasColecciones>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesTemporadasColecciones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(decimal codEntidad, int numTemporada)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(codEntidad, numTemporada)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadesTemporadasColecciones>>> SearchAsync(EntidadesTemporadasColeccionesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EntidadesTemporadasColecciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EntidadesTemporadasColecciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesTemporadasColeccionesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA, int NUM_COLECCION)
    {
        try { var r = await _repo.DeleteAsync(COD_ENTIDAD, NUM_TEMPORADA, NUM_COLECCION);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
