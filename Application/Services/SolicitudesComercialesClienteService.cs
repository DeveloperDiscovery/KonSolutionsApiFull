using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class SolicitudesComercialesClienteService : ISolicitudesComercialesClienteService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SolicitudesComercialesClienteService);
    private readonly ISolicitudesComercialesClienteRepository _repo;
    private readonly ErrorLogService _log;
    public SolicitudesComercialesClienteService(ISolicitudesComercialesClienteRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SolicitudesComerciales>>> GetAllAsync(decimal codEntidadCliente)
    {
        try { var d = (await _repo.ShowallAsync(codEntidadCliente)).ToList();
              return ApiResponse<IEnumerable<SolicitudesComerciales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SolicitudesComerciales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SolicitudesComerciales>>> SearchAsync(decimal codEntidadCliente, string? search)
    {
        try { var d = (await _repo.SearchAsync(codEntidadCliente, search)).ToList();
              return ApiResponse<IEnumerable<SolicitudesComerciales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SolicitudesComerciales>>.Fail(ex.Message); }
    }
}
