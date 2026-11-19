using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISolicitudesReclutamientoRepository
{
    Task<IEnumerable<SolicitudesReclutamiento>> ShowallAsync();
    Task<SolicitudesReclutamiento?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SolicitudesReclutamiento>> SearchAsync(SolicitudesReclutamientoSearchDto dto);
    Task<WriteResult> InsertAsync(SolicitudesReclutamientoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(SolicitudesReclutamientoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO);
}

public interface ISolicitudesReclutamientoService
{
    Task<ApiResponse<IEnumerable<SolicitudesReclutamiento>>> GetAllAsync();
    Task<ApiResponse<SolicitudesReclutamiento>> GetByIdAsync(int NUM_SOLICITUD_RECLUTAMIENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SolicitudesReclutamiento>>> SearchAsync(SolicitudesReclutamientoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesReclutamientoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(SolicitudesReclutamientoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO);
}