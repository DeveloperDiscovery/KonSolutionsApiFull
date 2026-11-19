using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class GuiasRemisionService : IGuiasRemisionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(GuiasRemisionService);

    private readonly IGuiasRemisionRepository _repo;
    private readonly IConfiguracionEmisionElectronicaService _config;
    private readonly ErrorLogService _log;
    private readonly Func<DateTime> _ahora;

    public GuiasRemisionService(IGuiasRemisionRepository repo, IConfiguracionEmisionElectronicaService config, ErrorLogService log)
        : this(repo, config, log, () => DateTime.Now) { }

    /// <summary>Para las pruebas: con el reloj en la mano.</summary>
    public GuiasRemisionService(IGuiasRemisionRepository repo, IConfiguracionEmisionElectronicaService config, ErrorLogService log, Func<DateTime> ahora)
    {
        _repo = repo; _config = config; _log = log; _ahora = ahora;
    }

    /// <summary>Los THROW de los procedimientos de guías van de 50500 a 50599: son reglas de negocio
    /// con un mensaje pensado para el usuario, no fallas que haya que registrar.</summary>
    private static bool EsReglaDeNegocio(SqlException ex) => ex.Number is >= 50500 and <= 50599;

    // ── Consultas ────────────────────────────────────────────────────────────────────────────

    public async Task<ApiResponse<IEnumerable<GuiaRemision>>> GetAllAsync(GuiaRemisionFiltroDto filtro)
    {
        try { var d = (await _repo.ShowallAsync(filtro)).ToList();
              return ApiResponse<IEnumerable<GuiaRemision>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<GuiaRemision>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GuiaRemision>> GetByIdAsync(int NUM_GUIA_REMISION)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_GUIA_REMISION);
              return d is null ? ApiResponse<GuiaRemision>.Fail("No encontrado.") : ApiResponse<GuiaRemision>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<GuiaRemision>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GuiaRemision>> GetByMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var d = await _repo.ShowbymovimientoAsync(NUM_MOVIMIENTO_ALMACEN);
              return d is null ? ApiResponse<GuiaRemision>.Fail("El movimiento no tiene guía de remisión.") : ApiResponse<GuiaRemision>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByMovimientoAsync));
              return ApiResponse<GuiaRemision>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<GuiaRemisionEnvio>>> GetEnviosAsync(int NUM_GUIA_REMISION)
    {
        try { var d = (await _repo.EnviosAsync(NUM_GUIA_REMISION)).ToList();
              return ApiResponse<IEnumerable<GuiaRemisionEnvio>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetEnviosAsync));
              return ApiResponse<IEnumerable<GuiaRemisionEnvio>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GuiaRemisionArchivo>> GetXmlAsync(int NUM_GUIA_REMISION)
    {
        try { var d = await _repo.XmlAsync(NUM_GUIA_REMISION);
              return d?.Contenido is not { Length: > 0 } ? ApiResponse<GuiaRemisionArchivo>.Fail("La guía todavía no tiene XML firmado.") : ApiResponse<GuiaRemisionArchivo>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetXmlAsync));
              return ApiResponse<GuiaRemisionArchivo>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GuiaRemisionArchivo>> GetCdrAsync(int NUM_GUIA_REMISION)
    {
        try { var d = await _repo.CdrAsync(NUM_GUIA_REMISION);
              return d?.Contenido is not { Length: > 0 } ? ApiResponse<GuiaRemisionArchivo>.Fail("La guía todavía no tiene constancia de recepción (CDR).") : ApiResponse<GuiaRemisionArchivo>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetCdrAsync));
              return ApiResponse<GuiaRemisionArchivo>.Fail(ex.Message); }
    }

    // ── Lo mismo, pidiendo la guía por su movimiento ────────────────────────────────────────

    /// <summary>La guía del movimiento (vigente o, si no hay, la última). Null si el movimiento nunca tuvo.</summary>
    private async Task<int?> GuiaDelMovimientoAsync(decimal numMovimiento)
        => (await _repo.ShowbymovimientoAsync(numMovimiento).ConfigureAwait(false))?.NUM_GUIA_REMISION;

    private const string SinGuia = "El movimiento no tiene guía de remisión.";

    public async Task<ApiResponse<IEnumerable<GuiaRemisionEnvio>>> GetEnviosPorMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { return await GuiaDelMovimientoAsync(NUM_MOVIMIENTO_ALMACEN).ConfigureAwait(false) is { } n
                  ? await GetEnviosAsync(n).ConfigureAwait(false)
                  : ApiResponse<IEnumerable<GuiaRemisionEnvio>>.Fail(SinGuia); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetEnviosPorMovimientoAsync));
              return ApiResponse<IEnumerable<GuiaRemisionEnvio>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GuiaRemisionArchivo>> GetXmlPorMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { return await GuiaDelMovimientoAsync(NUM_MOVIMIENTO_ALMACEN).ConfigureAwait(false) is { } n
                  ? await GetXmlAsync(n).ConfigureAwait(false)
                  : ApiResponse<GuiaRemisionArchivo>.Fail(SinGuia); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetXmlPorMovimientoAsync));
              return ApiResponse<GuiaRemisionArchivo>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GuiaRemisionArchivo>> GetCdrPorMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { return await GuiaDelMovimientoAsync(NUM_MOVIMIENTO_ALMACEN).ConfigureAwait(false) is { } n
                  ? await GetCdrAsync(n).ConfigureAwait(false)
                  : ApiResponse<GuiaRemisionArchivo>.Fail(SinGuia); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetCdrPorMovimientoAsync));
              return ApiResponse<GuiaRemisionArchivo>.Fail(ex.Message); }
    }

    // ── Validador previo ─────────────────────────────────────────────────────────────────────

    public async Task<ApiResponse<GuiaValidacionResultado>> ValidarAsync(decimal NUM_MOVIMIENTO_ALMACEN, bool trasladoVehiculoM1L = false)
    {
        try { return ApiResponse<GuiaValidacionResultado>.Ok(await ValidarInternoAsync(NUM_MOVIMIENTO_ALMACEN, trasladoVehiculoM1L).ConfigureAwait(false)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ValidarAsync));
              return ApiResponse<GuiaValidacionResultado>.Fail(ex.Message); }
    }

    private async Task<(GuiaValidacionResultado Resultado, IReadOnlyList<IReadOnlyDictionary<string, object?>> Filas)> ValidarConFilasAsync(decimal numMovimiento, bool m1l = false)
    {
        var filas = await _repo.ReporteMovimientoAsync(numMovimiento).ConfigureAwait(false);
        var guia = await _repo.ShowbymovimientoAsync(numMovimiento).ConfigureAwait(false);
        var series = await _repo.SeriesParaMovimientoAsync(numMovimiento).ConfigureAwait(false);
        var config = await _config.EstadoAsync().ConfigureAwait(false);
        var r = GuiaRemisionValidador.Validar(new GuiaValidacionContexto
        {
            Filas = filas, Guia = guia, SeriesDisponibles = series, Config = config, Hoy = _ahora().Date, TrasladoVehiculoM1L = m1l
        });
        r.NUM_MOVIMIENTO_ALMACEN = numMovimiento;
        return (r, filas);
    }

    private async Task<GuiaValidacionResultado> ValidarInternoAsync(decimal numMovimiento, bool m1l = false) => (await ValidarConFilasAsync(numMovimiento, m1l).ConfigureAwait(false)).Resultado;

    public async Task<ApiResponse<IEnumerable<GuiaRemisionCatalogoPendiente>>> GetCatalogosPendientesAsync()
    {
        try { var d = (await _repo.CatalogosPendientesAsync().ConfigureAwait(false)).ToList();
              return ApiResponse<IEnumerable<GuiaRemisionCatalogoPendiente>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetCatalogosPendientesAsync));
              return ApiResponse<IEnumerable<GuiaRemisionCatalogoPendiente>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GuiaRemisionBloqueo>> GetBloqueoPorMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { var m = await _repo.BloqueoMovimientoAsync(NUM_MOVIMIENTO_ALMACEN).ConfigureAwait(false);
              return ApiResponse<GuiaRemisionBloqueo>.Ok(new GuiaRemisionBloqueo { NUM_MOVIMIENTO_ALMACEN = NUM_MOVIMIENTO_ALMACEN, FLG_BLOQUEADO = m is not null, DES_MENSAJE = m }); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetBloqueoPorMovimientoAsync));
              return ApiResponse<GuiaRemisionBloqueo>.Fail(ex.Message); }
    }

    /// <summary>Lo que se copia de la fila del reporte a la tabla de guías "para buscar y listar".
    /// Sale del reporte y no de lo que mande el cliente: la guía es un documento legal.</summary>
    public static GuiaRemisionDatos DatosDesdeReporte(IReadOnlyDictionary<string, object?> f)
    {
        string? Txt(string c) => f.TryGetValue(c, out var v) && v is not (null or DBNull) ? Convert.ToString(v, System.Globalization.CultureInfo.InvariantCulture)?.Trim() is { Length: > 0 } s ? s : null : null;
        DateTime? Fec(string c) => f.TryGetValue(c, out var v) ? v switch { DateTime d => d, DateOnly d => d.ToDateTime(TimeOnly.MinValue), _ => null } : null;
        decimal? Dec(string c) => Txt(c) is { } s && decimal.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var d) ? d : null;
        return new GuiaRemisionDatos
        {
            FEC_INICIO_TRASLADO = Fec("FEC_INICIO_TRASLADO"),
            COD_MODALIDAD_TRASLADO = Txt("COD_MODALIDAD_TRASLADO"),
            COD_MOTIVO_SUNAT = Txt("COD_MOTIVO_SUNAT"),
            COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT = Txt("COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT"),
            NUM_DOCUMENTO_DESTINATARIO = Txt("NUM_DOCUMENTO_DESTINATARIO"),
            DES_NOMBRE_DESTINATARIO = Txt("DES_NOMBRE_DESTINATARIO"),
            NUM_PLACA_PRINCIPAL = Txt("NUM_PLACA_PRINCIPAL") is { } p ? GuiaRemisionValidador.NormalizarPlaca(p) : null,
            CAN_PESO_BRUTO_TOTAL = Dec("CAN_PESO_BRUTO_TOTAL"),
            COD_UNIDAD_PESO_SUNAT = Txt("COD_UNIDAD_PESO_SUNAT")
        };
    }

    // ── Operaciones de la emisión ────────────────────────────────────────────────────────────
    // Sin endpoints propios a propósito: las usa GuiasRemisionEmisionService (la emisión de punta a punta),
    // para que nadie deje una guía a medias.

    public async Task<ApiResponse<GuiaRemision>> CrearAsync(GuiaRemisionCrearDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var (v, filas) = await ValidarConFilasAsync(dto.NUM_MOVIMIENTO_ALMACEN).ConfigureAwait(false);
            if (!v.PuedeEmitir)
                return ApiResponse<GuiaRemision>.Fail("El movimiento no está en condiciones de emitir: " + string.Join(" ", v.Items.Where(i => i.Gravedad == GuiaValidacionGravedad.Error).Select(i => i.Mensaje)));
            var d = DatosDesdeReporte(filas[0]);
            dto.FEC_INICIO_TRASLADO = d.FEC_INICIO_TRASLADO; dto.COD_MODALIDAD_TRASLADO = d.COD_MODALIDAD_TRASLADO; dto.COD_MOTIVO_SUNAT = d.COD_MOTIVO_SUNAT;
            dto.COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT = d.COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT; dto.NUM_DOCUMENTO_DESTINATARIO = d.NUM_DOCUMENTO_DESTINATARIO;
            dto.DES_NOMBRE_DESTINATARIO = d.DES_NOMBRE_DESTINATARIO; dto.NUM_PLACA_PRINCIPAL = d.NUM_PLACA_PRINCIPAL;
            dto.CAN_PESO_BRUTO_TOTAL = d.CAN_PESO_BRUTO_TOTAL; dto.COD_UNIDAD_PESO_SUNAT = d.COD_UNIDAD_PESO_SUNAT;
            return ApiResponse<GuiaRemision>.Ok(await _repo.CrearAsync(dto, codUsuario, codEstacion).ConfigureAwait(false), "Guía creada.");
        }
        catch (SqlException ex) when (EsReglaDeNegocio(ex)) { return ApiResponse<GuiaRemision>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CrearAsync));
              return ApiResponse<GuiaRemision>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GuiaRemision>> ReintentarAsync(GuiaRemisionReintentarDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var guia = await _repo.ShowbyidAsync(dto.NUM_GUIA_REMISION).ConfigureAwait(false);
            if (guia is null) return ApiResponse<GuiaRemision>.Fail("No encontrado.");
            var (v, filas) = await ValidarConFilasAsync(guia.NUM_MOVIMIENTO_ALMACEN).ConfigureAwait(false);
            if (!v.PuedeEmitir)
                return ApiResponse<GuiaRemision>.Fail("El movimiento no está en condiciones de emitir: " + string.Join(" ", v.Items.Where(i => i.Gravedad == GuiaValidacionGravedad.Error).Select(i => i.Mensaje)));
            var d = DatosDesdeReporte(filas[0]);
            dto.FEC_INICIO_TRASLADO = d.FEC_INICIO_TRASLADO; dto.COD_MODALIDAD_TRASLADO = d.COD_MODALIDAD_TRASLADO; dto.COD_MOTIVO_SUNAT = d.COD_MOTIVO_SUNAT;
            dto.COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT = d.COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT; dto.NUM_DOCUMENTO_DESTINATARIO = d.NUM_DOCUMENTO_DESTINATARIO;
            dto.DES_NOMBRE_DESTINATARIO = d.DES_NOMBRE_DESTINATARIO; dto.NUM_PLACA_PRINCIPAL = d.NUM_PLACA_PRINCIPAL;
            dto.CAN_PESO_BRUTO_TOTAL = d.CAN_PESO_BRUTO_TOTAL; dto.COD_UNIDAD_PESO_SUNAT = d.COD_UNIDAD_PESO_SUNAT;
            return ApiResponse<GuiaRemision>.Ok(await _repo.ReintentarAsync(dto, codUsuario, codEstacion).ConfigureAwait(false), "Guía lista para reenviar.");
        }
        catch (SqlException ex) when (EsReglaDeNegocio(ex)) { return ApiResponse<GuiaRemision>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ReintentarAsync));
              return ApiResponse<GuiaRemision>.Fail(ex.Message); }
    }

    public Task<ApiResponse<bool>> RegistrarXmlAsync(GuiaRemisionXmlDto dto, string codUsuario, string codEstacion)
        => EscribirAsync(nameof(RegistrarXmlAsync), "XML registrado.", () => _repo.RegistrarXmlAsync(dto, codUsuario, codEstacion));

    public Task<ApiResponse<bool>> RegistrarEnvioAsync(GuiaRemisionEnvioDto dto, string codUsuario, string codEstacion)
        => EscribirAsync(nameof(RegistrarEnvioAsync), "Envío registrado.", () => _repo.RegistrarEnvioAsync(dto, codUsuario, codEstacion));

    public Task<ApiResponse<bool>> RegistrarRespuestaAsync(GuiaRemisionRespuestaDto dto, string codUsuario, string codEstacion)
        => EscribirAsync(nameof(RegistrarRespuestaAsync), "Respuesta registrada.", () => _repo.RegistrarRespuestaAsync(dto, codUsuario, codEstacion));

    private async Task<ApiResponse<bool>> EscribirAsync(string operacion, string mensaje, Func<Task> accion)
    {
        try { await accion().ConfigureAwait(false); return ApiResponse<bool>.Ok(true, mensaje); }
        catch (SqlException ex) when (EsReglaDeNegocio(ex)) { return ApiResponse<bool>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, operacion);
              return ApiResponse<bool>.Fail(ex.Message); }
    }
}
