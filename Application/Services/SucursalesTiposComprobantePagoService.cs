using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class SucursalesTiposComprobantePagoService : ISucursalesTiposComprobantePagoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SucursalesTiposComprobantePagoService);
    private readonly ISucursalesTiposComprobantePagoRepository _repo;
    private readonly ErrorLogService _log;
    public SucursalesTiposComprobantePagoService(ISucursalesTiposComprobantePagoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SucursalesTiposComprobantePago>>> GetAllAsync(string COD_SUCURSAL)
    {
        try { var d = (await _repo.ShowallAsync(COD_SUCURSAL)).ToList();
              return ApiResponse<IEnumerable<SucursalesTiposComprobantePago>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SucursalesTiposComprobantePago>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SucursalesTiposComprobantePago>> GetByIdAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO);
              return d is null ? ApiResponse<SucursalesTiposComprobantePago>.Fail("No encontrado.") : ApiResponse<SucursalesTiposComprobantePago>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SucursalesTiposComprobantePago>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_SUCURSAL)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(COD_SUCURSAL)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SucursalesTiposComprobantePago>>> SearchAsync(SucursalesTiposComprobantePagoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<SucursalesTiposComprobantePago>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SucursalesTiposComprobantePago>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(SucursalesTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO)
    {
        try { var r = await _repo.DeleteAsync(COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}