using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class ProductosAlmacenesService : IProductosAlmacenesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProductosAlmacenesService);
    private readonly IProductosAlmacenesRepository _repo;
    private readonly ErrorLogService _log;
    public ProductosAlmacenesService(IProductosAlmacenesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ProductosAlmacenes>>> GetAllAsync(string codProducto)
    {
        try { var d = (await _repo.ShowallAsync(codProducto)).ToList();
              return ApiResponse<IEnumerable<ProductosAlmacenes>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ProductosAlmacenes>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ProductosAlmacenes>>> GetAllPorAlmacenAsync(string codAlmacen)
    {
        try { var d = (await _repo.ShowallPorAlmacenAsync(codAlmacen)).ToList();
              return ApiResponse<IEnumerable<ProductosAlmacenes>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllPorAlmacenAsync));
              return ApiResponse<IEnumerable<ProductosAlmacenes>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ProductosAlmacenes>>> GetAllRequerirAsync()
    {
        try { var d = (await _repo.ShowallRequerirAsync()).ToList();
              return ApiResponse<IEnumerable<ProductosAlmacenes>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllRequerirAsync));
              return ApiResponse<IEnumerable<ProductosAlmacenes>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ProductosAlmacenes>> GetByIdAsync(string COD_PRODUCTO, string COD_ALMACEN)
    {
        try { var d = await _repo.ShowbyidAsync(COD_PRODUCTO, COD_ALMACEN);
              return d is null ? ApiResponse<ProductosAlmacenes>.Fail("No encontrado.") : ApiResponse<ProductosAlmacenes>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ProductosAlmacenes>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codProducto)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(codProducto)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ProductosAlmacenes>>> SearchAsync(string codProducto, ProductosAlmacenesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(codProducto, dto)).ToList();
              return ApiResponse<IEnumerable<ProductosAlmacenes>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ProductosAlmacenes>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ProductosAlmacenesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ProductosAlmacenesUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PRODUCTO, string COD_ALMACEN)
    {
        try { var r = await _repo.DeleteAsync(COD_PRODUCTO, COD_ALMACEN);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> ResetAsync(string? codAlmacen, string? codProducto)
    {
        try { var r = await _repo.ResetAsync(codAlmacen, codProducto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Requerimiento reiniciado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ResetAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> RequerimientoAsync(string codUsuario, string codEstacion)
    {
        try { var r = await _repo.RequerimientoAsync(codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Requerimiento de compra generado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(RequerimientoAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> CantidadesAsync(string lstAlmacenes, string lstProductos)
    {
        try { var r = await _repo.CantidadesAsync(lstAlmacenes, lstProductos);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Stock de seguridad recalculado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CantidadesAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> ReversionAsync(int numRequerimientoStock, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.ReversionAsync(numRequerimientoStock, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Requerimiento revertido."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ReversionAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
