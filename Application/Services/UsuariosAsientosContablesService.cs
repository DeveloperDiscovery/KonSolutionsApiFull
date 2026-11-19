using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

public class UsuariosAsientosContablesService : IUsuariosAsientosContablesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(UsuariosAsientosContablesService);
    private readonly IUsuariosAsientosContablesRepository _repo;
    private readonly ErrorLogService _log;
    public UsuariosAsientosContablesService(IUsuariosAsientosContablesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<UsuariosAsientosContables>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<UsuariosAsientosContables>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<UsuariosAsientosContables>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<UsuariosAsientosContables>> GetByIdAsync(string COD_USUARIO, string COD_ASIENTO_CONTABLE)
    {
        try { var d = await _repo.ShowbyidAsync(COD_USUARIO, COD_ASIENTO_CONTABLE);
              return d is null ? ApiResponse<UsuariosAsientosContables>.Fail("No encontrado.") : ApiResponse<UsuariosAsientosContables>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<UsuariosAsientosContables>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<UsuariosAsientosContables>>> SearchAsync(UsuariosAsientosContablesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<UsuariosAsientosContables>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<UsuariosAsientosContables>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(UsuariosAsientosContablesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_USUARIO, string COD_ASIENTO_CONTABLE)
    {
        try { var r = await _repo.DeleteAsync(COD_USUARIO, COD_ASIENTO_CONTABLE);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}