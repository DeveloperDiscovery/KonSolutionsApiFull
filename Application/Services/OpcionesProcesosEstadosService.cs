using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class OpcionesProcesosEstadosService : IOpcionesProcesosEstadosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(OpcionesProcesosEstadosService);
    private readonly IOpcionesProcesosEstadosRepository _repo;
    private readonly ErrorLogService _log;
    public OpcionesProcesosEstadosService(IOpcionesProcesosEstadosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<OpcionesProcesosEstados>>> GetAllAsync(string codAplicacion, string codOpcionAplicacion)
    {
        try { var d = (await _repo.ShowallAsync(codAplicacion, codOpcionAplicacion)).ToList();
              return ApiResponse<IEnumerable<OpcionesProcesosEstados>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<OpcionesProcesosEstados>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<OpcionesProcesosEstados>> GetByIdAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado)
    {
        try { var d = await _repo.ShowbyidAsync(codAplicacion, codOpcionAplicacion, codTipoEstado);
              return d is null ? ApiResponse<OpcionesProcesosEstados>.Fail("No encontrado.") : ApiResponse<OpcionesProcesosEstados>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<OpcionesProcesosEstados>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codAplicacion, string codOpcionAplicacion)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(codAplicacion, codOpcionAplicacion)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<OpcionesProcesosEstados>>> SearchAsync(OpcionesProcesosEstadosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<OpcionesProcesosEstados>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<OpcionesProcesosEstados>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(OpcionesProcesosEstadosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado)
    {
        try { var r = await _repo.DeleteAsync(codAplicacion, codOpcionAplicacion, codTipoEstado);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
