using System.Collections.Concurrent;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

/// <summary>La emisión de la guía de remisión electrónica, de punta a punta: lo que pasa entre que el usuario toca «Emitir» y que SUNAT
/// contesta. Es el único que junta las piezas (el validador, el XML, la firma, el cliente de SUNAT y los procedimientos que guardan la
/// evidencia) y el que decide qué se anota en cada paso.
///
/// El orden, y qué queda si algo falla:
///   1. Validar el movimiento (nada se gasta si no está en condiciones).
///   2. Crear la guía (toma el número) — PENDIENTE.                      Si falla algo desde aquí, la guía queda PENDIENTE con su número.
///   3. Armar el XML, firmarlo y empaquetarlo, y guardarlo con sus huellas.  Emitir otra vez sigue desde aquí con el MISMO número.
///   4. Mandarlo a SUNAT.  Si SUNAT lo rechaza de entrada (400 o 422) la guía queda RECHAZADA; si no se llega a SUNAT (red, 5xx, credenciales)
///      queda PENDIENTE, porque no se sabe si llegó.
///   5. Anotar el ticket — ENVIADA.                                        Desde aquí la guía es de SUNAT: solo se consulta.
///   6. Consultar el ticket unas veces seguidas. Contesta aceptada (con su constancia) o rechazada: se anota — ACEPTADA o RECHAZADA.
///      Si SUNAT todavía no termina, queda ENVIADA y «Consultar» pregunta de nuevo cuando el usuario quiera.
/// Una guía RECHAZADA se corrige y se usa «Reintentar»: vuelve a PENDIENTE con el mismo número y la hora de emisión nueva, y sigue el paso 3.
///
/// Dos personas emitiendo el mismo movimiento a la vez: la segunda recibe un aviso (hay un cerrojo por movimiento en este servidor) y, aunque
/// llegara a pasar, los procedimientos solo aceptan cada paso desde el estado que corresponde.</summary>
public class GuiasRemisionEmisionService : IGuiasRemisionEmisionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(GuiasRemisionEmisionService);
    private const int MaxRespuesta = 2000, MaxCodigo = 20;

    /// <summary>Un cerrojo por movimiento: dos emisiones del mismo movimiento en este servidor no corren a la vez.</summary>
    private static readonly ConcurrentDictionary<decimal, SemaphoreSlim> Cerrojos = new();

    /// <summary>Cuánto se espera entre una consulta del ticket y la siguiente al emitir (SUNAT suele tardar unos segundos).</summary>
    public static readonly TimeSpan[] EsperasPorDefecto = { TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5) };

    private readonly IGuiasRemisionRepository _repo;
    private readonly IConfiguracionEmisionElectronicaService _config;
    private readonly GuiaRemisionSunatCliente _sunat;
    private readonly ErrorLogService _log;
    private readonly Func<DateTime> _ahora;
    private readonly IReadOnlyList<TimeSpan> _esperas;
    private readonly Func<TimeSpan, CancellationToken, Task> _esperar;

    public GuiasRemisionEmisionService(IGuiasRemisionRepository repo, IConfiguracionEmisionElectronicaService config, GuiaRemisionSunatCliente sunat, ErrorLogService log)
        : this(repo, config, sunat, log, () => DateTime.Now, EsperasPorDefecto, (t, ct) => Task.Delay(t, ct)) { }

    /// <summary>Para las pruebas: con el reloj y las esperas en la mano.</summary>
    public GuiasRemisionEmisionService(IGuiasRemisionRepository repo, IConfiguracionEmisionElectronicaService config, GuiaRemisionSunatCliente sunat, ErrorLogService log,
                                       Func<DateTime> ahora, IReadOnlyList<TimeSpan> esperas, Func<TimeSpan, CancellationToken, Task> esperar)
    {
        _repo = repo; _config = config; _sunat = sunat; _log = log; _ahora = ahora; _esperas = esperas; _esperar = esperar;
    }

    private static bool EsReglaDeNegocio(SqlException ex) => ex.Number is >= 50500 and <= 50599;

    // ── Lo que se pide desde afuera ──────────────────────────────────────────────────────────

    public Task<ApiResponse<GuiaRemisionEmisionResultado>> EmitirAsync(GuiaRemisionEmitirDto dto, string codUsuario, string codEstacion, CancellationToken ct = default)
        => ConCerrojoAsync(dto.NUM_MOVIMIENTO_ALMACEN, nameof(EmitirAsync), () => EmitirInternoAsync(dto, codUsuario, codEstacion, ct));

    public Task<ApiResponse<GuiaRemisionEmisionResultado>> ConsultarAsync(int NUM_GUIA_REMISION, string codUsuario, string codEstacion, CancellationToken ct = default)
        => PorGuiaAsync(() => _repo.ShowbyidAsync(NUM_GUIA_REMISION), nameof(ConsultarAsync), g => ConsultarInternoAsync(g.NUM_GUIA_REMISION, codUsuario, codEstacion, ct), "No encontrado.");

    public Task<ApiResponse<GuiaRemisionEmisionResultado>> ReintentarAsync(int NUM_GUIA_REMISION, GuiaRemisionOpcionesDto? opciones, string codUsuario, string codEstacion, CancellationToken ct = default)
        => PorGuiaAsync(() => _repo.ShowbyidAsync(NUM_GUIA_REMISION), nameof(ReintentarAsync), g => ReintentarInternoAsync(g.NUM_GUIA_REMISION, opciones, codUsuario, codEstacion, ct), "No encontrado.");

    public Task<ApiResponse<GuiaRemisionEmisionResultado>> ConsultarPorMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN, string codUsuario, string codEstacion, CancellationToken ct = default)
        => PorGuiaAsync(() => _repo.ShowbymovimientoAsync(NUM_MOVIMIENTO_ALMACEN), nameof(ConsultarPorMovimientoAsync), g => ConsultarInternoAsync(g.NUM_GUIA_REMISION, codUsuario, codEstacion, ct));

    public Task<ApiResponse<GuiaRemisionEmisionResultado>> ReintentarPorMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN, GuiaRemisionOpcionesDto? opciones, string codUsuario, string codEstacion, CancellationToken ct = default)
        => PorGuiaAsync(() => _repo.ShowbymovimientoAsync(NUM_MOVIMIENTO_ALMACEN), nameof(ReintentarPorMovimientoAsync), g => ReintentarInternoAsync(g.NUM_GUIA_REMISION, opciones, codUsuario, codEstacion, ct));

    /// <summary>Busca la guía (por su número o por su movimiento) y corre la operación con el cerrojo de SU movimiento.</summary>
    private async Task<ApiResponse<GuiaRemisionEmisionResultado>> PorGuiaAsync(Func<Task<GuiaRemision?>> buscar, string operacion, Func<GuiaRemision, Task<ApiResponse<GuiaRemisionEmisionResultado>>> accion, string sinGuia = SinGuia)
    {
        GuiaRemision? guia;
        try { guia = await buscar().ConfigureAwait(false); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, operacion); return Fail(ex.Message); }
        return guia is null
            ? Fail(sinGuia)
            : await ConCerrojoAsync(guia.NUM_MOVIMIENTO_ALMACEN, operacion, () => accion(guia)).ConfigureAwait(false);
    }

    public async Task<ApiResponse<GuiaRemisionOpciones>> GetOpcionesAsync(int NUM_GUIA_REMISION)
    {
        try { var o = await _repo.OpcionesAsync(NUM_GUIA_REMISION).ConfigureAwait(false);
              return o is null ? ApiResponse<GuiaRemisionOpciones>.Fail("No encontrado.") : ApiResponse<GuiaRemisionOpciones>.Ok(o); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetOpcionesAsync));
              return ApiResponse<GuiaRemisionOpciones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GuiaRemisionOpciones>> GetOpcionesPorMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        try { return await _repo.ShowbymovimientoAsync(NUM_MOVIMIENTO_ALMACEN).ConfigureAwait(false) is { } g
                  ? await GetOpcionesAsync(g.NUM_GUIA_REMISION).ConfigureAwait(false)
                  : ApiResponse<GuiaRemisionOpciones>.Fail(SinGuia); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetOpcionesPorMovimientoAsync));
              return ApiResponse<GuiaRemisionOpciones>.Fail(ex.Message); }
    }

    private const string SinGuia = "El movimiento no tiene guía de remisión.";

    private async Task<ApiResponse<GuiaRemisionEmisionResultado>> ConCerrojoAsync(decimal numMovimiento, string operacion, Func<Task<ApiResponse<GuiaRemisionEmisionResultado>>> accion)
    {
        var cerrojo = Cerrojos.GetOrAdd(numMovimiento, _ => new SemaphoreSlim(1, 1));
        if (!await cerrojo.WaitAsync(0).ConfigureAwait(false))
            return ApiResponse<GuiaRemisionEmisionResultado>.Fail("Ya hay una emisión en curso para este movimiento. Espera a que termine y mira el estado de la guía.");
        try { return await accion().ConfigureAwait(false); }
        catch (SqlException ex) when (EsReglaDeNegocio(ex)) { return ApiResponse<GuiaRemisionEmisionResultado>.Fail(ex.Message); }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, operacion);
            return ApiResponse<GuiaRemisionEmisionResultado>.Fail(ex.Message);
        }
        finally { cerrojo.Release(); }
    }

    // ── Emitir ───────────────────────────────────────────────────────────────────────────────

    private async Task<ApiResponse<GuiaRemisionEmisionResultado>> EmitirInternoAsync(GuiaRemisionEmitirDto dto, string u, string e, CancellationToken ct)
    {
        var mov = dto.NUM_MOVIMIENTO_ALMACEN;
        var guia = await _repo.ShowbymovimientoAsync(mov).ConfigureAwait(false);

        if (guia is { FLG_VIGENTE: true })
        {
            switch (guia.COD_ESTADO_ALIAS)
            {
                case "ACE": return Fail($"La guía {guia.NUM_GUIA} ya fue aceptada por SUNAT.");
                case "ENV": return await ConsultarInternoAsync(guia.NUM_GUIA_REMISION, u, e, ct).ConfigureAwait(false);   // ya está en SUNAT: solo se pregunta
                case "REC": return Fail($"La guía {guia.NUM_GUIA} fue rechazada por SUNAT ({Resumen(guia.COD_RESPUESTA_SUNAT, guia.DES_RESPUESTA_SUNAT)}). Corrige lo que indica y usa Reintentar.");
            }
        }
        var pendiente = guia is { FLG_VIGENTE: true };   // PEN: se sigue con el mismo número

        // Las opciones: las que llegan; si no, las ya guardadas de la guía pendiente; si no, ninguna.
        var opciones = dto.OPCIONES is { } enviadas ? Normalizadas(enviadas)
                     : pendiente ? Dto(await _repo.OpcionesAsync(guia!.NUM_GUIA_REMISION).ConfigureAwait(false)) : new GuiaRemisionOpcionesDto();
        if (ErrorDeOpciones(opciones) is { } malas) return Fail(malas);

        var (v, filas) = await ValidarAsync(mov, pendiente ? null : guia, opciones.FLG_TRASLADO_M1L).ConfigureAwait(false);
        if (!v.PuedeEmitir) return Fail(NoPuedeEmitir(v));

        if (!pendiente)
        {
            var d = GuiasRemisionService.DatosDesdeReporte(filas[0]);
            guia = await _repo.CrearAsync(new GuiaRemisionCrearDto
            {
                NUM_MOVIMIENTO_ALMACEN = mov, COD_SERIE = dto.COD_SERIE,
                FEC_INICIO_TRASLADO = d.FEC_INICIO_TRASLADO, COD_MODALIDAD_TRASLADO = d.COD_MODALIDAD_TRASLADO, COD_MOTIVO_SUNAT = d.COD_MOTIVO_SUNAT,
                COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT = d.COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT, NUM_DOCUMENTO_DESTINATARIO = d.NUM_DOCUMENTO_DESTINATARIO,
                DES_NOMBRE_DESTINATARIO = d.DES_NOMBRE_DESTINATARIO, NUM_PLACA_PRINCIPAL = d.NUM_PLACA_PRINCIPAL,
                CAN_PESO_BRUTO_TOTAL = d.CAN_PESO_BRUTO_TOTAL, COD_UNIDAD_PESO_SUNAT = d.COD_UNIDAD_PESO_SUNAT
            }, u, e).ConfigureAwait(false);
        }
        // Se guardan las opciones cuando son nuevas (guía recién creada, o el usuario las mandó): así un reintento manda lo mismo.
        if (!pendiente || dto.OPCIONES is not null)
            await _repo.GuardarOpcionesAsync(guia!.NUM_GUIA_REMISION, opciones, u, e).ConfigureAwait(false);

        return await ProcesarAsync(guia!, filas, opciones, u, e, ct).ConfigureAwait(false);
    }

    // ── Reintentar ───────────────────────────────────────────────────────────────────────────

    private async Task<ApiResponse<GuiaRemisionEmisionResultado>> ReintentarInternoAsync(int numGuia, GuiaRemisionOpcionesDto? nuevas, string u, string e, CancellationToken ct)
    {
        var guia = await _repo.ShowbyidAsync(numGuia).ConfigureAwait(false);
        if (guia is null) return Fail("No encontrado.");
        if (guia.COD_ESTADO_ALIAS != "REC")
            return Fail($"La guía {guia.NUM_GUIA} está {guia.DES_ESTADO ?? guia.COD_ESTADO_ALIAS}: solo se reintenta una guía RECHAZADA.");

        nuevas = nuevas is null ? null : Normalizadas(nuevas);
        var opciones = nuevas ?? Dto(await _repo.OpcionesAsync(guia.NUM_GUIA_REMISION).ConfigureAwait(false));
        if (ErrorDeOpciones(opciones) is { } malas) return Fail(malas);

        // La guía rechazada se corrige: se vuelve a validar contra el movimiento como está HOY.
        var (v, filas) = await ValidarAsync(guia.NUM_MOVIMIENTO_ALMACEN, null, opciones.FLG_TRASLADO_M1L).ConfigureAwait(false);
        if (!v.PuedeEmitir) return Fail(NoPuedeEmitir(v));

        var d = GuiasRemisionService.DatosDesdeReporte(filas[0]);
        guia = await _repo.ReintentarAsync(new GuiaRemisionReintentarDto
        {
            NUM_GUIA_REMISION = guia.NUM_GUIA_REMISION,
            FEC_INICIO_TRASLADO = d.FEC_INICIO_TRASLADO, COD_MODALIDAD_TRASLADO = d.COD_MODALIDAD_TRASLADO, COD_MOTIVO_SUNAT = d.COD_MOTIVO_SUNAT,
            COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT = d.COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT, NUM_DOCUMENTO_DESTINATARIO = d.NUM_DOCUMENTO_DESTINATARIO,
            DES_NOMBRE_DESTINATARIO = d.DES_NOMBRE_DESTINATARIO, NUM_PLACA_PRINCIPAL = d.NUM_PLACA_PRINCIPAL,
            CAN_PESO_BRUTO_TOTAL = d.CAN_PESO_BRUTO_TOTAL, COD_UNIDAD_PESO_SUNAT = d.COD_UNIDAD_PESO_SUNAT
        }, u, e).ConfigureAwait(false);

        // Ya está PENDIENTE: ahora sí se pueden cambiar las opciones.
        if (nuevas is not null)
            await _repo.GuardarOpcionesAsync(guia.NUM_GUIA_REMISION, nuevas, u, e).ConfigureAwait(false);

        return await ProcesarAsync(guia, filas, opciones, u, e, ct).ConfigureAwait(false);
    }

    // ── Consultar ────────────────────────────────────────────────────────────────────────────

    private async Task<ApiResponse<GuiaRemisionEmisionResultado>> ConsultarInternoAsync(int numGuia, string u, string e, CancellationToken ct)
    {
        var guia = await _repo.ShowbyidAsync(numGuia).ConfigureAwait(false);
        if (guia is null) return Fail("No encontrado.");
        switch (guia.COD_ESTADO_ALIAS)
        {
            case "ACE": return Ok(guia, GuiaEmisionResultados.Aceptada, MensajeAceptada(guia.NUM_GUIA, guia.COD_RESPUESTA_SUNAT, guia.DES_RESPUESTA_SUNAT));
            case "REC": return Ok(guia, GuiaEmisionResultados.Rechazada, MensajeRechazada(guia.NUM_GUIA, guia.COD_RESPUESTA_SUNAT, guia.DES_RESPUESTA_SUNAT));
            case "ENV": break;
            default: return Fail($"La guía {guia.NUM_GUIA} está {guia.DES_ESTADO ?? guia.COD_ESTADO_ALIAS}: todavía no se mandó a SUNAT. Usa Emitir.");
        }
        if (string.IsNullOrWhiteSpace(guia.NUM_TICKET))
            return Fail($"La guía {guia.NUM_GUIA} está ENVIADA pero no tiene número de ticket guardado: no se puede preguntar a SUNAT.");

        var cred = await _config.ObtenerCredencialesAsync().ConfigureAwait(false);
        if (!cred.Success) return Fail(cred.Message);
        var filas = await _repo.ReporteMovimientoAsync(guia.NUM_MOVIMIENTO_ALMACEN).ConfigureAwait(false);
        var ruc = filas.Count > 0 ? Txt(filas[0], "NUM_RUC_REMITENTE") : null;
        if (ruc is null) return Fail("No se pudo saber el RUC del remitente para consultar a SUNAT. Revisa el RUC de la empresa en Configuración General.");

        // Una sola pregunta: quien consulta quiere la respuesta de ahora.
        return await ResolverTicketAsync(guia, cred.Data!, ruc, guia.NUM_TICKET!, Array.Empty<TimeSpan>(), u, e, ct).ConfigureAwait(false);
    }

    // ── Del XML a la respuesta ───────────────────────────────────────────────────────────────

    /// <summary>Con la guía PENDIENTE (recién creada, reintentada o dejada a medias): arma, firma, guarda, manda y consulta.</summary>
    private async Task<ApiResponse<GuiaRemisionEmisionResultado>> ProcesarAsync(GuiaRemision guia, IReadOnlyList<IReadOnlyDictionary<string, object?>> filas,
        GuiaRemisionOpcionesDto opciones, string u, string e, CancellationToken ct)
    {
        const string QuedaPendiente = " La guía queda PENDIENTE con el mismo número: corrígelo y vuelve a emitir.";
        var cred = await _config.ObtenerCredencialesAsync().ConfigureAwait(false);
        if (!cred.Success) return Fail($"{cred.Message}{QuedaPendiente}");
        var c = cred.Data!;

        var ruc = Txt(filas[0], "NUM_RUC_REMITENTE") ?? throw new InvalidOperationException("Falta el RUC del remitente.");
        var nombre = GuiaRemisionXmlGenerador.NombreArchivo(ruc, guia.COD_SERIE, guia.NUM_CORRELATIVO);

        // Armar y firmar. Una falla acá es de los datos o del certificado: no se llega a SUNAT.
        byte[] zip; string hashZip; GuiaFirmada firmada;
        try
        {
            var xml = GuiaRemisionXmlGenerador.Generar(new GuiaXmlEntrada
            {
                Filas = filas, Serie = guia.COD_SERIE, Correlativo = guia.NUM_CORRELATIVO, FechaHoraEmision = guia.FEC_HORA_EMISION,
                TrasladoVehiculoM1L = opciones.FLG_TRASLADO_M1L, TransbordoProgramado = opciones.FLG_TRANSBORDO_PROGRAMADO,
                DocumentosRelacionados = DocumentosDe(opciones)
            });
            firmada = GuiaRemisionFirma.Firmar(xml, c.Certificado, c.ClaveCertificado);
            (zip, hashZip) = GuiaRemisionPaquete.Crear(nombre, firmada.Xml);
        }
        catch (Exception ex) when (ex is InvalidOperationException or System.Security.Cryptography.CryptographicException)
        {
            return Fail($"No se pudo armar la guía {guia.NUM_GUIA}: {ex.Message}{QuedaPendiente}");
        }

        var qr = GuiaRemisionXmlGenerador.TextoQr(ruc, guia.COD_SERIE, guia.NUM_CORRELATIVO, guia.FEC_HORA_EMISION,
                    Txt(filas[0], "COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT") ?? string.Empty, Txt(filas[0], "NUM_DOCUMENTO_DESTINATARIO") ?? string.Empty);
        await _repo.RegistrarXmlAsync(new GuiaRemisionXmlDto
        {
            NUM_GUIA_REMISION = guia.NUM_GUIA_REMISION, ARC_XML_FIRMADO = firmada.Xml, DES_HASH_XML = firmada.Hash, DES_HASH_ZIP = hashZip, DES_TEXTO_QR = qr
        }, u, e).ConfigureAwait(false);

        // Mandar. Desde acá no se cancela con la petición: si SUNAT ya recibió la guía hay que anotar su ticket sí o sí.
        SunatEnvio envio;
        try
        {
            envio = await _sunat.EnviarAsync(c, ruc, nombre, zip, hashZip, CancellationToken.None).ConfigureAwait(false);
        }
        catch (SunatException ex) when (ex.Http is 400 or 422)
        {
            // SUNAT leyó la guía y la devolvió con error, sin ticket: queda RECHAZADA con lo que dijo.
            var cod = Corto(ex.Codigo ?? ex.Http!.Value.ToString(CultureInfo.InvariantCulture), MaxCodigo);
            var msg = Corto(ex.Message, MaxRespuesta);
            await _repo.RegistrarRespuestaAsync(new GuiaRemisionRespuestaDto
            {
                NUM_GUIA_REMISION = guia.NUM_GUIA_REMISION, FLG_ACEPTADA = false, COD_RESPUESTA_SUNAT = cod, DES_RESPUESTA_SUNAT = msg, DES_HASH_ZIP = hashZip
            }, u, e).ConfigureAwait(false);
            return Ok(await _repo.ShowbyidAsync(guia.NUM_GUIA_REMISION).ConfigureAwait(false), GuiaEmisionResultados.Rechazada, MensajeRechazada(guia.NUM_GUIA, cod, msg));
        }
        catch (SunatException ex)
        {
            // No se sabe si SUNAT recibió la guía: PENDIENTE, y emitir otra vez manda el mismo número.
            return Fail($"No se pudo enviar la guía {guia.NUM_GUIA} a SUNAT: {ex.Message}{QuedaPendiente}");
        }

        await _repo.RegistrarEnvioAsync(new GuiaRemisionEnvioDto { NUM_GUIA_REMISION = guia.NUM_GUIA_REMISION, NUM_TICKET = envio.Ticket, DES_HASH_ZIP = hashZip }, u, e).ConfigureAwait(false);

        return await ResolverTicketAsync(guia, c, ruc, envio.Ticket, _esperas, u, e, ct).ConfigureAwait(false);
    }

    /// <summary>Pregunta por el ticket (esperando entre pregunta y pregunta mientras SUNAT lo procesa) y anota la respuesta si ya hay.</summary>
    private async Task<ApiResponse<GuiaRemisionEmisionResultado>> ResolverTicketAsync(GuiaRemision guia, CredencialesEmisionElectronica cred, string ruc, string ticket,
        IReadOnlyList<TimeSpan> esperas, string u, string e, CancellationToken ct)
    {
        SunatConsulta r;
        try
        {
            for (var i = 0; ; i++)
            {
                r = await _sunat.ConsultarAsync(cred, ruc, ticket, ct).ConfigureAwait(false);
                if (r.Estado != SunatEstadoTicket.EnProceso || i >= esperas.Count) break;
                await _esperar(esperas[i], ct).ConfigureAwait(false);
            }
        }
        catch (SunatException ex)
        {
            // La guía ya está en SUNAT; lo que falló es la pregunta. Sigue ENVIADA y se vuelve a preguntar con Consultar.
            return Ok(await _repo.ShowbyidAsync(guia.NUM_GUIA_REMISION).ConfigureAwait(false), GuiaEmisionResultados.EnProceso,
                      $"La guía {guia.NUM_GUIA} está en SUNAT (ticket {ticket}) pero no se pudo preguntar por su resultado: {ex.Message} Usa Consultar en unos minutos.");
        }
        catch (OperationCanceledException)
        {
            // El usuario cerró la pantalla mientras se esperaba: la guía sigue ENVIADA y la próxima consulta la resuelve.
            return Ok(await _repo.ShowbyidAsync(guia.NUM_GUIA_REMISION).ConfigureAwait(false), GuiaEmisionResultados.EnProceso, MensajeEnProceso(guia.NUM_GUIA, ticket));
        }

        if (r.Estado == SunatEstadoTicket.EnProceso)
            return Ok(await _repo.ShowbyidAsync(guia.NUM_GUIA_REMISION).ConfigureAwait(false), GuiaEmisionResultados.EnProceso, MensajeEnProceso(guia.NUM_GUIA, ticket));

        var aceptada = r.Estado == SunatEstadoTicket.Aceptada;
        var cod = Corto(r.Codigo ?? string.Empty, MaxCodigo);
        var msg = Corto(r.Mensaje ?? string.Empty, MaxRespuesta);
        await _repo.RegistrarRespuestaAsync(new GuiaRemisionRespuestaDto
        {
            NUM_GUIA_REMISION = guia.NUM_GUIA_REMISION, FLG_ACEPTADA = aceptada, COD_RESPUESTA_SUNAT = cod, DES_RESPUESTA_SUNAT = msg, ARC_CDR = r.CdrZip
        }, u, e).ConfigureAwait(false);
        var final = await _repo.ShowbyidAsync(guia.NUM_GUIA_REMISION).ConfigureAwait(false);
        return aceptada
            ? Ok(final, GuiaEmisionResultados.Aceptada, MensajeAceptada(guia.NUM_GUIA, cod, msg))
            : Ok(final, GuiaEmisionResultados.Rechazada, MensajeRechazada(guia.NUM_GUIA, cod, msg));
    }

    // ── Validación ───────────────────────────────────────────────────────────────────────────

    /// <param name="guiaExistente">La guía contra la que se compara. Null para seguir con una PENDIENTE o reintentar una RECHAZADA: ya es «la misma» guía.</param>
    private async Task<(GuiaValidacionResultado, IReadOnlyList<IReadOnlyDictionary<string, object?>>)> ValidarAsync(decimal mov, GuiaRemision? guiaExistente, bool m1l)
    {
        var filas = await _repo.ReporteMovimientoAsync(mov).ConfigureAwait(false);
        var series = await _repo.SeriesParaMovimientoAsync(mov).ConfigureAwait(false);
        var config = await _config.EstadoAsync().ConfigureAwait(false);
        var r = GuiaRemisionValidador.Validar(new GuiaValidacionContexto
        {
            Filas = filas, Guia = guiaExistente, SeriesDisponibles = series, Config = config, Hoy = _ahora().Date, TrasladoVehiculoM1L = m1l
        });
        r.NUM_MOVIMIENTO_ALMACEN = mov;
        return (r, filas);
    }

    private static string NoPuedeEmitir(GuiaValidacionResultado v)
        => "El movimiento no está en condiciones de emitir: " + string.Join(" ", v.Items.Where(i => i.Gravedad == GuiaValidacionGravedad.Error).Select(i => i.Mensaje));

    // ── Las opciones ─────────────────────────────────────────────────────────────────────────

    private static GuiaRemisionOpcionesDto Dto(GuiaRemisionOpciones? o) => o is null ? new GuiaRemisionOpcionesDto() : new GuiaRemisionOpcionesDto
    {
        FLG_TRASLADO_M1L = o.FLG_TRASLADO_M1L, FLG_TRANSBORDO_PROGRAMADO = o.FLG_TRANSBORDO_PROGRAMADO,
        COD_TIPO_DOCUMENTO_RELACIONADO = o.COD_TIPO_DOCUMENTO_RELACIONADO, DES_TIPO_DOCUMENTO_RELACIONADO = o.DES_TIPO_DOCUMENTO_RELACIONADO,
        NUM_DOCUMENTO_RELACIONADO = o.NUM_DOCUMENTO_RELACIONADO, NUM_RUC_EMISOR_RELACIONADO = o.NUM_RUC_EMISOR_RELACIONADO
    };

    /// <summary>Lo que se guarda es lo que se validó: sin espacios en los bordes y con vacío como "no hay". El procedimiento recibe cada dato en una columna de largo fijo
    /// y corta lo que sobra ANTES de limpiarlo: " 01 " llegaría como " 0".</summary>
    private static GuiaRemisionOpcionesDto Normalizadas(GuiaRemisionOpcionesDto o) => new()
    {
        FLG_TRASLADO_M1L = o.FLG_TRASLADO_M1L, FLG_TRANSBORDO_PROGRAMADO = o.FLG_TRANSBORDO_PROGRAMADO,
        COD_TIPO_DOCUMENTO_RELACIONADO = Limpio(o.COD_TIPO_DOCUMENTO_RELACIONADO), DES_TIPO_DOCUMENTO_RELACIONADO = Limpio(o.DES_TIPO_DOCUMENTO_RELACIONADO),
        NUM_DOCUMENTO_RELACIONADO = Limpio(o.NUM_DOCUMENTO_RELACIONADO), NUM_RUC_EMISOR_RELACIONADO = Limpio(o.NUM_RUC_EMISOR_RELACIONADO)
    };

    /// <summary>Lo mismo que exige el procedimiento que guarda las opciones, pero ANTES de crear la guía: así un dato mal escrito no gasta un número.</summary>
    public static string? ErrorDeOpciones(GuiaRemisionOpcionesDto o)
    {
        var tipo = Limpio(o.COD_TIPO_DOCUMENTO_RELACIONADO); var desc = Limpio(o.DES_TIPO_DOCUMENTO_RELACIONADO);
        var numero = Limpio(o.NUM_DOCUMENTO_RELACIONADO); var ruc = Limpio(o.NUM_RUC_EMISOR_RELACIONADO);
        var algo = tipo is not null || desc is not null || numero is not null || ruc is not null;
        if (algo && (tipo is null || desc is null || numero is null))
            return "El documento relacionado va completo: tipo (código y descripción) y número. Complétalo o déjalo vacío.";
        if (tipo is not null && !Regex.IsMatch(tipo, @"^\d{2}$"))
            return "El código del tipo de documento relacionado son dos dígitos (por ejemplo 01 para una factura).";
        if (ruc is not null && !Regex.IsMatch(ruc, @"^\d{11}$"))
            return "El RUC del emisor del documento relacionado tiene 11 dígitos.";
        if (desc is { Length: > 100 })
            return "La descripción del tipo de documento relacionado admite hasta 100 caracteres.";
        if (numero is { Length: > 30 })
            return "El número del documento relacionado admite hasta 30 caracteres.";
        return null;
    }

    private static IReadOnlyList<GuiaDocumentoRelacionado> DocumentosDe(GuiaRemisionOpcionesDto o) =>
        Limpio(o.COD_TIPO_DOCUMENTO_RELACIONADO) is { } tipo && Limpio(o.NUM_DOCUMENTO_RELACIONADO) is { } numero
            ? new[] { new GuiaDocumentoRelacionado { Tipo = tipo, Numero = numero, Descripcion = Limpio(o.DES_TIPO_DOCUMENTO_RELACIONADO) ?? string.Empty, RucEmisor = Limpio(o.NUM_RUC_EMISOR_RELACIONADO) } }
            : Array.Empty<GuiaDocumentoRelacionado>();

    // ── Mensajes y ayudas ────────────────────────────────────────────────────────────────────

    private static string MensajeAceptada(string numGuia, string? cod, string? desc)
        => $"SUNAT aceptó la guía {numGuia}." + (int.TryParse(cod, NumberStyles.Integer, CultureInfo.InvariantCulture, out var n) && n >= 4000 ? $" Con observaciones: {desc}" : "");

    private static string MensajeRechazada(string numGuia, string? cod, string? desc)
        => $"SUNAT rechazó la guía {numGuia}: {Resumen(cod, desc)}. Corrige lo que indica la respuesta y usa Reintentar.";

    private static string MensajeEnProceso(string numGuia, string ticket)
        => $"SUNAT recibió la guía {numGuia} y todavía la está procesando (ticket {ticket}). Usa Consultar en unos minutos.";

    private static string Resumen(string? cod, string? desc)
        => string.Join(" - ", new[] { cod, desc }.Where(x => !string.IsNullOrWhiteSpace(x)));

    private static ApiResponse<GuiaRemisionEmisionResultado> Fail(string mensaje) => ApiResponse<GuiaRemisionEmisionResultado>.Fail(mensaje);

    private static ApiResponse<GuiaRemisionEmisionResultado> Ok(GuiaRemision? guia, string resultado, string mensaje) =>
        ApiResponse<GuiaRemisionEmisionResultado>.Ok(new GuiaRemisionEmisionResultado
        {
            GUIA = guia, COD_RESULTADO = resultado, DES_MENSAJE = mensaje,
            COD_RESPUESTA_SUNAT = guia?.COD_RESPUESTA_SUNAT, DES_RESPUESTA_SUNAT = guia?.DES_RESPUESTA_SUNAT
        }, mensaje);

    private static string? Limpio(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
    private static string Corto(string s, int max) => s.Length <= max ? s : s[..max];

    private static string? Txt(IReadOnlyDictionary<string, object?> f, string c)
        => f.TryGetValue(c, out var v) && v is not (null or DBNull) ? Limpio(Convert.ToString(v, CultureInfo.InvariantCulture)) : null;
}
