using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class TiposDesarrolloService : ITiposDesarrolloService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposDesarrolloService);
    private readonly ITiposDesarrolloRepository _repo;
    private readonly ErrorLogService _log;
    public TiposDesarrolloService(ITiposDesarrolloRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposDesarrollo>>> GetAllAsync(string codClaseDesarrollo)
    {
        try { var d = (await _repo.ShowallAsync(codClaseDesarrollo)).ToList();
              return ApiResponse<IEnumerable<TiposDesarrollo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposDesarrollo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposDesarrollo>> GetByIdAsync(string COD_CLASE_DESARROLLO, string COD_TIPO_DESARROLLO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CLASE_DESARROLLO, COD_TIPO_DESARROLLO);
              return d is null ? ApiResponse<TiposDesarrollo>.Fail("No encontrado.") : ApiResponse<TiposDesarrollo>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposDesarrollo>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposDesarrollo>>> GetComboboxAsync(string codClaseDesarrollo)
    {
        try { return ApiResponse<IEnumerable<TiposDesarrollo>>.Ok(await _repo.ShowcomboxAsync(codClaseDesarrollo)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<TiposDesarrollo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposDesarrollo>>> SearchAsync(string codClaseDesarrollo, TiposDesarrolloSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(codClaseDesarrollo, dto)).ToList();
              return ApiResponse<IEnumerable<TiposDesarrollo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposDesarrollo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposDesarrolloInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_DESARROLLO, string COD_TIPO_DESARROLLO)
    {
        try { var r = await _repo.DeleteAsync(COD_CLASE_DESARROLLO, COD_TIPO_DESARROLLO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
