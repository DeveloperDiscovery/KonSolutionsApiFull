using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class DisenosVersionService : IDisenosVersionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(DisenosVersionService);
    private readonly IDisenosVersionRepository _repo;
    private readonly ErrorLogService _log;
    public DisenosVersionService(IDisenosVersionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<DisenosVersion>>> GetAllAsync(string codDiseno)
    {
        try { var d = (await _repo.ShowallAsync(codDiseno)).ToList();
              return ApiResponse<IEnumerable<DisenosVersion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<DisenosVersion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<DisenosVersion>> GetByIdAsync(string COD_DISENO, int NUM_VERSION)
    {
        try { var d = await _repo.ShowbyidAsync(COD_DISENO, NUM_VERSION);
              return d is null ? ApiResponse<DisenosVersion>.Fail("No encontrado.") : ApiResponse<DisenosVersion>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<DisenosVersion>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<DisenosVersion>>> GetComboboxAsync(string codDiseno)
    {
        try { return ApiResponse<IEnumerable<DisenosVersion>>.Ok(await _repo.ShowcomboxAsync(codDiseno)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<DisenosVersion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<DisenosVersion>>> SearchAsync(string codDiseno, DisenosVersionSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(codDiseno, dto)).ToList();
              return ApiResponse<IEnumerable<DisenosVersion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<DisenosVersion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(DisenosVersionInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_DISENO, int NUM_VERSION)
    {
        try { var r = await _repo.DeleteAsync(COD_DISENO, NUM_VERSION);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<DisenosVersionLoadCombosResult>> GetLoadcombosAsync()
    {
        try { return ApiResponse<DisenosVersionLoadCombosResult>.Ok(await _repo.LoadcombosAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetLoadcombosAsync));
              return ApiResponse<DisenosVersionLoadCombosResult>.Fail(ex.Message); }
    }
}
