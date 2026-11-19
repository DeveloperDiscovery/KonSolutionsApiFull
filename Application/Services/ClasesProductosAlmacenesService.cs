using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Services;

public class ClasesProductosAlmacenesService : IClasesProductosAlmacenesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ClasesProductosAlmacenesService);
    private readonly IClasesProductosAlmacenesRepository _repo;
    private readonly ErrorLogService _log;
    public ClasesProductosAlmacenesService(IClasesProductosAlmacenesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ClasesProductosAlmacenes>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ClasesProductosAlmacenes>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ClasesProductosAlmacenes>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ClasesProductosAlmacenes>> GetByIdAsync(string COD_CLASE_PRODUCTO, string COD_ALMACEN)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CLASE_PRODUCTO, COD_ALMACEN);
              return d is null ? ApiResponse<ClasesProductosAlmacenes>.Fail("No encontrado.") : ApiResponse<ClasesProductosAlmacenes>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ClasesProductosAlmacenes>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ClasesProductosAlmacenes>>> SearchAsync(ClasesProductosAlmacenesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ClasesProductosAlmacenes>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ClasesProductosAlmacenes>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ClasesProductosAlmacenesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_PRODUCTO, string COD_ALMACEN)
    {
        try { var r = await _repo.DeleteAsync(COD_CLASE_PRODUCTO, COD_ALMACEN);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}