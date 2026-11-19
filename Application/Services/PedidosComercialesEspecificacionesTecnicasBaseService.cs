using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class PedidosComercialesEspecificacionesTecnicasBaseService : IPedidosComercialesEspecificacionesTecnicasBaseService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PedidosComercialesEspecificacionesTecnicasBaseService);
    private readonly IPedidosComercialesEspecificacionesTecnicasBaseRepository _repo;
    private readonly ErrorLogService _log;
    public PedidosComercialesEspecificacionesTecnicasBaseService(IPedidosComercialesEspecificacionesTecnicasBaseRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PedidosComercialesEspecificacionesTecnicasBase>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<PedidosComercialesEspecificacionesTecnicasBase>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PedidosComercialesEspecificacionesTecnicasBase>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PedidosComercialesEspecificacionesTecnicasBase>> GetByIdAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_ENTIDAD, string COD_GRUPO_PRODUCTO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PEDIDO_COMERCIAL, COD_TIPO_ENTIDAD, COD_GRUPO_PRODUCTO);
              return d is null ? ApiResponse<PedidosComercialesEspecificacionesTecnicasBase>.Fail("No encontrado.") : ApiResponse<PedidosComercialesEspecificacionesTecnicasBase>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PedidosComercialesEspecificacionesTecnicasBase>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PedidosComercialesEspecificacionesTecnicasBase>>> SearchAsync(PedidosComercialesEspecificacionesTecnicasBaseSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<PedidosComercialesEspecificacionesTecnicasBase>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<PedidosComercialesEspecificacionesTecnicasBase>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesEspecificacionesTecnicasBaseInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_ENTIDAD, string COD_GRUPO_PRODUCTO)
    {
        try { var r = await _repo.DeleteAsync(NUM_PEDIDO_COMERCIAL, COD_TIPO_ENTIDAD, COD_GRUPO_PRODUCTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}