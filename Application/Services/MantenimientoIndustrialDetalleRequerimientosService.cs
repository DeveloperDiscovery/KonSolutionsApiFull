using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;

namespace KonSolutionsApi.Application.Services;

public class MantenimientoIndustrialDetalleRequerimientosService : IMantenimientoIndustrialDetalleRequerimientosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(MantenimientoIndustrialDetalleRequerimientosService);
    private readonly IMantenimientoIndustrialDetalleRequerimientosRepository _repo;
    private readonly ErrorLogService _log;
    public MantenimientoIndustrialDetalleRequerimientosService(IMantenimientoIndustrialDetalleRequerimientosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalleRequerimientos>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalleRequerimientos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalleRequerimientos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MantenimientoIndustrialDetalleRequerimientos>> GetByIdAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_PRODUCTO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA, COD_PRODUCTO);
              return d is null ? ApiResponse<MantenimientoIndustrialDetalleRequerimientos>.Fail("No encontrado.") : ApiResponse<MantenimientoIndustrialDetalleRequerimientos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<MantenimientoIndustrialDetalleRequerimientos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalleRequerimientos>>> SearchAsync(MantenimientoIndustrialDetalleRequerimientosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalleRequerimientos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<MantenimientoIndustrialDetalleRequerimientos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(MantenimientoIndustrialDetalleRequerimientosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(MantenimientoIndustrialDetalleRequerimientosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_PRODUCTO)
    {
        try { var r = await _repo.DeleteAsync(NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA, COD_PRODUCTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}