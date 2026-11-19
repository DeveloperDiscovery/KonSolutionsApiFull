using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class TiposOperacionTiposComprobantePagoService : ITiposOperacionTiposComprobantePagoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposOperacionTiposComprobantePagoService);
    private readonly ITiposOperacionTiposComprobantePagoRepository _repo;
    private readonly ErrorLogService _log;
    public TiposOperacionTiposComprobantePagoService(ITiposOperacionTiposComprobantePagoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposOperacionTiposComprobantePago>>> GetAllAsync(string codClaseOperacion, string codTipoOperacion)
    {
        try { var d = (await _repo.ShowallAsync(codClaseOperacion, codTipoOperacion)).ToList();
              return ApiResponse<IEnumerable<TiposOperacionTiposComprobantePago>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposOperacionTiposComprobantePago>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposOperacionTiposComprobantePago>> GetByIdAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_COMPROBANTE_PAGO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_COMPROBANTE_PAGO);
              return d is null ? ApiResponse<TiposOperacionTiposComprobantePago>.Fail("No encontrado.") : ApiResponse<TiposOperacionTiposComprobantePago>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposOperacionTiposComprobantePago>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposOperacionTiposComprobantePago>>> SearchAsync(string codClaseOperacion, string codTipoOperacion, TiposOperacionTiposComprobantePagoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(codClaseOperacion, codTipoOperacion, dto)).ToList();
              return ApiResponse<IEnumerable<TiposOperacionTiposComprobantePago>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposOperacionTiposComprobantePago>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposOperacionTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_COMPROBANTE_PAGO)
    {
        try { var r = await _repo.DeleteAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_COMPROBANTE_PAGO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}