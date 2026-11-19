using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

/// <summary>Las guías de remisión recibidas: consultarlas, ponerles el producto de la empresa, descartarlas y generar con ellas el ingreso a almacén. Mismas reglas de errores que los comprobantes recibidos.</summary>
public class GreRecibidasService : IGreRecibidasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(GreRecibidasService);
    private const long MaxPdf = 20L * 1024 * 1024;

    private readonly IGreRecibidasRepository _repo;
    private readonly IXmlRecibidosRepository _config;
    private readonly XmlRecibidosArchivos _archivos;
    private readonly ErrorLogService _log;

    public GreRecibidasService(IGreRecibidasRepository repo, IXmlRecibidosRepository config, XmlRecibidosArchivos archivos, ErrorLogService log)
    { _repo = repo; _config = config; _archivos = archivos; _log = log; }

    private async Task<ApiResponse<T>> Corre<T>(Func<Task<ApiResponse<T>>> accion, string metodo)
    {
        try { return await accion(); }
        catch (SqlException ex) when (ex.Number >= 50000) { return ApiResponse<T>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    public Task<ApiResponse<IEnumerable<GreRecibida>>> SearchAsync(GreRecibidaSearchDto dto) => Corre(async () =>
    {
        var d = (await _repo.SearchAsync(dto)).ToList();
        return ApiResponse<IEnumerable<GreRecibida>>.Ok(d, total: d.Count);
    }, nameof(SearchAsync));

    public Task<ApiResponse<GreRecibidaFicha>> GetByIdAsync(decimal NUM_GRE) => Corre(async () =>
    {
        var d = await _repo.ShowbyidAsync(NUM_GRE);
        return d is null ? ApiResponse<GreRecibidaFicha>.Fail("La guía recibida no existe.") : ApiResponse<GreRecibidaFicha>.Ok(d);
    }, nameof(GetByIdAsync));

    public Task<ApiResponse<IEnumerable<GreRecibidaLinea>>> LineasAsync(decimal NUM_GRE) => Corre(async () =>
    {
        var d = (await _repo.LineasAsync(NUM_GRE)).ToList();
        return ApiResponse<IEnumerable<GreRecibidaLinea>>.Ok(d, total: d.Count);
    }, nameof(LineasAsync));

    public Task<ApiResponse<GreRecibidaXml>> XmlAsync(decimal NUM_GRE) => Corre(async () =>
    {
        var d = await _repo.XmlAsync(NUM_GRE);
        return d is null ? ApiResponse<GreRecibidaXml>.Fail("La guía recibida no existe.") : ApiResponse<GreRecibidaXml>.Ok(d);
    }, nameof(XmlAsync));

    public Task<ApiResponse<RecibidoArchivo>> PdfAsync(decimal NUM_GRE) => Corre(async () =>
    {
        var f = await _repo.ShowbyidAsync(NUM_GRE);
        if (f is null) return ApiResponse<RecibidoArchivo>.Fail("La guía recibida no existe.");
        if (string.IsNullOrWhiteSpace(f.DES_NOMBRE_ARCHIVO_PDF)) return ApiResponse<RecibidoArchivo>.Fail("Esta guía no trajo un PDF en el correo.");
        var (archivo, error) = await _archivos.LeerAsync(f.DES_NOMBRE_ARCHIVO_PDF, MaxPdf);
        return archivo is null ? ApiResponse<RecibidoArchivo>.Fail(error!) : ApiResponse<RecibidoArchivo>.Ok(archivo);
    }, nameof(PdfAsync));

    public Task<ApiResponse<bool>> AsignarProductoAsync(decimal NUM_GRE, int NUM_ITEM, RecibidoAsignarProductoDto dto, string codUsuario, string codEstacion) => Corre(async () =>
    {
        await _repo.AsignarProductoAsync(NUM_GRE, NUM_ITEM, dto.COD_PRODUCTO, codUsuario, codEstacion);
        return ApiResponse<bool>.Ok(true, string.IsNullOrWhiteSpace(dto.COD_PRODUCTO) ? "Producto quitado." : "Producto asignado.");
    }, nameof(AsignarProductoAsync));

    public Task<ApiResponse<bool>> DescartarAsync(decimal NUM_GRE, RecibidoDescartarDto dto, string codUsuario, string codEstacion) => Corre(async () =>
    {
        await _repo.DescartarAsync(NUM_GRE, dto.FLG_DESCARTAR, codUsuario, codEstacion);
        return ApiResponse<bool>.Ok(true, dto.FLG_DESCARTAR ? "Guía descartada." : "Guía devuelta a pendiente.");
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

    public Task<ApiResponse<RecibidoSunatResult>> ValidarSunatAsync(decimal? NUM_GRE) => Corre(async () =>
    {
        // SUNAT no consulta guías de terceros: la revisión es propia (RUC, serie, correlativo, fecha, firma y líneas)
        var r = await _repo.ValidarAsync(NUM_GRE);
        var texto = r.NUM_DOCUMENTOS == 0 ? "No hay guías pendientes de validar."
            : $"Validación de guías: {r.NUM_ACEPTADOS} en regla, {r.NUM_OBSERVADOS} con observaciones.";
        if (r.DES_PRIMER_ERROR is not null) texto += " " + r.DES_PRIMER_ERROR;
        return ApiResponse<RecibidoSunatResult>.Ok(r, texto);
    }, nameof(ValidarSunatAsync));

    public Task<ApiResponse<GreRecibidaGenerarResult>> GenerarMovimientoAsync(decimal NUM_GRE, GreRecibidaGenerarDto dto, string codUsuario, string codEstacion) => Corre(async () =>
    {
        if (string.IsNullOrWhiteSpace(dto.COD_ALMACEN)) return ApiResponse<GreRecibidaGenerarResult>.Fail("Elige el almacén de destino.");
        if (string.IsNullOrWhiteSpace(dto.COD_TIPO_MOVIMIENTO)) return ApiResponse<GreRecibidaGenerarResult>.Fail("Elige el tipo de movimiento.");
        var clase = dto.COD_CLASE_MOVIMIENTO;
        if (string.IsNullOrWhiteSpace(clase)) clase = (await _config.ConfigAsync())?.COD_CLASE_MOVIMIENTO_ALMACEN;
        if (string.IsNullOrWhiteSpace(clase)) return ApiResponse<GreRecibidaGenerarResult>.Fail("Falta la clase de movimiento de almacén: se define en Configuración general.");
        var r = await _repo.GenerarMovimientoAsync(NUM_GRE, dto, clase.Trim(), codUsuario, codEstacion);
        return ApiResponse<GreRecibidaGenerarResult>.Ok(r, $"Movimiento N° {r.NUM_MOVIMIENTO_ALMACEN:0} generado.");
    }, nameof(GenerarMovimientoAsync));
}
