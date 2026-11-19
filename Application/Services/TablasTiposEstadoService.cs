using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
namespace KonSolutionsApi.Application.Services;

public class TablasTiposEstadoService : ITablasTiposEstadoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TablasTiposEstadoService);
    private readonly ITablasTiposEstadoRepository _repo;
    private readonly ErrorLogService _log;
    public TablasTiposEstadoService(ITablasTiposEstadoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TablasTiposEstado>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList(); return ApiResponse<IEnumerable<TablasTiposEstado>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync)); return ApiResponse<IEnumerable<TablasTiposEstado>>.Fail(ex.Message); }
    }
    public async Task<ApiResponse<TablasTiposEstado>> GetByIdAsync(string DES_TABLA)
    {
        try { var d = await _repo.ShowbyidAsync(DES_TABLA); return d is null ? ApiResponse<TablasTiposEstado>.Fail("No encontrado.") : ApiResponse<TablasTiposEstado>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync)); return ApiResponse<TablasTiposEstado>.Fail(ex.Message); }
    }
    public async Task<ApiResponse<IEnumerable<TablasTiposEstado>>> SearchAsync(TablasTiposEstadoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList(); return ApiResponse<IEnumerable<TablasTiposEstado>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync)); return ApiResponse<IEnumerable<TablasTiposEstado>>.Fail(ex.Message); }
    }
    public async Task<ApiResponse<WriteResult>> InsertAsync(TablasTiposEstadoInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto); return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync)); return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
    public async Task<ApiResponse<WriteResult>> DeleteAsync(string DES_TABLA)
    {
        try { var r = await _repo.DeleteAsync(DES_TABLA); return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync)); return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
    public async Task<ApiResponse<IEnumerable<TablaDisponible>>> TablasDisponiblesAsync()
    {
        try { var d = (await _repo.TablasDisponiblesAsync()).ToList(); return ApiResponse<IEnumerable<TablaDisponible>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(TablasDisponiblesAsync)); return ApiResponse<IEnumerable<TablaDisponible>>.Fail(ex.Message); }
    }
}
