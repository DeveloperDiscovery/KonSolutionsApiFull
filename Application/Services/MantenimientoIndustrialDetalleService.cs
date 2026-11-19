using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;

namespace KonSolutionsApi.Application.Services;

public class MantenimientoIndustrialDetalleService : IMantenimientoIndustrialDetalleService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(MantenimientoIndustrialDetalleService);
    private readonly IMantenimientoIndustrialDetalleRepository _repo;
    private readonly ErrorLogService _log;
    public MantenimientoIndustrialDetalleService(IMantenimientoIndustrialDetalleRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalle>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MantenimientoIndustrialDetalle>> GetByIdAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA);
              return d is null ? ApiResponse<MantenimientoIndustrialDetalle>.Fail("No encontrado.") : ApiResponse<MantenimientoIndustrialDetalle>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<MantenimientoIndustrialDetalle>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalle>>> SearchAsync(MantenimientoIndustrialDetalleSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(MantenimientoIndustrialDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(MantenimientoIndustrialDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}