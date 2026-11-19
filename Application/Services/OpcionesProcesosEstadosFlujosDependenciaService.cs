using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class OpcionesProcesosEstadosFlujosDependenciaService : IOpcionesProcesosEstadosFlujosDependenciaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(OpcionesProcesosEstadosFlujosDependenciaService);
    private readonly IOpcionesProcesosEstadosFlujosDependenciaRepository _repo;
    private readonly ErrorLogService _log;
    public OpcionesProcesosEstadosFlujosDependenciaService(IOpcionesProcesosEstadosFlujosDependenciaRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<OpcionesProcesosEstadosFlujosDependencia>>> GetAllAsync(
        string codAplicacion, string codOpcionAplicacion, short codTipoEstado, string codBotonOpcion)
    {
        try { var d = (await _repo.ShowallAsync(codAplicacion, codOpcionAplicacion, codTipoEstado, codBotonOpcion)).ToList();
              return ApiResponse<IEnumerable<OpcionesProcesosEstadosFlujosDependencia>>.Ok(d, total: d.Count); }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
            return ApiResponse<IEnumerable<OpcionesProcesosEstadosFlujosDependencia>>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(OpcionesProcesosEstadosFlujosDependenciaInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Estado previo agregado."); }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
            return ApiResponse<WriteResult>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado, string codBotonOpcion, short codEstado)
    {
        try { var r = await _repo.DeleteAsync(codAplicacion, codOpcionAplicacion, codTipoEstado, codBotonOpcion, codEstado);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Estado previo quitado."); }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
            return ApiResponse<WriteResult>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<OpcionesProcesosEstadosFlujosDependenciaValidaResult>> ValidarAsync(
        string codAplicacion, string codOpcionAplicacion, string codBotonOpcion, short codTipoEstado, short codEstadoActual)
    {
        try { return ApiResponse<OpcionesProcesosEstadosFlujosDependenciaValidaResult>.Ok(
                  await _repo.ValidarAsync(codAplicacion, codOpcionAplicacion, codBotonOpcion, codTipoEstado, codEstadoActual)); }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(ValidarAsync));
            return ApiResponse<OpcionesProcesosEstadosFlujosDependenciaValidaResult>.Fail(ex.Message);
        }
    }
}
