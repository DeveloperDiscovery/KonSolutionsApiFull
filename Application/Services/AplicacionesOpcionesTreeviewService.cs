using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

/// <summary>Servicio que obtiene y devuelve los nodos planos del treeview.</summary>
public class AplicacionesOpcionesTreeviewService : IAplicacionesOpcionesTreeviewService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase    = nameof(AplicacionesOpcionesTreeviewService);

    private readonly IAplicacionesOpcionesTreeviewRepository _repo;
    private readonly ErrorLogService _log;

    public AplicacionesOpcionesTreeviewService(
        IAplicacionesOpcionesTreeviewRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    /// <summary>Retorna todos los nodos planos; el frontend arma el árbol.</summary>
    public async Task<ApiResponse<IEnumerable<NodoOpcion>>> GetTreeviewAsync()
    {
        try
        {
            var data = (await _repo.GetTreeviewAsync()).ToList();
            return ApiResponse<IEnumerable<NodoOpcion>>.Ok(data, total: data.Count);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(GetTreeviewAsync));
            return ApiResponse<IEnumerable<NodoOpcion>>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<IEnumerable<NodoOpcionLogo>>> GetLogosAsync()
    {
        try
        {
            var data = (await _repo.GetLogosAsync()).ToList();
            return ApiResponse<IEnumerable<NodoOpcionLogo>>.Ok(data, total: data.Count);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(GetLogosAsync));
            return ApiResponse<IEnumerable<NodoOpcionLogo>>.Fail(ex.Message);
        }
    }
}
