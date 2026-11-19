using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Activos;

namespace KonSolutionsApi.Application.Services;

public class ActivosEntidadesService : IActivosEntidadesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ActivosEntidadesService);
    private readonly IActivosEntidadesRepository _repo;
    private readonly ErrorLogService _log;
    public ActivosEntidadesService(IActivosEntidadesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ActivosEntidades>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ActivosEntidades>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ActivosEntidades>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ActivosEntidades>> GetByIdAsync(string COD_ACTIVO, decimal COD_ENTIDAD, DateTime FEC_ASIGNACION_INICIO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ACTIVO, COD_ENTIDAD, FEC_ASIGNACION_INICIO);
              return d is null ? ApiResponse<ActivosEntidades>.Fail("No encontrado.") : ApiResponse<ActivosEntidades>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ActivosEntidades>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ActivosEntidades>>> SearchAsync(ActivosEntidadesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ActivosEntidades>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ActivosEntidades>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ActivosEntidadesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ActivosEntidadesUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ACTIVO, decimal COD_ENTIDAD, DateTime FEC_ASIGNACION_INICIO)
    {
        try { var r = await _repo.DeleteAsync(COD_ACTIVO, COD_ENTIDAD, FEC_ASIGNACION_INICIO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}