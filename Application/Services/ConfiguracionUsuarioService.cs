using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class ConfiguracionUsuarioService : IConfiguracionUsuarioService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase    = nameof(ConfiguracionUsuarioService);
    private readonly IConfiguracionUsuarioRepository _repo;
    private readonly ErrorLogService _log;
    public ConfiguracionUsuarioService(IConfiguracionUsuarioRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ConfiguracionUsuario>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ConfiguracionUsuario>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ConfiguracionUsuario>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ConfiguracionUsuario>> GetByIdAsync(string COD_USUARIO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_USUARIO);
              return d is null ? ApiResponse<ConfiguracionUsuario>.Ok(new ConfiguracionUsuario { COD_USUARIO = COD_USUARIO })
                               : ApiResponse<ConfiguracionUsuario>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ConfiguracionUsuario>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ConfiguracionUsuario>>> SearchAsync(ConfiguracionUsuarioSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ConfiguracionUsuario>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ConfiguracionUsuario>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> SaveAsync(ConfiguracionUsuarioSaveDto dto, string codUsuarioSesion, string codEstacion)
    {
        try { var r = await _repo.SaveAsync(dto, codUsuarioSesion, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Configuración de usuario guardada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SaveAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_USUARIO)
    {
        try { var r = await _repo.DeleteAsync(COD_USUARIO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
