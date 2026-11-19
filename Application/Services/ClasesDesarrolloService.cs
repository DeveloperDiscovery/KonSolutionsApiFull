using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class ClasesDesarrolloService : IClasesDesarrolloService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ClasesDesarrolloService);
    private readonly IClasesDesarrolloRepository _repo;
    private readonly ErrorLogService _log;
    public ClasesDesarrolloService(IClasesDesarrolloRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ClasesDesarrollo>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ClasesDesarrollo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ClasesDesarrollo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ClasesDesarrollo>> GetByIdAsync(string COD_CLASE_DESARROLLO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CLASE_DESARROLLO);
              return d is null ? ApiResponse<ClasesDesarrollo>.Fail("No encontrado.") : ApiResponse<ClasesDesarrollo>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ClasesDesarrollo>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ClasesDesarrollo>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ClasesDesarrollo>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ClasesDesarrollo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ClasesDesarrolloInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_DESARROLLO)
    {
        try { var r = await _repo.DeleteAsync(COD_CLASE_DESARROLLO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
