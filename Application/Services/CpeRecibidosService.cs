using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Sunat;

namespace KonSolutionsApi.Application.Services;

/// <summary>Los comprobantes recibidos de los proveedores: consultarlos, ponerles el producto de la empresa, descartarlos y generar con ellos el registro de compras (con el PDF que vino en el correo).
/// Un error de regla del servidor (SqlException con número de usuario, 50000 en adelante) vuelve con su mensaje y sin ir al log; cualquier otro error se anota en el log.</summary>
public class CpeRecibidosService : ICpeRecibidosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(CpeRecibidosService);
    private const long MaxPdf = 20L * 1024 * 1024;

    private readonly ICpeRecibidosRepository _repo;
    private readonly IRegistroComprasService _registro;
    private readonly XmlRecibidosArchivos _archivos;
    private readonly ErrorLogService _log;
    private readonly ISunatValidador _sunat;
    private readonly IConfiguracionGeneralRepository _config;

    public CpeRecibidosService(ICpeRecibidosRepository repo, IRegistroComprasService registro, XmlRecibidosArchivos archivos, ErrorLogService log, ISunatValidador sunat, IConfiguracionGeneralRepository config)
    { _repo = repo; _registro = registro; _archivos = archivos; _log = log; _sunat = sunat; _config = config; }

    private async Task<ApiResponse<T>> Corre<T>(Func<Task<ApiResponse<T>>> accion, string metodo)
    {
        try { return await accion(); }
        catch (SqlException ex) when (ex.Number >= 50000) { return ApiResponse<T>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    public Task<ApiResponse<IEnumerable<CpeRecibido>>> SearchAsync(CpeRecibidoSearchDto dto) => Corre(async () =>
    {
        var d = (await _repo.SearchAsync(dto)).ToList();
        return ApiResponse<IEnumerable<CpeRecibido>>.Ok(d, total: d.Count);
    }, nameof(SearchAsync));

    public Task<ApiResponse<CpeRecibidoFicha>> GetByIdAsync(decimal NUM_CPE) => Corre(async () =>
    {
        var d = await _repo.ShowbyidAsync(NUM_CPE);
        return d is null ? ApiResponse<CpeRecibidoFicha>.Fail("El comprobante recibido no existe.") : ApiResponse<CpeRecibidoFicha>.Ok(d);
    }, nameof(GetByIdAsync));

    public Task<ApiResponse<IEnumerable<CpeRecibidoLinea>>> LineasAsync(decimal NUM_CPE) => Corre(async () =>
    {
        var d = (await _repo.LineasAsync(NUM_CPE)).ToList();
        return ApiResponse<IEnumerable<CpeRecibidoLinea>>.Ok(d, total: d.Count);
    }, nameof(LineasAsync));

    public Task<ApiResponse<CpeRecibidoXml>> XmlAsync(decimal NUM_CPE) => Corre(async () =>
    {
        var d = await _repo.XmlAsync(NUM_CPE);
        return d is null ? ApiResponse<CpeRecibidoXml>.Fail("El comprobante recibido no existe.") : ApiResponse<CpeRecibidoXml>.Ok(d);
    }, nameof(XmlAsync));

    public Task<ApiResponse<CpeRecibidoTipoOperacion>> TipoOperacionAsync(decimal NUM_CPE, string? codClaseOperacion) => Corre(async () =>
    {
        var clase = string.IsNullOrWhiteSpace(codClaseOperacion) ? "CMPRS" : codClaseOperacion.Trim();
        var d = await _repo.TipoOperacionAsync(NUM_CPE, clase);
        return d is null ? ApiResponse<CpeRecibidoTipoOperacion>.Fail("El comprobante recibido no existe.") : ApiResponse<CpeRecibidoTipoOperacion>.Ok(d);
    }, nameof(TipoOperacionAsync));

    public Task<ApiResponse<RecibidoArchivo>> PdfAsync(decimal NUM_CPE) => Corre(async () =>
    {
        var f = await _repo.ShowbyidAsync(NUM_CPE);
        if (f is null) return ApiResponse<RecibidoArchivo>.Fail("El comprobante recibido no existe.");
        if (string.IsNullOrWhiteSpace(f.DES_NOMBRE_ARCHIVO_PDF)) return ApiResponse<RecibidoArchivo>.Fail("Este comprobante no trajo un PDF en el correo.");
        var (archivo, error) = await _archivos.LeerAsync(f.DES_NOMBRE_ARCHIVO_PDF, MaxPdf);
        return archivo is null ? ApiResponse<RecibidoArchivo>.Fail(error!) : ApiResponse<RecibidoArchivo>.Ok(archivo);
    }, nameof(PdfAsync));

    public Task<ApiResponse<bool>> AsignarProductoAsync(decimal NUM_CPE, int NUM_ITEM, RecibidoAsignarProductoDto dto, string codUsuario, string codEstacion) => Corre(async () =>
    {
        await _repo.AsignarProductoAsync(NUM_CPE, NUM_ITEM, dto.COD_PRODUCTO, codUsuario, codEstacion);
        return ApiResponse<bool>.Ok(true, string.IsNullOrWhiteSpace(dto.COD_PRODUCTO) ? "Producto quitado." : "Producto asignado.");
    }, nameof(AsignarProductoAsync));

    public Task<ApiResponse<bool>> DescartarAsync(decimal NUM_CPE, RecibidoDescartarDto dto, string codUsuario, string codEstacion) => Corre(async () =>
    {
        await _repo.DescartarAsync(NUM_CPE, dto.FLG_DESCARTAR, codUsuario, codEstacion);
        return ApiResponse<bool>.Ok(true, dto.FLG_DESCARTAR ? "Comprobante descartado." : "Comprobante devuelto a pendiente.");
    }, nameof(DescartarAsync));

    public Task<ApiResponse<RecibidoCompletarResult>> CompletarDatosAsync(decimal? NUM_DOCUMENTO, string codUsuario, string codEstacion) => Corre(async () =>
    {
        var r = await _repo.CompletarDatosAsync(NUM_DOCUMENTO, codUsuario, codEstacion);
        var partes = new List<string>();
        if (r.NUM_PROVEEDORES_NUEVOS > 0) partes.Add($"{r.NUM_PROVEEDORES_NUEVOS} proveedor(es) registrado(s)");
        if (r.NUM_PROVEEDORES_CON_TIPO_PRV > 0) partes.Add($"{r.NUM_PROVEEDORES_CON_TIPO_PRV} con el tipo Proveedor");
        if (r.NUM_LINEAS_PRODUCTO_ASIGNADO > 0) partes.Add($"{r.NUM_LINEAS_PRODUCTO_ASIGNADO} línea(s) con producto asignado");
        if (r.NUM_LOTES_NUEVOS > 0) partes.Add($"{r.NUM_LOTES_NUEVOS} lote(s) registrado(s)");
        if (r.NUM_LINEAS_CON_LOTE > 0) partes.Add($"{r.NUM_LINEAS_CON_LOTE} línea(s) con su lote");
        var texto = r.NUM_DOCUMENTOS == 0 ? "No hay documentos pendientes para completar."
            : partes.Count == 0 ? $"Se revisaron {r.NUM_DOCUMENTOS} documento(s): no había nada que completar."
            : $"Se revisaron {r.NUM_DOCUMENTOS} documento(s): " + string.Join(", ", partes) + ".";
        if (r.NUM_LOTES_NO_REGISTRADOS > 0) texto += $" {r.NUM_LOTES_NO_REGISTRADOS} lote(s) no se pudieron registrar (revisa que exista un tipo de lote de control).";
        return ApiResponse<RecibidoCompletarResult>.Ok(r, texto);
    }, nameof(CompletarDatosAsync));

    public Task<ApiResponse<RecibidoSunatResult>> ValidarSunatAsync(decimal? NUM_CPE) => Corre(async () =>
    {
        var (usuario, clave, ruc) = await _config.CredencialesVcdpAsync();
        if (string.IsNullOrWhiteSpace(ruc) || string.IsNullOrWhiteSpace(usuario) || string.IsNullOrEmpty(clave))
            return ApiResponse<RecibidoSunatResult>.Fail("Falta guardar el RUC de la empresa y el usuario y la clave SOL en Configuración general.");
        var cred = new CredencialSol(ruc.Trim(), usuario.Trim(), clave);
        var nums = NUM_CPE is { } uno ? new List<decimal> { uno } : (await _repo.SunatPendientesAsync(50)).ToList();
        var r = new RecibidoSunatResult { NUM_DOCUMENTOS = nums.Count };
        foreach (var num in nums)
        {
            var f = await _repo.ShowbyidAsync(num);
            if (f is null) { r.NUM_DOCUMENTOS--; continue; }
            var c = new KonSolutionsApi.Infrastructure.Sunat.SunatConsulta
            {
                RucEmisor = f.NUM_RUC_EMISOR?.Trim() ?? "", Tipo = f.COD_TIPO_COMPROBANTE_SUNAT?.Trim() ?? "", Serie = f.NUM_SERIE?.Trim() ?? "", Numero = f.NUM_CORRELATIVO?.Trim() ?? "",
                Fecha = f.FEC_EMISION, Importe = f.VAL_TOTAL, TipoDocReceptor = f.COD_TIPO_DOC_RECEPTOR?.Trim(), NumDocReceptor = f.NUM_DOC_RECEPTOR?.Trim()
            };
            var s = await _sunat.ValidarAsync(c, cred, CancellationToken.None);
            await _repo.SunatRegistrarAsync(num, s.Codigo, s.Mensaje);
            if (!s.Consultado) { r.NUM_SIN_CONSULTAR++; r.DES_PRIMER_ERROR ??= s.Mensaje; }
            else if (s.Codigo == "0001") r.NUM_ACEPTADOS++;
            else r.NUM_OBSERVADOS++;
        }
        var texto = r.NUM_DOCUMENTOS == 0 ? "No hay comprobantes pendientes de validar con SUNAT."
            : $"SUNAT: {r.NUM_ACEPTADOS} aceptado(s), {r.NUM_OBSERVADOS} con otra respuesta, {r.NUM_SIN_CONSULTAR} sin consultar.";
        if (r.DES_PRIMER_ERROR is not null) texto += " " + r.DES_PRIMER_ERROR;
        return ApiResponse<RecibidoSunatResult>.Ok(r, texto);
    }, nameof(ValidarSunatAsync));

    public Task<ApiResponse<CpeRecibidoGenerarResult>> GenerarRegistroAsync(decimal NUM_CPE, CpeRecibidoGenerarDto dto, string codUsuario, string codEstacion) => Corre(async () =>
    {
        if (string.IsNullOrWhiteSpace(dto.COD_CLASE_OPERACION)) return ApiResponse<CpeRecibidoGenerarResult>.Fail("Falta la clase de operación: la fija el parámetro de la opción de menú del Registro de Compras.");
        var ficha = await _repo.ShowbyidAsync(NUM_CPE);
        if (ficha is null) return ApiResponse<CpeRecibidoGenerarResult>.Fail("El comprobante recibido no existe.");

        var r = await _repo.GenerarRegistroAsync(NUM_CPE, dto, codUsuario, codEstacion);

        // el PDF que vino en el correo pasa a ser el PDF del registro; si no se puede, el registro queda igual y se avisa
        if (!string.IsNullOrWhiteSpace(ficha.DES_NOMBRE_ARCHIVO_PDF))
        {
            try
            {
                var (archivo, error) = await _archivos.LeerAsync(ficha.DES_NOMBRE_ARCHIVO_PDF, MaxPdf);
                if (archivo is null) r.AVISO_PDF = error;
                else
                {
                    using var ms = new MemoryStream(archivo.CONTENIDO);
                    var p = await _registro.PdfGuardarAsync(r.NUM_REGISTRO, archivo.NOMBRE, ms, archivo.CONTENIDO.Length);
                    r.PDF_COPIADO = p.Success;
                    if (!p.Success) r.AVISO_PDF = p.Message;
                }
            }
            catch (Exception ex) { _log.LogError(ex, Programa, Clase, "CopiarPdf"); r.AVISO_PDF = "No se pudo copiar el PDF al registro: " + ex.Message; }
        }
        return ApiResponse<CpeRecibidoGenerarResult>.Ok(r, $"Registro N° {r.NUM_REGISTRO:0} generado.");
    }, nameof(GenerarRegistroAsync));
}
