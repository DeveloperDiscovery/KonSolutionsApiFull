using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class InventariosAlmacenesService : IInventariosAlmacenesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(InventariosAlmacenesService);
    private readonly IInventariosAlmacenesRepository _repo;
    private readonly ErrorLogService _log;
    public InventariosAlmacenesService(IInventariosAlmacenesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenes>>> GetAllAsync(int? numInventario = null)
    {
        try { var d = (await _repo.ShowallAsync(numInventario)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenes>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenes>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<InventariosAlmacenes>> GetByIdAsync(int NUM_INVENTARIO, string COD_ALMACEN)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_INVENTARIO, COD_ALMACEN);
              return d is null ? ApiResponse<InventariosAlmacenes>.Fail("No encontrado.") : ApiResponse<InventariosAlmacenes>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<InventariosAlmacenes>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenes>>> SearchAsync(InventariosAlmacenesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenes>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenes>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(InventariosAlmacenesUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_INVENTARIO, string COD_ALMACEN)
    {
        try { var r = await _repo.DeleteAsync(NUM_INVENTARIO, COD_ALMACEN);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}