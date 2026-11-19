using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Controlcalidad;

namespace KonSolutionsApi.Application.Services;

public class ClasesDefectosTecnicosService : IClasesDefectosTecnicosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ClasesDefectosTecnicosService);
    private readonly IClasesDefectosTecnicosRepository _repo;
    private readonly ErrorLogService _log;
    public ClasesDefectosTecnicosService(IClasesDefectosTecnicosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ClasesDefectosTecnicos>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ClasesDefectosTecnicos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ClasesDefectosTecnicos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ClasesDefectosTecnicos>> GetByIdAsync(string COD_CLASE_DEFECTO_TECNICO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CLASE_DEFECTO_TECNICO);
              return d is null ? ApiResponse<ClasesDefectosTecnicos>.Fail("No encontrado.") : ApiResponse<ClasesDefectosTecnicos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ClasesDefectosTecnicos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ClasesDefectosTecnicos>>> SearchAsync(ClasesDefectosTecnicosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ClasesDefectosTecnicos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ClasesDefectosTecnicos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ClasesDefectosTecnicosInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ClasesDefectosTecnicosUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_DEFECTO_TECNICO)
    {
        try { var r = await _repo.DeleteAsync(COD_CLASE_DEFECTO_TECNICO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}