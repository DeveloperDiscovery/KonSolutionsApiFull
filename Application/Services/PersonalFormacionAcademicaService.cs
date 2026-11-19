using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class PersonalFormacionAcademicaService : IPersonalFormacionAcademicaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PersonalFormacionAcademicaService);
    private readonly IPersonalFormacionAcademicaRepository _repo;
    private readonly ErrorLogService _log;
    public PersonalFormacionAcademicaService(IPersonalFormacionAcademicaRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PersonalFormacionAcademica>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<PersonalFormacionAcademica>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PersonalFormacionAcademica>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PersonalFormacionAcademica>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_ACADEMICA, string COD_CARRERA_ACADEMICA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ENTIDAD_PERSONAL, COD_ENTIDAD_ACADEMICA, COD_CARRERA_ACADEMICA);
              return d is null ? ApiResponse<PersonalFormacionAcademica>.Fail("No encontrado.") : ApiResponse<PersonalFormacionAcademica>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PersonalFormacionAcademica>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PersonalFormacionAcademica>>> SearchAsync(PersonalFormacionAcademicaSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<PersonalFormacionAcademica>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<PersonalFormacionAcademica>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PersonalFormacionAcademicaInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(PersonalFormacionAcademicaUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_ACADEMICA, string COD_CARRERA_ACADEMICA)
    {
        try { var r = await _repo.DeleteAsync(COD_ENTIDAD_PERSONAL, COD_ENTIDAD_ACADEMICA, COD_CARRERA_ACADEMICA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}