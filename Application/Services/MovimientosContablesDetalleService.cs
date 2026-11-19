using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Services;

public class MovimientosContablesDetalleService : IMovimientosContablesDetalleService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(MovimientosContablesDetalleService);
    private readonly IMovimientosContablesDetalleRepository _repo;
    private readonly ErrorLogService _log;
    public MovimientosContablesDetalleService(IMovimientosContablesDetalleRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<MovimientosContablesDetalle>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<MovimientosContablesDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<MovimientosContablesDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MovimientosContablesDetalle>> GetByIdAsync(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PERIODO_CONTABLE, COD_ASIENTO_CONTABLE, NUM_ASIENTO_CONTABLE);
              return d is null ? ApiResponse<MovimientosContablesDetalle>.Fail("No encontrado.") : ApiResponse<MovimientosContablesDetalle>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<MovimientosContablesDetalle>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientosContablesDetalle>>> SearchAsync(MovimientosContablesDetalleSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<MovimientosContablesDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<MovimientosContablesDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(MovimientosContablesDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(MovimientosContablesDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE)
    {
        try { var r = await _repo.DeleteAsync(NUM_PERIODO_CONTABLE, COD_ASIENTO_CONTABLE, NUM_ASIENTO_CONTABLE);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}