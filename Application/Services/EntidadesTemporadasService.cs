using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class EntidadesTemporadasService : IEntidadesTemporadasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesTemporadasService);
    private readonly IEntidadesTemporadasRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesTemporadasService(IEntidadesTemporadasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesTemporadas>>> GetAllAsync(decimal codEntidad)
    {
        try { var d = (await _repo.ShowallAsync(codEntidad)).ToList();
              return ApiResponse<IEnumerable<EntidadesTemporadas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesTemporadas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesTemporadas>> GetByIdAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ENTIDAD, NUM_TEMPORADA);
              return d is null ? ApiResponse<EntidadesTemporadas>.Fail("No encontrado.") : ApiResponse<EntidadesTemporadas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesTemporadas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(decimal codEntidad)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(codEntidad)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadesTemporadas>>> SearchAsync(EntidadesTemporadasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EntidadesTemporadas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EntidadesTemporadas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesTemporadasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA)
    {
        try { var r = await _repo.DeleteAsync(COD_ENTIDAD, NUM_TEMPORADA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesTemporadasLoadCombosResult>> LoadcombosAsync()
    {
        try { return ApiResponse<EntidadesTemporadasLoadCombosResult>.Ok(await _repo.LoadcombosAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<EntidadesTemporadasLoadCombosResult>.Fail(ex.Message); }
    }

}