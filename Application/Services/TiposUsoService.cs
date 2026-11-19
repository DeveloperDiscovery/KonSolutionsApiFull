using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class TiposUsoService : ITiposUsoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposUsoService);
    private readonly ITiposUsoRepository _repo;
    private readonly ErrorLogService _log;
    public TiposUsoService(ITiposUsoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposUso>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<TiposUso>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposUso>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposUso>> GetByIdAsync(string COD_TIPO_USO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TIPO_USO);
              return d is null ? ApiResponse<TiposUso>.Fail("No encontrado.") : ApiResponse<TiposUso>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposUso>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposUso>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<TiposUso>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<TiposUso>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposUsoInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_USO)
    {
        try { var r = await _repo.DeleteAsync(COD_TIPO_USO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
