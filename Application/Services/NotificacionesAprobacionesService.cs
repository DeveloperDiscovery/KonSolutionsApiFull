using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class NotificacionesAprobacionesService : INotificacionesAprobacionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase    = nameof(NotificacionesAprobacionesService);
    private readonly INotificacionesAprobacionesRepository _repo;
    private readonly ErrorLogService _log;
    public NotificacionesAprobacionesService(INotificacionesAprobacionesRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<NotificacionAprobacionResult>> GetByIdAsync(int numNotificacion, string codUsuario)
    {
        try
        {
            var d = await _repo.ShowbyidAsync(numNotificacion, codUsuario);
            return d.Ficha is null
                ? ApiResponse<NotificacionAprobacionResult>.Fail("Notificación no encontrada.")
                : ApiResponse<NotificacionAprobacionResult>.Ok(d);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
            return ApiResponse<NotificacionAprobacionResult>.Fail(ex.Message);
        }
    }

    /// <summary>APROBAR, RECHAZAR u OBSERVAR (en la ruta: aprobar, rechazar, observar). Los errores de regla (no es tu turno, no hay comando...) vuelven como mensaje sin ensuciar el log.</summary>
    public async Task<ApiResponse<NotificacionAprobacionAccionResult>> AccionAsync(int numNotificacion, string codAccion, NotificacionAprobacionAccionDto dto, string codUsuario, string codEstacion)
    {
        var accion = (codAccion ?? string.Empty).Trim().ToLowerInvariant() switch
        {
            "aprobar"  => "APROBAR",
            "rechazar" => "RECHAZAR",
            "observar" => "OBSERVAR",
            _          => null
        };
        if (accion is null) return ApiResponse<NotificacionAprobacionAccionResult>.Fail("La acción debe ser aprobar, rechazar u observar.");
        if (accion != "APROBAR" && string.IsNullOrWhiteSpace(dto?.DES_OBSERVACION))
            return ApiResponse<NotificacionAprobacionAccionResult>.Fail($"Escribe la justificación para {(accion == "RECHAZAR" ? "rechazar" : "observar")}.");
        try
        {
            var (r, a) = await _repo.AccionAsync(numNotificacion, accion, codUsuario, codEstacion, dto?.DES_OBSERVACION);
            if (r.IsError || a is null)
            {
                WriteResult.OnSpError?.Invoke(r.Mensaje);
                return ApiResponse<NotificacionAprobacionAccionResult>.Fail(r.Mensaje);
            }
            return ApiResponse<NotificacionAprobacionAccionResult>.Ok(a, a.FLG_FLUJO_CERRADO ? "Listo." : "Listo: pasó al siguiente aprobador.");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(AccionAsync));
            return ApiResponse<NotificacionAprobacionAccionResult>.Fail(ex.Message);
        }
    }
}
