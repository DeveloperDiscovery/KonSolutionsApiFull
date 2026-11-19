using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;

namespace KonSolutionsApi.Application.Services;

public class LiquidacionesComisionVentaService : ILiquidacionesComisionVentaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(LiquidacionesComisionVentaService);
    private readonly ILiquidacionesComisionVentaRepository _repo;
    private readonly ErrorLogService _log;
    public LiquidacionesComisionVentaService(ILiquidacionesComisionVentaRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<LiquidacionesComisionVenta>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<LiquidacionesComisionVenta>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<LiquidacionesComisionVenta>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<LiquidacionesComisionVenta>> GetByIdAsync(int NUM_LIQUIDACION_COMISION_VENTA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_LIQUIDACION_COMISION_VENTA);
              return d is null ? ApiResponse<LiquidacionesComisionVenta>.Fail("No encontrado.") : ApiResponse<LiquidacionesComisionVenta>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<LiquidacionesComisionVenta>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<LiquidacionesComisionVenta>>> SearchAsync(LiquidacionesComisionVentaSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<LiquidacionesComisionVenta>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<LiquidacionesComisionVenta>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(LiquidacionesComisionVentaInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(LiquidacionesComisionVentaUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_LIQUIDACION_COMISION_VENTA)
    {
        try { var r = await _repo.DeleteAsync(NUM_LIQUIDACION_COMISION_VENTA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}