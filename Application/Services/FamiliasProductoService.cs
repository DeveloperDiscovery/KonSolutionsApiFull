using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class FamiliasProductoService : IFamiliasProductoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(FamiliasProductoService);
    private readonly IFamiliasProductoRepository _repo;
    private readonly ErrorLogService _log;
    public FamiliasProductoService(IFamiliasProductoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<FamiliasProducto>>> GetAllAsync(string COD_GRUPO_PRODUCTO)
    {
        try { var d = (await _repo.ShowallAsync(COD_GRUPO_PRODUCTO)).ToList();
              return ApiResponse<IEnumerable<FamiliasProducto>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<FamiliasProducto>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<FamiliasProducto>> GetByIdAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO);
              return d is null ? ApiResponse<FamiliasProducto>.Fail("No encontrado.") : ApiResponse<FamiliasProducto>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<FamiliasProducto>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<FamiliasProducto>>> SearchAsync(FamiliasProductoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<FamiliasProducto>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<FamiliasProducto>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(FamiliasProductoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO)
    {
        try { var r = await _repo.DeleteAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<FamiliasProductoLoadCombosResult>> GetLoadcombosAsync()
    {
        try { return ApiResponse<FamiliasProductoLoadCombosResult>.Ok(await _repo.LoadcombosAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetLoadcombosAsync));
              return ApiResponse<FamiliasProductoLoadCombosResult>.Fail(ex.Message); }
    }

}