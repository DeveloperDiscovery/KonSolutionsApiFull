using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Services;

public class OrdenProduccionUnidadService : IOrdenProduccionUnidadService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(OrdenProduccionUnidadService);
    private readonly IOrdenProduccionUnidadRepository _repo;
    private readonly ErrorLogService _log;
    public OrdenProduccionUnidadService(IOrdenProduccionUnidadRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<OrdenProduccionUnidad>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<OrdenProduccionUnidad>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<OrdenProduccionUnidad>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<OrdenProduccionUnidad>> GetByIdAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM)
    {
        try { var d = await _repo.ShowbyidAsync(COD_PROCESO, NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM);
              return d is null ? ApiResponse<OrdenProduccionUnidad>.Fail("No encontrado.") : ApiResponse<OrdenProduccionUnidad>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<OrdenProduccionUnidad>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxPorItemAsync(string codProceso, int numOrdenProduccion, int numOrdenProduccionItem)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxPorItemAsync(codProceso, numOrdenProduccion, numOrdenProduccionItem)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxPorItemAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<OrdenProduccionUnidad>>> SearchAsync(OrdenProduccionUnidadSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<OrdenProduccionUnidad>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<OrdenProduccionUnidad>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(OrdenProduccionUnidadInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(OrdenProduccionUnidadUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM)
    {
        try { var r = await _repo.DeleteAsync(COD_PROCESO, NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}