using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class TiposCatalogoPreciosService : ITiposCatalogoPreciosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposCatalogoPreciosService);
    private readonly ITiposCatalogoPreciosRepository _repo;
    private readonly ErrorLogService _log;
    public TiposCatalogoPreciosService(ITiposCatalogoPreciosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposCatalogoPrecios>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<TiposCatalogoPrecios>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposCatalogoPrecios>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposCatalogoPrecios>> GetByIdAsync(string COD_TIPO_CATALOGO_PRECIO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TIPO_CATALOGO_PRECIO);
              return d is null ? ApiResponse<TiposCatalogoPrecios>.Fail("No encontrado.") : ApiResponse<TiposCatalogoPrecios>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposCatalogoPrecios>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposCatalogoPrecios>>> SearchAsync(TiposCatalogoPreciosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposCatalogoPrecios>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposCatalogoPrecios>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposCatalogoPreciosInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(TiposCatalogoPreciosUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CATALOGO_PRECIO)
    {
        try { var r = await _repo.DeleteAsync(COD_TIPO_CATALOGO_PRECIO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}