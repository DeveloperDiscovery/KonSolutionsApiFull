using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Services;

public class GruposPlaneamientosPedidosDetalleService : IGruposPlaneamientosPedidosDetalleService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(GruposPlaneamientosPedidosDetalleService);
    private readonly IGruposPlaneamientosPedidosDetalleRepository _repo;
    private readonly ErrorLogService _log;
    public GruposPlaneamientosPedidosDetalleService(IGruposPlaneamientosPedidosDetalleRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<GruposPlaneamientosPedidosDetalle>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<GruposPlaneamientosPedidosDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<GruposPlaneamientosPedidosDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GruposPlaneamientosPedidosDetalle>> GetByIdAsync(int COD_GRUPO_PLANEAMIENTO, int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_GRUPO_PLANEAMIENTO, NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA);
              return d is null ? ApiResponse<GruposPlaneamientosPedidosDetalle>.Fail("No encontrado.") : ApiResponse<GruposPlaneamientosPedidosDetalle>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<GruposPlaneamientosPedidosDetalle>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<GruposPlaneamientosPedidosDetalle>>> SearchAsync(GruposPlaneamientosPedidosDetalleSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<GruposPlaneamientosPedidosDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<GruposPlaneamientosPedidosDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(GruposPlaneamientosPedidosDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(GruposPlaneamientosPedidosDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int COD_GRUPO_PLANEAMIENTO, int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(COD_GRUPO_PLANEAMIENTO, NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}