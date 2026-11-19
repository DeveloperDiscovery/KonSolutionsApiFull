using System.Globalization;
using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

/// <summary>Lo que SUNAT contesta cuando se le manda una guía: el número de ticket para preguntar después.</summary>
public sealed class SunatEnvio
{
    public string Ticket { get; init; } = string.Empty;
    public DateTime? FechaRecepcion { get; init; }
}

/// <summary>Cómo va una guía en SUNAT, según el ticket.</summary>
public enum SunatEstadoTicket { EnProceso, Aceptada, Rechazada }

/// <summary>La respuesta a la consulta de un ticket. EnProceso (98): todavía no hay respuesta. Aceptada o Rechazada: ya hay CDR
/// (la constancia) y, con ella, el código y el mensaje de SUNAT. Un envío con error (99) llega como Rechazada con el error en el mensaje.</summary>
public sealed class SunatConsulta
{
    public SunatEstadoTicket Estado { get; init; }
    /// <summary>Código de SUNAT: el de la constancia (0 = aceptada) o el número del error (99).</summary>
    public string? Codigo { get; init; }
    public string? Mensaje { get; init; }
    /// <summary>El ZIP de la constancia de recepción (CDR), tal como lo mandó SUNAT. Null si no la generó.</summary>
    public byte[]? CdrZip { get; init; }
}

/// <summary>Un fallo al hablar con SUNAT. Lleva el código y el mensaje que dio SUNAT (si dio alguno) y si vale la pena volver a intentar.</summary>
public sealed class SunatException : Exception
{
    public int? Http { get; }
    public string? Codigo { get; }
    /// <summary>Verdadero si el problema es de la conexión o del servicio de SUNAT (se puede reintentar más tarde); falso si es de lo enviado.</summary>
    public bool Reintentable { get; }
    public SunatException(string mensaje, int? http = null, string? codigo = null, bool reintentable = false, Exception? interna = null) : base(mensaje, interna)
    { Http = http; Codigo = codigo; Reintentable = reintentable; }
}

/// <summary>La constancia de recepción (CDR) de SUNAT: un ZIP con un XML ApplicationResponse. Se lee el código y el mensaje.</summary>
public static class GuiaRemisionCdr
{
    /// <summary>El código y la descripción de la respuesta. 0 es aceptada; de 2000 a 3999, rechazada; de 4000 en adelante, aceptada con observaciones.</summary>
    public static (string Codigo, string Descripcion, bool Aceptada) Leer(byte[] zip)
    {
        using var za = new ZipArchive(new MemoryStream(zip), ZipArchiveMode.Read);
        var entrada = za.Entries.FirstOrDefault(e => e.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                      ?? throw new InvalidOperationException("La constancia de SUNAT no trae un XML.");
        using var s = entrada.Open();
        var xml = XDocument.Load(s);
        var respuesta = xml.Descendants().FirstOrDefault(e => e.Name.LocalName == "Response");
        var codigo = respuesta?.Descendants().FirstOrDefault(e => e.Name.LocalName == "ResponseCode")?.Value.Trim()
                     ?? throw new InvalidOperationException("La constancia de SUNAT no trae el código de respuesta.");
        var descripcion = respuesta?.Descendants().FirstOrDefault(e => e.Name.LocalName == "Description")?.Value.Trim() ?? string.Empty;
        return (codigo, descripcion, EsAceptada(codigo));
    }

    public static bool EsAceptada(string codigo) =>
        codigo == "0" || (int.TryParse(codigo, NumberStyles.Integer, CultureInfo.InvariantCulture, out var n) && (n == 0 || n >= 4000));
}

/// <summary>El cliente de la API de guías de remisión de SUNAT (GRE): pide el token, manda el ZIP firmado y consulta el ticket.
///
/// La URL de seguridad y la de la API vienen de la configuración de emisión (se cambian entre pruebas y producción sin tocar el código).
/// El token (vale 1 hora) se guarda en memoria y se renueva solo: al vencer, o si SUNAT contesta 401. Los fallos de conexión y los 5xx
/// se reintentan unas veces con espera; los 4xx no: son de lo que se mandó y repetirlo no lo arregla.
///
/// El secreto del cliente y la clave SOL viajan solo a SUNAT por HTTPS y no se escriben en ningún registro ni mensaje.</summary>
public sealed class GuiaRemisionSunatCliente
{
    private const string AlcanceApi = "https://api-cpe.sunat.gob.pe";
    private readonly HttpClient _http;
    private readonly Func<TimeSpan, CancellationToken, Task> _esperar;
    private readonly Func<DateTime> _ahora;
    private readonly int _intentos;
    private readonly TimeSpan _espera;
    private readonly object _cerrojo = new();
    private readonly Dictionary<string, (string Token, DateTime Vence)> _tokens = new();

