using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class ProgramacionEntregaImplementosPersonalService : IProgramacionEntregaImplementosPersonalService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProgramacionEntregaImplementosPersonalService);
    private readonly IProgramacionEntregaImplementosPersonalRepository _repo;
    private readonly ErrorLogService _log;
    public ProgramacionEntregaImplementosPersonalService(IProgramacionEntregaImplementosPersonalRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ProgramacionEntregaImplementosPersonal>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ProgramacionEntregaImplementosPersonal>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ProgramacionEntregaImplementosPersonal>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ProgramacionEntregaImplementosPersonal>> GetByIdAsync(int NUM_ENTREGA_IMPLEMENTOS, decimal COD_ENTIDAD_PERSONAL)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_ENTREGA_IMPLEMENTOS, COD_ENTIDAD_PERSONAL);
              return d is null ? ApiResponse<ProgramacionEntregaImplementosPersonal>.Fail("No encontrado.") : ApiResponse<ProgramacionEntregaImplementosPersonal>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ProgramacionEntregaImplementosPersonal>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ProgramacionEntregaImplementosPersonal>>> SearchAsync(ProgramacionEntregaImplementosPersonalSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ProgramacionEntregaImplementosPersonal>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ProgramacionEntregaImplementosPersonal>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ProgramacionEntregaImplementosPersonalInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ProgramacionEntregaImplementosPersonalUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ENTREGA_IMPLEMENTOS, decimal COD_ENTIDAD_PERSONAL)
    {
        try { var r = await _repo.DeleteAsync(NUM_ENTREGA_IMPLEMENTOS, COD_ENTIDAD_PERSONAL);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}