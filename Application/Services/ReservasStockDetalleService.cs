using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class ReservasStockDetalleService : IReservasStockDetalleService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ReservasStockDetalleService);
    private readonly IReservasStockDetalleRepository _repo;
    private readonly ErrorLogService _log;
    public ReservasStockDetalleService(IReservasStockDetalleRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ReservasStockDetalle>>> GetAllAsync(int NUM_RESERVA_STOCK)
    {
        try { var d = (await _repo.ShowallAsync(NUM_RESERVA_STOCK)).ToList();
              return ApiResponse<IEnumerable<ReservasStockDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ReservasStockDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ReservasStockDetalle>> GetByIdAsync(int NUM_RESERVA_STOCK, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_RESERVA_STOCK, NUM_SECUENCIA);
              return d is null ? ApiResponse<ReservasStockDetalle>.Fail("No encontrado.") : ApiResponse<ReservasStockDetalle>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ReservasStockDetalle>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ReservasStockDetalle>>> SearchAsync(ReservasStockDetalleSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ReservasStockDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ReservasStockDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ReservasStockDetalleInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ReservasStockDetalleUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_RESERVA_STOCK, int NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(NUM_RESERVA_STOCK, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}