    public GuiaRemisionSunatCliente(HttpClient http, int intentos = 3, TimeSpan? espera = null, Func<TimeSpan, CancellationToken, Task>? esperar = null, Func<DateTime>? ahora = null)
    {
        _http = http;
        _intentos = Math.Max(1, intentos);
        _espera = espera ?? TimeSpan.FromSeconds(2);
        _esperar = esperar ?? ((t, ct) => Task.Delay(t, ct));
        _ahora = ahora ?? (() => DateTime.UtcNow);
    }

    /// <summary>Las direcciones de SUNAT llevan la versión («…sunat.gob.pe/v1»). En la configuración se guarda solo el servidor
    /// («https://api-cpe.sunat.gob.pe»), así que se agrega «/v1» si falta; si ya viene, se respeta.</summary>
    public static string Base(string url)
    {
        var u = url.Trim().TrimEnd('/');
        return System.Text.RegularExpressions.Regex.IsMatch(u, @"/v\d+$") ? u : u + "/v1";
    }

    // ── Token ────────────────────────────────────────────────────────────────────────────────

    /// <summary>El usuario de la API es el RUC del emisor y el usuario SOL, juntos (a menos que el usuario ya traiga el RUC).</summary>
    public static string UsuarioApi(string ruc, string usuarioSol) => usuarioSol.StartsWith(ruc, StringComparison.Ordinal) ? usuarioSol : ruc + usuarioSol;

