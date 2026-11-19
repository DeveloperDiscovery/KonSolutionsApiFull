using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Controlcalidad;

namespace KonSolutionsApi.Application.Services;

public class ControlCalidadProduccionEspecificacionesService : IControlCalidadProduccionEspecificacionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ControlCalidadProduccionEspecificacionesService);
    private readonly IControlCalidadProduccionEspecificacionesRepository _repo;
    private readonly ErrorLogService _log;
    public ControlCalidadProduccionEspecificacionesService(IControlCalidadProduccionEspecificacionesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ControlCalidadProduccionEspecificaciones>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ControlCalidadProduccionEspecificaciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ControlCalidadProduccionEspecificaciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ControlCalidadProduccionEspecificaciones>> GetByIdAsync(int NUM_CONTROL_CALIDAD, string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_CONTROL_CALIDAD, COD_ESPECIFICACION_TECNICA, COD_ITEM_TECNICO);
              return d is null ? ApiResponse<ControlCalidadProduccionEspecificaciones>.Fail("No encontrado.") : ApiResponse<ControlCalidadProduccionEspecificaciones>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ControlCalidadProduccionEspecificaciones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ControlCalidadProduccionEspecificaciones>>> SearchAsync(ControlCalidadProduccionEspecificacionesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ControlCalidadProduccionEspecificaciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ControlCalidadProduccionEspecificaciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ControlCalidadProduccionEspecificacionesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ControlCalidadProduccionEspecificacionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_CONTROL_CALIDAD, string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO)
    {
        try { var r = await _repo.DeleteAsync(NUM_CONTROL_CALIDAD, COD_ESPECIFICACION_TECNICA, COD_ITEM_TECNICO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}