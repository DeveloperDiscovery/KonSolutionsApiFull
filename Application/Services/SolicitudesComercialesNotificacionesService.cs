using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class SolicitudesComercialesNotificacionesService : ISolicitudesComercialesNotificacionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SolicitudesComercialesNotificacionesService);
    private readonly ISolicitudesComercialesNotificacionesRepository _repo;
    private readonly ErrorLogService _log;
    public SolicitudesComercialesNotificacionesService(ISolicitudesComercialesNotificacionesRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SolicitudesComercialesNotificaciones>>> GetAllAsync(int numSolicitudComercial)
    {
        try { var d = (await _repo.ShowallAsync(numSolicitudComercial)).ToList();
              return ApiResponse<IEnumerable<SolicitudesComercialesNotificaciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SolicitudesComercialesNotificaciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SolicitudesComercialesNotificaciones>> GetByIdAsync(int numSolicitudComercial, int numNotificacion)
    {
        try { var d = await _repo.ShowbyidAsync(numSolicitudComercial, numNotificacion);
              return d is null ? ApiResponse<SolicitudesComercialesNotificaciones>.Fail("No encontrado.") : ApiResponse<SolicitudesComercialesNotificaciones>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SolicitudesComercialesNotificaciones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SolicitudesComercialesNotificaciones>>> SearchAsync(SolicitudesComercialesNotificacionesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<SolicitudesComercialesNotificaciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SolicitudesComercialesNotificaciones>>.Fail(ex.Message); }
    }
}