    private async Task<string> TokenAsync(CredencialesEmisionElectronica c, string ruc, bool renovar, CancellationToken ct)
    {
        var clave = $"{c.UrlSeguridad}|{c.ClientId}|{UsuarioApi(ruc, c.UsuarioSol)}";
        if (!renovar)
            lock (_cerrojo)
                if (_tokens.TryGetValue(clave, out var t) && t.Vence > _ahora()) return t.Token;

        var url = $"{Base(c.UrlSeguridad)}/clientessol/{Uri.EscapeDataString(c.ClientId)}/oauth2/token/";
        var cuerpo = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "password", ["scope"] = AlcanceApi, ["client_id"] = c.ClientId, ["client_secret"] = c.ClientSecret,
            ["username"] = UsuarioApi(ruc, c.UsuarioSol), ["password"] = c.ClaveSol
        });
        var r = await Con_reintentos(() => _http.PostAsync(url, cuerpo, ct), ct);
        var texto = await r.Content.ReadAsStringAsync(ct);
        if (!r.IsSuccessStatusCode)
            throw new SunatException($"SUNAT no dio el token ({(int)r.StatusCode}): {Resumen(texto)}. Revisa el client_id, el client_secret, el usuario y la clave SOL.", (int)r.StatusCode, reintentable: (int)r.StatusCode >= 500);
        var token = JsonSerializer.Deserialize<TokenRespuesta>(texto, Opciones);
        if (string.IsNullOrWhiteSpace(token?.AccessToken))
            throw new SunatException("SUNAT contestó sin token de acceso.", (int)r.StatusCode);
        // Se renueva un minuto antes de que venza.
        var vence = _ahora().AddSeconds(Math.Max(30, (token.ExpiresIn > 0 ? token.ExpiresIn : 3600) - 60));
        lock (_cerrojo) _tokens[clave] = (token.AccessToken!, vence);
        return token.AccessToken!;
    }

    // ── Envío ────────────────────────────────────────────────────────────────────────────────

    /// <summary>Manda la guía (el ZIP con el XML firmado). Devuelve el ticket. <paramref name="nombreBase"/>: RUC-09-SERIE-CORRELATIVO, sin extensión.</summary>
    public async Task<SunatEnvio> EnviarAsync(CredencialesEmisionElectronica c, string ruc, string nombreBase, byte[] zip, string hashZipHex, CancellationToken ct = default)
    {
        var url = $"{Base(c.UrlApi)}/contribuyente/gem/comprobantes/{Uri.EscapeDataString(nombreBase)}";
        var cuerpo = new EnvioCuerpo { Archivo = new() { NombreArchivo = nombreBase + ".zip", ZipBase64 = Convert.ToBase64String(zip), Hash = hashZipHex } };
        var r = await ConToken(c, ruc, ct, token => new HttpRequestMessage(HttpMethod.Post, url) { Content = JsonContent.Create(cuerpo, options: Opciones), Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) } });
        var texto = await r.Content.ReadAsStringAsync(ct);
        if (!r.IsSuccessStatusCode) throw Falla("SUNAT no recibió la guía", r.StatusCode, texto);
        var ok = JsonSerializer.Deserialize<EnvioRespuesta>(texto, Opciones);
        if (string.IsNullOrWhiteSpace(ok?.Ticket)) throw new SunatException("SUNAT contestó sin número de ticket.", (int)r.StatusCode);
        return new SunatEnvio { Ticket = ok.Ticket!, FechaRecepcion = ok.FechaRecepcion };
    }

    // ── Consulta del ticket ──────────────────────────────────────────────────────────────────

    /// <summary>Pregunta cómo va el ticket. 98: en proceso. 0: aceptada o rechazada (con la constancia). 99: SUNAT no pudo procesar el envío (error).</summary>
    public async Task<SunatConsulta> ConsultarAsync(CredencialesEmisionElectronica c, string ruc, string numTicket, CancellationToken ct = default)
    {
        var url = $"{Base(c.UrlApi)}/contribuyente/gem/comprobantes/envios/{Uri.EscapeDataString(numTicket)}";
        var r = await ConToken(c, ruc, ct, token => new HttpRequestMessage(HttpMethod.Get, url) { Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) } });
        var texto = await r.Content.ReadAsStringAsync(ct);
        if (!r.IsSuccessStatusCode) throw Falla("SUNAT no dio el estado del ticket", r.StatusCode, texto);
        var s = JsonSerializer.Deserialize<ConsultaRespuesta>(texto, Opciones) ?? throw new SunatException("SUNAT contestó sin cuerpo.", (int)r.StatusCode);
        switch (s.Codigo)
        {
            case "98":
                return new SunatConsulta { Estado = SunatEstadoTicket.EnProceso, Codigo = "98", Mensaje = "SUNAT todavía está procesando la guía." };
            case "99":
                return new SunatConsulta { Estado = SunatEstadoTicket.Rechazada, Codigo = s.Error?.Numero ?? "99", Mensaje = s.Error?.Descripcion ?? "SUNAT no pudo procesar el envío.",
                                           CdrZip = string.IsNullOrEmpty(s.CdrBase64) ? null : Convert.FromBase64String(s.CdrBase64) };
            case "0":
                if (string.IsNullOrEmpty(s.CdrBase64))
                    throw new SunatException("SUNAT dice que el envío está bien pero no mandó la constancia (CDR).", (int)r.StatusCode);
                var zip = Convert.FromBase64String(s.CdrBase64);
                var (codigo, descripcion, aceptada) = GuiaRemisionCdr.Leer(zip);
                return new SunatConsulta { Estado = aceptada ? SunatEstadoTicket.Aceptada : SunatEstadoTicket.Rechazada, Codigo = codigo, Mensaje = descripcion, CdrZip = zip };
            default:
                throw new SunatException($"SUNAT contestó un código que no conozco ({s.Codigo}).", (int)r.StatusCode);
        }
    }

    // ── Los pedidos: con token, con renovación del token y con reintentos ───────────────────

    private async Task<HttpResponseMessage> ConToken(CredencialesEmisionElectronica c, string ruc, CancellationToken ct, Func<string, HttpRequestMessage> crear)
    {
        var token = await TokenAsync(c, ruc, false, ct);
        var r = await Con_reintentos(() => _http.SendAsync(crear(token), ct), ct);
        if (r.StatusCode != HttpStatusCode.Unauthorized) return r;
        // Un 401 es un token vencido o revocado: se pide uno nuevo y se repite una sola vez.
        r.Dispose();
        token = await TokenAsync(c, ruc, true, ct);
        return await Con_reintentos(() => _http.SendAsync(crear(token), ct), ct);
    }

    private async Task<HttpResponseMessage> Con_reintentos(Func<Task<HttpResponseMessage>> pedir, CancellationToken ct)
    {
        for (var intento = 1; ; intento++)
        {
            try
            {
                var r = await pedir();
                if ((int)r.StatusCode < 500 || intento >= _intentos) return r;
                r.Dispose();
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException && !ct.IsCancellationRequested)
            {
                if (intento >= _intentos)
                    throw new SunatException("No se pudo conectar con SUNAT (sin respuesta o fuera de tiempo). La guía queda como está: vuelve a intentar en unos minutos.", reintentable: true, interna: ex);
            }
            await _esperar(TimeSpan.FromMilliseconds(_espera.TotalMilliseconds * intento), ct);
        }
    }

    private static SunatException Falla(string que, HttpStatusCode http, string cuerpo)
    {
        string? codigo = null, mensaje = null;
        try
        {
            var e = JsonSerializer.Deserialize<ErrorRespuesta>(cuerpo, Opciones);
            codigo = e?.Codigo;
            mensaje = e?.Mensaje;
            if (e?.Errores is { Count: > 0 })
                mensaje = string.Join(" | ", e.Errores.Select(x => $"{x.Codigo}: {x.Mensaje}").Prepend(mensaje ?? "").Where(x => x.Length > 0));
        }
        catch (JsonException) { mensaje = Resumen(cuerpo); }
        return new SunatException($"{que} ({(int)http}{(codigo is null ? "" : " " + codigo)}): {mensaje ?? Resumen(cuerpo)}", (int)http, codigo, reintentable: (int)http >= 500);
    }

    private static string Resumen(string s) => s.Length <= 300 ? s : s[..300] + "…";

    private static readonly JsonSerializerOptions Opciones = new() { PropertyNameCaseInsensitive = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    // ── Los cuerpos de la API de SUNAT (los nombres son los de su especificación) ───────────

    private sealed class TokenRespuesta
    {
        [JsonPropertyName("access_token")] public string? AccessToken { get; set; }
        [JsonPropertyName("token_type")] public string? TipoToken { get; set; }
        [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }
    }
    private sealed class EnvioCuerpo { [JsonPropertyName("archivo")] public ArchivoCuerpo Archivo { get; set; } = new(); }
    private sealed class ArchivoCuerpo
    {
        [JsonPropertyName("nomArchivo")] public string NombreArchivo { get; set; } = "";
        [JsonPropertyName("arcGreZip")] public string ZipBase64 { get; set; } = "";
        [JsonPropertyName("hashZip")] public string Hash { get; set; } = "";
    }
    private sealed class EnvioRespuesta
    {
        [JsonPropertyName("numTicket")] public string? Ticket { get; set; }
        [JsonPropertyName("fecRecepcion")] public DateTime? FechaRecepcion { get; set; }
    }
    private sealed class ConsultaRespuesta
    {
        [JsonPropertyName("codRespuesta")] public string? Codigo { get; set; }
        [JsonPropertyName("error")] public ErrorTicket? Error { get; set; }
        [JsonPropertyName("arcCdr")] public string? CdrBase64 { get; set; }
        [JsonPropertyName("indCdrGenerado")] public string? CdrGenerado { get; set; }
    }
    private sealed class ErrorTicket
    {
        [JsonPropertyName("numError")] public string? Numero { get; set; }
        [JsonPropertyName("desError")] public string? Descripcion { get; set; }
    }
    private sealed class ErrorRespuesta
    {
        [JsonPropertyName("cod")] public string? Codigo { get; set; }
        [JsonPropertyName("msg")] public string? Mensaje { get; set; }
        [JsonPropertyName("errors")] public List<ErrorRespuesta>? Errores { get; set; }
    }
}
