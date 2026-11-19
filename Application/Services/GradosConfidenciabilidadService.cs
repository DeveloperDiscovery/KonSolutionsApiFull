using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class GradosConfidenciabilidadService : IGradosConfidenciabilidadService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(GradosConfidenciabilidadService);
    private readonly IGradosConfidenciabilidadRepository _repo;
    private readonly ErrorLogService _log;
    public GradosConfidenciabilidadService(IGradosConfidenciabilidadRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<GradosConfidenciabilidad>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<GradosConfidenciabilidad>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<GradosConfidenciabilidad>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GradosConfidenciabilidad>> GetByIdAsync(int COD_GRADO_CONFIDENCIABILIDAD)
    {
        try { var d = await _repo.ShowbyidAsync(COD_GRADO_CONFIDENCIABILIDAD);
              return d is null ? ApiResponse<GradosConfidenciabilidad>.Fail("No encontrado.") : ApiResponse<GradosConfidenciabilidad>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<GradosConfidenciabilidad>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<GradosConfidenciabilidad>>> SearchAsync(GradosConfidenciabilidadSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<GradosConfidenciabilidad>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<GradosConfidenciabilidad>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(GradosConfidenciabilidadInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(GradosConfidenciabilidadUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int COD_GRADO_CONFIDENCIABILIDAD)
    {
        try { var r = await _repo.DeleteAsync(COD_GRADO_CONFIDENCIABILIDAD);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}