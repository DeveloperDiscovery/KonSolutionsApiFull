using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Services;

public class GruposRequerimientoService : IGruposRequerimientoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(GruposRequerimientoService);
    private readonly IGruposRequerimientoRepository _repo;
    private readonly ErrorLogService _log;
    public GruposRequerimientoService(IGruposRequerimientoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<GruposRequerimiento>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<GruposRequerimiento>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<GruposRequerimiento>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GruposRequerimiento>> GetByIdAsync(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_GRUPO_PLANEAMIENTO, COD_GRUPO_REQUERIMIENTO);
              return d is null ? ApiResponse<GruposRequerimiento>.Fail("No encontrado.") : ApiResponse<GruposRequerimiento>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<GruposRequerimiento>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<GruposRequerimiento>>> SearchAsync(GruposRequerimientoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<GruposRequerimiento>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<GruposRequerimiento>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(GruposRequerimientoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(GruposRequerimientoUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO)
    {
        try { var r = await _repo.DeleteAsync(COD_GRUPO_PLANEAMIENTO, COD_GRUPO_REQUERIMIENTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}