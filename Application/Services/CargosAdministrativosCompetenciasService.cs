using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class CargosAdministrativosCompetenciasService : ICargosAdministrativosCompetenciasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(CargosAdministrativosCompetenciasService);
    private readonly ICargosAdministrativosCompetenciasRepository _repo;
    private readonly ErrorLogService _log;
    public CargosAdministrativosCompetenciasService(ICargosAdministrativosCompetenciasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<CargosAdministrativosCompetencias>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<CargosAdministrativosCompetencias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<CargosAdministrativosCompetencias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<CargosAdministrativosCompetencias>> GetByIdAsync(int COD_CARGO, string COD_COMPETENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CARGO, COD_COMPETENCIA);
              return d is null ? ApiResponse<CargosAdministrativosCompetencias>.Fail("No encontrado.") : ApiResponse<CargosAdministrativosCompetencias>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<CargosAdministrativosCompetencias>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<CargosAdministrativosCompetencias>>> SearchAsync(CargosAdministrativosCompetenciasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<CargosAdministrativosCompetencias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<CargosAdministrativosCompetencias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(CargosAdministrativosCompetenciasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(CargosAdministrativosCompetenciasUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int COD_CARGO, string COD_COMPETENCIA)
    {
        try { var r = await _repo.DeleteAsync(COD_CARGO, COD_COMPETENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}