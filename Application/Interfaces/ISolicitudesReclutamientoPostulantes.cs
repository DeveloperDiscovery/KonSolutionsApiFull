using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISolicitudesReclutamientoPostulantesRepository
{
    Task<IEnumerable<SolicitudesReclutamientoPostulantes>> ShowallAsync();
    Task<SolicitudesReclutamientoPostulantes?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SolicitudesReclutamientoPostulantes>> SearchAsync(SolicitudesReclutamientoPostulantesSearchDto dto);
    Task<WriteResult> InsertAsync(SolicitudesReclutamientoPostulantesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(SolicitudesReclutamientoPostulantesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE);
}

public interface ISolicitudesReclutamientoPostulantesService
{
    Task<ApiResponse<IEnumerable<SolicitudesReclutamientoPostulantes>>> GetAllAsync();
    Task<ApiResponse<SolicitudesReclutamientoPostulantes>> GetByIdAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SolicitudesReclutamientoPostulantes>>> SearchAsync(SolicitudesReclutamientoPostulantesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesReclutamientoPostulantesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(SolicitudesReclutamientoPostulantesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE);
}