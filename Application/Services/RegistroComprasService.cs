using System.Text;
using System.Text.RegularExpressions;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class RegistroComprasService : IRegistroComprasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(RegistroComprasService);
    // una sola importación de la carpeta a la vez: dos corridas juntas leerían el mismo archivo
    private static readonly SemaphoreSlim _importando = new(1, 1);

    private readonly IRegistroComprasRepository _repo;
    private readonly IPathFilesRepository _paths;
    private readonly ErrorLogService _log;

    static RegistroComprasService() => Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

    public RegistroComprasService(IRegistroComprasRepository repo, IPathFilesRepository paths, ErrorLogService log)
    { _repo = repo; _paths = paths; _log = log; }

    private async Task<ApiResponse<IEnumerable<T>>> Lista<T>(Func<Task<IEnumerable<T>>> consulta, string metodo)
    {
        try { var d = (await consulta()).ToList();
              return ApiResponse<IEnumerable<T>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo);
              return ApiResponse<IEnumerable<T>>.Fail(ex.Message); }
    }

    private async Task<ApiResponse<WriteResult>> Escribe(Func<Task<WriteResult>> accion, string ok, string metodo)
    {
        try { var r = await accion(); return ApiResponse<WriteResult>.FromWriteResult(r, ok); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo);
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    // ───────── cabecera ─────────

    public Task<ApiResponse<IEnumerable<RegistroCompra>>> SearchAsync(RegistroCompraSearchDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.COD_CLASE_OPERACION))
            return Task.FromResult(ApiResponse<IEnumerable<RegistroCompra>>.Fail("Falta la clase de operación: la fija el parámetro de la opción de menú (COD_CLASE_OPERACION = '…')."));
        return Lista(() => _repo.SearchAsync(dto), nameof(SearchAsync));
    }

    public async Task<ApiResponse<RegistroCompra>> GetByIdAsync(decimal NUM_REGISTRO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_REGISTRO);
              return d is null ? ApiResponse<RegistroCompra>.Fail("No encontrado.") : ApiResponse<RegistroCompra>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<RegistroCompra>.Fail(ex.Message); }
    }

    /// <summary>Lo que había que cuidar al grabar la cabecera: que vengan los datos, que el tipo de operación exista en la clase, el número del comprobante en mayúsculas y sin espacios, y que se guarde solo lo que
    /// el tipo de operación habilita (centro de costo, proyecto, lote de control e incoterm). Devuelve el error o nulo. Lo demás (proveedor del tipo que pide la operación, comprobante permitido, duplicado) lo dice la
    /// validación o el procedimiento estándar.</summary>
    internal async Task<string?> PreparaAsync(string codClase, RegistroCompraSaveDto dto)
    {
        if (string.IsNullOrWhiteSpace(codClase)) return "Falta la clase de operación del registro de compras: la fija el parámetro de la opción de menú.";
        if (string.IsNullOrWhiteSpace(dto.COD_TIPO_OPERACION)) return "Elige el tipo de operación.";
        var tipo = await _repo.TipoOperacionAsync(codClase.Trim(), dto.COD_TIPO_OPERACION.Trim());
        if (tipo is null) return $"El tipo de operación {dto.COD_TIPO_OPERACION.Trim()} no existe en la clase {codClase.Trim()}.";
        if (dto.COD_ENTIDAD is null or <= 0) return "Elige el proveedor.";
        if (string.IsNullOrWhiteSpace(dto.COD_TIPO_COMPROBANTE_PAGO)) return "Elige el tipo de comprobante.";
        dto.NUM_COMPROBANTE_PAGO = (dto.NUM_COMPROBANTE_PAGO ?? "").Trim().ToUpperInvariant();
        if (dto.NUM_COMPROBANTE_PAGO.Length == 0) return "Falta el número del comprobante (serie y correlativo).";
        if (dto.FEC_COMPROBANTE_PAGO is null) return "Falta la fecha del comprobante.";
        if (!tipo.FLG_CENTRO_COSTO) { dto.COD_CENTRO_COSTO_SOLICITANTE = null; dto.COD_CENTRO_COSTO_DESTINO = null; }
        if (!tipo.FLG_PROYECTO) dto.NUM_PROYECTO = null;
        if (!tipo.FLG_LOTE_CONTROL) dto.NUM_LOTE_CONTROL = null;
        if (!tipo.FLG_COMERCIO_EXTERIOR) dto.COD_INCOTERM = null;
        return null;
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(RegistroCompraInsertDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var error = await PreparaAsync(dto.COD_CLASE_OPERACION, dto);
            if (error is not null) return ApiResponse<WriteResult>.Fail(error);
            return ApiResponse<WriteResult>.FromWriteResult(await _repo.InsertAsync(dto, codUsuario, codEstacion), "Comprobante registrado.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(RegistroCompraUpdateDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var actual = await _repo.ShowbyidAsync(dto.NUM_REGISTRO);
            if (actual is null) return ApiResponse<WriteResult>.Fail("El comprobante no existe.");
            var error = await PreparaAsync(actual.COD_CLASE_OPERACION, dto);
            if (error is not null) return ApiResponse<WriteResult>.Fail(error);
            return ApiResponse<WriteResult>.FromWriteResult(await _repo.UpdateAsync(dto, codUsuario, codEstacion), "Comprobante modificado.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_REGISTRO)
    {
        var r = await Escribe(() => _repo.DeleteAsync(NUM_REGISTRO), "Comprobante eliminado.", nameof(DeleteAsync));
        if (r.Success) { try { var (carpeta, _) = await CarpetaPdfAsync(); if (carpeta is not null) { var f = RutaPdf(carpeta, NUM_REGISTRO); if (File.Exists(f)) File.Delete(f); } } catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync)); } }
        return r;
    }

    // ───────── PDF del comprobante ─────────
    // Un PDF por comprobante, en la carpeta de Compras de Configuración general (la misma de los XML), dentro de la subcarpeta «pdf» y con el número de registro como nombre
    // («pdf/123.pdf»): el nombre original no manda en el disco (no choca ni se presta a rutas raras) y se guarda en la cabecera, en DES_NOMBRE_ARCHIVO_PDF.
    private const string SubcarpetaPdf = "pdf";
    private const long MaxPdf = 20L * 1024 * 1024;
    private static string RutaPdf(string carpeta, decimal numRegistro) => Path.Combine(carpeta, SubcarpetaPdf, numRegistro.ToString("0", System.Globalization.CultureInfo.InvariantCulture) + ".pdf");

    /// <summary>La carpeta de Compras configurada, o el motivo por el que no se puede usar.</summary>
    private async Task<(string? Carpeta, string? Error)> CarpetaPdfAsync()
    {
        var cfg = await _repo.ConfigXmlAsync();
        var clave = cfg?.COD_KEY_PATHFILE_COMPRAS_XML;
        if (string.IsNullOrWhiteSpace(clave)) return (null, "No hay una carpeta de Compras configurada: elígela en Configuración general, pestaña Compras.");
        var ruta = await _paths.ShowbyidAsync(clave);
        if (ruta is null || string.IsNullOrWhiteSpace(ruta.DES_PATH_FILE)) return (null, $"La ruta «{clave}» no existe en Rutas de archivos o no tiene carpeta.");
        var carpeta = ruta.DES_PATH_FILE!.Trim();
        if (!Directory.Exists(carpeta)) return (null, $"La carpeta de Compras «{carpeta}» no existe o la API no puede verla.");
        return (carpeta, null);
    }

    public async Task<ApiResponse<bool>> PdfGuardarAsync(decimal NUM_REGISTRO, string? nombreArchivo, Stream contenido, long largo)
    {
        try
        {
            var f = await _repo.ShowbyidAsync(NUM_REGISTRO);
            if (f is null) return ApiResponse<bool>.Fail("No encontrado.");
            if (f.FLG_EDITABLE != true) return ApiResponse<bool>.Fail($"El comprobante está {(f.DES_ESTADO_CONCILIACION ?? f.COD_ESTADO_ALIAS_CONCILIACION).ToLowerInvariant()} y ya no se puede modificar: no se le puede cambiar el PDF.");
            if (!string.Equals(Path.GetExtension(nombreArchivo ?? ""), ".pdf", StringComparison.OrdinalIgnoreCase)) return ApiResponse<bool>.Fail("Solo se permiten archivos PDF.");
            if (largo <= 0) return ApiResponse<bool>.Fail("El archivo está vacío.");
            if (largo > MaxPdf) return ApiResponse<bool>.Fail("El PDF pesa más de 20 MB.");
            var (carpeta, error) = await CarpetaPdfAsync();
            if (carpeta is null) return ApiResponse<bool>.Fail(error!);

            using var ms = new MemoryStream();
            await contenido.CopyToAsync(ms);
            var bytes = ms.ToArray();
            if (bytes.Length < 5 || bytes[0] != '%' || bytes[1] != 'P' || bytes[2] != 'D' || bytes[3] != 'F' || bytes[4] != '-') return ApiResponse<bool>.Fail("El archivo no es un PDF válido.");

            var destino = RutaPdf(carpeta, NUM_REGISTRO);
            Directory.CreateDirectory(Path.GetDirectoryName(destino)!);
            var temporal = destino + ".tmp";
            await File.WriteAllBytesAsync(temporal, bytes);
            File.Move(temporal, destino, true);
            return ApiResponse<bool>.Ok(true, "PDF guardado.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(PdfGuardarAsync)); return ApiResponse<bool>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RegistroCompraPdf>> PdfLeerAsync(decimal NUM_REGISTRO)
    {
        try
        {
            var f = await _repo.ShowbyidAsync(NUM_REGISTRO);
            if (f is null) return ApiResponse<RegistroCompraPdf>.Fail("No encontrado.");
            var (carpeta, error) = await CarpetaPdfAsync();
            if (carpeta is null) return ApiResponse<RegistroCompraPdf>.Fail(error!);
            var ruta = RutaPdf(carpeta, NUM_REGISTRO);
            if (!File.Exists(ruta)) return ApiResponse<RegistroCompraPdf>.Fail("Este comprobante no tiene un PDF guardado en el servidor.");
            var nombre = string.IsNullOrWhiteSpace(f.DES_NOMBRE_ARCHIVO_PDF) ? Path.GetFileName(ruta) : Path.GetFileName(f.DES_NOMBRE_ARCHIVO_PDF.Trim());
            return ApiResponse<RegistroCompraPdf>.Ok(new RegistroCompraPdf { NOMBRE = nombre, CONTENIDO = await File.ReadAllBytesAsync(ruta) });
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(PdfLeerAsync)); return ApiResponse<RegistroCompraPdf>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<bool>> PdfQuitarAsync(decimal NUM_REGISTRO)
    {
        try
        {
            var f = await _repo.ShowbyidAsync(NUM_REGISTRO);
            if (f is null) return ApiResponse<bool>.Fail("No encontrado.");
            if (f.FLG_EDITABLE != true) return ApiResponse<bool>.Fail($"El comprobante está {(f.DES_ESTADO_CONCILIACION ?? f.COD_ESTADO_ALIAS_CONCILIACION).ToLowerInvariant()} y ya no se puede modificar: no se le puede quitar el PDF.");
            var (carpeta, error) = await CarpetaPdfAsync();
            if (carpeta is null) return ApiResponse<bool>.Fail(error!);
            var ruta = RutaPdf(carpeta, NUM_REGISTRO);
            if (File.Exists(ruta)) File.Delete(ruta);
            return ApiResponse<bool>.Ok(true, "PDF quitado.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(PdfQuitarAsync)); return ApiResponse<bool>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RegistroCompraCombos>> LoadcombosAsync(string codClaseOperacion)
    {
        if (string.IsNullOrWhiteSpace(codClaseOperacion))
            return ApiResponse<RegistroCompraCombos>.Fail("Falta la clase de operación.");
        try { return ApiResponse<RegistroCompraCombos>.Ok(await _repo.LoadcombosAsync(codClaseOperacion.Trim())); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<RegistroCompraCombos>.Fail(ex.Message); }
    }

    public Task<ApiResponse<IEnumerable<RegistroCompraCalculo>>> CalculoAsync(decimal NUM_REGISTRO) => Lista(() => _repo.CalculoAsync(NUM_REGISTRO), nameof(CalculoAsync));

    // ───────── líneas ─────────

    public Task<ApiResponse<IEnumerable<RegistroCompraLinea>>> LineasAsync(decimal NUM_REGISTRO) => Lista(() => _repo.LineasAsync(NUM_REGISTRO), nameof(LineasAsync));

    public Task<ApiResponse<WriteResult>> LineaGuardarAsync(decimal NUM_REGISTRO, RegistroCompraLineaDto dto, string codUsuario, string codEstacion)
        => Escribe(() => _repo.LineaGuardarAsync(NUM_REGISTRO, dto, codUsuario, codEstacion), "Línea guardada.", nameof(LineaGuardarAsync));

    public Task<ApiResponse<WriteResult>> LineaDeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA)
        => Escribe(() => _repo.LineaDeleteAsync(NUM_REGISTRO, NUM_SECUENCIA), "Línea eliminada.", nameof(LineaDeleteAsync));

    // ───────── órdenes ─────────

    public Task<ApiResponse<IEnumerable<RegistroCompraPedido>>> PedidosAsync(decimal NUM_REGISTRO) => Lista(() => _repo.PedidosAsync(NUM_REGISTRO), nameof(PedidosAsync));
    public Task<ApiResponse<IEnumerable<RegistroCompraPedido>>> PedidosDisponiblesAsync(decimal NUM_REGISTRO) => Lista(() => _repo.PedidosDisponiblesAsync(NUM_REGISTRO), nameof(PedidosDisponiblesAsync));
    public Task<ApiResponse<IEnumerable<RegistroCompraPedidoLinea>>> PedidosLineasAsync(decimal NUM_REGISTRO) => Lista(() => _repo.PedidosLineasAsync(NUM_REGISTRO), nameof(PedidosLineasAsync));

    public Task<ApiResponse<WriteResult>> PedidoAsociarAsync(decimal NUM_REGISTRO, RegistroCompraPedidoAsociarDto dto, string codUsuario, string codEstacion)
        => Escribe(() => _repo.PedidoAsociarAsync(NUM_REGISTRO, dto, codUsuario, codEstacion), "Orden asociada.", nameof(PedidoAsociarAsync));

    public Task<ApiResponse<WriteResult>> PedidoDesasociarAsync(decimal NUM_REGISTRO, string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
        => Escribe(() => _repo.PedidoDesasociarAsync(NUM_REGISTRO, codClaseOperacion, codTipoOperacion, numPedidoComercial), "Orden quitada.", nameof(PedidoDesasociarAsync));

    // Notas de credito/debito por item
    public Task<ApiResponse<IEnumerable<RegistroCompraIncidenciaOrigen>>> IncidenciasOrigenesAsync(decimal NUM_REGISTRO, string? search) => Lista(() => _repo.IncidenciasOrigenesAsync(NUM_REGISTRO, search), nameof(IncidenciasOrigenesAsync));
    public Task<ApiResponse<IEnumerable<RegistroCompraIncidenciaLinea>>> IncidenciasDisponiblesAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ORIGEN) => Lista(() => _repo.IncidenciasDisponiblesAsync(NUM_REGISTRO, NUM_REGISTRO_ORIGEN), nameof(IncidenciasDisponiblesAsync));
    public Task<ApiResponse<IEnumerable<RegistroCompraIncidencia>>> IncidenciasAsync(decimal NUM_REGISTRO) => Lista(() => _repo.IncidenciasAsync(NUM_REGISTRO), nameof(IncidenciasAsync));
    public Task<ApiResponse<WriteResult>> IncidenciasGuardarAsync(decimal NUM_REGISTRO, RegistroCompraIncidenciasGuardarDto dto, string codUsuario, string codEstacion)
        => Escribe(() => _repo.IncidenciasGuardarAsync(NUM_REGISTRO, dto, codUsuario, codEstacion), "Asociación guardada.", nameof(IncidenciasGuardarAsync));
    public Task<ApiResponse<WriteResult>> IncidenciasAnulacionTotalAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ORIGEN, string codUsuario, string codEstacion)
        => Escribe(() => _repo.IncidenciasAnulacionTotalAsync(NUM_REGISTRO, NUM_REGISTRO_ORIGEN, codUsuario, codEstacion), "Factura anulada por completo: se copiaron y asociaron todas sus líneas.", nameof(IncidenciasAnulacionTotalAsync));
    public Task<ApiResponse<WriteResult>> IncidenciasAdicionarAsync(decimal NUM_REGISTRO, RegistroCompraIncidenciaAdicionarDto dto, string codUsuario, string codEstacion)
        => Escribe(() => _repo.IncidenciasAdicionarAsync(NUM_REGISTRO, dto, codUsuario, codEstacion), "Líneas adicionadas al comprobante.", nameof(IncidenciasAdicionarAsync));
    public Task<ApiResponse<IEnumerable<RegistroCompraIncidenciaGuia>>> IncidenciasGuiasDisponiblesAsync(decimal NUM_REGISTRO) => Lista(() => _repo.IncidenciasGuiasDisponiblesAsync(NUM_REGISTRO), nameof(IncidenciasGuiasDisponiblesAsync));
    public Task<ApiResponse<IEnumerable<RegistroCompraIncidenciaGuiaAdjunta>>> IncidenciasGuiasAsync(decimal NUM_REGISTRO) => Lista(() => _repo.IncidenciasGuiasAsync(NUM_REGISTRO), nameof(IncidenciasGuiasAsync));
    public async Task<ApiResponse<RegistroCompraIncidenciaLineaGuia?>> IncidenciasLineaGuiaAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA)
    {
        try { return ApiResponse<RegistroCompraIncidenciaLineaGuia?>.Ok(await _repo.IncidenciasLineaGuiaAsync(NUM_REGISTRO, NUM_SECUENCIA)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(IncidenciasLineaGuiaAsync));
              return ApiResponse<RegistroCompraIncidenciaLineaGuia?>.Fail(ex.Message); }
    }
    public Task<ApiResponse<WriteResult>> IncidenciasGuiaAsociarAsync(decimal NUM_REGISTRO, decimal NUM_MOVIMIENTO_ALMACEN, string codUsuario, string codEstacion)
        => Escribe(() => _repo.IncidenciasGuiaAsociarAsync(NUM_REGISTRO, NUM_MOVIMIENTO_ALMACEN, codUsuario, codEstacion), "Guía de devolución adjuntada.", nameof(IncidenciasGuiaAsociarAsync));
    public Task<ApiResponse<IEnumerable<RegistroCompraAnticipo>>> AnticiposDisponiblesAsync(decimal NUM_REGISTRO) => Lista(() => _repo.AnticiposDisponiblesAsync(NUM_REGISTRO), nameof(AnticiposDisponiblesAsync));
    public Task<ApiResponse<IEnumerable<RegistroCompraAnticipo>>> AnticiposAsync(decimal NUM_REGISTRO) => Lista(() => _repo.AnticiposAsync(NUM_REGISTRO), nameof(AnticiposAsync));
    public Task<ApiResponse<WriteResult>> AnticipoAsociarAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ANTICIPO, string codUsuario, string codEstacion)
        => Escribe(() => _repo.AnticipoAsociarAsync(NUM_REGISTRO, NUM_REGISTRO_ANTICIPO, codUsuario, codEstacion), "Anticipo aplicado a la factura.", nameof(AnticipoAsociarAsync));
    public Task<ApiResponse<WriteResult>> AnticipoQuitarAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ANTICIPO)
        => Escribe(() => _repo.AnticipoQuitarAsync(NUM_REGISTRO, NUM_REGISTRO_ANTICIPO), "Anticipo quitado de la factura.", nameof(AnticipoQuitarAsync));
    public Task<ApiResponse<WriteResult>> IncidenciasGuiaQuitarAsync(decimal NUM_REGISTRO, decimal NUM_MOVIMIENTO_ALMACEN)
        => Escribe(() => _repo.IncidenciasGuiaQuitarAsync(NUM_REGISTRO, NUM_MOVIMIENTO_ALMACEN), "Guía de devolución quitada.", nameof(IncidenciasGuiaQuitarAsync));

    public async Task<ApiResponse<WriteResult>> AutovincularAsync(decimal NUM_REGISTRO)
    {
        try
        {
            var r = await _repo.AutovincularAsync(NUM_REGISTRO);
            return ApiResponse<WriteResult>.FromWriteResult(r, r.AffectedRows == 0
                ? "No hubo líneas que vincular: revisa que tengan producto y que las órdenes asociadas lo traigan."
                : $"{r.AffectedRows} línea(s) vinculada(s) a las órdenes.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(AutovincularAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    // ───────── conciliación y flujo ─────────

    public Task<ApiResponse<IEnumerable<RegistroCompraHallazgo>>> ConciliacionAsync(decimal NUM_REGISTRO) => Lista(() => _repo.ConciliacionAsync(NUM_REGISTRO), nameof(ConciliacionAsync));

    public async Task<ApiResponse<RegistroCompraValidacion>> ValidarAsync(decimal NUM_REGISTRO, string codUsuario, string codEstacion)
    {
        try
        {
            var (r, v) = await _repo.ValidarAsync(NUM_REGISTRO, codUsuario, codEstacion);
            if (r.IsError || v is null)
            {
                WriteResult.OnSpError?.Invoke(r.Mensaje);
                return ApiResponse<RegistroCompraValidacion>.Fail(r.Mensaje);
            }
            return ApiResponse<RegistroCompraValidacion>.Ok(v, total: v.HALLAZGOS.Count);
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ValidarAsync));
              return ApiResponse<RegistroCompraValidacion>.Fail(ex.Message); }
    }

    public Task<ApiResponse<WriteResult>> EnviarAprobacionAsync(decimal NUM_REGISTRO, RegistroCompraJustificacionDto dto, string codUsuario, string codEstacion)
        => Escribe(() => _repo.EnviarAprobacionAsync(NUM_REGISTRO, dto?.DES_JUSTIFICACION, codUsuario, codEstacion), "Comprobante enviado a aprobación.", nameof(EnviarAprobacionAsync));

    public Task<ApiResponse<WriteResult>> AprobarAsync(decimal NUM_REGISTRO, RegistroCompraJustificacionDto dto, string codUsuario, string codEstacion)
        => Escribe(() => _repo.AprobarAsync(NUM_REGISTRO, dto?.DES_JUSTIFICACION, codUsuario, codEstacion), "Comprobante aprobado.", nameof(AprobarAsync));

    public Task<ApiResponse<WriteResult>> ContabilizarAsync(decimal NUM_REGISTRO, string codUsuario, string codEstacion)
        => Escribe(() => _repo.ContabilizarAsync(NUM_REGISTRO, codUsuario, codEstacion), "Comprobante contabilizado: su deuda quedó en el cronograma.", nameof(ContabilizarAsync));

    public Task<ApiResponse<WriteResult>> AnularAsync(decimal NUM_REGISTRO, RegistroCompraJustificacionDto dto, string codUsuario, string codEstacion)
        => Escribe(() => _repo.AnularAsync(NUM_REGISTRO, dto?.DES_JUSTIFICACION, codUsuario, codEstacion), "Comprobante anulado.", nameof(AnularAsync));

    // ───────── XML ─────────

    public async Task<ApiResponse<RegistroCompraXml>> XmlAsync(decimal NUM_REGISTRO)
    {
        try { var d = await _repo.XmlAsync(NUM_REGISTRO);
              return d is null ? ApiResponse<RegistroCompraXml>.Fail("El comprobante no tiene XML.") : ApiResponse<RegistroCompraXml>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(XmlAsync));
              return ApiResponse<RegistroCompraXml>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RegistroCompraImportado>> ImportarXmlAsync(RegistroCompraImportarXmlDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var (r, i) = await _repo.ImportarXmlAsync(dto, codUsuario, codEstacion);
            if (r.IsError || i is null)
            {
                WriteResult.OnSpError?.Invoke(r.Mensaje);
                return ApiResponse<RegistroCompraImportado>.Fail(r.Mensaje);
            }
            return ApiResponse<RegistroCompraImportado>.Ok(i);
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ImportarXmlAsync));
              return ApiResponse<RegistroCompraImportado>.Fail(ex.Message); }
    }

    /// <summary>El texto de un archivo XML con la codificación que declara (los de la SUNAT suelen ser ISO-8859-1 o UTF-8; con BOM, la del BOM).</summary>
    internal static string LeeTexto(byte[] bytes)
    {
        if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF) return Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);
        if (bytes.Length >= 2 && bytes[0] == 0xFF && bytes[1] == 0xFE) return Encoding.Unicode.GetString(bytes, 2, bytes.Length - 2);
        if (bytes.Length >= 2 && bytes[0] == 0xFE && bytes[1] == 0xFF) return Encoding.BigEndianUnicode.GetString(bytes, 2, bytes.Length - 2);
        var cabecera = Encoding.ASCII.GetString(bytes, 0, Math.Min(bytes.Length, 200));
        var m = Regex.Match(cabecera, "<\\?xml[^>]*encoding\\s*=\\s*[\"']([^\"']+)[\"']", RegexOptions.IgnoreCase);
        if (m.Success)
        {
            try { return Encoding.GetEncoding(m.Groups[1].Value).GetString(bytes); }
            catch (ArgumentException) { /* codificación desconocida: se prueba como UTF-8 */ }
        }
        return new UTF8Encoding(false, false).GetString(bytes);
    }

    private static bool NombreSimple(string n) => !string.IsNullOrWhiteSpace(n) && n == Path.GetFileName(n) && n != "." && n != "..";

    private static string Libre(string carpeta, string nombre)
    {
        var destino = Path.Combine(carpeta, nombre);
        if (!File.Exists(destino)) return destino;
        var baseNombre = Path.GetFileNameWithoutExtension(nombre);
        var ext = Path.GetExtension(nombre);
        return Path.Combine(carpeta, $"{baseNombre}_{DateTime.Now:yyyyMMddHHmmssfff}{ext}");
    }

    public async Task<ApiResponse<RegistroCompraImportarCarpetaResult>> ImportarCarpetaAsync(RegistroCompraImportarCarpetaDto dto, string codUsuario, string codEstacion)
    {
        if (string.IsNullOrWhiteSpace(dto.COD_CLASE_OPERACION))
            return ApiResponse<RegistroCompraImportarCarpetaResult>.Fail("Falta la clase de operación: la fija el parámetro de la opción de menú.");
        if (!await _importando.WaitAsync(0))
            return ApiResponse<RegistroCompraImportarCarpetaResult>.Fail("Ya hay una importación de XML en curso: espera a que termine.");
        try
        {
            var cfg = await _repo.ConfigXmlAsync();
            var clave = cfg?.COD_KEY_PATHFILE_COMPRAS_XML;
            if (string.IsNullOrWhiteSpace(clave))
                return ApiResponse<RegistroCompraImportarCarpetaResult>.Fail("No hay una ruta configurada para los XML: elígela en Configuración general, pestaña Compras.");
            var ruta = await _paths.ShowbyidAsync(clave);
            if (ruta is null || string.IsNullOrWhiteSpace(ruta.DES_PATH_FILE))
                return ApiResponse<RegistroCompraImportarCarpetaResult>.Fail($"La ruta «{clave}» no existe en Rutas de archivos o no tiene carpeta.");
            var carpeta = ruta.DES_PATH_FILE!.Trim();
            if (!Directory.Exists(carpeta))
                return ApiResponse<RegistroCompraImportarCarpetaResult>.Fail($"La carpeta de XML «{carpeta}» no existe o la API no puede verla.");

            var procesados = string.IsNullOrWhiteSpace(cfg!.DES_CARPETA_COMPRAS_PROCESADOS) ? "procesados" : cfg.DES_CARPETA_COMPRAS_PROCESADOS!.Trim();
            var errores = string.IsNullOrWhiteSpace(cfg.DES_CARPETA_COMPRAS_ERRORES) ? "errores" : cfg.DES_CARPETA_COMPRAS_ERRORES!.Trim();
            if (!NombreSimple(procesados) || !NombreSimple(errores))
                return ApiResponse<RegistroCompraImportarCarpetaResult>.Fail("Las subcarpetas «procesados» y «errores» de la configuración tienen que ser un nombre de carpeta, sin rutas.");
            if (string.Equals(procesados, errores, StringComparison.OrdinalIgnoreCase))
                return ApiResponse<RegistroCompraImportarCarpetaResult>.Fail("Las subcarpetas «procesados» y «errores» tienen que ser distintas.");
            var dirProcesados = Path.Combine(carpeta, procesados);
            var dirErrores = Path.Combine(carpeta, errores);
            var tipo = string.IsNullOrWhiteSpace(dto.COD_TIPO_OPERACION) ? null : dto.COD_TIPO_OPERACION.Trim();

            var res = new RegistroCompraImportarCarpetaResult { CARPETA = carpeta };
            var archivos = Directory.EnumerateFiles(carpeta, "*.xml", SearchOption.TopDirectoryOnly).OrderBy(f => f, StringComparer.OrdinalIgnoreCase).ToList();
            res.NUM_ARCHIVOS = archivos.Count;
            foreach (var archivo in archivos)
            {
                var nombre = Path.GetFileName(archivo);
                var fila = new RegistroCompraArchivoImportado { ARCHIVO = nombre };
                try
                {
                    var texto = LeeTexto(await File.ReadAllBytesAsync(archivo));
                    var (r, i) = await _repo.ImportarXmlAsync(new RegistroCompraImportarXmlDto
                    {
                        COD_CLASE_OPERACION = dto.COD_CLASE_OPERACION.Trim(), DES_XML = texto, DES_NOMBRE_ARCHIVO = nombre, COD_TIPO_OPERACION = tipo, VALIDAR = dto.VALIDAR
                    }, codUsuario, codEstacion);
                    if (!r.IsError && i is not null)
                    {
                        fila.IMPORTADO = true; fila.NUM_REGISTRO = i.NUM_REGISTRO; fila.COD_ESTADO_ALIAS_CONCILIACION = i.COD_ESTADO_ALIAS_CONCILIACION;
                        fila.MENSAJE = $"{i.NUM_LINEAS} línea(s), {i.NUM_PEDIDOS_ASOCIADOS} orden(es) asociada(s), {i.NUM_LINEAS_VINCULADAS} línea(s) vinculada(s).";
                        Directory.CreateDirectory(dirProcesados);
                        File.Move(archivo, Libre(dirProcesados, nombre));
                        res.NUM_IMPORTADOS++;
                    }
                    else
                    {
                        fila.MENSAJE = r.Mensaje;
                        MueveConError(archivo, nombre, dirErrores, r.Mensaje);
                        res.NUM_ERRORES++;
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex, Programa, Clase, nameof(ImportarCarpetaAsync));
                    fila.IMPORTADO = false; fila.MENSAJE = ex.Message;
                    try { MueveConError(archivo, nombre, dirErrores, ex.Message); } catch (Exception ex2) { fila.MENSAJE += " (además no se pudo mover a «errores»: " + ex2.Message + ")"; }
                    res.NUM_ERRORES++;
                }
                res.ARCHIVOS.Add(fila);
            }
            return ApiResponse<RegistroCompraImportarCarpetaResult>.Ok(res,
                message: archivos.Count == 0 ? "No hay XML en la carpeta." : $"{res.NUM_IMPORTADOS} importado(s), {res.NUM_ERRORES} con error.");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(ImportarCarpetaAsync));
            return ApiResponse<RegistroCompraImportarCarpetaResult>.Fail(ex.Message);
        }
        finally { _importando.Release(); }
    }

    /// <summary>Mueve el archivo a «errores» y deja al lado un .txt con el motivo, para que quien lo revise no tenga que adivinar.</summary>
    private static void MueveConError(string archivo, string nombre, string dirErrores, string motivo)
    {
        Directory.CreateDirectory(dirErrores);
        var destino = Libre(dirErrores, nombre);
        File.Move(archivo, destino);
        File.WriteAllText(destino + ".error.txt", motivo ?? "", Encoding.UTF8);
    }
}
