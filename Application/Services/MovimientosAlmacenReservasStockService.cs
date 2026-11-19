using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class MovimientosAlmacenReservasStockService : IMovimientosAlmacenReservasStockService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(MovimientosAlmacenReservasStockService);
    private readonly IMovimientosAlmacenReservasStockRepository _repo;
    private readonly ErrorLogService _log;
    public MovimientosAlmacenReservasStockService(IMovimientosAlmacenReservasStockRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<MovimientosAlmacenReservasStock>>> GetAllAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var d = (await _repo.ShowallAsync(NUM_MOVIMIENTO_ALMACEN)).ToList();
              return ApiResponse<IEnumerable<MovimientosAlmacenReservasStock>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<MovimientosAlmacenReservasStock>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MovimientosAlmacenReservasStock>> GetByIdAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_MOVIMIENTO_ALMACEN, NUM_RESERVA_STOCK);
              return d is null ? ApiResponse<MovimientosAlmacenReservasStock>.Fail("No encontrado.") : ApiResponse<MovimientosAlmacenReservasStock>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<MovimientosAlmacenReservasStock>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientosAlmacenReservasStock>>> SearchAsync(MovimientosAlmacenReservasStockSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<MovimientosAlmacenReservasStock>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<MovimientosAlmacenReservasStock>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(MovimientosAlmacenReservasStockInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK)
    {
        try { var r = await _repo.DeleteAsync(NUM_MOVIMIENTO_ALMACEN, NUM_RESERVA_STOCK);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
