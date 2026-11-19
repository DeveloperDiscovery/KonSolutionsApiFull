using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;

namespace KonSolutionsApi.Application.Services;

public class MantenimientoIndustrialDetalleEntidadesService : IMantenimientoIndustrialDetalleEntidadesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(MantenimientoIndustrialDetalleEntidadesService);
    private readonly IMantenimientoIndustrialDetalleEntidadesRepository _repo;
    private readonly ErrorLogService _log;
    public MantenimientoIndustrialDetalleEntidadesService(IMantenimientoIndustrialDetalleEntidadesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEntidades>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEntidades>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEntidades>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MantenimientoIndustrialDetalleEntidades>> GetByIdAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, decimal COD_ENTIDAD_ASIGNADA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA, COD_ENTIDAD_ASIGNADA);
              return d is null ? ApiResponse<MantenimientoIndustrialDetalleEntidades>.Fail("No encontrado.") : ApiResponse<MantenimientoIndustrialDetalleEntidades>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<MantenimientoIndustrialDetalleEntidades>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEntidades>>> SearchAsync(MantenimientoIndustrialDetalleEntidadesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEntidades>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEntidades>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(MantenimientoIndustrialDetalleEntidadesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, decimal COD_ENTIDAD_ASIGNADA)
    {
        try { var r = await _repo.DeleteAsync(NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA, COD_ENTIDAD_ASIGNADA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}