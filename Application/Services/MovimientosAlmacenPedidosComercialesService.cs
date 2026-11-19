using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class MovimientosAlmacenPedidosComercialesService : IMovimientosAlmacenPedidosComercialesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(MovimientosAlmacenPedidosComercialesService);
    private readonly IMovimientosAlmacenPedidosComercialesRepository _repo;
    private readonly ErrorLogService _log;
    public MovimientosAlmacenPedidosComercialesService(IMovimientosAlmacenPedidosComercialesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<MovimientosAlmacenPedidosComerciales>>> GetAllAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var d = (await _repo.ShowallAsync(NUM_MOVIMIENTO_ALMACEN)).ToList();
              return ApiResponse<IEnumerable<MovimientosAlmacenPedidosComerciales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<MovimientosAlmacenPedidosComerciales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MovimientosAlmacenPedidosComerciales>> GetByIdAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_MOVIMIENTO_ALMACEN, NUM_PEDIDO_COMERCIAL);
              return d is null ? ApiResponse<MovimientosAlmacenPedidosComerciales>.Fail("No encontrado.") : ApiResponse<MovimientosAlmacenPedidosComerciales>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<MovimientosAlmacenPedidosComerciales>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientosAlmacenPedidosComerciales>>> SearchAsync(MovimientosAlmacenPedidosComercialesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<MovimientosAlmacenPedidosComerciales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<MovimientosAlmacenPedidosComerciales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(MovimientosAlmacenPedidosComercialesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL)
    {
        try { var r = await _repo.DeleteAsync(NUM_MOVIMIENTO_ALMACEN, NUM_PEDIDO_COMERCIAL);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
