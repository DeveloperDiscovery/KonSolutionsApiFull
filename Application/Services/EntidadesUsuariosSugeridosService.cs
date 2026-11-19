using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class EntidadesUsuariosSugeridosService : IEntidadesUsuariosSugeridosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesUsuariosSugeridosService);
    private readonly IEntidadesUsuariosSugeridosRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesUsuariosSugeridosService(IEntidadesUsuariosSugeridosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadUsuarioSugerido>>> GetComercialesAsync()
    {
        try { var d = (await _repo.ComercialesAsync()).ToList();
              return ApiResponse<IEnumerable<EntidadUsuarioSugerido>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComercialesAsync));
              return ApiResponse<IEnumerable<EntidadUsuarioSugerido>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadUsuarioSugerido>>> GetDependientesAsync()
    {
        try { var d = (await _repo.DependientesAsync()).ToList();
              return ApiResponse<IEnumerable<EntidadUsuarioSugerido>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetDependientesAsync));
              return ApiResponse<IEnumerable<EntidadUsuarioSugerido>>.Fail(ex.Message); }
    }

    /// <summary>Los dependientes de UNA entidad — el combo de Vendedor del Pedido Comercial.</summary>
    public async Task<ApiResponse<IEnumerable<EntidadUsuarioSugerido>>> GetDependientesPorEntidadAsync(decimal codEntidad)
    {
        try { var d = (await _repo.DependientesPorEntidadAsync(codEntidad)).ToList();
              return ApiResponse<IEnumerable<EntidadUsuarioSugerido>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetDependientesPorEntidadAsync));
              return ApiResponse<IEnumerable<EntidadUsuarioSugerido>>.Fail(ex.Message); }
    }
}
