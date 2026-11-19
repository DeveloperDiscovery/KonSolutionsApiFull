using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class PedidosComercialesRequerimientosService : IPedidosComercialesRequerimientosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PedidosComercialesRequerimientosService);
    private readonly IPedidosComercialesRequerimientosRepository _repo;
    private readonly ErrorLogService _log;
    public PedidosComercialesRequerimientosService(IPedidosComercialesRequerimientosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<RequerimientoStockRegistrado>>> GetRegistradosAsync(decimal numPedidoComercial)
    {
        try { var d = (await _repo.RegistradosAsync(numPedidoComercial)).ToList();
              return ApiResponse<IEnumerable<RequerimientoStockRegistrado>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetRegistradosAsync));
              return ApiResponse<IEnumerable<RequerimientoStockRegistrado>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RequerimientoStockDetalleRegistrado>>> GetRegistradosDetalleAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        try { var d = (await _repo.RegistradosDetalleAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial)).ToList();
              return ApiResponse<IEnumerable<RequerimientoStockDetalleRegistrado>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetRegistradosDetalleAsync));
              return ApiResponse<IEnumerable<RequerimientoStockDetalleRegistrado>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PedidoComercialRequerimientoStock>>> GetCabecerasAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        try { var d = (await _repo.ShowallCabeceraAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial)).ToList();
              return ApiResponse<IEnumerable<PedidoComercialRequerimientoStock>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetCabecerasAsync));
              return ApiResponse<IEnumerable<PedidoComercialRequerimientoStock>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertCabeceraAsync(PedidoComercialRequerimientoStockDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertCabeceraAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Requerimiento vinculado al pedido."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertCabeceraAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteCabeceraAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numRequerimientoStock)
    {
        try { var r = await _repo.DeleteCabeceraAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial, numRequerimientoStock);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Vínculo eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteCabeceraAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PedidoComercialDetalleRequerimientoStockDetalle>>> GetDetallesAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia, int numRequerimientoStock)
    {
        try { var d = (await _repo.ShowallDetalleAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial, numSecuencia, numRequerimientoStock)).ToList();
              return ApiResponse<IEnumerable<PedidoComercialDetalleRequerimientoStockDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetDetallesAsync));
              return ApiResponse<IEnumerable<PedidoComercialDetalleRequerimientoStockDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertDetalleAsync(PedidoComercialDetalleRequerimientoStockDetalleDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertDetalleAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Línea vinculada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertDetalleAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteDetalleAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia, int numRequerimientoStock, int numSecuenciaRequerimientoStock)
    {
        try { var r = await _repo.DeleteDetalleAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial, numSecuencia, numRequerimientoStock, numSecuenciaRequerimientoStock);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Vínculo de línea eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteDetalleAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
