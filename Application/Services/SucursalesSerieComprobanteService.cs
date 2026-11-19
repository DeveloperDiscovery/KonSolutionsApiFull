using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class SucursalesSerieComprobanteService : ISucursalesSerieComprobanteService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SucursalesSerieComprobanteService);
    private readonly ISucursalesSerieComprobanteRepository _repo;
    private readonly ErrorLogService _log;
    public SucursalesSerieComprobanteService(ISucursalesSerieComprobanteRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SucursalesSerieComprobante>>> GetAllAsync(string codSucursal, string codTipoComprobantePago)
    {
        try { var d = (await _repo.ShowallAsync(codSucursal, codTipoComprobantePago)).ToList();
              return ApiResponse<IEnumerable<SucursalesSerieComprobante>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SucursalesSerieComprobante>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SucursalesSerieComprobante>> GetByIdAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, int NUM_SERIE_COMPROBANTE)
    {
        try { var d = await _repo.ShowbyidAsync(COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO, NUM_SERIE_COMPROBANTE);
              return d is null ? ApiResponse<SucursalesSerieComprobante>.Fail("No encontrado.") : ApiResponse<SucursalesSerieComprobante>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SucursalesSerieComprobante>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codSucursal, string codTipoComprobantePago)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(codSucursal, codTipoComprobantePago)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SucursalesSerieComprobante>>> SearchAsync(string codSucursal, string codTipoComprobantePago, SucursalesSerieComprobanteSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(codSucursal, codTipoComprobantePago, dto)).ToList();
              return ApiResponse<IEnumerable<SucursalesSerieComprobante>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SucursalesSerieComprobante>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(SucursalesSerieComprobanteInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(SucursalesSerieComprobanteUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, int NUM_SERIE_COMPROBANTE)
    {
        try { var r = await _repo.DeleteAsync(COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO, NUM_SERIE_COMPROBANTE);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}