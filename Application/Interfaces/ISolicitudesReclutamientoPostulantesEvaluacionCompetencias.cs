using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISolicitudesReclutamientoPostulantesEvaluacionCompetenciasRepository
{
    Task<IEnumerable<SolicitudesReclutamientoPostulantesEvaluacionCompetencias>> ShowallAsync();
    Task<SolicitudesReclutamientoPostulantesEvaluacionCompetencias?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, string COD_COMPETENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SolicitudesReclutamientoPostulantesEvaluacionCompetencias>> SearchAsync(SolicitudesReclutamientoPostulantesEvaluacionCompetenciasSearchDto dto);
    Task<WriteResult> InsertAsync(SolicitudesReclutamientoPostulantesEvaluacionCompetenciasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(SolicitudesReclutamientoPostulantesEvaluacionCompetenciasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, string COD_COMPETENCIA);
}

public interface ISolicitudesReclutamientoPostulantesEvaluacionCompetenciasService
{
    Task<ApiResponse<IEnumerable<SolicitudesReclutamientoPostulantesEvaluacionCompetencias>>> GetAllAsync();
    Task<ApiResponse<SolicitudesReclutamientoPostulantesEvaluacionCompetencias>> GetByIdAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, string COD_COMPETENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SolicitudesReclutamientoPostulantesEvaluacionCompetencias>>> SearchAsync(SolicitudesReclutamientoPostulantesEvaluacionCompetenciasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesReclutamientoPostulantesEvaluacionCompetenciasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(SolicitudesReclutamientoPostulantesEvaluacionCompetenciasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, string COD_COMPETENCIA);
}