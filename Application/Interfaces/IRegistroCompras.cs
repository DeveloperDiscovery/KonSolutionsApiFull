using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRegistroComprasRepository
{
    // cabecera
    Task<IEnumerable<RegistroCompra>> SearchAsync(RegistroCompraSearchDto dto);
    Task<RegistroCompra?> ShowbyidAsync(decimal NUM_REGISTRO);
    Task<WriteResult> InsertAsync(RegistroCompraInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(RegistroCompraUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO);
    Task<RegistroCompraCombos> LoadcombosAsync(string codClaseOperacion);
    /// <summary>El tipo de operación de la clase con lo que habilita; nulo si no existe en esa clase.</summary>
    Task<RegistroCompraTipoOperacion?> TipoOperacionAsync(string codClaseOperacion, string codTipoOperacion);
    /// <summary>El detalle de los valores totales del documento (FINANZAS.COMPROBANTES_PAGO_CALCULO), en el orden en que se presentan.</summary>
    Task<IEnumerable<RegistroCompraCalculo>> CalculoAsync(decimal NUM_REGISTRO);
    // líneas
    Task<IEnumerable<RegistroCompraLinea>> LineasAsync(decimal NUM_REGISTRO);
    Task<WriteResult> LineaGuardarAsync(decimal NUM_REGISTRO, RegistroCompraLineaDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> LineaDeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA);
    // órdenes
    Task<IEnumerable<RegistroCompraPedido>> PedidosAsync(decimal NUM_REGISTRO);
    Task<IEnumerable<RegistroCompraPedido>> PedidosDisponiblesAsync(decimal NUM_REGISTRO);
    Task<IEnumerable<RegistroCompraPedidoLinea>> PedidosLineasAsync(decimal NUM_REGISTRO);
    Task<WriteResult> PedidoAsociarAsync(decimal NUM_REGISTRO, RegistroCompraPedidoAsociarDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> PedidoDesasociarAsync(decimal NUM_REGISTRO, string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<IEnumerable<RegistroCompraIncidenciaOrigen>> IncidenciasOrigenesAsync(decimal NUM_REGISTRO, string? search);
    Task<IEnumerable<RegistroCompraIncidenciaLinea>> IncidenciasDisponiblesAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ORIGEN);
    Task<IEnumerable<RegistroCompraIncidencia>> IncidenciasAsync(decimal NUM_REGISTRO);
    Task<WriteResult> IncidenciasGuardarAsync(decimal NUM_REGISTRO, RegistroCompraIncidenciasGuardarDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> IncidenciasAnulacionTotalAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ORIGEN, string codUsuario, string codEstacion);
    Task<WriteResult> IncidenciasAdicionarAsync(decimal NUM_REGISTRO, RegistroCompraIncidenciaAdicionarDto dto, string codUsuario, string codEstacion);
    Task<IEnumerable<RegistroCompraIncidenciaGuia>> IncidenciasGuiasDisponiblesAsync(decimal NUM_REGISTRO);
    Task<IEnumerable<RegistroCompraIncidenciaGuiaAdjunta>> IncidenciasGuiasAsync(decimal NUM_REGISTRO);
    Task<IEnumerable<RegistroCompraAnticipo>> AnticiposDisponiblesAsync(decimal NUM_REGISTRO);
    Task<IEnumerable<RegistroCompraAnticipo>> AnticiposAsync(decimal NUM_REGISTRO);
    Task<WriteResult> AnticipoAsociarAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ANTICIPO, string codUsuario, string codEstacion);
    Task<WriteResult> AnticipoQuitarAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ANTICIPO);
    Task<RegistroCompraIncidenciaLineaGuia?> IncidenciasLineaGuiaAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA);
    Task<WriteResult> IncidenciasGuiaAsociarAsync(decimal NUM_REGISTRO, decimal NUM_MOVIMIENTO_ALMACEN, string codUsuario, string codEstacion);
    Task<WriteResult> IncidenciasGuiaQuitarAsync(decimal NUM_REGISTRO, decimal NUM_MOVIMIENTO_ALMACEN);
    Task<WriteResult> AutovincularAsync(decimal NUM_REGISTRO);
    // conciliación y flujo
    Task<IEnumerable<RegistroCompraHallazgo>> ConciliacionAsync(decimal NUM_REGISTRO);
    Task<(WriteResult Resultado, RegistroCompraValidacion? Validacion)> ValidarAsync(decimal NUM_REGISTRO, string codUsuario, string codEstacion);
    Task<WriteResult> EnviarAprobacionAsync(decimal NUM_REGISTRO, string? justificacion, string codUsuario, string codEstacion);
    Task<WriteResult> AprobarAsync(decimal NUM_REGISTRO, string? justificacion, string codUsuario, string codEstacion);
    Task<WriteResult> ContabilizarAsync(decimal NUM_REGISTRO, string codUsuario, string codEstacion);
    Task<WriteResult> AnularAsync(decimal NUM_REGISTRO, string? justificacion, string codUsuario, string codEstacion);
    // XML
    Task<RegistroCompraXml?> XmlAsync(decimal NUM_REGISTRO);
    Task<(WriteResult Resultado, RegistroCompraImportado? Importado)> ImportarXmlAsync(RegistroCompraImportarXmlDto dto, string codUsuario, string codEstacion);
    /// <summary>La configuración general que necesita la importación desde la carpeta: ruta (ENTORNO.PATHFILES), subcarpetas y tipo de operación.</summary>
    Task<RegistroComprasConfigXml?> ConfigXmlAsync();
}

