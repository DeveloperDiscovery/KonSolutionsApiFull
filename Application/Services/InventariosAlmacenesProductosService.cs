using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class InventariosAlmacenesProductosService : IInventariosAlmacenesProductosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(InventariosAlmacenesProductosService);
    private readonly IInventariosAlmacenesProductosRepository _repo;
    private readonly ErrorLogService _log;
    public InventariosAlmacenesProductosService(IInventariosAlmacenesProductosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesProductos>>> GetAllAsync(int numInventario, string codAlmacen)
    {
        try { var d = (await _repo.ShowallAsync(numInventario, codAlmacen)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesProductos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesProductos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<InventariosAlmacenesProductos>> GetByIdAsync(int numInventario, string codAlmacen, string codProducto)
    {
        try { var d = await _repo.ShowbyidAsync(numInventario, codAlmacen, codProducto);
              return d is null ? ApiResponse<InventariosAlmacenesProductos>.Fail("No encontrado.") : ApiResponse<InventariosAlmacenesProductos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<InventariosAlmacenesProductos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(int numInventario, string codAlmacen)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(numInventario, codAlmacen)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesProductos>>> SearchAsync(InventariosAlmacenesProductosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesProductos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesProductos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesProductosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int numInventario, string codAlmacen, string codProducto)
    {
        try { var r = await _repo.DeleteAsync(numInventario, codAlmacen, codProducto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
