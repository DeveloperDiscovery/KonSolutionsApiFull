using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

public class AplicacionesOpcionesBotonesService : IAplicacionesOpcionesBotonesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(AplicacionesOpcionesBotonesService);
    private readonly IAplicacionesOpcionesBotonesRepository _repo;
    private readonly ErrorLogService _log;
    public AplicacionesOpcionesBotonesService(IAplicacionesOpcionesBotonesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<AplicacionesOpcionesBotones>>> GetAllAsync(string codAplicacion, string codOpcionAplicacion)
    {
        try { var d = (await _repo.ShowallAsync(codAplicacion, codOpcionAplicacion)).ToList();
              return ApiResponse<IEnumerable<AplicacionesOpcionesBotones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<AplicacionesOpcionesBotones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<AplicacionesOpcionesBotones>> GetByIdAsync(string COD_APLICACION, string COD_OPCION_APLICACION, string COD_BOTON_OPCION)
    {
        try { var d = await _repo.ShowbyidAsync(COD_APLICACION, COD_OPCION_APLICACION, COD_BOTON_OPCION);
              return d is null ? ApiResponse<AplicacionesOpcionesBotones>.Fail("No encontrado.") : ApiResponse<AplicacionesOpcionesBotones>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<AplicacionesOpcionesBotones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<AplicacionesOpcionesBotones>>> SearchAsync(AplicacionesOpcionesBotonesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<AplicacionesOpcionesBotones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<AplicacionesOpcionesBotones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(AplicacionesOpcionesBotonesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_APLICACION, string COD_OPCION_APLICACION, string COD_BOTON_OPCION)
    {
        try { var r = await _repo.DeleteAsync(COD_APLICACION, COD_OPCION_APLICACION, COD_BOTON_OPCION);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<AplicacionesOpcionesBotonesLoadCombosResult>> LoadcombosAsync(int codTipoEstado)
    {
        try { return ApiResponse<AplicacionesOpcionesBotonesLoadCombosResult>.Ok(await _repo.LoadcombosAsync(codTipoEstado)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<AplicacionesOpcionesBotonesLoadCombosResult>.Fail(ex.Message); }
    }

}