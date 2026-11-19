using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class ContenedoresAlmacenajeService : IContenedoresAlmacenajeService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ContenedoresAlmacenajeService);
    private readonly IContenedoresAlmacenajeRepository _repo;
    private readonly ErrorLogService _log;
    public ContenedoresAlmacenajeService(IContenedoresAlmacenajeRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ContenedoresAlmacenaje>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ContenedoresAlmacenaje>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ContenedoresAlmacenaje>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ContenedoresAlmacenaje>> GetByIdAsync(string COD_CONTENEDOR)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CONTENEDOR);
              return d is null ? ApiResponse<ContenedoresAlmacenaje>.Fail("No encontrado.") : ApiResponse<ContenedoresAlmacenaje>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ContenedoresAlmacenaje>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ContenedoresAlmacenaje>>> SearchAsync(ContenedoresAlmacenajeSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ContenedoresAlmacenaje>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ContenedoresAlmacenaje>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ContenedoresAlmacenajeInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CONTENEDOR)
    {
        try { var r = await _repo.DeleteAsync(COD_CONTENEDOR);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ContenedoresAlmacenajeLoadCombosResult>> LoadcombosAsync()
    {
        try { return ApiResponse<ContenedoresAlmacenajeLoadCombosResult>.Ok(await _repo.LoadcombosAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<ContenedoresAlmacenajeLoadCombosResult>.Fail(ex.Message); }
    }
}
