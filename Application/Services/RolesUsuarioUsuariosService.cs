using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

public class RolesUsuarioUsuariosService : IRolesUsuarioUsuariosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(RolesUsuarioUsuariosService);
    private readonly IRolesUsuarioUsuariosRepository _repo;
    private readonly ErrorLogService _log;
    public RolesUsuarioUsuariosService(IRolesUsuarioUsuariosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioUsuarios>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioUsuarios>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<RolesUsuarioUsuarios>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioUsuarios>>> GetByRolAsync(string codRol)
    {
        try { var d = (await _repo.ShowByRolAsync(codRol)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioUsuarios>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByRolAsync));
              return ApiResponse<IEnumerable<RolesUsuarioUsuarios>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioUsuarios>>> GetByUsuarioAsync(string codUsuario)
    {
        try { var d = (await _repo.ShowbyusuarioAsync(codUsuario)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioUsuarios>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByUsuarioAsync));
              return ApiResponse<IEnumerable<RolesUsuarioUsuarios>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RolesUsuarioUsuarios>> GetByIdAsync(string COD_ROL_USUARIO, string COD_USUARIO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ROL_USUARIO, COD_USUARIO);
              return d is null ? ApiResponse<RolesUsuarioUsuarios>.Fail("No encontrado.") : ApiResponse<RolesUsuarioUsuarios>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<RolesUsuarioUsuarios>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioUsuarios>>> SearchAsync(RolesUsuarioUsuariosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioUsuarios>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<RolesUsuarioUsuarios>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuarioUsuariosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO, string COD_USUARIO)
    {
        try { var r = await _repo.DeleteAsync(COD_ROL_USUARIO, COD_USUARIO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}