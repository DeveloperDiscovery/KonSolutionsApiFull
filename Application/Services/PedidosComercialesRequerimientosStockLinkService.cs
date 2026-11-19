using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class PedidosComercialesRequerimientosStockLinkService : IPedidosComercialesRequerimientosStockLinkService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PedidosComercialesRequerimientosStockLinkService);
    private readonly IPedidosComercialesRequerimientosStockLinkRepository _repo;
    private readonly ErrorLogService _log;
    public PedidosComercialesRequerimientosStockLinkService(IPedidosComercialesRequerimientosStockLinkRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PedidoComercialRequerimientoStockLink>>> GetCabeceraAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        try { var d = (await _repo.ShowallCabeceraAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial)).ToList();
              return ApiResponse<IEnumerable<PedidoComercialRequerimientoStockLink>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetCabeceraAsync));
              return ApiResponse<IEnumerable<PedidoComercialRequerimientoStockLink>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PedidoComercialDetalleRequerimientoStockDetalleLinea>>> GetDetalleAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia, int? numRequerimientoStock)
    {
        try { var d = (await _repo.ShowallDetalleAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial, numSecuencia, numRequerimientoStock)).ToList();
              return ApiResponse<IEnumerable<PedidoComercialDetalleRequerimientoStockDetalleLinea>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetDetalleAsync));
              return ApiResponse<IEnumerable<PedidoComercialDetalleRequerimientoStockDetalleLinea>>.Fail(ex.Message); }
    }
}
