using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class EspecificacionesItemsTecnicosService : IEspecificacionesItemsTecnicosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EspecificacionesItemsTecnicosService);
    private readonly IEspecificacionesItemsTecnicosRepository _repo;
    private readonly ErrorLogService _log;
    public EspecificacionesItemsTecnicosService(IEspecificacionesItemsTecnicosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EspecificacionesItemsTecnicosTreeList>>> GetTreelistAsync(decimal codEntidad)
    {
        try { return ApiResponse<IEnumerable<EspecificacionesItemsTecnicosTreeList>>.Ok(await _repo.TreelistAsync(codEntidad)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetTreelistAsync));
              return ApiResponse<IEnumerable<EspecificacionesItemsTecnicosTreeList>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EspecificacionesItemsTecnicosTreeList>>> GetTreelistPorEspecificacionAsync(string? codEspecificacionTecnica)
    {
        try { return ApiResponse<IEnumerable<EspecificacionesItemsTecnicosTreeList>>.Ok(await _repo.TreelistPorEspecificacionAsync(codEspecificacionTecnica)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetTreelistPorEspecificacionAsync));
              return ApiResponse<IEnumerable<EspecificacionesItemsTecnicosTreeList>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EspecificacionesItemsTecnicosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { return ApiResponse<WriteResult>.FromWriteResult(await _repo.UpdateAsync(dto, codUsuario, codEstacion)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codEspecificacionTecnica, string codItemTecnico)
    {
        try { return ApiResponse<WriteResult>.FromWriteResult(await _repo.DeleteAsync(codEspecificacionTecnica, codItemTecnico)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EspecificacionesItemsTecnicosComboDependiente>>> GetComboDependientesAsync(
        string codEspecificacionTecnica, string? codEspecificacionTecnicaDependiente, int? codItemTecnicoDependiente, decimal? codEntidadDependiente)
    {
        try
        {
            return ApiResponse<IEnumerable<EspecificacionesItemsTecnicosComboDependiente>>.Ok(
                await _repo.ComboDependientesAsync(codEspecificacionTecnica, codEspecificacionTecnicaDependiente, codItemTecnicoDependiente, codEntidadDependiente));
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(GetComboDependientesAsync));
            return ApiResponse<IEnumerable<EspecificacionesItemsTecnicosComboDependiente>>.Fail(ex.Message);
        }
    }
}
