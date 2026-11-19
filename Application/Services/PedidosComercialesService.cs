using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class PedidosComercialesService : IPedidosComercialesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PedidosComercialesService);
    private readonly IPedidosComercialesRepository _repo;
    private readonly ErrorLogService _log;
    public PedidosComercialesService(IPedidosComercialesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PedidosComerciales>>> GetAllAsync(string codClaseOperacion, string codTipoOperacion)
    {
        try { var d = (await _repo.ShowallAsync(codClaseOperacion, codTipoOperacion)).ToList();
              return ApiResponse<IEnumerable<PedidosComerciales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PedidosComerciales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PedidosComerciales>> GetByIdAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        try { var d = await _repo.ShowbyidAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial);
              return d is null ? ApiResponse<PedidosComerciales>.Fail("No encontrado.") : ApiResponse<PedidosComerciales>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PedidosComerciales>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PedidosComerciales>>> SearchAsync(PedidosComercialesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<PedidosComerciales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<PedidosComerciales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(PedidosComercialesUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro actualizado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        try { var r = await _repo.DeleteAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    /// <summary>Los nueve catálogos de la cabecera en una sola llamada.</summary>
    public async Task<ApiResponse<PedidosComercialesLoadCombosResult>> LoadcombosAsync(string codClaseOperacion)
    {
        try { return ApiResponse<PedidosComercialesLoadCombosResult>.Ok(await _repo.LoadcombosAsync(codClaseOperacion)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<PedidosComercialesLoadCombosResult>.Fail(ex.Message); }
    }

    /// <summary>El porcentaje de atención de los pedidos de una clase (y tipo) de operación, para la barra de la grilla.</summary>
    public async Task<ApiResponse<IEnumerable<PedidoComercialAtencion>>> AtencionAsync(string codClaseOperacion, string? codTipoOperacion)
    {
        try { var d = (await _repo.AtencionAsync(codClaseOperacion, codTipoOperacion)).ToList();
              return ApiResponse<IEnumerable<PedidoComercialAtencion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(AtencionAsync));
              return ApiResponse<IEnumerable<PedidoComercialAtencion>>.Fail(ex.Message); }
    }
}
