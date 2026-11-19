using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class ProduccionRutasOperacionesEstructuraService : IProduccionRutasOperacionesEstructuraService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProduccionRutasOperacionesEstructuraService);
    private readonly IProduccionRutasOperacionesEstructuraRepository _repo;
    private readonly ErrorLogService _log;
    public ProduccionRutasOperacionesEstructuraService(IProduccionRutasOperacionesEstructuraRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ProduccionRutasOperacionesEstructura>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ProduccionRutasOperacionesEstructura>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ProduccionRutasOperacionesEstructura>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ProduccionRutasOperacionesEstructura>> GetByIdAsync(int COD_RUTA_PRODUCCION, int NUM_ESTRUCTURA_RUTA_PROCESO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_RUTA_PRODUCCION, NUM_ESTRUCTURA_RUTA_PROCESO);
              return d is null ? ApiResponse<ProduccionRutasOperacionesEstructura>.Fail("No encontrado.") : ApiResponse<ProduccionRutasOperacionesEstructura>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ProduccionRutasOperacionesEstructura>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ProduccionRutasOperacionesEstructura>>> SearchAsync(ProduccionRutasOperacionesEstructuraSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ProduccionRutasOperacionesEstructura>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ProduccionRutasOperacionesEstructura>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ProduccionRutasOperacionesEstructuraInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ProduccionRutasOperacionesEstructuraUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int COD_RUTA_PRODUCCION, int NUM_ESTRUCTURA_RUTA_PROCESO)
    {
        try { var r = await _repo.DeleteAsync(COD_RUTA_PRODUCCION, NUM_ESTRUCTURA_RUTA_PROCESO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}