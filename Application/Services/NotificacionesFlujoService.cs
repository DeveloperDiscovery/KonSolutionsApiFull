using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class NotificacionesFlujoService : INotificacionesFlujoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase    = nameof(NotificacionesFlujoService);
    private readonly INotificacionesFlujoRepository _repo;
    private readonly ErrorLogService _log;
    public NotificacionesFlujoService(INotificacionesFlujoRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<NotificacionesFlujoResult>> UsuarioPendienteAsync(string codUsuario)
    {
        try { return ApiResponse<NotificacionesFlujoResult>.Ok(await _repo.UsuarioPendienteAsync(codUsuario)); }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(UsuarioPendienteAsync));
            return ApiResponse<NotificacionesFlujoResult>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<List<NotificacionFlujoDetalle>>> ShowallAsync(string codUsuario, bool recepcionada)
    {
        try { return ApiResponse<List<NotificacionFlujoDetalle>>.Ok((await _repo.ShowallAsync(codUsuario, recepcionada)).ToList()); }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(ShowallAsync));
            return ApiResponse<List<NotificacionFlujoDetalle>>.Fail(ex.Message);
        }
    }
}
