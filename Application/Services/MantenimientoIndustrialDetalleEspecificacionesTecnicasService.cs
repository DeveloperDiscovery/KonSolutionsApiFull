using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;

namespace KonSolutionsApi.Application.Services;

public class MantenimientoIndustrialDetalleEspecificacionesTecnicasService : IMantenimientoIndustrialDetalleEspecificacionesTecnicasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(MantenimientoIndustrialDetalleEspecificacionesTecnicasService);
    private readonly IMantenimientoIndustrialDetalleEspecificacionesTecnicasRepository _repo;
    private readonly ErrorLogService _log;
    public MantenimientoIndustrialDetalleEspecificacionesTecnicasService(IMantenimientoIndustrialDetalleEspecificacionesTecnicasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEspecificacionesTecnicas>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEspecificacionesTecnicas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEspecificacionesTecnicas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MantenimientoIndustrialDetalleEspecificacionesTecnicas>> GetByIdAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA);
              return d is null ? ApiResponse<MantenimientoIndustrialDetalleEspecificacionesTecnicas>.Fail("No encontrado.") : ApiResponse<MantenimientoIndustrialDetalleEspecificacionesTecnicas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<MantenimientoIndustrialDetalleEspecificacionesTecnicas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEspecificacionesTecnicas>>> SearchAsync(MantenimientoIndustrialDetalleEspecificacionesTecnicasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEspecificacionesTecnicas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEspecificacionesTecnicas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(MantenimientoIndustrialDetalleEspecificacionesTecnicasInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(MantenimientoIndustrialDetalleEspecificacionesTecnicasUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    {
        try { var r = await _repo.DeleteAsync(NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}