using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISolicitudesReclutamientoTiposEvaluacionRepository
{
    Task<IEnumerable<SolicitudesReclutamientoTiposEvaluacion>> ShowallAsync();
    Task<SolicitudesReclutamientoTiposEvaluacion?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_TIPO_EVALUACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SolicitudesReclutamientoTiposEvaluacion>> SearchAsync(SolicitudesReclutamientoTiposEvaluacionSearchDto dto);
    Task<WriteResult> InsertAsync(SolicitudesReclutamientoTiposEvaluacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(SolicitudesReclutamientoTiposEvaluacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_TIPO_EVALUACION);
}

public interface ISolicitudesReclutamientoTiposEvaluacionService
{
    Task<ApiResponse<IEnumerable<SolicitudesReclutamientoTiposEvaluacion>>> GetAllAsync();
    Task<ApiResponse<SolicitudesReclutamientoTiposEvaluacion>> GetByIdAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_TIPO_EVALUACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SolicitudesReclutamientoTiposEvaluacion>>> SearchAsync(SolicitudesReclutamientoTiposEvaluacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesReclutamientoTiposEvaluacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(SolicitudesReclutamientoTiposEvaluacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_TIPO_EVALUACION);
}