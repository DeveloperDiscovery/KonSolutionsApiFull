using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class PersonalBeneficiariosService : IPersonalBeneficiariosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PersonalBeneficiariosService);
    private readonly IPersonalBeneficiariosRepository _repo;
    private readonly ErrorLogService _log;
    public PersonalBeneficiariosService(IPersonalBeneficiariosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PersonalBeneficiarios>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<PersonalBeneficiarios>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PersonalBeneficiarios>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PersonalBeneficiarios>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_BENEFECIARIO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ENTIDAD_PERSONAL, COD_ENTIDAD_BENEFECIARIO);
              return d is null ? ApiResponse<PersonalBeneficiarios>.Fail("No encontrado.") : ApiResponse<PersonalBeneficiarios>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PersonalBeneficiarios>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PersonalBeneficiarios>>> SearchAsync(PersonalBeneficiariosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<PersonalBeneficiarios>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<PersonalBeneficiarios>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PersonalBeneficiariosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(PersonalBeneficiariosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_BENEFECIARIO)
    {
        try { var r = await _repo.DeleteAsync(COD_ENTIDAD_PERSONAL, COD_ENTIDAD_BENEFECIARIO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}