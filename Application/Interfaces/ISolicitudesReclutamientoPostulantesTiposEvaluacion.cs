using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISolicitudesReclutamientoPostulantesTiposEvaluacionRepository
{
    Task<IEnumerable<SolicitudesReclutamientoPostulantesTiposEvaluacion>> ShowallAsync();
    Task<SolicitudesReclutamientoPostulantesTiposEvaluacion?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SolicitudesReclutamientoPostulantesTiposEvaluacion>> SearchAsync(SolicitudesReclutamientoPostulantesTiposEvaluacionSearchDto dto);
    Task<WriteResult> InsertAsync(SolicitudesReclutamientoPostulantesTiposEvaluacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(SolicitudesReclutamientoPostulantesTiposEvaluacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, int NUM_SECUENCIA);
}

public interface ISolicitudesReclutamientoPostulantesTiposEvaluacionService
{
    Task<ApiResponse<IEnumerable<SolicitudesReclutamientoPostulantesTiposEvaluacion>>> GetAllAsync();
    Task<ApiResponse<SolicitudesReclutamientoPostulantesTiposEvaluacion>> GetByIdAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SolicitudesReclutamientoPostulantesTiposEvaluacion>>> SearchAsync(SolicitudesReclutamientoPostulantesTiposEvaluacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesReclutamientoPostulantesTiposEvaluacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(SolicitudesReclutamientoPostulantesTiposEvaluacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, int NUM_SECUENCIA);
}