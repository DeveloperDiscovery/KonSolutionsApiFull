using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class NueveBoxDesempenoPotencialService : INueveBoxDesempenoPotencialService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(NueveBoxDesempenoPotencialService);
    private readonly INueveBoxDesempenoPotencialRepository _repo;
    private readonly ErrorLogService _log;
    public NueveBoxDesempenoPotencialService(INueveBoxDesempenoPotencialRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<NueveBoxDesempenoPotencial>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<NueveBoxDesempenoPotencial>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<NueveBoxDesempenoPotencial>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<NueveBoxDesempenoPotencial>> GetByIdAsync(string COD_DESEMPENO_9BOX, string COD_POTENCIAL_9BOX)
    {
        try { var d = await _repo.ShowbyidAsync(COD_DESEMPENO_9BOX, COD_POTENCIAL_9BOX);
              return d is null ? ApiResponse<NueveBoxDesempenoPotencial>.Fail("No encontrado.") : ApiResponse<NueveBoxDesempenoPotencial>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<NueveBoxDesempenoPotencial>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<NueveBoxDesempenoPotencial>>> SearchAsync(NueveBoxDesempenoPotencialSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<NueveBoxDesempenoPotencial>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<NueveBoxDesempenoPotencial>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(NueveBoxDesempenoPotencialInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(NueveBoxDesempenoPotencialUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_DESEMPENO_9BOX, string COD_POTENCIAL_9BOX)
    {
        try { var r = await _repo.DeleteAsync(COD_DESEMPENO_9BOX, COD_POTENCIAL_9BOX);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}