using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class InventariosAlmacenesImagenService : IInventariosAlmacenesImagenService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(InventariosAlmacenesImagenService);
    private readonly IInventariosAlmacenesImagenRepository _repo;
    private readonly ErrorLogService _log;
    public InventariosAlmacenesImagenService(IInventariosAlmacenesImagenRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesImagen>>> GetAllAsync(int numInventario, string codAlmacen)
    {
        try { var d = (await _repo.ShowallAsync(numInventario, codAlmacen)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesImagen>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesImagen>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<InventariosAlmacenesImagen>> GetByIdAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen, string codProducto, string numLoteControl, string codProceso, int numOrdenProduccion, int numOrdenProduccionItem, int numOrdenProduccionUnidad)
    {
        try { var d = await _repo.ShowbyidAsync(numInventario, codAlmacen, codUbicacionAlmacen, codProducto, numLoteControl, codProceso, numOrdenProduccion, numOrdenProduccionItem, numOrdenProduccionUnidad);
              return d is null ? ApiResponse<InventariosAlmacenesImagen>.Fail("No encontrado.") : ApiResponse<InventariosAlmacenesImagen>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<InventariosAlmacenesImagen>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(int numInventario, string codAlmacen)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(numInventario, codAlmacen)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesImagen>>> SearchAsync(InventariosAlmacenesImagenSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesImagen>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesImagen>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesImagenInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(InventariosAlmacenesImagenUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen, string codProducto, string numLoteControl, string codProceso, int numOrdenProduccion, int numOrdenProduccionItem, int numOrdenProduccionUnidad)
    {
        try { var r = await _repo.DeleteAsync(numInventario, codAlmacen, codUbicacionAlmacen, codProducto, numLoteControl, codProceso, numOrdenProduccion, numOrdenProduccionItem, numOrdenProduccionUnidad);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
