using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface INotificacionesAprobacionesRepository
{
    Task<NotificacionAprobacionResult> ShowbyidAsync(int numNotificacion, string codUsuario);
    Task<(WriteResult Resultado, NotificacionAprobacionAccionResult? Accion)> AccionAsync(int numNotificacion, string codAccion, string codUsuario, string codEstacion, string? desObservacion);
}

public interface INotificacionesAprobacionesService
{
    Task<ApiResponse<NotificacionAprobacionResult>> GetByIdAsync(int numNotificacion, string codUsuario);
    Task<ApiResponse<NotificacionAprobacionAccionResult>> AccionAsync(int numNotificacion, string codAccion, NotificacionAprobacionAccionDto dto, string codUsuario, string codEstacion);
}
