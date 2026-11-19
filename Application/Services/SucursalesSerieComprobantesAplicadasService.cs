using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class SucursalesSerieComprobantesAplicadasService : ISucursalesSerieComprobantesAplicadasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SucursalesSerieComprobantesAplicadasService);
    private readonly ISucursalesSerieComprobantesAplicadasRepository _repo;
    private readonly ErrorLogService _log;
    public SucursalesSerieComprobantesAplicadasService(ISucursalesSerieComprobantesAplicadasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SucursalesSerieComprobantesAplicadas>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<SucursalesSerieComprobantesAplicadas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SucursalesSerieComprobantesAplicadas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SucursalesSerieComprobantesAplicadas>> GetByIdAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, string COD_TIPO_COMPROBANTE_PAGO_APLICADO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO, COD_TIPO_COMPROBANTE_PAGO_APLICADO);
              return d is null ? ApiResponse<SucursalesSerieComprobantesAplicadas>.Fail("No encontrado.") : ApiResponse<SucursalesSerieComprobantesAplicadas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SucursalesSerieComprobantesAplicadas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SucursalesSerieComprobantesAplicadas>>> SearchAsync(SucursalesSerieComprobantesAplicadasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<SucursalesSerieComprobantesAplicadas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SucursalesSerieComprobantesAplicadas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(SucursalesSerieComprobantesAplicadasInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(SucursalesSerieComprobantesAplicadasUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, string COD_TIPO_COMPROBANTE_PAGO_APLICADO)
    {
        try { var r = await _repo.DeleteAsync(COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO, COD_TIPO_COMPROBANTE_PAGO_APLICADO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}