using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class PedidosEspecificacionesDespachoService : IPedidosEspecificacionesDespachoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PedidosEspecificacionesDespachoService);
    private readonly IPedidosEspecificacionesDespachoRepository _repo;
    private readonly ErrorLogService _log;
    public PedidosEspecificacionesDespachoService(IPedidosEspecificacionesDespachoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PedidosEspecificacionesDespacho>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<PedidosEspecificacionesDespacho>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PedidosEspecificacionesDespacho>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PedidosEspecificacionesDespacho>> GetByIdAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA);
              return d is null ? ApiResponse<PedidosEspecificacionesDespacho>.Fail("No encontrado.") : ApiResponse<PedidosEspecificacionesDespacho>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PedidosEspecificacionesDespacho>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PedidosEspecificacionesDespacho>>> SearchAsync(PedidosEspecificacionesDespachoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<PedidosEspecificacionesDespacho>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<PedidosEspecificacionesDespacho>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PedidosEspecificacionesDespachoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}