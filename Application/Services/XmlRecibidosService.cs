using System.Net;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.XmlRecibidos;

namespace KonSolutionsApi.Application.Services;

/// <summary>Lo último que pasó con el servicio, para mostrarlo: se guarda en memoria (una sola copia para toda la API).</summary>
public class XmlRecibidosEstado
{
    private readonly object _l = new();
    private bool _enCurso; private DateTime? _ultima; private XmlRecibidosCiclo? _ultimo;
    public void Inicia() { lock (_l) _enCurso = true; }
    public void Termina(XmlRecibidosCiclo c) { lock (_l) { _enCurso = false; _ultima = c.Fin ?? DateTime.Now; if (!c.Omitida) _ultimo = c; } }
    public (bool EnCurso, DateTime? Ultima, XmlRecibidosCiclo? Ultimo) Leer() { lock (_l) return (_enCurso, _ultima, _ultimo); }
}

/// <summary>El servicio de XML recibidos. Cada corrida: lee la carpeta de recepción del buzón de Microsoft 365, baja los XML y PDF de cada correo (desempaquetando los ZIP), carga los XML con PROC_XML_RECIBIDOS_CARGAR,
/// guarda los archivos en la ruta de archivos (subcarpeta de procesados o de errores, con un .txt con el motivo al lado de los que fallaron) y mueve el correo: a la carpeta de procesados si se bajó y cargó todo,
/// a la de errores si algo no se pudo. Una falla pasajera (sin red, Microsoft 365 saturado, la base caída) no mueve nada: el correo se reintenta en la siguiente corrida. Los XML repetidos no son un error.</summary>
public class XmlRecibidosService : IXmlRecibidosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(XmlRecibidosService);
    private const string Usuario = "SERVICIO XML";
    // una sola corrida a la vez en toda la API (la programada y la manual no se pisan)
    private static readonly SemaphoreSlim _una = new(1, 1);

    private readonly IGraphCorreo _graph;
    private readonly IXmlRecibidosRepository _repo;
    private readonly IPathFilesRepository _paths;
    private readonly ErrorLogService _log;
    private readonly XmlRecibidosOpciones _o;
    private readonly XmlRecibidosEstado _estado;

    public XmlRecibidosService(IGraphCorreo graph, IXmlRecibidosRepository repo, IPathFilesRepository paths, ErrorLogService log, IOptions<XmlRecibidosOpciones> o, XmlRecibidosEstado estado)
    { _graph = graph; _repo = repo; _paths = paths; _log = log; _o = o.Value; _estado = estado; }

    public async Task<XmlRecibidosEstadoDto> EstadoAsync()
    {
        var (enCurso, ultima, ultimo) = _estado.Leer();
        ConfiguracionGeneral? cfg = null;
        try { cfg = await _repo.ConfigAsync(); } catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(EstadoAsync)); }
        return new XmlRecibidosEstadoDto
        {
            CuentaConfigurada = _graph.Configurado, Buzon = _graph.Buzon, Activo = cfg?.FLG_XML_RECIBIDOS_ACTIVO == true, Minutos = cfg?.CAN_MINUTOS_XML_RECIBIDOS ?? 15,
            EnCurso = enCurso, UltimaCorrida = ultima, Ultimo = ultimo
        };
    }

    private static bool NombreSimple(string n) => n.Length > 0 && !n.Contains("..") && n.IndexOfAny(new[] { '/', '\\', ':' }) < 0;

    public async Task<XmlRecibidosCiclo> EjecutarAsync(bool ignorarActivo, CancellationToken ct)
    {
        var ciclo = new XmlRecibidosCiclo();
        if (!await _una.WaitAsync(0, ct)) { ciclo.Mensaje = "Ya hay una corrida en curso: espera a que termine."; ciclo.Omitida = true; ciclo.Fin = DateTime.Now; return ciclo; }
        _estado.Inicia();
        try
        {
            await CorreAsync(ciclo, ignorarActivo, ct);
        }
        catch (OperationCanceledException) { ciclo.Mensaje ??= "La corrida se canceló."; }
        catch (GraphException gx) { ciclo.Mensaje = gx.Message; _log.LogError(gx, Programa, Clase, nameof(EjecutarAsync)); }
        catch (Exception ex) { ciclo.Mensaje = "La corrida se cortó: " + ex.Message; _log.LogError(ex, Programa, Clase, nameof(EjecutarAsync)); }
        finally
        {
            ciclo.Fin = DateTime.Now;
            _estado.Termina(ciclo);
            _una.Release();
        }
        return ciclo;
    }

    private async Task CorreAsync(XmlRecibidosCiclo ciclo, bool ignorarActivo, CancellationToken ct)
    {
        if (!_graph.Configurado) { ciclo.Mensaje = "Falta configurar la cuenta de Microsoft 365 en la configuración del servicio (sección XmlRecibidos de appsettings.json: TenantId, ClientId, el secreto y el Buzon)."; ciclo.Omitida = !ignorarActivo; return; }
        var cfg = await _repo.ConfigAsync();
        if (cfg is null) { ciclo.Mensaje = "No hay configuración general."; return; }
        if (!ignorarActivo && cfg.FLG_XML_RECIBIDOS_ACTIVO != true) { ciclo.Omitida = true; return; }

        // la configuración completa
        string? Falta(string? v, string nombre) => string.IsNullOrWhiteSpace(v) ? nombre : null;
        var faltan = new[] { Falta(cfg.COD_KEY_PATHFILE_COMPRAS_XML, "la ruta de archivos de Compras"), Falta(cfg.DES_CARPETA_CORREO_RECEPCION, "la carpeta de Outlook de recepción"),
                             Falta(cfg.DES_CARPETA_CORREO_PROCESADOS, "la carpeta de Outlook de procesados"), Falta(cfg.DES_CARPETA_CORREO_ERRORES, "la carpeta de Outlook de errores") }.Where(x => x is not null).ToList();
        if (faltan.Count > 0) { ciclo.Mensaje = "Falta en la configuración general (pestaña Compras): " + string.Join(", ", faltan) + "."; return; }
        var recepcionTxt = cfg.DES_CARPETA_CORREO_RECEPCION!.Trim(); var procesadosTxt = cfg.DES_CARPETA_CORREO_PROCESADOS!.Trim(); var erroresTxt = cfg.DES_CARPETA_CORREO_ERRORES!.Trim();
        if (new[] { recepcionTxt, procesadosTxt, erroresTxt }.Distinct(StringComparer.OrdinalIgnoreCase).Count() != 3) { ciclo.Mensaje = "Las tres carpetas de Outlook tienen que ser distintas."; return; }

        var clave = cfg.COD_KEY_PATHFILE_COMPRAS_XML!.Trim();
        var ruta = await _paths.ShowbyidAsync(clave);
        if (ruta is null || string.IsNullOrWhiteSpace(ruta.DES_PATH_FILE)) { ciclo.Mensaje = $"La ruta «{clave}» no existe en Rutas de archivos o no tiene carpeta."; return; }
        var raiz = ruta.DES_PATH_FILE!.Trim();
        if (!Directory.Exists(raiz)) { ciclo.Mensaje = $"La carpeta «{raiz}» no existe o la API no puede verla."; return; }
        var subProc = string.IsNullOrWhiteSpace(cfg.DES_CARPETA_COMPRAS_PROCESADOS) ? "procesados" : cfg.DES_CARPETA_COMPRAS_PROCESADOS!.Trim();
        var subErr = string.IsNullOrWhiteSpace(cfg.DES_CARPETA_COMPRAS_ERRORES) ? "errores" : cfg.DES_CARPETA_COMPRAS_ERRORES!.Trim();
        if (!NombreSimple(subProc) || !NombreSimple(subErr) || string.Equals(subProc, subErr, StringComparison.OrdinalIgnoreCase)) { ciclo.Mensaje = "Las subcarpetas de procesados y de errores tienen que ser nombres de carpeta distintos, sin rutas."; return; }
        var dirProc = Path.Combine(raiz, subProc); var dirErr = Path.Combine(raiz, subErr);

        // las tres carpetas del buzón: si falta una no se toca nada
        var idRecepcion = await _graph.ResolverCarpetaAsync(recepcionTxt, ct);
        var idProcesados = await _graph.ResolverCarpetaAsync(procesadosTxt, ct);
        var idErrores = await _graph.ResolverCarpetaAsync(erroresTxt, ct);
        var noExisten = new[] { (idRecepcion, recepcionTxt), (idProcesados, procesadosTxt), (idErrores, erroresTxt) }.Where(x => x.Item1 is null).Select(x => $"«{x.Item2}»").ToList();
        if (noExisten.Count > 0) { ciclo.Mensaje = $"No existe en el buzón {_graph.Buzon} la carpeta de Outlook " + string.Join(", ", noExisten) + "."; return; }

        Directory.CreateDirectory(dirProc); Directory.CreateDirectory(dirErr);
        var correos = await _graph.ListarCorreosAsync(idRecepcion!, Math.Max(1, _o.MaxCorreosPorCiclo), ct);
        ciclo.Correos = correos.Count;
        var transitoriosSeguidos = 0;
        foreach (var correo in correos)
        {
            ct.ThrowIfCancellationRequested();
            var res = new XmlRecibidosCorreoResultado { Id = correo.Id, Asunto = correo.Asunto, De = correo.De };
            ciclo.Detalle.Add(res);
            var cortar = false;
            try
            {
                var ok = await ProcesaCorreoAsync(correo, res, ciclo, dirProc, dirErr, ct);
                transitoriosSeguidos = 0;
                var destino = ok ? idProcesados! : idErrores!;
                res.Destino = ok ? "PROCESADOS" : "ERRORES";
                try { await _graph.MoverAsync(correo.Id, destino, ct); }
                catch (GraphException gx) when (!gx.Fatal)
                {
                    // el correo ya se cargo pero no se pudo mover: queda en recepcion y la siguiente corrida lo reconoce (los XML repetidos no son un error)
                    res.Destino = "SIN_MOVER"; res.Problemas.Add("No se pudo mover el correo a su carpeta (se reintenta en la siguiente corrida): " + gx.Message);
                    _log.LogError(gx, Programa, Clase, "MoverCorreo");
                    if (gx.Transitorio && ++transitoriosSeguidos >= 3) { ciclo.Mensaje = "Microsoft 365 falló tres veces seguidas; se corta la corrida y se reintenta en la siguiente. Último error: " + gx.Message; cortar = true; }
                }
            }
            catch (GraphException gx) when (gx.Transitorio && !gx.Fatal)
            {
                // una falla pasajera: el correo queda donde esta y se vuelve a intentar; tres seguidas = Microsoft 365 no esta bien, se corta la corrida
                res.Destino = "SIN_MOVER"; res.Problemas.Add("Falla pasajera con Microsoft 365 (se reintenta en la siguiente corrida): " + gx.Message);
                if (++transitoriosSeguidos >= 3) { ciclo.Mensaje = "Microsoft 365 falló tres veces seguidas; se corta la corrida y se reintenta en la siguiente. Último error: " + gx.Message; cortar = true; }
            }
            if (res.Destino == "PROCESADOS") ciclo.CorreosProcesados++;
            else if (res.Destino == "ERRORES") ciclo.CorreosConError++;
            else ciclo.CorreosSinMover++;
            if (cortar) break;
        }
    }

    private static string Mb(long bytes) => (bytes / 1048576.0).ToString("0.#", System.Globalization.CultureInfo.InvariantCulture);
    private static string Ext(string nombre) => Path.GetExtension(nombre).ToLowerInvariant();
    private static string BaseN(string nombre) => Path.GetFileNameWithoutExtension(nombre).ToLowerInvariant();

    /// <summary>Procesa un correo. Devuelve true si todo se bajó y cargó (va a procesados) y false si algo no se pudo (va a errores). Una falla pasajera sale como GraphException transitoria o como otra excepción: no se mueve nada.</summary>
    private async Task<bool> ProcesaCorreoAsync(CorreoResumen correo, XmlRecibidosCorreoResultado res, XmlRecibidosCiclo ciclo, string dirProc, string dirErr, CancellationToken ct)
    {
        var adjuntos = await _graph.ListarAdjuntosAsync(correo.Id, ct);
        var candidatos = adjuntos.Where(a => a.EsArchivo && !a.EsInline && (new[] { ".xml", ".pdf", ".zip" }.Contains(Ext(a.Nombre)))).ToList();
        if (candidatos.Count == 0)
        {
            res.Problemas.Add("El correo no trae ningún XML, PDF ni ZIP adjunto.");
            return string.Equals(_o.SinAdjuntosVaA, "Procesados", StringComparison.OrdinalIgnoreCase);
        }

        var archivos = new List<ArchivoRecibido>();
        var maxBytes = (long)_o.MaxAdjuntoMB * 1024 * 1024;
        foreach (var a in candidatos)
        {
            ct.ThrowIfCancellationRequested();
            var nombre = XmlTexto.NombreSeguro(a.Nombre);
            if (a.Tamano > maxBytes) { res.Problemas.Add($"«{nombre}» pesa {Mb(a.Tamano)} MB y el máximo es {_o.MaxAdjuntoMB} MB."); continue; }
            byte[] bytes;
            try { bytes = await _graph.DescargarAdjuntoAsync(correo.Id, a.Id, ct); }
            catch (GraphException gx) when (gx.Estado == HttpStatusCode.NotFound) { res.Problemas.Add($"«{nombre}» ya no está en el correo."); continue; }
            if (bytes.Length > maxBytes) { res.Problemas.Add($"«{nombre}» pesa {Mb(bytes.Length)} MB y el máximo es {_o.MaxAdjuntoMB} MB."); continue; }
            if (Ext(nombre) == ".zip")
            {
                try { archivos.AddRange(ZipSeguro.Extraer(bytes, nombre, (long)_o.MaxZipDescomprimidoMB * 1024 * 1024, _o.MaxArchivosPorZip)); }
                catch (ZipInvalidoException zx) { res.Problemas.Add(zx.Message); }
            }
            else archivos.Add(new ArchivoRecibido(nombre, bytes, null));
        }

        // los XML: cada uno se carga; el archivo va a procesados si entró y a errores (con un .txt con el motivo) si no
        var cargados = new List<(string Base, string Clase, decimal Num)>();
        var cdrs = new List<ArchivoRecibido>();
        foreach (var x in archivos.Where(f => Ext(f.Nombre) == ".xml"))
        {
            ct.ThrowIfCancellationRequested();
            res.Archivos.Add(x.Nombre);
            try
            {
                var texto = XmlTexto.Decodificar(x.Contenido);
                // la constancia de recepción (CDR) de SUNAT que muchos proveedores mandan junto a la factura: no es un comprobante ni un error; se guarda y ya
                if (XmlTexto.Raiz(texto) == "ApplicationResponse") { cdrs.Add(x); res.Avisos.Add($"«{x.Nombre}» es una constancia de recepción de SUNAT (CDR): se guarda pero no se carga."); continue; }
                var (clase, num, nuevo) = await _repo.CargarAsync(texto, x.Nombre, Usuario, Environment.MachineName);
                if (nuevo) ciclo.XmlCargados++; else ciclo.XmlRepetidos++;
                res.Documentos.Add($"{clase} {num}{(nuevo ? "" : " (ya estaba)")}");
                cargados.Add((BaseN(x.Nombre), clase, num));
                Guarda(dirProc, x);
            }
            catch (SqlException sx) when (sx.Number is >= 50700 and <= 50734)
            {
                res.Problemas.Add($"«{x.Nombre}»: {sx.Message}");
                Guarda(dirErr, x, sx.Message);
            }
            // cualquier otra falla (la base caída, sin conexión) sube: el correo no se mueve y se reintenta
        }

        // un correo que no dejó ningún comprobante ni guía (sin XML, solo PDF, o solo constancias de recepción) no se puede procesar: va a errores con sus archivos
        if (cargados.Count == 0 && res.Problemas.Count == 0)
            res.Problemas.Add(cdrs.Count > 0 ? "El correo solo trae constancias de recepción (CDR): ningún comprobante ni guía." : "El correo no trae ningún XML (ni suelto ni dentro de un ZIP).");

        // los PDF: el que acompaña a un XML cargado va junto a los procesados y se anota en el documento; el que no acompaña a nadie sigue la suerte del correo
        var pdfs = archivos.Where(f => Ext(f.Nombre) == ".pdf").ToList();
        var correoOk = res.Problemas.Count == 0;
        var distintos = cargados.Select(c => (c.Clase, c.Num)).Distinct().ToList();
        var usados = new HashSet<ArchivoRecibido>();
        foreach (var doc in distintos)
        {
            var delDoc = cargados.Where(c => c.Clase == doc.Clase && c.Num == doc.Num).Select(c => c.Base).ToList();
            var pdf = pdfs.FirstOrDefault(p => !usados.Contains(p) && delDoc.Contains(BaseN(p.Nombre)));
            if (pdf is null && distintos.Count == 1 && pdfs.Count == 1 && !usados.Contains(pdfs[0])) pdf = pdfs[0];
            if (pdf is null) continue;
            usados.Add(pdf);
            var nombreFinal = Guarda(dirProc, pdf);
            await _repo.AsignarPdfAsync(doc.Clase, doc.Num, Path.GetFileName(dirProc) + "/" + nombreFinal);
            ciclo.PdfGuardados++;
            res.Archivos.Add(pdf.Nombre);
        }
        foreach (var cdr in cdrs) Guarda(correoOk ? dirProc : dirErr, cdr);
        foreach (var p in pdfs.Where(p => !usados.Contains(p)))
        {
            Guarda(correoOk ? dirProc : dirErr, p);
            ciclo.PdfGuardados++;
            res.Archivos.Add(p.Nombre);
        }
        return correoOk;
    }

    /// <summary>Guarda el archivo en la carpeta. Si ya hay uno con ese nombre y es el mismo contenido, no lo repite; si es otro, le agrega (2), (3)... Devuelve el nombre con el que quedó. Con un motivo, deja al lado un .txt.</summary>
    private static string Guarda(string dir, ArchivoRecibido f, string? motivo = null)
    {
        Directory.CreateDirectory(dir);
        var baseN = Path.GetFileNameWithoutExtension(f.Nombre); var ext = Path.GetExtension(f.Nombre);
        var nombre = f.Nombre; var n = 1;
        while (true)
        {
            var destino = Path.Combine(dir, nombre);
            if (!File.Exists(destino)) { File.WriteAllBytes(destino, f.Contenido); break; }
            if (new FileInfo(destino).Length == f.Contenido.Length && SHA256.HashData(File.ReadAllBytes(destino)).AsSpan().SequenceEqual(SHA256.HashData(f.Contenido))) break;
            nombre = $"{baseN} ({++n}){ext}";
        }
        if (motivo is not null) File.WriteAllText(Path.Combine(dir, nombre + ".txt"), motivo + Environment.NewLine + "Origen: " + (f.Origen ?? "adjunto del correo") + Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        return nombre;
    }
}
