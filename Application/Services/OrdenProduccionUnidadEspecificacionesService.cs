using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Services;

public class OrdenProduccionUnidadEspecificacionesService : IOrdenProduccionUnidadEspecificacionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(OrdenProduccionUnidadEspecificacionesService);
    private readonly IOrdenProduccionUnidadEspecificacionesRepository _repo;
    private readonly ErrorLogService _log;
    public OrdenProduccionUnidadEspecificacionesService(IOrdenProduccionUnidadEspecificacionesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<OrdenProduccionUnidadEspecificaciones>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<OrdenProduccionUnidadEspecificaciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<OrdenProduccionUnidadEspecificaciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<OrdenProduccionUnidadEspecificaciones>> GetByIdAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM)
    {
        try { var d = await _repo.ShowbyidAsync(COD_PROCESO, NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM);
              return d is null ? ApiResponse<OrdenProduccionUnidadEspecificaciones>.Fail("No encontrado.") : ApiResponse<OrdenProduccionUnidadEspecificaciones>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<OrdenProduccionUnidadEspecificaciones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<OrdenProduccionUnidadEspecificaciones>>> SearchAsync(OrdenProduccionUnidadEspecificacionesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<OrdenProduccionUnidadEspecificaciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<OrdenProduccionUnidadEspecificaciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(OrdenProduccionUnidadEspecificacionesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(OrdenProduccionUnidadEspecificacionesUpdateDto dto, string codUsuario, string codEstacion)
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