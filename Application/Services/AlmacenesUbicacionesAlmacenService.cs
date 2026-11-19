using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class AlmacenesUbicacionesAlmacenService : IAlmacenesUbicacionesAlmacenService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(AlmacenesUbicacionesAlmacenService);
    private readonly IAlmacenesUbicacionesAlmacenRepository _repo;
    private readonly ErrorLogService _log;
    public AlmacenesUbicacionesAlmacenService(IAlmacenesUbicacionesAlmacenRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<AlmacenesUbicacionesAlmacen>>> GetAllAsync(string COD_ALMACEN)
    {
        try { var d = (await _repo.ShowallAsync(COD_ALMACEN)).ToList();
              return ApiResponse<IEnumerable<AlmacenesUbicacionesAlmacen>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<AlmacenesUbicacionesAlmacen>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<AlmacenesUbicacionesAlmacen>> GetByIdAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ALMACEN, COD_UBICACION_ALMACEN);
              return d is null ? ApiResponse<AlmacenesUbicacionesAlmacen>.Fail("No encontrado.") : ApiResponse<AlmacenesUbicacionesAlmacen>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<AlmacenesUbicacionesAlmacen>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_ALMACEN)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(COD_ALMACEN)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<AlmacenesUbicacionesAlmacen>>> SearchAsync(AlmacenesUbicacionesAlmacenSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<AlmacenesUbicacionesAlmacen>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<AlmacenesUbicacionesAlmacen>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(AlmacenesUbicacionesAlmacenInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN)
    {
        try { var r = await _repo.DeleteAsync(COD_ALMACEN, COD_UBICACION_ALMACEN);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}