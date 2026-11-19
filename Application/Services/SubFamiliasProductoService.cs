using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class SubFamiliasProductoService : ISubFamiliasProductoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SubFamiliasProductoService);
    private readonly ISubFamiliasProductoRepository _repo;
    private readonly ErrorLogService _log;
    public SubFamiliasProductoService(ISubFamiliasProductoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SubFamiliasProducto>>> GetAllAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO)
    {
        try { var d = (await _repo.ShowallAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO)).ToList();
              return ApiResponse<IEnumerable<SubFamiliasProducto>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SubFamiliasProducto>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SubFamiliasProducto>> GetByIdAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO);
              return d is null ? ApiResponse<SubFamiliasProducto>.Fail("No encontrado.") : ApiResponse<SubFamiliasProducto>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SubFamiliasProducto>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SubFamiliasProducto>>> SearchAsync(SubFamiliasProductoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<SubFamiliasProducto>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SubFamiliasProducto>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(SubFamiliasProductoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO)
    {
        try { var r = await _repo.DeleteAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SubFamiliasProductoLoadCombosResult>> GetLoadcombosAsync()
    {
        try { return ApiResponse<SubFamiliasProductoLoadCombosResult>.Ok(await _repo.LoadcombosAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetLoadcombosAsync));
              return ApiResponse<SubFamiliasProductoLoadCombosResult>.Fail(ex.Message); }
    }

}