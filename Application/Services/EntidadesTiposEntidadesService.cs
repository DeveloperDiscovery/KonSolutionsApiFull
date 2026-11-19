using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class EntidadesTiposEntidadesService : IEntidadesTiposEntidadesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesTiposEntidadesService);
    private readonly IEntidadesTiposEntidadesRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesTiposEntidadesService(IEntidadesTiposEntidadesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesTiposEntidades>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<EntidadesTiposEntidades>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesTiposEntidades>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesTiposEntidades>> GetByIdAsync(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ENTIDAD, COD_TIPO_ENTIDAD);
              return d is null ? ApiResponse<EntidadesTiposEntidades>.Fail("No encontrado.") : ApiResponse<EntidadesTiposEntidades>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesTiposEntidades>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadesTiposEntidades>>> SearchAsync(EntidadesTiposEntidadesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EntidadesTiposEntidades>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EntidadesTiposEntidades>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesTiposEntidadesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD)
    {
        try { var r = await _repo.DeleteAsync(COD_ENTIDAD, COD_TIPO_ENTIDAD);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadesTiposEntidadesShowselectRow>>> GetShowselectAsync(decimal COD_ENTIDAD)
    {
        try { var d = (await _repo.ShowselectAsync(COD_ENTIDAD)).ToList();
              return ApiResponse<IEnumerable<EntidadesTiposEntidadesShowselectRow>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetShowselectAsync));
              return ApiResponse<IEnumerable<EntidadesTiposEntidadesShowselectRow>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxidAsync(string codTipoEntidad)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxidAsync(codTipoEntidad)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxidAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

}