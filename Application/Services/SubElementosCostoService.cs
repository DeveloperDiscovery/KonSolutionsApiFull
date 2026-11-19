using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Services;

public class SubElementosCostoService : ISubElementosCostoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SubElementosCostoService);
    private readonly ISubElementosCostoRepository _repo;
    private readonly ErrorLogService _log;
    public SubElementosCostoService(ISubElementosCostoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SubElementosCosto>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<SubElementosCosto>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SubElementosCosto>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SubElementosCosto>> GetByIdAsync(string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ELEMENTO_COSTO, COD_SUB_ELEMENTO_COSTO);
              return d is null ? ApiResponse<SubElementosCosto>.Fail("No encontrado.") : ApiResponse<SubElementosCosto>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SubElementosCosto>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SubElementosCosto>>> SearchAsync(SubElementosCostoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<SubElementosCosto>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SubElementosCosto>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(SubElementosCostoInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(SubElementosCostoUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO)
    {
        try { var r = await _repo.DeleteAsync(COD_ELEMENTO_COSTO, COD_SUB_ELEMENTO_COSTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}