using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAplicacionesOpcionesAprobacionesRepository
{
    Task<IEnumerable<AplicacionesOpcionesAprobaciones>> ShowallAsync(string codAplicacion, string codOpcion);
    Task<AplicacionesOpcionesAprobaciones?> ShowbyidAsync(string codAplicacion, string codOpcion, int numSecuencia);
    Task<IEnumerable<AplicacionesOpcionesAprobaciones>> SearchAsync(AplicacionesOpcionesAprobacionesSearchDto dto);
    /// <summary>Conteo por opción de TODAS las opciones configuradas, en un solo viaje.</summary>
    Task<IEnumerable<AplicacionesOpcionesAprobacionesResumen>> ResumenAsync();
    Task<WriteResult> InsertAsync(AplicacionesOpcionesAprobacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(AplicacionesOpcionesAprobacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string codAplicacion, string codOpcion, int numSecuencia);
}

public interface IAplicacionesOpcionesAprobacionesService
{
    Task<ApiResponse<IEnumerable<AplicacionesOpcionesAprobaciones>>> GetAllAsync(string codAplicacion, string codOpcion);
    Task<ApiResponse<AplicacionesOpcionesAprobaciones>> GetByIdAsync(string codAplicacion, string codOpcion, int numSecuencia);
    Task<ApiResponse<IEnumerable<AplicacionesOpcionesAprobaciones>>> SearchAsync(AplicacionesOpcionesAprobacionesSearchDto dto);
    Task<ApiResponse<IEnumerable<AplicacionesOpcionesAprobacionesResumen>>> GetResumenAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(AplicacionesOpcionesAprobacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(AplicacionesOpcionesAprobacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codAplicacion, string codOpcion, int numSecuencia);
}
