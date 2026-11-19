using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Zonificacion;

namespace KonSolutionsApi.Application.Services;

public class ZonificacionService : IZonificacionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ZonificacionService);
    private readonly IZonificacionRepository _repo;
    private readonly ErrorLogService _log;
    public ZonificacionService(IZonificacionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<Zonificacion>>> GetAllAsync(string COD_SUCURSAL)
    {
        try { var d = (await _repo.ShowallAsync(COD_SUCURSAL)).ToList();
              return ApiResponse<IEnumerable<Zonificacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<Zonificacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<Zonificacion>> GetByIdAsync(string COD_SUCURSAL, string COD_ZONIFICACION)
    {
        try { var d = await _repo.ShowbyidAsync(COD_SUCURSAL, COD_ZONIFICACION);
              return d is null ? ApiResponse<Zonificacion>.Fail("No encontrado.") : ApiResponse<Zonificacion>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<Zonificacion>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_SUCURSAL)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(COD_SUCURSAL)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<Zonificacion>>> SearchAsync(ZonificacionSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<Zonificacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<Zonificacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ZonificacionInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_SUCURSAL, string COD_ZONIFICACION)
    {
        try { var r = await _repo.DeleteAsync(COD_SUCURSAL, COD_ZONIFICACION);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ZonificacionLoadCombosResult>> LoadcombosAsync(string codSucursal, int codTipoEstado)
    {
        try { return ApiResponse<ZonificacionLoadCombosResult>.Ok(await _repo.LoadcombosAsync(codSucursal, codTipoEstado)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<ZonificacionLoadCombosResult>.Fail(ex.Message); }
    }

}