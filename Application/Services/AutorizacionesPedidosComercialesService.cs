using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class AutorizacionesPedidosComercialesService : IAutorizacionesPedidosComercialesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(AutorizacionesPedidosComercialesService);
    private readonly IAutorizacionesPedidosComercialesRepository _repo;
    private readonly ErrorLogService _log;
    public AutorizacionesPedidosComercialesService(IAutorizacionesPedidosComercialesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<AutorizacionesPedidosComerciales>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<AutorizacionesPedidosComerciales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<AutorizacionesPedidosComerciales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<AutorizacionesPedidosComerciales>> GetByIdAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, decimal COD_ENTIDAD_AUTORIZADA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_ENTIDAD_AUTORIZADA);
              return d is null ? ApiResponse<AutorizacionesPedidosComerciales>.Fail("No encontrado.") : ApiResponse<AutorizacionesPedidosComerciales>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<AutorizacionesPedidosComerciales>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<AutorizacionesPedidosComerciales>>> SearchAsync(AutorizacionesPedidosComercialesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<AutorizacionesPedidosComerciales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<AutorizacionesPedidosComerciales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(AutorizacionesPedidosComercialesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(AutorizacionesPedidosComercialesUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, decimal COD_ENTIDAD_AUTORIZADA)
    {
        try { var r = await _repo.DeleteAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_ENTIDAD_AUTORIZADA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}