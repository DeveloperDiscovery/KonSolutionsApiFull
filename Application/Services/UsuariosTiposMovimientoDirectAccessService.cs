using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class UsuariosTiposMovimientoDirectAccessService : IUsuariosTiposMovimientoDirectAccessService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(UsuariosTiposMovimientoDirectAccessService);
    private readonly IUsuariosTiposMovimientoDirectAccessRepository _repo;
    private readonly ErrorLogService _log;
    public UsuariosTiposMovimientoDirectAccessService(IUsuariosTiposMovimientoDirectAccessRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<UsuariosTiposMovimientoDirectAccess>>> GetAllAsync(string codUsuario, string codClaseMovimiento)
    {
        try { var d = (await _repo.ShowallAsync(codUsuario, codClaseMovimiento)).ToList();
              return ApiResponse<IEnumerable<UsuariosTiposMovimientoDirectAccess>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<UsuariosTiposMovimientoDirectAccess>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<UsuariosTiposMovimientoDirectAccess>> GetByIdAsync(string codUsuario, string codClaseMovimiento, string codTipoMovimiento)
    {
        try { var d = await _repo.ShowbyidAsync(codUsuario, codClaseMovimiento, codTipoMovimiento);
              return d is null ? ApiResponse<UsuariosTiposMovimientoDirectAccess>.Fail("No encontrado.") : ApiResponse<UsuariosTiposMovimientoDirectAccess>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<UsuariosTiposMovimientoDirectAccess>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codUsuario, string codClaseMovimiento)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(codUsuario, codClaseMovimiento)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<UsuariosTiposMovimientoDirectAccess>>> SearchAsync(string codUsuario, string codClaseMovimiento, UsuariosTiposMovimientoDirectAccessSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(codUsuario, codClaseMovimiento, dto)).ToList();
              return ApiResponse<IEnumerable<UsuariosTiposMovimientoDirectAccess>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<UsuariosTiposMovimientoDirectAccess>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(UsuariosTiposMovimientoDirectAccessInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Acceso directo agregado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codUsuario, string codClaseMovimiento, string codTipoMovimiento)
    {
        try { var r = await _repo.DeleteAsync(codUsuario, codClaseMovimiento, codTipoMovimiento);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Acceso directo quitado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
