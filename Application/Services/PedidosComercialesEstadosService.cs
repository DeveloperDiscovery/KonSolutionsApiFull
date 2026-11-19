using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class PedidosComercialesEstadosService : IPedidosComercialesEstadosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PedidosComercialesEstadosService);
    private readonly IPedidosComercialesEstadosRepository _repo;
    private readonly ErrorLogService _log;
    public PedidosComercialesEstadosService(IPedidosComercialesEstadosRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PedidosComercialesEstados>>> GetAllAsync(string codClaseOperacion, string codTipoOperacion, int numPedidoComercial)
    {
        try
        {
            var d = (await _repo.ShowallAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial)).ToList();
            return ApiResponse<IEnumerable<PedidosComercialesEstados>>.Ok(d, total: d.Count);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
            return ApiResponse<IEnumerable<PedidosComercialesEstados>>.Fail(ex.Message);
        }
    }
}
