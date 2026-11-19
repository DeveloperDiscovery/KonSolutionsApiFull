using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class MovimientosAlmacenService : IMovimientosAlmacenService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(MovimientosAlmacenService);
    private readonly IMovimientosAlmacenRepository _repo;
    private readonly ErrorLogService _log;
    public MovimientosAlmacenService(IMovimientosAlmacenRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<MovimientosAlmacen>>> GetAllAsync(string codUsuario)
    {
        try { var d = (await _repo.ShowallAsync(codUsuario)).ToList();
              return ApiResponse<IEnumerable<MovimientosAlmacen>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<MovimientosAlmacen>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MovimientosAlmacen>> GetByIdAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_MOVIMIENTO_ALMACEN);
              return d is null ? ApiResponse<MovimientosAlmacen>.Fail("No encontrado.") : ApiResponse<MovimientosAlmacen>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<MovimientosAlmacen>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientosAlmacen>>> SearchAsync(MovimientosAlmacenSearchDto dto, string codUsuario)
    {
        try { var d = (await _repo.SearchAsync(dto, codUsuario)).ToList();
              return ApiResponse<IEnumerable<MovimientosAlmacen>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<MovimientosAlmacen>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(MovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(MovimientosAlmacenUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var r = await _repo.DeleteAsync(NUM_MOVIMIENTO_ALMACEN);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }


    public async Task<ApiResponse<MovimientosAlmacenLoadCombosResult>> GetLoadcombosAsync(string? codClaseMovimiento)
    {
        try { return ApiResponse<MovimientosAlmacenLoadCombosResult>.Ok(await _repo.LoadcombosAsync(codClaseMovimiento)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetLoadcombosAsync));
              return ApiResponse<MovimientosAlmacenLoadCombosResult>.Fail(ex.Message); }
    }

    // Los tres comparten cuerpo: cambia el procedimiento y el nombre de la clave del origen.
    private async Task<ApiResponse<MovimientosAlmacenAgregarResult>> AgregarAsync(
        string sp, string nombreClave, int? valorClave, decimal num,
        MovimientosAlmacenAgregarDto dto, string u, string e, string queFalta)
    {
        if (valorClave is null or 0)
            return ApiResponse<MovimientosAlmacenAgregarResult>.Fail($"Falta indicar {queFalta}.");
        try { var r = await _repo.AgregarDesdeAsync(sp, num, nombreClave, valorClave.Value, dto.DES_SECUENCIAS, u, e);
              return ApiResponse<MovimientosAlmacenAgregarResult>.Ok(r); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(AgregarAsync));
              return ApiResponse<MovimientosAlmacenAgregarResult>.Fail(ex.Message); }
    }

    public Task<ApiResponse<MovimientosAlmacenAgregarResult>> AgregarDesdeReservaAsync(decimal num, MovimientosAlmacenAgregarDto dto, string u, string e)
        => AgregarAsync(SP.SCHEMALogistica.MovimientosAlmacen.AgregarDesdeReserva,
                        "NUM_RESERVA_STOCK", dto.NUM_RESERVA_STOCK, num, dto, u, e, "la reserva de origen");

    public Task<ApiResponse<MovimientosAlmacenAgregarResult>> AgregarDesdeRequerimientoAsync(decimal num, MovimientosAlmacenAgregarDto dto, string u, string e)
        => AgregarAsync(SP.SCHEMALogistica.MovimientosAlmacen.AgregarDesdeRequerimiento,
                        "NUM_REQUERIMIENTO_STOCK", dto.NUM_REQUERIMIENTO_STOCK, num, dto, u, e, "el requerimiento de origen");

    public Task<ApiResponse<MovimientosAlmacenAgregarResult>> AgregarDesdePedidoComercialAsync(decimal num, MovimientosAlmacenAgregarDto dto, string u, string e)
        => AgregarAsync(SP.SCHEMALogistica.MovimientosAlmacen.AgregarDesdePedidoComercial,
                        "NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL, num, dto, u, e, "la orden de compra de origen");

    public async Task<ApiResponse<MovimientosAlmacenNumeroImpresionResult>> NumeroImpresionAsync(decimal num, string u, string e)
    {
        try { return ApiResponse<MovimientosAlmacenNumeroImpresionResult>.Ok(await _repo.NumeroImpresionAsync(num, u, e)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(NumeroImpresionAsync));
              return ApiResponse<MovimientosAlmacenNumeroImpresionResult>.Fail(ex.Message); }
    }

    /// <summary>Botón TRANSFERIR. Los errores del SP (THROW 50311-50314: no existe, sin tipo de
    /// estado, sin estado ENV, ya transferido) ya vienen redactados para el usuario.</summary>
    public async Task<ApiResponse<bool>> TransferirAsync(decimal num, string u, string e)
    {
        try { await _repo.TransferirAsync(num, u, e); return ApiResponse<bool>.Ok(true); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(TransferirAsync));
              return ApiResponse<bool>.Fail(ex.Message); }
    }

    // ---- Devolucion de compra: guias del mismo proveedor, items y cantidades a devolver ----
    public async Task<ApiResponse<IEnumerable<MovimientoDevolucionGuia>>> DevolucionGuiasDisponiblesAsync(decimal num)
    {
        try { return ApiResponse<IEnumerable<MovimientoDevolucionGuia>>.Ok(await _repo.DevolucionGuiasDisponiblesAsync(num)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DevolucionGuiasDisponiblesAsync));
              return ApiResponse<IEnumerable<MovimientoDevolucionGuia>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientoDevolucionGuia>>> DevolucionGuiasAsync(decimal num)
    {
        try { return ApiResponse<IEnumerable<MovimientoDevolucionGuia>>.Ok(await _repo.DevolucionGuiasAsync(num)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DevolucionGuiasAsync));
              return ApiResponse<IEnumerable<MovimientoDevolucionGuia>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientoDevolucionLinea>>> DevolucionLineasAsync(decimal num, string? origenes)
    {
        try { return ApiResponse<IEnumerable<MovimientoDevolucionLinea>>.Ok(await _repo.DevolucionLineasAsync(num, origenes)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DevolucionLineasAsync));
              return ApiResponse<IEnumerable<MovimientoDevolucionLinea>>.Fail(ex.Message); }
    }

    /// <summary>Arma el XML que espera el procedimiento: &lt;r&gt;&lt;i m s c/&gt;&lt;/r&gt;. Cantidad 0 quita el item.</summary>
    internal static string ArmarXmlDevolucion(IEnumerable<MovimientoDevolucionItemDto> items)
    {
        var inv = System.Globalization.CultureInfo.InvariantCulture;
        var sb = new System.Text.StringBuilder("<r>");
        foreach (var i in items)
            sb.Append("<i m=\"").Append(i.NUM_MOVIMIENTO_ALMACEN_ORIGEN.ToString(inv))
              .Append("\" s=\"").Append(i.NUM_SECUENCIA_ORIGEN.ToString(inv))
              .Append("\" c=\"").Append(i.CAN_DEVOLVER.ToString(inv)).Append("\"/>");
        return sb.Append("</r>").ToString();
    }

    public async Task<ApiResponse<MovimientosAlmacenAgregarResult>> DevolucionAdicionarAsync(decimal num, MovimientoDevolucionAdicionarDto dto, string u, string e)
    {
        if (dto?.Items is null || dto.Items.Count == 0)
            return ApiResponse<MovimientosAlmacenAgregarResult>.Fail("Indica al menos un item para devolver.");
        try { return ApiResponse<MovimientosAlmacenAgregarResult>.Ok(
                  await _repo.DevolucionAdicionarAsync(num, ArmarXmlDevolucion(dto.Items), u, e)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DevolucionAdicionarAsync));
              return ApiResponse<MovimientosAlmacenAgregarResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<int>> DevolucionQuitarGuiaAsync(decimal num, int origen)
    {
        try { return ApiResponse<int>.Ok(await _repo.DevolucionQuitarGuiaAsync(num, origen)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DevolucionQuitarGuiaAsync));
              return ApiResponse<int>.Fail(ex.Message); }
    }
}