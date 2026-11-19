using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class EntidadesConfigurarService : IEntidadesConfigurarService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesConfigurarService);
    private readonly IEntidadesConfigurarRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesConfigurarService(IEntidadesConfigurarRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesConfigurar>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<EntidadesConfigurar>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesConfigurar>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesConfigurar>> GetByIdAsync(string desTable, string desColumn)
    {
        try { var d = await _repo.ShowbyidAsync(desTable, desColumn);
              return d is null ? ApiResponse<EntidadesConfigurar>.Fail("No encontrado.") : ApiResponse<EntidadesConfigurar>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesConfigurar>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string desTable)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(desTable)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadesConfigurar>>> SearchAsync(EntidadesConfigurarSearchDto dto)
    {
        try { return ApiResponse<IEnumerable<EntidadesConfigurar>>.Ok(await _repo.SearchAsync(dto)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EntidadesConfigurar>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesConfigurarInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesConfigurarUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string desTable, string desColumn)
    {
        try { var r = await _repo.DeleteAsync(desTable, desColumn);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
