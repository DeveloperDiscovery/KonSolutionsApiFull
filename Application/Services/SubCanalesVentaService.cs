using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class SubCanalesVentaService : ISubCanalesVentaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SubCanalesVentaService);
    private readonly ISubCanalesVentaRepository _repo;
    private readonly ErrorLogService _log;
    public SubCanalesVentaService(ISubCanalesVentaRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SubCanalesVenta>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<SubCanalesVenta>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SubCanalesVenta>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SubCanalesVenta>> GetByIdAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_UNIDAD_NEGOCIO, NUM_CANAL_VENTA, NUM_SUB_CANAL_VENTA);
              return d is null ? ApiResponse<SubCanalesVenta>.Fail("No encontrado.") : ApiResponse<SubCanalesVenta>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SubCanalesVenta>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SubCanalesVenta>>> SearchAsync(SubCanalesVentaSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<SubCanalesVenta>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SubCanalesVenta>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(SubCanalesVentaInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(SubCanalesVentaUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA)
    {
        try { var r = await _repo.DeleteAsync(NUM_UNIDAD_NEGOCIO, NUM_CANAL_VENTA, NUM_SUB_CANAL_VENTA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}