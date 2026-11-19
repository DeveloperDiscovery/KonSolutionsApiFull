using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISolicitudesReclutamientoCompetenciasRepository
{
    Task<IEnumerable<SolicitudesReclutamientoCompetencias>> ShowallAsync();
    Task<SolicitudesReclutamientoCompetencias?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_COMPETENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SolicitudesReclutamientoCompetencias>> SearchAsync(SolicitudesReclutamientoCompetenciasSearchDto dto);
    Task<WriteResult> InsertAsync(SolicitudesReclutamientoCompetenciasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(SolicitudesReclutamientoCompetenciasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_COMPETENCIA);
}

public interface ISolicitudesReclutamientoCompetenciasService
{
    Task<ApiResponse<IEnumerable<SolicitudesReclutamientoCompetencias>>> GetAllAsync();
    Task<ApiResponse<SolicitudesReclutamientoCompetencias>> GetByIdAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_COMPETENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SolicitudesReclutamientoCompetencias>>> SearchAsync(SolicitudesReclutamientoCompetenciasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesReclutamientoCompetenciasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(SolicitudesReclutamientoCompetenciasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_COMPETENCIA);
}