using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class IncotermsFasesService : IIncotermsFasesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(IncotermsFasesService);
    private readonly IIncotermsFasesRepository _repo;
    private readonly ErrorLogService _log;
    public IncotermsFasesService(IIncotermsFasesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<IncotermsFases>>> GetAllAsync(string codIncoterm)
    {
        try { var d = (await _repo.ShowallAsync(codIncoterm)).ToList();
              return ApiResponse<IEnumerable<IncotermsFases>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<IncotermsFases>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IncotermsFases>> GetByIdAsync(string COD_INCOTERM, string COD_FASE_INCOTERM)
    {
        try { var d = await _repo.ShowbyidAsync(COD_INCOTERM, COD_FASE_INCOTERM);
              return d is null ? ApiResponse<IncotermsFases>.Fail("No encontrado.") : ApiResponse<IncotermsFases>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<IncotermsFases>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<IncotermsFases>>> SearchAsync(IncotermsFasesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<IncotermsFases>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<IncotermsFases>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(IncotermsFasesInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(IncotermsFasesUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_INCOTERM, string COD_FASE_INCOTERM)
    {
        try { var r = await _repo.DeleteAsync(COD_INCOTERM, COD_FASE_INCOTERM);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}