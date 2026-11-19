using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISolicitudesReclutamientoPostulantesFormacionAcademicaRepository
{
    Task<IEnumerable<SolicitudesReclutamientoPostulantesFormacionAcademica>> ShowallAsync();
    Task<SolicitudesReclutamientoPostulantesFormacionAcademica?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, string COD_CARRERA_ACADEMICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SolicitudesReclutamientoPostulantesFormacionAcademica>> SearchAsync(SolicitudesReclutamientoPostulantesFormacionAcademicaSearchDto dto);
    Task<WriteResult> InsertAsync(SolicitudesReclutamientoPostulantesFormacionAcademicaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(SolicitudesReclutamientoPostulantesFormacionAcademicaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, string COD_CARRERA_ACADEMICA);
}

public interface ISolicitudesReclutamientoPostulantesFormacionAcademicaService
{
    Task<ApiResponse<IEnumerable<SolicitudesReclutamientoPostulantesFormacionAcademica>>> GetAllAsync();
    Task<ApiResponse<SolicitudesReclutamientoPostulantesFormacionAcademica>> GetByIdAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, string COD_CARRERA_ACADEMICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SolicitudesReclutamientoPostulantesFormacionAcademica>>> SearchAsync(SolicitudesReclutamientoPostulantesFormacionAcademicaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesReclutamientoPostulantesFormacionAcademicaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(SolicitudesReclutamientoPostulantesFormacionAcademicaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, decimal COD_ENTIDAD_POSTULANTE, string COD_CARRERA_ACADEMICA);
}