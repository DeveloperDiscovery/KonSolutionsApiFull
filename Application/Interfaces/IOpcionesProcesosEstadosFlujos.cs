using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IOpcionesProcesosEstadosFlujosRepository
{
    Task<IEnumerable<OpcionesProcesosEstadosFlujosTreeList>> TreelistAsync(
        string codAplicacion, string codOpcionAplicacion, short codTipoEstado);

    Task<IEnumerable<OpcionesProcesosEstadosFlujos>> ShowallAsync(
        string codAplicacion, string codOpcionAplicacion, short codTipoEstado);
    Task<WriteResult> InsertAsync(OpcionesProcesosEstadosFlujosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(OpcionesProcesosEstadosFlujosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado, string codBotonOpcion);
}

public interface IOpcionesProcesosEstadosFlujosService
{
    Task<ApiResponse<IEnumerable<OpcionesProcesosEstadosFlujosTreeList>>> GetTreelistAsync(
        string codAplicacion, string codOpcionAplicacion, short codTipoEstado);

    Task<ApiResponse<IEnumerable<OpcionesProcesosEstadosFlujos>>> GetAllAsync(
        string codAplicacion, string codOpcionAplicacion, short codTipoEstado);
    Task<ApiResponse<WriteResult>> InsertAsync(OpcionesProcesosEstadosFlujosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(OpcionesProcesosEstadosFlujosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado, string codBotonOpcion);
}
