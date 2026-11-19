using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

public class RolesUsuariosAplicacionesOpcionesService : IRolesUsuariosAplicacionesOpcionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(RolesUsuariosAplicacionesOpcionesService);
    private readonly IRolesUsuariosAplicacionesOpcionesRepository _repo;
    private readonly ErrorLogService _log;
    public RolesUsuariosAplicacionesOpcionesService(IRolesUsuariosAplicacionesOpcionesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpciones>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpciones>>.Fail(ex.Message); }
    }

    /// <summary>Opciones filtradas por rol y aplicación — solo el servidor filtra, no el cliente.</summary>
    public async Task<ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpciones>>> GetByRolAplicacionAsync(string codRol, string codAplicacion)
    {
        try { var d = (await _repo.ShowByRolAplicacionAsync(codRol, codAplicacion)).ToList();
              return ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByRolAplicacionAsync));
              return ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RolesUsuariosAplicacionesOpciones>> GetByIdAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ROL_USUARIO, COD_APLICACION, COD_OPCION_APLICACION);
              return d is null ? ApiResponse<RolesUsuariosAplicacionesOpciones>.Fail("No encontrado.") : ApiResponse<RolesUsuariosAplicacionesOpciones>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<RolesUsuariosAplicacionesOpciones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpciones>>> SearchAsync(RolesUsuariosAplicacionesOpcionesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuariosAplicacionesOpcionesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(RolesUsuariosAplicacionesOpcionesUpdateDto dto, string codUsuario, string codEstacion)
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

    public async Task<ApiResponse<IEnumerable<NodoOpcion>>> TreeviewAsignadasAsync(string codRolUsuario)
    {
        try { var d = (await _repo.TreeviewAsignadasAsync(codRolUsuario)).ToList();
              return ApiResponse<IEnumerable<NodoOpcion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(TreeviewAsignadasAsync));
              return ApiResponse<IEnumerable<NodoOpcion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<NodoOpcion>>> TreeviewDisponiblesAsync(string codRolUsuario)
    {
        try { var d = (await _repo.TreeviewDisponiblesAsync(codRolUsuario)).ToList();
              return ApiResponse<IEnumerable<NodoOpcion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(TreeviewDisponiblesAsync));
              return ApiResponse<IEnumerable<NodoOpcion>>.Fail(ex.Message); }
    }

}