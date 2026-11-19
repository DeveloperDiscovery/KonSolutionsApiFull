using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class PedidosComercialesCronogramaEntregasService : IPedidosComercialesCronogramaEntregasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PedidosComercialesCronogramaEntregasService);
    private readonly IPedidosComercialesCronogramaEntregasRepository _repo;
    private readonly ErrorLogService _log;
    public PedidosComercialesCronogramaEntregasService(IPedidosComercialesCronogramaEntregasRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PedidoComercialCronogramaEntrega>>> GetPorPedidoAsync(
        string codTipoOperacion, string codClaseOperacion, decimal numPedidoComercial)
    {
        try { var d = (await _repo.ShowallPedidoAsync(codTipoOperacion, codClaseOperacion, numPedidoComercial)).ToList();
              return ApiResponse<IEnumerable<PedidoComercialCronogramaEntrega>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetPorPedidoAsync));
              return ApiResponse<IEnumerable<PedidoComercialCronogramaEntrega>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PedidoComercialCronogramaEntregaDto dto)
    {
        try { return ApiResponse<WriteResult>.FromWriteResult(await _repo.InsertAsync(dto), "Entrega programada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(PedidoComercialCronogramaEntregaDto dto)
    {
        try { return ApiResponse<WriteResult>.FromWriteResult(await _repo.UpdateAsync(dto), "Entrega modificada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codTipoOperacion, string codClaseOperacion,
                                                            decimal numPedidoComercial, int numSecuencia, DateTime fecEntrega)
    {
        try { return ApiResponse<WriteResult>.FromWriteResult(
                  await _repo.DeleteAsync(codTipoOperacion, codClaseOperacion, numPedidoComercial, numSecuencia, fecEntrega),
                  "Entrega eliminada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
