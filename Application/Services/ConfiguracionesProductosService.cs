using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class ConfiguracionesProductosService : IConfiguracionesProductosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ConfiguracionesProductosService);
    private readonly IConfiguracionesProductosRepository _repo;
    private readonly ErrorLogService _log;
    public ConfiguracionesProductosService(IConfiguracionesProductosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ConfiguracionesProductos>>> GetAllAsync(string codTipoConfiguracion)
    {
        try { var d = (await _repo.ShowallAsync(codTipoConfiguracion)).ToList();
              return ApiResponse<IEnumerable<ConfiguracionesProductos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ConfiguracionesProductos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ConfiguracionesProductos>> GetByIdAsync(string codTipoConfiguracion, short numSecuencia)
    {
        try { var d = await _repo.ShowbyidAsync(codTipoConfiguracion, numSecuencia);
              return d is null ? ApiResponse<ConfiguracionesProductos>.Fail("No encontrado.") : ApiResponse<ConfiguracionesProductos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ConfiguracionesProductos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codTipoConfiguracion)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(codTipoConfiguracion)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ConfiguracionesProductos>>> SearchAsync(string codTipoConfiguracion, string? search)
    {
        try { var d = (await _repo.SearchAsync(codTipoConfiguracion, search)).ToList();
              return ApiResponse<IEnumerable<ConfiguracionesProductos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ConfiguracionesProductos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ConfiguracionesProductosInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ConfiguracionesProductosUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codTipoConfiguracion, short numSecuencia)
    {
        try { var r = await _repo.DeleteAsync(codTipoConfiguracion, numSecuencia);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}
