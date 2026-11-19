using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class GruposProductoService : IGruposProductoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(GruposProductoService);
    private readonly IGruposProductoRepository _repo;
    private readonly ErrorLogService _log;
    public GruposProductoService(IGruposProductoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<GruposProducto>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<GruposProducto>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<GruposProducto>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GruposProducto>> GetByIdAsync(string COD_GRUPO_PRODUCTO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_GRUPO_PRODUCTO);
              return d is null ? ApiResponse<GruposProducto>.Fail("No encontrado.") : ApiResponse<GruposProducto>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<GruposProducto>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<GruposProducto>>> SearchAsync(GruposProductoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<GruposProducto>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<GruposProducto>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(GruposProductoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_GRUPO_PRODUCTO)
    {
        try { var r = await _repo.DeleteAsync(COD_GRUPO_PRODUCTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GruposProductoLoadCombosResult>> GetLoadcombosAsync()
    {
        try { return ApiResponse<GruposProductoLoadCombosResult>.Ok(await _repo.LoadcombosAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetLoadcombosAsync));
              return ApiResponse<GruposProductoLoadCombosResult>.Fail(ex.Message); }
    }

}