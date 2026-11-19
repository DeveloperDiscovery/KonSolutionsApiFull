using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Services;

public class ProgramacionVacacionesService : IProgramacionVacacionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProgramacionVacacionesService);
    private readonly IProgramacionVacacionesRepository _repo;
    private readonly ErrorLogService _log;
    public ProgramacionVacacionesService(IProgramacionVacacionesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ProgramacionVacaciones>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ProgramacionVacaciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ProgramacionVacaciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ProgramacionVacaciones>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ENTIDAD_PERSONAL, NUM_SECUENCIA);
              return d is null ? ApiResponse<ProgramacionVacaciones>.Fail("No encontrado.") : ApiResponse<ProgramacionVacaciones>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ProgramacionVacaciones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ProgramacionVacaciones>>> SearchAsync(ProgramacionVacacionesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ProgramacionVacaciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ProgramacionVacaciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ProgramacionVacacionesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ProgramacionVacacionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(COD_ENTIDAD_PERSONAL, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}