/// <summary>La pestaña Compras de la configuración general, en lo que toca a la importación de XML.</summary>
public class RegistroComprasConfigXml
{
    public string? COD_KEY_PATHFILE_COMPRAS_XML { get; set; }
    public string? DES_CARPETA_COMPRAS_PROCESADOS { get; set; }
    public string? DES_CARPETA_COMPRAS_ERRORES { get; set; }
    public string? COD_TIPO_OPERACION_COMPRAS_XML { get; set; }
}

public interface IRegistroComprasService
{
    Task<ApiResponse<IEnumerable<RegistroCompra>>> SearchAsync(RegistroCompraSearchDto dto);
    Task<ApiResponse<RegistroCompra>> GetByIdAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<WriteResult>> InsertAsync(RegistroCompraInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(RegistroCompraUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<RegistroCompraCombos>> LoadcombosAsync(string codClaseOperacion);
    /// <summary>El detalle de los valores totales del documento: Concepto / Tasa / Valor.</summary>
    Task<ApiResponse<IEnumerable<RegistroCompraCalculo>>> CalculoAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<IEnumerable<RegistroCompraLinea>>> LineasAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<WriteResult>> LineaGuardarAsync(decimal NUM_REGISTRO, RegistroCompraLineaDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> LineaDeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<RegistroCompraPedido>>> PedidosAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<IEnumerable<RegistroCompraPedido>>> PedidosDisponiblesAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<IEnumerable<RegistroCompraPedidoLinea>>> PedidosLineasAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<WriteResult>> PedidoAsociarAsync(decimal NUM_REGISTRO, RegistroCompraPedidoAsociarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> PedidoDesasociarAsync(decimal NUM_REGISTRO, string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<ApiResponse<IEnumerable<RegistroCompraIncidenciaOrigen>>> IncidenciasOrigenesAsync(decimal NUM_REGISTRO, string? search);
    Task<ApiResponse<IEnumerable<RegistroCompraIncidenciaLinea>>> IncidenciasDisponiblesAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ORIGEN);
    Task<ApiResponse<IEnumerable<RegistroCompraIncidencia>>> IncidenciasAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<WriteResult>> IncidenciasGuardarAsync(decimal NUM_REGISTRO, RegistroCompraIncidenciasGuardarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> IncidenciasAnulacionTotalAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ORIGEN, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> IncidenciasAdicionarAsync(decimal NUM_REGISTRO, RegistroCompraIncidenciaAdicionarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<IEnumerable<RegistroCompraIncidenciaGuia>>> IncidenciasGuiasDisponiblesAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<IEnumerable<RegistroCompraIncidenciaGuiaAdjunta>>> IncidenciasGuiasAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<IEnumerable<RegistroCompraAnticipo>>> AnticiposDisponiblesAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<IEnumerable<RegistroCompraAnticipo>>> AnticiposAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<WriteResult>> AnticipoAsociarAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ANTICIPO, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> AnticipoQuitarAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ANTICIPO);
    Task<ApiResponse<RegistroCompraIncidenciaLineaGuia?>> IncidenciasLineaGuiaAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA);
    Task<ApiResponse<WriteResult>> IncidenciasGuiaAsociarAsync(decimal NUM_REGISTRO, decimal NUM_MOVIMIENTO_ALMACEN, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> IncidenciasGuiaQuitarAsync(decimal NUM_REGISTRO, decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<WriteResult>> AutovincularAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<IEnumerable<RegistroCompraHallazgo>>> ConciliacionAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<RegistroCompraValidacion>> ValidarAsync(decimal NUM_REGISTRO, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> EnviarAprobacionAsync(decimal NUM_REGISTRO, RegistroCompraJustificacionDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> AprobarAsync(decimal NUM_REGISTRO, RegistroCompraJustificacionDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> ContabilizarAsync(decimal NUM_REGISTRO, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> AnularAsync(decimal NUM_REGISTRO, RegistroCompraJustificacionDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<RegistroCompraXml>> XmlAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<RegistroCompraImportado>> ImportarXmlAsync(RegistroCompraImportarXmlDto dto, string codUsuario, string codEstacion);
    /// <summary>Importa los XML de la carpeta configurada (Configuración general, pestaña Compras). Cada archivo se mueve a «procesados» si entró o a «errores» si no.</summary>
    Task<ApiResponse<RegistroCompraImportarCarpetaResult>> ImportarCarpetaAsync(RegistroCompraImportarCarpetaDto dto, string codUsuario, string codEstacion);
    /// <summary>Guarda el PDF del comprobante (uno por comprobante; reemplaza el anterior) en la carpeta de Compras de Configuración general. El nombre del archivo se graba con la cabecera (DES_NOMBRE_ARCHIVO_PDF).</summary>
    Task<ApiResponse<bool>> PdfGuardarAsync(decimal NUM_REGISTRO, string? nombreArchivo, Stream contenido, long largo);
    /// <summary>El PDF guardado del comprobante.</summary>
    Task<ApiResponse<RegistroCompraPdf>> PdfLeerAsync(decimal NUM_REGISTRO);
    /// <summary>Quita el PDF guardado del comprobante (si no hay, no pasa nada).</summary>
    Task<ApiResponse<bool>> PdfQuitarAsync(decimal NUM_REGISTRO);
}
