using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IOpcionesProcesosEstadosRepository
{
    Task<IEnumerable<OpcionesProcesosEstados>> ShowallAsync(string codAplicacion, string codOpcionAplicacion);
    Task<OpcionesProcesosEstados?> ShowbyidAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codAplicacion, string codOpcionAplicacion);
    Task<IEnumerable<OpcionesProcesosEstados>> SearchAsync(OpcionesProcesosEstadosSearchDto dto);
    Task<WriteResult> InsertAsync(OpcionesProcesosEstadosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado);
}

public interface IOpcionesProcesosEstadosService
{
    Task<ApiResponse<IEnumerable<OpcionesProcesosEstados>>> GetAllAsync(string codAplicacion, string codOpcionAplicacion);
    Task<ApiResponse<OpcionesProcesosEstados>> GetByIdAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codAplicacion, string codOpcionAplicacion);
    Task<ApiResponse<IEnumerable<OpcionesProcesosEstados>>> SearchAsync(OpcionesProcesosEstadosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(OpcionesProcesosEstadosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado);
}
