using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class OpcionesProcesosEstadosFlujosService : IOpcionesProcesosEstadosFlujosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(OpcionesProcesosEstadosFlujosService);
    private readonly IOpcionesProcesosEstadosFlujosRepository _repo;
    private readonly ErrorLogService _log;
    public OpcionesProcesosEstadosFlujosService(IOpcionesProcesosEstadosFlujosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<OpcionesProcesosEstadosFlujosTreeList>>> GetTreelistAsync(
        string codAplicacion, string codOpcionAplicacion, short codTipoEstado)
    {
        try { return ApiResponse<IEnumerable<OpcionesProcesosEstadosFlujosTreeList>>.Ok(await _repo.TreelistAsync(codAplicacion, codOpcionAplicacion, codTipoEstado)); }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(GetTreelistAsync));
            return ApiResponse<IEnumerable<OpcionesProcesosEstadosFlujosTreeList>>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<IEnumerable<OpcionesProcesosEstadosFlujos>>> GetAllAsync(
        string codAplicacion, string codOpcionAplicacion, short codTipoEstado)
    {
        try { var d = (await _repo.ShowallAsync(codAplicacion, codOpcionAplicacion, codTipoEstado)).ToList();
              return ApiResponse<IEnumerable<OpcionesProcesosEstadosFlujos>>.Ok(d, total: d.Count); }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
            return ApiResponse<IEnumerable<OpcionesProcesosEstadosFlujos>>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(OpcionesProcesosEstadosFlujosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Flujo agregado."); }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
            return ApiResponse<WriteResult>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(OpcionesProcesosEstadosFlujosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Flujo actualizado."); }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
            return ApiResponse<WriteResult>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado, string codBotonOpcion)
    {
        try { var r = await _repo.DeleteAsync(codAplicacion, codOpcionAplicacion, codTipoEstado, codBotonOpcion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Flujo eliminado."); }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
            return ApiResponse<WriteResult>.Fail(ex.Message);
        }
    }
}
