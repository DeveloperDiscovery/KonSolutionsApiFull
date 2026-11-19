using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

public class UsuariosService : IUsuariosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(UsuariosService);
    private readonly IUsuariosRepository _repo;
    private readonly ErrorLogService _log;
    public UsuariosService(IUsuariosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<Usuarios>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<Usuarios>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<Usuarios>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<Usuarios>> GetByIdAsync(string COD_USUARIO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_USUARIO);
              return d is null ? ApiResponse<Usuarios>.Fail("No encontrado.") : ApiResponse<Usuarios>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<Usuarios>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string? search = null)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(search)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<Usuarios>>> SearchAsync(UsuariosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<Usuarios>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<Usuarios>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(UsuariosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_USUARIO)
    {
        try { var r = await _repo.DeleteAsync(COD_USUARIO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<dynamic>>> EstadosAsync(UsuariosEstadosDto dto, string codUsuario, string codEstacion)
    {
        try { var d = (await _repo.EstadosAsync(dto, codUsuario, codEstacion)).ToList();
              return ApiResponse<IEnumerable<dynamic>>.Ok(d, "Proceso ejecutado.", d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(EstadosAsync));
              return ApiResponse<IEnumerable<dynamic>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<dynamic>>> PasswordAsync(UsuariosPasswordDto dto, string codUsuario, string codEstacion)
    {
        try { var d = (await _repo.PasswordAsync(dto, codUsuario, codEstacion)).ToList();
              return ApiResponse<IEnumerable<dynamic>>.Ok(d, "Proceso ejecutado.", d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(PasswordAsync));
              return ApiResponse<IEnumerable<dynamic>>.Fail(ex.Message); }
    }

    /// <summary>
    /// Reset de password: deja el password del usuario en NULL para que lo
    /// redefina en su próximo login (PROC_SEGURIDAD_USUARIOS_PASSWORDRESET).
    /// </summary>
    public async Task<ApiResponse<IEnumerable<dynamic>>> PasswordResetAsync(UsuariosPasswordResetDto dto, string codUsuario, string codEstacion)
    {
        try { var d = (await _repo.PasswordResetAsync(dto, codUsuario, codEstacion)).ToList();
              return ApiResponse<IEnumerable<dynamic>>.Ok(d, "Password reseteado.", d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(PasswordResetAsync));
              return ApiResponse<IEnumerable<dynamic>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<dynamic>>> ResetintentosAsync(UsuariosResetintentosDto dto, string codUsuario, string codEstacion)
    {
        try { var d = (await _repo.ResetintentosAsync(dto, codUsuario, codEstacion)).ToList();
              return ApiResponse<IEnumerable<dynamic>>.Ok(d, "Proceso ejecutado.", d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ResetintentosAsync));
              return ApiResponse<IEnumerable<dynamic>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<UsuariosLoadCombosResult>> LoadcombosAsync(int codTipoEstado)
    {
        try { return ApiResponse<UsuariosLoadCombosResult>.Ok(await _repo.LoadcombosAsync(codTipoEstado)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<UsuariosLoadCombosResult>.Fail(ex.Message); }
    }

}