using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class ProductosService : IProductosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProductosService);
    private readonly IProductosRepository _repo;
    private readonly ErrorLogService _log;
    public ProductosService(IProductosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<Productos>>> GetAllAsync(ProductosSearchDto? filtros = null)
    {
        try { var d = (await _repo.ShowallAsync(filtros)).ToList();
              return ApiResponse<IEnumerable<Productos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<Productos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<Productos>> GetByIdAsync(string COD_PRODUCTO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_PRODUCTO);
              return d is null ? ApiResponse<Productos>.Fail("No encontrado.") : ApiResponse<Productos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<Productos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<Productos>>> SearchAsync(ProductosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<Productos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<Productos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ProductoEspecificacionArbol>>> GetEspecificacionesArbolAsync(ProductosSearchDto? filtros)
    {
        try { var d = (await _repo.EspecificacionesArbolAsync(filtros)).ToList();
              return ApiResponse<IEnumerable<ProductoEspecificacionArbol>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetEspecificacionesArbolAsync));
              return ApiResponse<IEnumerable<ProductoEspecificacionArbol>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<Productos>>> FiltrarPorEspecificacionesAsync(ProductosSearchDto dto)
    {
        try { var d = (await _repo.FiltrarPorEspecificacionesAsync(dto)).ToList();
              return ApiResponse<IEnumerable<Productos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(FiltrarPorEspecificacionesAsync));
              return ApiResponse<IEnumerable<Productos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ProductosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ProductosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PRODUCTO)
    {
        try { var r = await _repo.DeleteAsync(COD_PRODUCTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ProductosLoadCombosResult>> GetLoadcombosAsync()
    {
        try { return ApiResponse<ProductosLoadCombosResult>.Ok(await _repo.LoadcombosAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetLoadcombosAsync));
              return ApiResponse<ProductosLoadCombosResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<bool>> ActualizarDescripcionDetalleAsync(string COD_PRODUCTO)
    {
        try { await _repo.ActualizarDescripcionDetalleAsync(COD_PRODUCTO);
              return ApiResponse<bool>.Ok(true); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ActualizarDescripcionDetalleAsync));
              return ApiResponse<bool>.Fail(ex.Message); }
    }

}