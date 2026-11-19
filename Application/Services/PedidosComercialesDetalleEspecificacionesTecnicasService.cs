using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class PedidosComercialesDetalleEspecificacionesTecnicasService : IPedidosComercialesDetalleEspecificacionesTecnicasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PedidosComercialesDetalleEspecificacionesTecnicasService);
    private readonly IPedidosComercialesDetalleEspecificacionesTecnicasRepository _repo;
    private readonly ErrorLogService _log;
    public PedidosComercialesDetalleEspecificacionesTecnicasService(IPedidosComercialesDetalleEspecificacionesTecnicasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PedidosComercialesDetalleEspecificacionesTecnicas>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<PedidosComercialesDetalleEspecificacionesTecnicas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PedidosComercialesDetalleEspecificacionesTecnicas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PedidosComercialesDetalleEspecificacionesTecnicas>> GetByIdAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA);
              return d is null ? ApiResponse<PedidosComercialesDetalleEspecificacionesTecnicas>.Fail("No encontrado.") : ApiResponse<PedidosComercialesDetalleEspecificacionesTecnicas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PedidosComercialesDetalleEspecificacionesTecnicas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PedidosComercialesDetalleEspecificacionesTecnicas>>> SearchAsync(PedidosComercialesDetalleEspecificacionesTecnicasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<PedidosComercialesDetalleEspecificacionesTecnicas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<PedidosComercialesDetalleEspecificacionesTecnicas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesDetalleEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(PedidosComercialesDetalleEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    {
        try { var r = await _repo.DeleteAsync(NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}