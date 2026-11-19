using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class TiposImplementoLaboralService : ITiposImplementoLaboralService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposImplementoLaboralService);
    private readonly ITiposImplementoLaboralRepository _repo;
    private readonly ErrorLogService _log;
    public TiposImplementoLaboralService(ITiposImplementoLaboralRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposImplementoLaboral>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<TiposImplementoLaboral>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposImplementoLaboral>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposImplementoLaboral>> GetByIdAsync(string COD_TIPO_IMPLEMENTO_LABORAL)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TIPO_IMPLEMENTO_LABORAL);
              return d is null ? ApiResponse<TiposImplementoLaboral>.Fail("No encontrado.") : ApiResponse<TiposImplementoLaboral>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposImplementoLaboral>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposImplementoLaboral>>> SearchAsync(TiposImplementoLaboralSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposImplementoLaboral>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposImplementoLaboral>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposImplementoLaboralInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(TiposImplementoLaboralUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_IMPLEMENTO_LABORAL)
    {
        try { var r = await _repo.DeleteAsync(COD_TIPO_IMPLEMENTO_LABORAL);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}