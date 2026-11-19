using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

// Solo lectura a propósito: las filas de COMERCIAL.SOLICITUDES_COMERCIALES_NOTIFICACIONES
// las genera el flujo de notificación (PROC_..._NOTIFICACION), no la UI — por eso no hay
// Insert/Delete acá aunque los SP existan.
public interface ISolicitudesComercialesNotificacionesRepository
{
    Task<IEnumerable<SolicitudesComercialesNotificaciones>> ShowallAsync(int numSolicitudComercial);
    Task<SolicitudesComercialesNotificaciones?> ShowbyidAsync(int numSolicitudComercial, int numNotificacion);
    Task<IEnumerable<SolicitudesComercialesNotificaciones>> SearchAsync(SolicitudesComercialesNotificacionesSearchDto dto);
}

public interface ISolicitudesComercialesNotificacionesService
{
    Task<ApiResponse<IEnumerable<SolicitudesComercialesNotificaciones>>> GetAllAsync(int numSolicitudComercial);
    Task<ApiResponse<SolicitudesComercialesNotificaciones>> GetByIdAsync(int numSolicitudComercial, int numNotificacion);
    Task<ApiResponse<IEnumerable<SolicitudesComercialesNotificaciones>>> SearchAsync(SolicitudesComercialesNotificacionesSearchDto dto);
}
