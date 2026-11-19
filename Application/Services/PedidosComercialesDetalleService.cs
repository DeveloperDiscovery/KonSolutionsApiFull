using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class PedidosComercialesDetalleService : IPedidosComercialesDetalleService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PedidosComercialesDetalleService);
    private readonly IPedidosComercialesDetalleRepository _repo;
    private readonly ErrorLogService _log;
    public PedidosComercialesDetalleService(IPedidosComercialesDetalleRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PedidosComercialesDetalle>>> GetAllAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        try { var d = (await _repo.ShowallAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial)).ToList();
              return ApiResponse<IEnumerable<PedidosComercialesDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PedidosComercialesDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PedidosComercialesDetalle>> GetByIdAsync(decimal numPedidoComercial, int numSecuencia)
    {
        try { var d = await _repo.ShowbyidAsync(numPedidoComercial, numSecuencia);
              return d is null ? ApiResponse<PedidosComercialesDetalle>.Fail("No encontrado.") : ApiResponse<PedidosComercialesDetalle>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PedidosComercialesDetalle>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PedidosComercialesDetalle>>> SearchAsync(PedidosComercialesDetalleSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<PedidosComercialesDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<PedidosComercialesDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesDetalleInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Ítem guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(PedidosComercialesDetalleUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Ítem actualizado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia)
    {
        try { var r = await _repo.DeleteAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial, numSecuencia);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Ítem eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
