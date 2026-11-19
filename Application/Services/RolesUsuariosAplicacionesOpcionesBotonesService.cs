using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

public class RolesUsuariosAplicacionesOpcionesBotonesService : IRolesUsuariosAplicacionesOpcionesBotonesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(RolesUsuariosAplicacionesOpcionesBotonesService);
    private readonly IRolesUsuariosAplicacionesOpcionesBotonesRepository _repo;
    private readonly ErrorLogService _log;
    public RolesUsuariosAplicacionesOpcionesBotonesService(IRolesUsuariosAplicacionesOpcionesBotonesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RolesUsuariosAplicacionesOpcionesBotones>> GetByIdAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ROL_USUARIO, COD_APLICACION, COD_OPCION_APLICACION);
              return d is null ? ApiResponse<RolesUsuariosAplicacionesOpcionesBotones>.Fail("No encontrado.") : ApiResponse<RolesUsuariosAplicacionesOpcionesBotones>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<RolesUsuariosAplicacionesOpcionesBotones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>>> SearchAsync(RolesUsuariosAplicacionesOpcionesBotonesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuariosAplicacionesOpcionesBotonesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(RolesUsuariosAplicacionesOpcionesBotonesUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        try { var r = await _repo.DeleteAsync(COD_ROL_USUARIO, COD_APLICACION, COD_OPCION_APLICACION);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TreeviewResult>> TreeviewAsync(string codRolUsuario)
    {
        try { var d = await _repo.TreeviewAsync(codRolUsuario);
              return ApiResponse<TreeviewResult>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(TreeviewAsync));
              return ApiResponse<TreeviewResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TreeviewLogosResult>> TreeviewLogosAsync(string codRolUsuario)
    {
        try { var d = await _repo.TreeviewLogosAsync(codRolUsuario);
              return ApiResponse<TreeviewLogosResult>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(TreeviewLogosAsync));
              return ApiResponse<TreeviewLogosResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>>> GetByOpcionAsync(string codRolUsuario, string codAplicacion, string codOpcion)
    {
        try { var d = (await _repo.ShowByOpcionAsync(codRolUsuario, codAplicacion, codOpcion)).ToList();
              return ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByOpcionAsync));
              return ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>>.Fail(ex.Message); }
    }

}