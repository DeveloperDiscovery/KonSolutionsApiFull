using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface INotificacionesFlujoRepository
{
    Task<NotificacionesFlujoResult> UsuarioPendienteAsync(string codUsuario);
    Task<IEnumerable<NotificacionFlujoDetalle>> ShowallAsync(string codUsuario, bool recepcionada);
}

public interface INotificacionesFlujoService
{
    Task<ApiResponse<NotificacionesFlujoResult>> UsuarioPendienteAsync(string codUsuario);
    Task<ApiResponse<List<NotificacionFlujoDetalle>>> ShowallAsync(string codUsuario, bool recepcionada);
}
