using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

/// <summary>Los comprobantes recibidos (FINANZAS.CPE_RECIBIDOS) que bajó el servicio de XML. Todo pasa por los procedimientos de los scripts 41 y 42; un documento rechazado por una regla lanza SqlException (50700 a 50770) con su mensaje.</summary>
public interface ICpeRecibidosRepository
{
    Task<IEnumerable<CpeRecibido>> SearchAsync(CpeRecibidoSearchDto dto);
    Task<CpeRecibidoFicha?> ShowbyidAsync(decimal NUM_CPE);
    Task<IEnumerable<CpeRecibidoLinea>> LineasAsync(decimal NUM_CPE);
    Task<CpeRecibidoXml?> XmlAsync(decimal NUM_CPE);
    Task<CpeRecibidoTipoOperacion?> TipoOperacionAsync(decimal NUM_CPE, string codClaseOperacion);
    Task AsignarProductoAsync(decimal NUM_CPE, int NUM_ITEM, string? codProducto, string codUsuario, string codEstacion);
    Task DescartarAsync(decimal NUM_CPE, bool descartar, string codUsuario, string codEstacion);
    Task<RecibidoCompletarResult> CompletarDatosAsync(decimal? NUM_CPE, string codUsuario, string codEstacion);
    Task SunatRegistrarAsync(decimal NUM_CPE, string codigo, string mensaje);
    Task<IReadOnlyList<decimal>> SunatPendientesAsync(int max);
    Task<CpeRecibidoGenerarResult> GenerarRegistroAsync(decimal NUM_CPE, CpeRecibidoGenerarDto dto, string codUsuario, string codEstacion);
}

public interface ICpeRecibidosService
{
    Task<ApiResponse<IEnumerable<CpeRecibido>>> SearchAsync(CpeRecibidoSearchDto dto);
    Task<ApiResponse<CpeRecibidoFicha>> GetByIdAsync(decimal NUM_CPE);
    Task<ApiResponse<IEnumerable<CpeRecibidoLinea>>> LineasAsync(decimal NUM_CPE);
    Task<ApiResponse<CpeRecibidoXml>> XmlAsync(decimal NUM_CPE);
    /// <summary>El tipo de operación que le toca según el XML (código SUNAT y bienes o servicios), para proponerlo al elegir el comprobante.</summary>
    Task<ApiResponse<CpeRecibidoTipoOperacion>> TipoOperacionAsync(decimal NUM_CPE, string? codClaseOperacion);
    Task<ApiResponse<RecibidoArchivo>> PdfAsync(decimal NUM_CPE);
    Task<ApiResponse<bool>> AsignarProductoAsync(decimal NUM_CPE, int NUM_ITEM, RecibidoAsignarProductoDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> DescartarAsync(decimal NUM_CPE, RecibidoDescartarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<RecibidoCompletarResult>> CompletarDatosAsync(decimal? NUM_CPE, string codUsuario, string codEstacion);
    /// <summary>Consulta a SUNAT (con la Clave SOL de Configuración general) el comprobante indicado, o todos los que aún no tienen resultado o fallaron, y deja el resultado en el comprobante.</summary>
    Task<ApiResponse<RecibidoSunatResult>> ValidarSunatAsync(decimal? NUM_CPE);
    /// <summary>Crea el registro de compras del comprobante, deja el comprobante como usado y copia al registro el PDF que vino en el correo.</summary>
    Task<ApiResponse<CpeRecibidoGenerarResult>> GenerarRegistroAsync(decimal NUM_CPE, CpeRecibidoGenerarDto dto, string codUsuario, string codEstacion);
}

public interface IGreRecibidasRepository
{
    Task<IEnumerable<GreRecibida>> SearchAsync(GreRecibidaSearchDto dto);
    Task<GreRecibidaFicha?> ShowbyidAsync(decimal NUM_GRE);
    Task<IEnumerable<GreRecibidaLinea>> LineasAsync(decimal NUM_GRE);
    Task<GreRecibidaXml?> XmlAsync(decimal NUM_GRE);
    Task AsignarProductoAsync(decimal NUM_GRE, int NUM_ITEM, string? codProducto, string codUsuario, string codEstacion);
    Task DescartarAsync(decimal NUM_GRE, bool descartar, string codUsuario, string codEstacion);
    Task<RecibidoCompletarResult> CompletarDatosAsync(decimal? NUM_GRE, string codUsuario, string codEstacion);
    Task<RecibidoSunatResult> ValidarAsync(decimal? NUM_GRE);
    Task<GreRecibidaGenerarResult> GenerarMovimientoAsync(decimal NUM_GRE, GreRecibidaGenerarDto dto, string codClaseMovimiento, string codUsuario, string codEstacion);
}

public interface IGreRecibidasService
{
    Task<ApiResponse<IEnumerable<GreRecibida>>> SearchAsync(GreRecibidaSearchDto dto);
    Task<ApiResponse<GreRecibidaFicha>> GetByIdAsync(decimal NUM_GRE);
    Task<ApiResponse<IEnumerable<GreRecibidaLinea>>> LineasAsync(decimal NUM_GRE);
    Task<ApiResponse<GreRecibidaXml>> XmlAsync(decimal NUM_GRE);
    Task<ApiResponse<RecibidoArchivo>> PdfAsync(decimal NUM_GRE);
    Task<ApiResponse<bool>> AsignarProductoAsync(decimal NUM_GRE, int NUM_ITEM, RecibidoAsignarProductoDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> DescartarAsync(decimal NUM_GRE, RecibidoDescartarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<RecibidoCompletarResult>> CompletarDatosAsync(decimal? NUM_GRE, string codUsuario, string codEstacion);
    Task<ApiResponse<RecibidoSunatResult>> ValidarSunatAsync(decimal? NUM_GRE);
    /// <summary>Crea el ingreso a almacén de la guía y la deja como usada.</summary>
    Task<ApiResponse<GreRecibidaGenerarResult>> GenerarMovimientoAsync(decimal NUM_GRE, GreRecibidaGenerarDto dto, string codUsuario, string codEstacion);
}
