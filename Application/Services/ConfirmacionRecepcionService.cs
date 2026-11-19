using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

/// <summary>Confirmación de recepción de transferencias: las salidas que esperan al almacén
/// destino, su detalle, los motivos de rechazo y las dos acciones (confirmar y rechazar).</summary>
public class ConfirmacionRecepcionService : IConfirmacionRecepcionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ConfirmacionRecepcionService);
    /// <summary>El largo de DES_OBSERVACION (DES_2XL). El SP arma "RECHAZO usuario fecha: motivo -
    /// observación" y lo agrega a la observación del movimiento, así que lo escrito no puede
    /// ocuparla toda.</summary>
    private const int LargoObservacion = 300;
    private readonly IConfirmacionRecepcionRepository _repo;
    private readonly ErrorLogService _log;
    public ConfirmacionRecepcionService(IConfirmacionRecepcionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<RecepcionPendiente>>> PendientesAsync(string? codRol, string codUsuario, string? codAlmacen)
    {
        if (string.IsNullOrWhiteSpace(codRol)) return ApiResponse<IEnumerable<RecepcionPendiente>>.Fail("Falta el rol.");
        try
        {
            var d = (await _repo.PendientesAsync(codRol.Trim(), codUsuario,
                string.IsNullOrWhiteSpace(codAlmacen) ? null : codAlmacen.Trim())).ToList();
            return ApiResponse<IEnumerable<RecepcionPendiente>>.Ok(d, total: d.Count);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(PendientesAsync));
            return ApiResponse<IEnumerable<RecepcionPendiente>>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<IEnumerable<RecepcionLinea>>> DetalleAsync(decimal numMovimiento)
    {
        try { var d = (await _repo.DetalleAsync(numMovimiento)).ToList();
              return ApiResponse<IEnumerable<RecepcionLinea>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DetalleAsync));
              return ApiResponse<IEnumerable<RecepcionLinea>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RecepcionMotivosRechazo>> MotivosRechazoAsync(decimal numMovimiento)
    {
        try { return ApiResponse<RecepcionMotivosRechazo>.Ok(await _repo.MotivosRechazoAsync(numMovimiento)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(MotivosRechazoAsync));
              return ApiResponse<RecepcionMotivosRechazo>.Fail(ex.Message); }
    }

    /// <summary>Lo básico se valida acá para responder rápido; lo demás (estado ENVIADO, rol,
    /// ubicaciones del almacén destino, tipo de ingreso) lo valida el SP dentro de la transacción,
    /// que es donde vale. Los errores del SP (THROW 503xx) ya vienen redactados para el usuario.</summary>
    public async Task<ApiResponse<RecepcionResultado>> ConfirmarAsync(RecepcionConfirmarDto dto, string codUsuario, string codEstacion)
    {
        if (dto.NUM_MOVIMIENTO_ALMACEN is null) return ApiResponse<RecepcionResultado>.Fail("Elige la salida.");
        if (string.IsNullOrWhiteSpace(dto.COD_ROL_USUARIO)) return ApiResponse<RecepcionResultado>.Fail("Falta el rol.");
        if (dto.Lineas is not { Count: > 0 }) return ApiResponse<RecepcionResultado>.Fail("La salida no tiene líneas.");
        if (dto.Lineas.Any(l => string.IsNullOrWhiteSpace(l.COD_UBICACION_DESTINO)))
            return ApiResponse<RecepcionResultado>.Fail("Todas las líneas tienen que tener su ubicación de ingreso.");
        if (dto.Etiquetas.Any(e => e.NUM_SECUENCIA is null || string.IsNullOrWhiteSpace(e.COD_STICKER_RFID)))
            return ApiResponse<RecepcionResultado>.Fail("Cada etiqueta leída tiene que traer su código y la línea de la salida.");
        return await EjecutarAsync(() => _repo.ConfirmarAsync(dto, codUsuario, codEstacion), nameof(ConfirmarAsync));
    }

    public async Task<ApiResponse<RecepcionResultado>> RechazarAsync(RecepcionRechazarDto dto, string codUsuario, string codEstacion)
    {
        if (dto.NUM_MOVIMIENTO_ALMACEN is null) return ApiResponse<RecepcionResultado>.Fail("Elige la salida.");
        if (string.IsNullOrWhiteSpace(dto.COD_ROL_USUARIO)) return ApiResponse<RecepcionResultado>.Fail("Falta el rol.");
        if (dto.COD_TIPO_MOTIVO is null || dto.COD_MOTIVO is null) return ApiResponse<RecepcionResultado>.Fail("Elige el motivo del rechazo.");
        if ((dto.DES_OBSERVACION?.Trim().Length ?? 0) > LargoObservacion)
            return ApiResponse<RecepcionResultado>.Fail($"La observación puede tener hasta {LargoObservacion} caracteres.");
        return await EjecutarAsync(() => _repo.RechazarAsync(dto, codUsuario, codEstacion), nameof(RechazarAsync));
    }

    public async Task<ApiResponse<IEnumerable<RecepcionLecturaLinea>>> LecturaContextoAsync(decimal numMovimiento)
    {
        try { var d = (await _repo.LecturaContextoAsync(numMovimiento)).ToList();
              return ApiResponse<IEnumerable<RecepcionLecturaLinea>>.Ok(d, total: d.Count); }
        catch (SqlException ex) when (ex.Number is >= 50000)
        { return ApiResponse<IEnumerable<RecepcionLecturaLinea>>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LecturaContextoAsync));
              return ApiResponse<IEnumerable<RecepcionLecturaLinea>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MovimientosAlmacenEtiqueta>>> EtiquetasAsync(decimal numMovimiento, int? numSecuencia)
    {
        try { var d = (await _repo.EtiquetasAsync(numMovimiento, numSecuencia)).ToList();
              return ApiResponse<IEnumerable<MovimientosAlmacenEtiqueta>>.Ok(d, total: d.Count); }
        catch (SqlException ex) when (ex.Number is >= 50000)
        { return ApiResponse<IEnumerable<MovimientosAlmacenEtiqueta>>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(EtiquetasAsync));
              return ApiResponse<IEnumerable<MovimientosAlmacenEtiqueta>>.Fail(ex.Message); }
    }

    /// <summary>Una etiqueta leída al recibir. Si el SP la rechaza, la respuesta es un error con el mensaje del SP y, en
    /// data.errorNumber, su número (50708 falta el peso, 50706 se puede forzar...).</summary>
    public async Task<ApiResponse<RecepcionLecturaResultado>> LeerAsync(RecepcionLecturaDto dto)
    {
        try
        {
            var r = await _repo.LeerAsync(dto);
            if (r.ErrorNumber is null) return ApiResponse<RecepcionLecturaResultado>.Ok(r, "Etiqueta revisada.");
            var mensaje = string.IsNullOrWhiteSpace(r.DES_ADVERTENCIA) ? "No se pudo revisar la etiqueta." : r.DES_ADVERTENCIA!;
            return new ApiResponse<RecepcionLecturaResultado>
            {
                Success = false, Message = mensaje,
                Data = new RecepcionLecturaResultado { ErrorNumber = r.ErrorNumber }
            };
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LeerAsync));
              return ApiResponse<RecepcionLecturaResultado>.Fail(ex.Message); }
    }

    private async Task<ApiResponse<RecepcionResultado>> EjecutarAsync(Func<Task<RecepcionResultado>> accion, string metodo)
    {
        try
        {
            return ApiResponse<RecepcionResultado>.Ok(await accion());
        }
        catch (SqlException ex) when (ex.Number is >= 50000)
        {
            // Validación del SP: el mensaje es para el usuario, no hace falta el log de errores.
            return ApiResponse<RecepcionResultado>.Fail(ex.Message);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, metodo);
            return ApiResponse<RecepcionResultado>.Fail(ex.Message);
        }
    }
}
