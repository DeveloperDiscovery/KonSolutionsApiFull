using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class PersonalAfpConceptosRemunerativosService : IPersonalAfpConceptosRemunerativosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PersonalAfpConceptosRemunerativosService);
    private readonly IPersonalAfpConceptosRemunerativosRepository _repo;
    private readonly ErrorLogService _log;
    public PersonalAfpConceptosRemunerativosService(IPersonalAfpConceptosRemunerativosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PersonalAfpConceptosRemunerativos>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<PersonalAfpConceptosRemunerativos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PersonalAfpConceptosRemunerativos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PersonalAfpConceptosRemunerativos>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA, int COD_CONCEPTO_REMUNERATIVO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ENTIDAD_PERSONAL, NUM_SECUENCIA, COD_CONCEPTO_REMUNERATIVO);
              return d is null ? ApiResponse<PersonalAfpConceptosRemunerativos>.Fail("No encontrado.") : ApiResponse<PersonalAfpConceptosRemunerativos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PersonalAfpConceptosRemunerativos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PersonalAfpConceptosRemunerativos>>> SearchAsync(PersonalAfpConceptosRemunerativosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<PersonalAfpConceptosRemunerativos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<PersonalAfpConceptosRemunerativos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PersonalAfpConceptosRemunerativosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(PersonalAfpConceptosRemunerativosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA, int COD_CONCEPTO_REMUNERATIVO)
    {
        try { var r = await _repo.DeleteAsync(COD_ENTIDAD_PERSONAL, NUM_SECUENCIA, COD_CONCEPTO_REMUNERATIVO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}