using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rfid;

namespace KonSolutionsApi.Application.Services;

public class RfidEmisionesService : IRfidEmisionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(RfidEmisionesService);
    private readonly IRfidEmisionesRepository _repo;
    private readonly ErrorLogService _log;
    public RfidEmisionesService(IRfidEmisionesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    private async Task<ApiResponse<IEnumerable<T>>> Lista<T>(Func<Task<IEnumerable<T>>> consulta, string metodo)
    {
        try { var d = (await consulta()).ToList();
              return ApiResponse<IEnumerable<T>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo);
              return ApiResponse<IEnumerable<T>>.Fail(ex.Message); }
    }

    public Task<ApiResponse<IEnumerable<RfidEmision>>> GetAllAsync() => Lista(_repo.ShowallAsync, nameof(GetAllAsync));

    public async Task<ApiResponse<RfidEmision>> GetByIdAsync(int NUM_EMISION)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_EMISION);
              return d is null ? ApiResponse<RfidEmision>.Fail("No encontrado.") : ApiResponse<RfidEmision>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<RfidEmision>.Fail(ex.Message); }
    }

    public Task<ApiResponse<IEnumerable<RfidEmision>>> SearchAsync(RfidEmisionSearchDto dto) => Lista(() => _repo.SearchAsync(dto), nameof(SearchAsync));
    public Task<ApiResponse<IEnumerable<RfidEmisionDetalle>>> DetalleAsync(int NUM_EMISION) => Lista(() => _repo.DetalleAsync(NUM_EMISION), nameof(DetalleAsync));
    public Task<ApiResponse<IEnumerable<RfidEtiqueta>>> EtiquetasAsync(int NUM_EMISION, int? NUM_ITEM, string? COD_ESTADO_ETIQUETA, int? TOP)
        => Lista(() => _repo.EtiquetasAsync(NUM_EMISION, NUM_ITEM, COD_ESTADO_ETIQUETA, TOP), nameof(EtiquetasAsync));
    public Task<ApiResponse<IEnumerable<RfidEtiqueta>>> EtiquetasSearchAsync(RfidEtiquetaSearchDto dto) => Lista(() => _repo.EtiquetasSearchAsync(dto), nameof(EtiquetasSearchAsync));
    public Task<ApiResponse<IEnumerable<RfidOrigenCompra>>> OrigenComprasAsync(RfidOrigenCompraSearchDto dto) => Lista(() => _repo.OrigenComprasAsync(dto), nameof(OrigenComprasAsync));
    public Task<ApiResponse<IEnumerable<RfidOrigenCompraLinea>>> OrigenComprasLineasAsync(decimal NUM_MOVIMIENTO_ALMACEN) => Lista(() => _repo.OrigenComprasLineasAsync(NUM_MOVIMIENTO_ALMACEN), nameof(OrigenComprasLineasAsync));
    public Task<ApiResponse<IEnumerable<RfidOrigenStock>>> OrigenStockAsync(RfidOrigenStockSearchDto dto) => Lista(() => _repo.OrigenStockAsync(dto), nameof(OrigenStockAsync));

    public async Task<ApiResponse<WriteResult>> InsertAsync(RfidEmisionInsertDto dto, string codUsuario, string codEstacion)
    {
        if (dto.LINEAS is not { Count: > 0 }) return ApiResponse<WriteResult>.Fail("Indica al menos una línea para emitir etiquetas.");
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, $"Emisión {r.Codigo} creada con {r.AffectedRows} etiquetas pendientes."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(RfidEmisionUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Emisión modificada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> AnularAsync(int NUM_EMISION, RfidAnularDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.AnularAsync(NUM_EMISION, dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Emisión anulada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(AnularAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_EMISION)
    {
        try { var r = await _repo.DeleteAsync(NUM_EMISION);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Emisión eliminada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RfidEnrolado>> EnrolarAsync(int NUM_EMISION, RfidEnrolarDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var (r, e) = await _repo.EnrolarAsync(NUM_EMISION, dto, codUsuario, codEstacion);
            if (r.IsError) { WriteResult.OnSpError?.Invoke(r.Mensaje); return ApiResponse<RfidEnrolado>.Fail(r.Mensaje); }
            return e is null ? ApiResponse<RfidEnrolado>.Fail("El procedimiento no devolvió la etiqueta enrolada.")
                             : ApiResponse<RfidEnrolado>.Ok(e, e.FLG_YA_ENROLADA ? "Ese TID ya estaba enrolado en esta emisión: se devuelve lo ya hecho." : "Etiqueta enrolada.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(EnrolarAsync));
              return ApiResponse<RfidEnrolado>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> GrabadaAsync(RfidGrabadaDto dto, string codUsuario)
    {
        try { var r = await _repo.GrabadaAsync(dto, codUsuario);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Grabación registrada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GrabadaAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RfidEtiquetaAnulada>> AnularEtiquetaAsync(int NUM_EMISION, int NUM_ITEM, int NUM_ETIQUETA, RfidEtiquetaAnularDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var (r, a) = await _repo.AnularEtiquetaAsync(NUM_EMISION, NUM_ITEM, NUM_ETIQUETA, dto, codUsuario, codEstacion);
            if (r.IsError) { WriteResult.OnSpError?.Invoke(r.Mensaje); return ApiResponse<RfidEtiquetaAnulada>.Fail(r.Mensaje); }
            return ApiResponse<RfidEtiquetaAnulada>.Ok(a ?? new RfidEtiquetaAnulada(), a?.NUM_ETIQUETA_REPUESTA is null ? "Etiqueta anulada." : $"Etiqueta anulada; se repuso la {a.NUM_ETIQUETA_REPUESTA}.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(AnularEtiquetaAsync));
              return ApiResponse<RfidEtiquetaAnulada>.Fail(ex.Message); }
    }
}
