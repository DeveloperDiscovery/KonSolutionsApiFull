using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

public class AplicacionesOpcionesService : IAplicacionesOpcionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(AplicacionesOpcionesService);
    private readonly IAplicacionesOpcionesRepository _repo;
    private readonly ErrorLogService _log;
    public AplicacionesOpcionesService(IAplicacionesOpcionesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<AplicacionesOpciones>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<AplicacionesOpciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<AplicacionesOpciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<AplicacionesOpciones>> GetByIdAsync(string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        try { var d = await _repo.ShowbyidAsync(COD_APLICACION, COD_OPCION_APLICACION);
              return d is null ? ApiResponse<AplicacionesOpciones>.Fail("No encontrado.") : ApiResponse<AplicacionesOpciones>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<AplicacionesOpciones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<AplicacionesOpciones>>> SearchAsync(AplicacionesOpcionesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<AplicacionesOpciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<AplicacionesOpciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(AplicacionesOpcionesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(AplicacionesOpcionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro actualizado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        try { var r = await _repo.DeleteAsync(COD_APLICACION, COD_OPCION_APLICACION);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> MoveAsync(AplicacionesOpcionesMoveDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.MoveAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Movido correctamente."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(MoveAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<AplicacionesOpcionesLoadCombosResult>> LoadcombosAsync(int codTipoEstado)
    {
        try { return ApiResponse<AplicacionesOpcionesLoadCombosResult>.Ok(await _repo.LoadcombosAsync(codTipoEstado)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<AplicacionesOpcionesLoadCombosResult>.Fail(ex.Message); }
    }

}