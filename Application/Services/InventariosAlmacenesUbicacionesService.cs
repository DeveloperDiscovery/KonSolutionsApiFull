using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class InventariosAlmacenesUbicacionesService : IInventariosAlmacenesUbicacionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(InventariosAlmacenesUbicacionesService);
    private readonly IInventariosAlmacenesUbicacionesRepository _repo;
    private readonly ErrorLogService _log;
    public InventariosAlmacenesUbicacionesService(IInventariosAlmacenesUbicacionesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesUbicaciones>>> GetAllAsync(int numInventario, string codAlmacen)
    {
        try { var d = (await _repo.ShowallAsync(numInventario, codAlmacen)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesUbicaciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesUbicaciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<InventariosAlmacenesUbicaciones>> GetByIdAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen)
    {
        try { var d = await _repo.ShowbyidAsync(numInventario, codAlmacen, codUbicacionAlmacen);
              return d is null ? ApiResponse<InventariosAlmacenesUbicaciones>.Fail("No encontrado.") : ApiResponse<InventariosAlmacenesUbicaciones>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<InventariosAlmacenesUbicaciones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(int numInventario, string codAlmacen)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(numInventario, codAlmacen)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesUbicaciones>>> SearchAsync(InventariosAlmacenesUbicacionesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesUbicaciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesUbicaciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesUbicacionesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen)
    {
        try { var r = await _repo.DeleteAsync(numInventario, codAlmacen, codUbicacionAlmacen);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
