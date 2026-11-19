using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;

namespace KonSolutionsApi.Application.Services;

public class TiposMantenimientoIndustrialService : ITiposMantenimientoIndustrialService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposMantenimientoIndustrialService);
    private readonly ITiposMantenimientoIndustrialRepository _repo;
    private readonly ErrorLogService _log;
    public TiposMantenimientoIndustrialService(ITiposMantenimientoIndustrialRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposMantenimientoIndustrial>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<TiposMantenimientoIndustrial>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposMantenimientoIndustrial>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposMantenimientoIndustrial>> GetByIdAsync(string COD_TIPO_MANTENIMIENTO_INDUSTRIAL)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TIPO_MANTENIMIENTO_INDUSTRIAL);
              return d is null ? ApiResponse<TiposMantenimientoIndustrial>.Fail("No encontrado.") : ApiResponse<TiposMantenimientoIndustrial>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposMantenimientoIndustrial>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposMantenimientoIndustrial>>> SearchAsync(TiposMantenimientoIndustrialSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposMantenimientoIndustrial>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposMantenimientoIndustrial>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposMantenimientoIndustrialInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(TiposMantenimientoIndustrialUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_MANTENIMIENTO_INDUSTRIAL)
    {
        try { var r = await _repo.DeleteAsync(COD_TIPO_MANTENIMIENTO_INDUSTRIAL);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}