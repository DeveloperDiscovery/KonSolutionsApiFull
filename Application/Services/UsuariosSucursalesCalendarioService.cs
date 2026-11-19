using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

public class UsuariosSucursalesCalendarioService : IUsuariosSucursalesCalendarioService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(UsuariosSucursalesCalendarioService);
    private readonly IUsuariosSucursalesCalendarioRepository _repo;
    private readonly ErrorLogService _log;
    public UsuariosSucursalesCalendarioService(IUsuariosSucursalesCalendarioRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<UsuariosSucursalesCalendario>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<UsuariosSucursalesCalendario>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<UsuariosSucursalesCalendario>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<UsuariosSucursalesCalendario>> GetByIdAsync(string COD_USUARIO, string COD_SUCURSAL, DateTime FEC_INICIAL)
    {
        try { var d = await _repo.ShowbyidAsync(COD_USUARIO, COD_SUCURSAL, FEC_INICIAL);
              return d is null ? ApiResponse<UsuariosSucursalesCalendario>.Fail("No encontrado.") : ApiResponse<UsuariosSucursalesCalendario>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<UsuariosSucursalesCalendario>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<UsuariosSucursalesCalendario>>> SearchAsync(UsuariosSucursalesCalendarioSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<UsuariosSucursalesCalendario>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<UsuariosSucursalesCalendario>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(UsuariosSucursalesCalendarioInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(UsuariosSucursalesCalendarioUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_USUARIO, string COD_SUCURSAL, DateTime FEC_INICIAL)
    {
        try { var r = await _repo.DeleteAsync(COD_USUARIO, COD_SUCURSAL, FEC_INICIAL);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}