using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEspecificacionesItemsTecnicosRepository
{
    Task<IEnumerable<EspecificacionesItemsTecnicosTreeList>> TreelistAsync(decimal codEntidad);
    Task<IEnumerable<EspecificacionesItemsTecnicosTreeList>> TreelistPorEspecificacionAsync(string? codEspecificacionTecnica);
    Task<WriteResult> UpdateAsync(EspecificacionesItemsTecnicosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string codEspecificacionTecnica, string codItemTecnico);
    Task<IEnumerable<EspecificacionesItemsTecnicosComboDependiente>> ComboDependientesAsync(
        string codEspecificacionTecnica, string? codEspecificacionTecnicaDependiente, int? codItemTecnicoDependiente, decimal? codEntidadDependiente);
}

public interface IEspecificacionesItemsTecnicosService
{
    Task<ApiResponse<IEnumerable<EspecificacionesItemsTecnicosTreeList>>> GetTreelistAsync(decimal codEntidad);
    Task<ApiResponse<IEnumerable<EspecificacionesItemsTecnicosTreeList>>> GetTreelistPorEspecificacionAsync(string? codEspecificacionTecnica);
    Task<ApiResponse<WriteResult>> UpdateAsync(EspecificacionesItemsTecnicosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codEspecificacionTecnica, string codItemTecnico);
    Task<ApiResponse<IEnumerable<EspecificacionesItemsTecnicosComboDependiente>>> GetComboDependientesAsync(
        string codEspecificacionTecnica, string? codEspecificacionTecnicaDependiente, int? codItemTecnicoDependiente, decimal? codEntidadDependiente);
}
