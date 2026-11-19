using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class EntidadesService : IEntidadesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesService);
    private readonly IEntidadesRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesService(IEntidadesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<Entidades>>> GetAllAsync(string? desTiposEntidad = null)
    {
        try { var d = (await _repo.ShowallAsync(desTiposEntidad)).ToList();
              return ApiResponse<IEnumerable<Entidades>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<Entidades>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<Entidades>> GetByIdAsync(decimal COD_ENTIDAD)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ENTIDAD);
              return d is null ? ApiResponse<Entidades>.Fail("No encontrado.") : ApiResponse<Entidades>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<Entidades>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<Entidades>>> SearchAsync(EntidadesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<Entidades>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<Entidades>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD)
    {
        try { var r = await _repo.DeleteAsync(COD_ENTIDAD);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetParaDependerAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowparadependerAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetParaDependerAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadImagen>>> GetImagesAsync()
    {
        try { return ApiResponse<IEnumerable<EntidadImagen>>.Ok(await _repo.ShowimagesAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetImagesAsync));
              return ApiResponse<IEnumerable<EntidadImagen>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<Entidades>>> GetAllPorTipoAsync(string codTipoEntidad)
    {
        try { var d = (await _repo.ShowallPorTipoAsync(codTipoEntidad)).ToList();
              return ApiResponse<IEnumerable<Entidades>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllPorTipoAsync));
              return ApiResponse<IEnumerable<Entidades>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<Entidades>>> GetAllPorTipoDependienteAsync(string codTipoEntidad, decimal codEntidadDependencia)
    {
        try { var d = (await _repo.ShowallPorTipoDependienteAsync(codTipoEntidad, codEntidadDependencia)).ToList();
              return ApiResponse<IEnumerable<Entidades>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllPorTipoDependienteAsync));
              return ApiResponse<IEnumerable<Entidades>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<Entidades>>> SearchPorTipoAsync(string codTipoEntidad, string? search)
    {
        try { var d = (await _repo.SearchPorTipoAsync(codTipoEntidad, search ?? "")).ToList();
              return ApiResponse<IEnumerable<Entidades>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchPorTipoAsync));
              return ApiResponse<IEnumerable<Entidades>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesLoadCombosResult>> LoadcombosAsync(int codTipoEstado)
    {
        try { return ApiResponse<EntidadesLoadCombosResult>.Ok(await _repo.LoadcombosAsync(codTipoEstado)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<EntidadesLoadCombosResult>.Fail(ex.Message); }
    }

}