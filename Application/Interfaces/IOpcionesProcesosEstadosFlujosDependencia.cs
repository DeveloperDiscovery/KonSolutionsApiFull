using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IOpcionesProcesosEstadosFlujosDependenciaRepository
{
    Task<IEnumerable<OpcionesProcesosEstadosFlujosDependencia>> ShowallAsync(
        string codAplicacion, string codOpcionAplicacion, short codTipoEstado, string codBotonOpcion);
    Task<WriteResult> InsertAsync(OpcionesProcesosEstadosFlujosDependenciaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado, string codBotonOpcion, short codEstado);
    Task<OpcionesProcesosEstadosFlujosDependenciaValidaResult> ValidarAsync(
        string codAplicacion, string codOpcionAplicacion, string codBotonOpcion, short codTipoEstado, short codEstadoActual);
}

public interface IOpcionesProcesosEstadosFlujosDependenciaService
{
    Task<ApiResponse<IEnumerable<OpcionesProcesosEstadosFlujosDependencia>>> GetAllAsync(
        string codAplicacion, string codOpcionAplicacion, short codTipoEstado, string codBotonOpcion);
    Task<ApiResponse<WriteResult>> InsertAsync(OpcionesProcesosEstadosFlujosDependenciaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado, string codBotonOpcion, short codEstado);
    Task<ApiResponse<OpcionesProcesosEstadosFlujosDependenciaValidaResult>> ValidarAsync(
        string codAplicacion, string codOpcionAplicacion, string codBotonOpcion, short codTipoEstado, short codEstadoActual);
}
