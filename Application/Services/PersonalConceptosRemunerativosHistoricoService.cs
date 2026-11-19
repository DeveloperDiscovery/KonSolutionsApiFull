using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class PersonalConceptosRemunerativosHistoricoService : IPersonalConceptosRemunerativosHistoricoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PersonalConceptosRemunerativosHistoricoService);
    private readonly IPersonalConceptosRemunerativosHistoricoRepository _repo;
    private readonly ErrorLogService _log;
    public PersonalConceptosRemunerativosHistoricoService(IPersonalConceptosRemunerativosHistoricoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PersonalConceptosRemunerativosHistorico>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<PersonalConceptosRemunerativosHistorico>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PersonalConceptosRemunerativosHistorico>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PersonalConceptosRemunerativosHistorico>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO, NUM_SECUENCIA);
              return d is null ? ApiResponse<PersonalConceptosRemunerativosHistorico>.Fail("No encontrado.") : ApiResponse<PersonalConceptosRemunerativosHistorico>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PersonalConceptosRemunerativosHistorico>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PersonalConceptosRemunerativosHistorico>>> SearchAsync(PersonalConceptosRemunerativosHistoricoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<PersonalConceptosRemunerativosHistorico>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<PersonalConceptosRemunerativosHistorico>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PersonalConceptosRemunerativosHistoricoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(PersonalConceptosRemunerativosHistoricoUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}