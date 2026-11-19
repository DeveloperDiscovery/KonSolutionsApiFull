using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class InventarosAlmacenesInventariadoresService : IInventarosAlmacenesInventariadoresService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(InventarosAlmacenesInventariadoresService);
    private readonly IInventarosAlmacenesInventariadoresRepository _repo;
    private readonly ErrorLogService _log;
    public InventarosAlmacenesInventariadoresService(IInventarosAlmacenesInventariadoresRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<InventarosAlmacenesInventariadores>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<InventarosAlmacenesInventariadores>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<InventarosAlmacenesInventariadores>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<InventarosAlmacenesInventariadores>> GetByIdAsync(int NUM_INVENTARIO, string COD_ALMACEN, decimal COD_ENTIDAD_INVENTARIADOR)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_INVENTARIO, COD_ALMACEN, COD_ENTIDAD_INVENTARIADOR);
              return d is null ? ApiResponse<InventarosAlmacenesInventariadores>.Fail("No encontrado.") : ApiResponse<InventarosAlmacenesInventariadores>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<InventarosAlmacenesInventariadores>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<InventarosAlmacenesInventariadores>>> SearchAsync(InventarosAlmacenesInventariadoresSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<InventarosAlmacenesInventariadores>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<InventarosAlmacenesInventariadores>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(InventarosAlmacenesInventariadoresInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(InventarosAlmacenesInventariadoresUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_INVENTARIO, string COD_ALMACEN, decimal COD_ENTIDAD_INVENTARIADOR)
    {
        try { var r = await _repo.DeleteAsync(NUM_INVENTARIO, COD_ALMACEN, COD_ENTIDAD_INVENTARIADOR);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}