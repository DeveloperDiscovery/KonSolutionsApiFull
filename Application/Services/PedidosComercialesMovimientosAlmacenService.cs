using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class PedidosComercialesMovimientosAlmacenService : IPedidosComercialesMovimientosAlmacenService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PedidosComercialesMovimientosAlmacenService);
    private readonly IPedidosComercialesMovimientosAlmacenRepository _repo;
    private readonly ErrorLogService _log;
    public PedidosComercialesMovimientosAlmacenService(IPedidosComercialesMovimientosAlmacenRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PedidosComercialesMovimientosAlmacen>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<PedidosComercialesMovimientosAlmacen>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PedidosComercialesMovimientosAlmacen>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PedidosComercialesMovimientosAlmacen>> GetByIdAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA_PEDIDO, decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA_PEDIDO, NUM_MOVIMIENTO_ALMACEN);
              return d is null ? ApiResponse<PedidosComercialesMovimientosAlmacen>.Fail("No encontrado.") : ApiResponse<PedidosComercialesMovimientosAlmacen>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PedidosComercialesMovimientosAlmacen>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PedidosComercialesMovimientosAlmacen>>> SearchAsync(PedidosComercialesMovimientosAlmacenSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<PedidosComercialesMovimientosAlmacen>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<PedidosComercialesMovimientosAlmacen>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesMovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA_PEDIDO, decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var r = await _repo.DeleteAsync(NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA_PEDIDO, NUM_MOVIMIENTO_ALMACEN);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}