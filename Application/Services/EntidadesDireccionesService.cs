using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class EntidadesDireccionesService : IEntidadesDireccionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesDireccionesService);
    private readonly IEntidadesDireccionesRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesDireccionesService(IEntidadesDireccionesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesDirecciones>>> GetAllAsync(decimal COD_ENTIDAD)
    {
        try { var d = (await _repo.ShowallAsync(COD_ENTIDAD)).ToList();
              return ApiResponse<IEnumerable<EntidadesDirecciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesDirecciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesDirecciones>> GetByIdAsync(decimal COD_ENTIDAD, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ENTIDAD, NUM_SECUENCIA);
              return d is null ? ApiResponse<EntidadesDirecciones>.Fail("No encontrado.") : ApiResponse<EntidadesDirecciones>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesDirecciones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadesDirecciones>>> SearchAsync(EntidadesDireccionesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EntidadesDirecciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EntidadesDirecciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesDireccionesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesDireccionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, int NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(COD_ENTIDAD, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesDireccionesLoadCombosResult>> LoadcombosAsync(int codTipoEstado)
    {
        try { return ApiResponse<EntidadesDireccionesLoadCombosResult>.Ok(await _repo.LoadcombosAsync(codTipoEstado)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<EntidadesDireccionesLoadCombosResult>.Fail(ex.Message); }
    }

}