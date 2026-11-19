using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class PedidosComercialesSecuenciasEspecificacionesTecnicasService : IPedidosComercialesSecuenciasEspecificacionesTecnicasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PedidosComercialesSecuenciasEspecificacionesTecnicasService);
    private readonly IPedidosComercialesSecuenciasEspecificacionesTecnicasRepository _repo;
    private readonly ErrorLogService _log;
    public PedidosComercialesSecuenciasEspecificacionesTecnicasService(IPedidosComercialesSecuenciasEspecificacionesTecnicasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PedidosComercialesSecuenciasEspecificacionesTecnicas>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<PedidosComercialesSecuenciasEspecificacionesTecnicas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PedidosComercialesSecuenciasEspecificacionesTecnicas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PedidosComercialesSecuenciasEspecificacionesTecnicas>> GetByIdAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA);
              return d is null ? ApiResponse<PedidosComercialesSecuenciasEspecificacionesTecnicas>.Fail("No encontrado.") : ApiResponse<PedidosComercialesSecuenciasEspecificacionesTecnicas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PedidosComercialesSecuenciasEspecificacionesTecnicas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PedidosComercialesSecuenciasEspecificacionesTecnicas>>> SearchAsync(PedidosComercialesSecuenciasEspecificacionesTecnicasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<PedidosComercialesSecuenciasEspecificacionesTecnicas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<PedidosComercialesSecuenciasEspecificacionesTecnicas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesSecuenciasEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(PedidosComercialesSecuenciasEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion)
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