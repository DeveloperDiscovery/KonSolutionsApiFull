using System.ComponentModel.DataAnnotations;

namespace KonSolutionsApi.Domain.Models.Comercial;

// Registro de Compras (Comercial/RegistroCompras): FINANZAS.COMPROBANTES_PAGO y FINANZAS.COMPROBANTES_PAGO_DETALLE.
// La clase de operación (COD_CLASE_OPERACION) la fija la opción de menú con su parámetro («COD_CLASE_OPERACION = 'CMPRS'»); nunca va escrita en el código.

/// <summary>Un comprobante del proveedor. La lista (SEARCH) trae las primeras columnas; la ficha (SHOWBYID) trae todas.</summary>
public class RegistroCompra
{
    public decimal NUM_REGISTRO { get; set; }
    public string COD_CLASE_OPERACION { get; set; } = "";
    public string COD_TIPO_OPERACION { get; set; } = "";
    public string? DES_TIPO_OPERACION { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public string? DES_ENTIDAD { get; set; }
    public string? NUM_DOCUMENTO_IDENTIDAD { get; set; }
    public int? NUM_SECUENCIA_DIRECCION { get; set; }
    public string? COD_TIPO_COMPROBANTE_PAGO { get; set; }
    public string? DES_TIPO_COMPROBANTE_PAGO { get; set; }
    public string? NUM_COMPROBANTE_PAGO { get; set; }
    public DateTime? FEC_COMPROBANTE_PAGO { get; set; }
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public DateTime? FEC_RECEPCION { get; set; }
    public string? COD_MONEDA { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public int? COD_FORMA_PAGO { get; set; }
    public int? COD_CONDICION_PAGO { get; set; }
    public string? COD_CENTRO_COSTO_SOLICITANTE { get; set; }
    public string? COD_CENTRO_COSTO_DESTINO { get; set; }
    public int? NUM_PROYECTO { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    public string? COD_SUCURSAL { get; set; }
    public string? COD_INCOTERM { get; set; }
    /// <summary>Motivo de la devolucion o del cargo (solo con un tipo de operacion de devolucion o cargo; script 86).</summary>
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    /// <summary>El descuento de la cabecera: el tipo (COMERCIAL.TIPOS_DESCUENTO) y su tasa. Salen de la orden de compra al asociarla (si el comprobante no los tiene) o se eligen a mano.</summary>
    public int? COD_TIPO_DESCUENTO { get; set; }
    public string? DES_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    /// <summary>El importe total del documento: lo deja calculado PROC_FINANZAS_COMPROBANTES_PAGO_CALCULA al registrar las líneas (no se escribe). El detalle de los valores (base, impuestos…) está en FINANZAS.COMPROBANTES_PAGO_CALCULO: GET {id}/calculo.</summary>
    public decimal? VAL_TOTAL { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public string? DES_NOMBRE_ARCHIVO_PDF { get; set; }
    public string? DES_JUSTIFICACION { get; set; }
    /// <summary>REG registrada · OBS observada · RET retenida (falta recepción) · EXC excepción (sin orden) · BLQ bloqueada · VAL validada · APR aprobada · CON contabilizada · ANU anulada.</summary>
    /// <summary>El par estándar del estado (ESTANDAR.ESTADOS): el tipo de estado de la conciliación y el estado.</summary>
    public short? COD_TIPO_ESTADO_CONCILIACION { get; set; }
    public short? COD_ESTADO_CONCILIACION { get; set; }
    public string COD_ESTADO_ALIAS_CONCILIACION { get; set; } = "REG";
    public string? DES_ESTADO_CONCILIACION { get; set; }
    public bool? FLG_EDITABLE { get; set; }
    public int? NUM_PEDIDOS { get; set; }
    public bool? FLG_TIENE_XML { get; set; }
    public DateTime? FEC_VALIDACION { get; set; }
    public string? COD_USUARIO_APROBACION { get; set; }
    public DateTime? FEC_APROBACION { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }

    // Lo que habilita el tipo de operación (ESTANDAR.TIPOS_OPERACION): decide qué datos se piden en la pantalla.
    public bool? FLG_BIEN { get; set; }
    public bool? FLG_POSITIVO { get; set; }
    public bool? FLG_CENTRO_COSTO { get; set; }
    public bool? FLG_PROYECTO { get; set; }
    public bool? FLG_LOTE_CONTROL { get; set; }
    public bool? FLG_COMERCIO_EXTERIOR { get; set; }
    public bool? FLG_ANTICIPO { get; set; }
    public bool? FLG_PROVISION { get; set; }
    public bool? FLG_CAJA { get; set; }
    public bool? FLG_BANCO { get; set; }
}

public class RegistroCompraSearchDto
{
    /// <summary>Obligatoria: la que trae el parámetro de la opción de menú.</summary>
    public string? COD_CLASE_OPERACION { get; set; }
    public string? SEARCH { get; set; }
    public string? COD_TIPO_OPERACION { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public string? COD_ESTADO_ALIAS_CONCILIACION { get; set; }
    public DateTime? FEC_DESDE { get; set; }
    public DateTime? FEC_HASTA { get; set; }
    public int? NUM_FILAS { get; set; }
}

/// <summary>Los datos de la cabecera. El tipo de operación decide cuáles se guardan: el centro de costo, el proyecto, el lote y el incoterm solo si el tipo los habilita (el servicio limpia los demás).</summary>
public class RegistroCompraSaveDto
{
    [Required] public string COD_TIPO_OPERACION { get; set; } = "";
    [Required] public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = "";
    /// <summary>Serie y correlativo: «F001-00000123».</summary>
    [Required] public string NUM_COMPROBANTE_PAGO { get; set; } = "";
    [Required] public DateTime? FEC_COMPROBANTE_PAGO { get; set; }
    public int? NUM_SECUENCIA_DIRECCION { get; set; }
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public DateTime? FEC_RECEPCION { get; set; }
    public string? COD_MONEDA { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public int? COD_FORMA_PAGO { get; set; }
    public int? COD_CONDICION_PAGO { get; set; }
    public string? COD_CENTRO_COSTO_SOLICITANTE { get; set; }
    public string? COD_CENTRO_COSTO_DESTINO { get; set; }
    public int? NUM_PROYECTO { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    public string? COD_SUCURSAL { get; set; }
    public string? COD_INCOTERM { get; set; }
    /// <summary>Motivo de la devolucion o del cargo; sin motivo toma el por defecto del tipo de operacion y, si el tipo no es de devolucion ni cargo, queda vacio.</summary>
    public int? COD_MOTIVO { get; set; }
    /// <summary>El descuento de la cabecera (tipo y tasa). Si no se manda, queda sin descuento; al asociar una orden el comprobante lo toma de ella.</summary>
    public int? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    // Los importes del documento no se escriben: salen del cálculo de las líneas (FINANZAS.COMPROBANTES_PAGO_CALCULO).
    public string? DES_OBSERVACION { get; set; }
    public string? DES_NOMBRE_ARCHIVO_PDF { get; set; }
}

public class RegistroCompraInsertDto : RegistroCompraSaveDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = "";
}

public class RegistroCompraUpdateDto : RegistroCompraSaveDto
{
    [Required] public decimal NUM_REGISTRO { get; set; }
}

/// <summary>Un concepto del cálculo de totales del documento (FINANZAS.COMPROBANTES_PAGO_CALCULO): base imponible, inafecta, IGV, importe total… los que tenga configurados el tipo de operación
/// (COMERCIAL.TIPOS_OPERACION_CALCULO), en el orden en que se presentan. Es el mismo detalle «Concepto / Tasa / Valor» de las órdenes de compra.</summary>
public class RegistroCompraCalculo
{
    public decimal NUM_REGISTRO { get; set; }
    public string COD_TIPO_VALOR_CALCULO { get; set; } = "";
    public string? DES_TIPO_VALOR_CALCULO { get; set; }
    public string? COD_TIPO_IMPUESTO { get; set; }
    /// <summary>La tasa del impuesto vigente a la fecha del comprobante; vacía en los conceptos que no son impuesto.</summary>
    public decimal? TAS_IMPUESTO { get; set; }
    public decimal? VAL_CALCULO { get; set; }
}

/// <summary>Una línea del comprobante. Si lleva orden, los cuatro datos de la orden (clase, tipo, número y línea) van juntos.</summary>
public class RegistroCompraLinea
{
    public decimal NUM_REGISTRO { get; set; }
    public int NUM_SECUENCIA { get; set; }
    public string? COD_PRODUCTO { get; set; }
    public string? DES_PRODUCTO { get; set; }
    public string? DES_PRODUCTO_COMPROBANTE { get; set; }
    public string? COD_PRODUCTO_ENTIDAD { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public string? COD_UNIDAD_MEDIDA_COMPROBANTE { get; set; }
    public int? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public decimal? PRE_UNITARIO { get; set; }
    public decimal? VAL_DESCUENTO { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    public string? COD_CLASE_OPERACION_PEDIDO { get; set; }
    public string? COD_TIPO_OPERACION_PEDIDO { get; set; }
    public decimal? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA_PEDIDO_COMERCIAL { get; set; }
    public string? DES_PEDIDO { get; set; }
    /// <summary>Lo que dice la línea de la orden: cantidad (en la unidad del proveedor) y precio.</summary>
    public decimal? CAN_ORDEN { get; set; }
    public decimal? PRE_ORDEN { get; set; }
}

public class RegistroCompraLineaDto
{
    /// <summary>Vacío en el alta; con valor modifica esa línea.</summary>
    public int? NUM_SECUENCIA { get; set; }
    public string? COD_PRODUCTO { get; set; }
    public string? DES_PRODUCTO_COMPROBANTE { get; set; }
    public string? COD_PRODUCTO_ENTIDAD { get; set; }
    [Required] public decimal? CAN_PRODUCTO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public string? COD_UNIDAD_MEDIDA_COMPROBANTE { get; set; }
    public int? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public decimal? PRE_UNITARIO { get; set; }
    public decimal? VAL_DESCUENTO { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    public string? COD_CLASE_OPERACION_PEDIDO { get; set; }
    public string? COD_TIPO_OPERACION_PEDIDO { get; set; }
    public decimal? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA_PEDIDO_COMERCIAL { get; set; }
}

/// <summary>Una orden de compra o de servicio, asociada a un comprobante o disponible para asociarla.</summary>
public class RegistroCompraPedido
{
    public decimal? NUM_REGISTRO { get; set; }
    public string COD_CLASE_OPERACION { get; set; } = "";
    public string COD_TIPO_OPERACION { get; set; } = "";
    public string? DES_TIPO_OPERACION { get; set; }
    public decimal NUM_PEDIDO_COMERCIAL { get; set; }
    public string? DES_PEDIDO_COMERCIAL { get; set; }
    public DateTime? FEC_PEDIDO { get; set; }
    public string? COD_MONEDA { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    public string? DES_ESTADO { get; set; }
    public string? COD_ESTADO_ALIAS { get; set; }
    /// <summary>Verdadero si la orden es de bienes; falso si es de servicios.</summary>
    public bool? FLG_BIEN { get; set; }
    public int? NUM_LINEAS_VINCULADAS { get; set; }
}

public class RegistroCompraPedidoAsociarDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = "";
    [Required] public string COD_TIPO_OPERACION { get; set; } = "";
    [Required] public decimal? NUM_PEDIDO_COMERCIAL { get; set; }
}

/// <summary>Una línea de las órdenes asociadas, con lo que falta facturar (en la unidad del proveedor).</summary>
public class RegistroCompraPedidoLinea
{
    public string COD_CLASE_OPERACION { get; set; } = "";
    public string COD_TIPO_OPERACION { get; set; } = "";
    public decimal NUM_PEDIDO_COMERCIAL { get; set; }
    public int NUM_SECUENCIA { get; set; }
    public string? COD_PRODUCTO { get; set; }
    public string? DES_PRODUCTO { get; set; }
    public decimal? CAN_ORDEN { get; set; }
    public decimal? PRE_UNITARIO { get; set; }
    /// <summary>El descuento de la línea de la orden (tipo y tasa), para traerlo con la línea.</summary>
    public int? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public decimal? CAN_FACTURADA { get; set; }
    public decimal? CAN_PENDIENTE { get; set; }
    /// <summary>true en un comprobante de bienes: la cantidad a facturar es la de la guía (lo recibido en almacén) y no se edita.</summary>
    public bool FLG_POR_GUIA { get; set; }
    /// <summary>Lo recibido en almacén de esa línea de la orden (ingresos menos devoluciones), en la unidad del proveedor.</summary>
    public decimal? CAN_GUIA { get; set; }
    /// <summary>Lo que se puede facturar: en bienes, lo recibido menos lo ya facturado (nunca negativo); en servicios, lo que falta de la orden.</summary>
    public decimal? CAN_POR_FACTURAR { get; set; }
}

/// <summary>Un hallazgo de la validación. NUM_PASO: 2 SUNAT/OSE · 3 proveedor y órdenes · 4 ingresos · 5 cantidades · 6 precios · 7 importes · 8 duplicidad y condiciones.
/// COD_RESULTADO: OK · OBS observación · RET retenida · EXC excepción · BLQ bloqueo.</summary>
public class RegistroCompraHallazgo
{
    public decimal NUM_REGISTRO { get; set; }
    public int NUM_ORDEN { get; set; }
    public int NUM_PASO { get; set; }
    public string COD_RESULTADO { get; set; } = "";
    public int? NUM_SECUENCIA { get; set; }
    public string DES_MENSAJE { get; set; } = "";
    public decimal? CAN_ORDEN { get; set; }
    public decimal? CAN_INGRESADA { get; set; }
    public decimal? CAN_FACTURADA { get; set; }
    public decimal? PRE_ORDEN { get; set; }
    public decimal? PRE_COMPROBANTE { get; set; }
    public decimal? VAL_ESPERADO { get; set; }
    public decimal? VAL_COMPROBANTE { get; set; }
    public string? COD_USUARIO_VALIDACION { get; set; }
    public DateTime? FEC_VALIDACION { get; set; }
}

/// <summary>Resultado de validar: el estado en que quedó el comprobante y los hallazgos de cada paso.</summary>
public class RegistroCompraValidacion
{
    public decimal NUM_REGISTRO { get; set; }
    public string COD_ESTADO_ALIAS_CONCILIACION { get; set; } = "";
    public List<RegistroCompraHallazgo> HALLAZGOS { get; set; } = new();
}

/// <summary>Para enviar a aprobación, aprobar y anular. Enviar a aprobación y anular exigen la justificación; aprobar la exige si el comprobante está bloqueado o es una excepción.</summary>
public class RegistroCompraJustificacionDto
{
    public string? DES_JUSTIFICACION { get; set; }
}

public class RegistroCompraXml
{
    public decimal NUM_REGISTRO { get; set; }
    public string? DES_NOMBRE_ARCHIVO { get; set; }
    public string? COD_HASH { get; set; }
    public string? DES_XML { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
}

public class RegistroCompraImportarXmlDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = "";
    /// <summary>El texto del XML (factura o boleta electrónica UBL 2.1).</summary>
    [Required] public string DES_XML { get; set; } = "";
    public string? DES_NOMBRE_ARCHIVO { get; set; }
    /// <summary>Si no se manda, el de la configuración general (pestaña Compras).</summary>
    public string? COD_TIPO_OPERACION { get; set; }
    /// <summary>Validar el comprobante apenas se importa.</summary>
    public bool VALIDAR { get; set; }
}

/// <summary>Lo que dejó importar un XML.</summary>
public class RegistroCompraImportado
{
    public decimal NUM_REGISTRO { get; set; }
    public string COD_ESTADO_ALIAS_CONCILIACION { get; set; } = "";
    public int NUM_LINEAS { get; set; }
    public int NUM_PEDIDOS_ASOCIADOS { get; set; }
    public int NUM_LINEAS_VINCULADAS { get; set; }
}

public class RegistroCompraImportarCarpetaDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = "";
    public string? COD_TIPO_OPERACION { get; set; }
    public bool VALIDAR { get; set; }
}

/// <summary>Un archivo de la carpeta de XML: si se importó (y como qué comprobante) o por qué no.</summary>
public class RegistroCompraArchivoImportado
{
    public string ARCHIVO { get; set; } = "";
    public bool IMPORTADO { get; set; }
    public decimal? NUM_REGISTRO { get; set; }
    public string? COD_ESTADO_ALIAS_CONCILIACION { get; set; }
    public string? MENSAJE { get; set; }
}

public class RegistroCompraImportarCarpetaResult
{
    public string CARPETA { get; set; } = "";
    public int NUM_ARCHIVOS { get; set; }
    public int NUM_IMPORTADOS { get; set; }
    public int NUM_ERRORES { get; set; }
    public List<RegistroCompraArchivoImportado> ARCHIVOS { get; set; } = new();
}

/// <summary>Los catálogos de la pantalla, en un solo viaje.</summary>
public class RegistroCompraTipoOperacion
{
    public string COD_TIPO_OPERACION { get; set; } = "";
    public string? DES_TIPO_OPERACION { get; set; }
    public bool FLG_BIEN { get; set; }
    public bool FLG_POSITIVO { get; set; }
    public bool FLG_CENTRO_COSTO { get; set; }
    public bool FLG_PROYECTO { get; set; }
    public bool FLG_LOTE_CONTROL { get; set; }
    public bool FLG_COMERCIO_EXTERIOR { get; set; }
    public bool FLG_ANTICIPO { get; set; }
    public bool FLG_INCIDENCIA_PRODUCTO { get; set; }
    public bool FLG_DEVOLUCION { get; set; }
    public bool FLG_ANULACION { get; set; }
    public bool FLG_CARGO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO_DEFAULT { get; set; }
    public string? COD_TIPO_ENTIDAD { get; set; }
}

public class RegistroCompraTipoComprobante
{
    public string? COD_TIPO_OPERACION { get; set; }
    public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = "";
    public string? DES_TIPO_COMPROBANTE_PAGO { get; set; }
    public int? COD_COMPROBANTE_PAGO_SUNAT { get; set; }
    /// <summary>El formato del número (serie - correlativo): el prefijo (el de SUNAT primero; el interno de respaldo) y los dígitos de la serie y del correlativo. Sin longitudes no hay formato que aplicar.</summary>
    public string? COD_PREFIJO_SUNAT { get; set; }
    public string? COD_PREFIJO { get; set; }
    public int? VAL_LONGITUD_SERIE { get; set; }
    public int? VAL_LONGITUD_NUMERO { get; set; }
}

/// <summary>Un elemento de un combo (código y texto) tal como lo devuelven los SHOWCOMBOX existentes.</summary>
public class RegistroCompraCombo
{
    public string? Id { get; set; }
    public string? Texto { get; set; }
}

/// <summary>El PDF del comprobante tal como se guardó: su nombre original y su contenido.</summary>
public class RegistroCompraPdf
{
    public string NOMBRE { get; set; } = "";
    public byte[] CONTENIDO { get; set; } = Array.Empty<byte>();
}

/// <summary>Un tipo de descuento (COMERCIAL.TIPOS_DESCUENTO) con su tasa.</summary>
public class RegistroCompraTipoDescuento
{
    public int COD_TIPO_DESCUENTO { get; set; }
    public string? DES_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
}

public class RegistroCompraCombos
{
    public List<RegistroCompraTipoOperacion> TIPOS_OPERACION { get; set; } = new();
    /// <summary>Los tipos de comprobante que admite cada tipo de operación. Si un tipo de operación no figura aquí, admite cualquiera de TIPOS_COMPROBANTE.</summary>
    public List<RegistroCompraTipoComprobante> TIPOS_COMPROBANTE_POR_OPERACION { get; set; } = new();
    public List<RegistroCompraTipoComprobante> TIPOS_COMPROBANTE { get; set; } = new();
    public List<RegistroCompraCombo> MONEDAS { get; set; } = new();
    public List<RegistroCompraCombo> FORMAS_PAGO { get; set; } = new();
    public List<RegistroCompraCombo> CONDICIONES_PAGO { get; set; } = new();
    public List<RegistroCompraCombo> INCOTERMS { get; set; } = new();
    public List<RegistroCompraCombo> PROYECTOS { get; set; } = new();
    /// <summary>Los tipos de descuento con su tasa (la cabecera del comprobante elige uno).</summary>
    public List<RegistroCompraTipoDescuento> TIPOS_DESCUENTO { get; set; } = new();
}

// ───────── Notas de crédito y débito por ítem (script 85) ─────────

/// <summary>Una factura que la nota puede afectar (del mismo proveedor, no anulada, que no sea otra nota).</summary>
public class RegistroCompraIncidenciaOrigen
{
    public decimal NUM_REGISTRO { get; set; }
    public string? COD_TIPO_COMPROBANTE_PAGO { get; set; }
    public string? DES_TIPO_COMPROBANTE_PAGO { get; set; }
    public string? NUM_COMPROBANTE_PAGO { get; set; }
    public DateTime? FEC_COMPROBANTE_PAGO { get; set; }
    public string? COD_MONEDA { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    public string? COD_ESTADO_ALIAS { get; set; }
}

/// <summary>Una línea de la factura afectada, con su saldo y lo que esta nota ya aplica (si algo).</summary>
public class RegistroCompraIncidenciaLinea
{
    public int NUM_SECUENCIA_ASOCIADO { get; set; }
    public string? COD_PRODUCTO { get; set; }
    /// <summary>La descripción de la línea o, si no tiene, la del producto.</summary>
    public string? DES_PRODUCTO { get; set; }
    public string? DES_PRODUCTO_COMPROBANTE { get; set; }
    public string? COD_PRODUCTO_ENTIDAD { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public decimal? PRE_UNITARIO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public decimal CAN_FACTURADA { get; set; }
    public decimal VAL_FACTURADO { get; set; }
    public decimal CAN_APLICADA_OTRAS { get; set; }
    public decimal VAL_APLICADO_OTRAS { get; set; }
    public decimal CAN_SALDO { get; set; }
    public decimal VAL_SALDO { get; set; }
    public int? NUM_SECUENCIA_NOTA { get; set; }
    public decimal? CAN_APLICADA { get; set; }
    public decimal? VAL_APLICADO { get; set; }
    /// <summary>Cantidad que dicen las guías asociadas a la nota para este producto (null si no hay guías).</summary>
    public decimal? CAN_GUIA { get; set; }
    /// <summary>True si la lista está filtrada porque la nota ya tiene guías: solo salen los items que coinciden con ellas.</summary>
    public bool FLG_FILTRADO_POR_GUIA { get; set; }
}

/// <summary>Lo que la nota ya tiene asociado: línea de la nota, factura y línea de la factura.</summary>
public class RegistroCompraIncidencia
{
    public int NUM_SECUENCIA_NOTA { get; set; }
    public decimal NUM_REGISTRO_ASOCIADO { get; set; }
    public string? COD_TIPO_COMPROBANTE_PAGO { get; set; }
    public string? NUM_COMPROBANTE_PAGO { get; set; }
    public int NUM_SECUENCIA_ASOCIADO { get; set; }
    public string? COD_PRODUCTO { get; set; }
    public string? DES_PRODUCTO_COMPROBANTE { get; set; }
    public decimal? CAN_FACTURADA { get; set; }
    public decimal? VAL_FACTURADO { get; set; }
    public decimal? CAN_APLICADA { get; set; }
    public decimal? VAL_APLICADO { get; set; }
}

public class RegistroCompraIncidenciaItemDto
{
    /// <summary>Línea de la nota.</summary>
    public int NUM_SECUENCIA { get; set; }
    public decimal NUM_REGISTRO_ASOCIADO { get; set; }
    public int NUM_SECUENCIA_ASOCIADO { get; set; }
    public decimal? CAN_APLICADA { get; set; }
    public decimal? VAL_APLICADO { get; set; }
}

/// <summary>Reemplaza TODA la asociación de la nota.</summary>
public class RegistroCompraIncidenciasGuardarDto
{
    public List<RegistroCompraIncidenciaItemDto> Items { get; set; } = new();
}

/// <summary>Lo elegido en la grilla de la factura: cantidad devuelta y/o importe de la diferencia por línea.</summary>
public class RegistroCompraIncidenciaAdicionarItemDto
{
    public int NUM_SECUENCIA_ASOCIADO { get; set; }
    public decimal? CAN_APLICADA { get; set; }
    public decimal? VAL_APLICADO { get; set; }
}

public class RegistroCompraIncidenciaAdicionarDto
{
    [Required] public decimal? NUM_REGISTRO_ORIGEN { get; set; }
    public List<RegistroCompraIncidenciaAdicionarItemDto> Items { get; set; } = new();
}

/// <summary>Movimiento de almacén de devolución al proveedor (la guía de devolución) que se puede adjuntar.</summary>
public class RegistroCompraIncidenciaGuia
{
    public decimal NUM_MOVIMIENTO_ALMACEN { get; set; }
    public DateTime? FEC_MOVIMENTO_ALMACEN { get; set; }
    public string? DES_TIPO_MOVIMIENTO { get; set; }
    public string? COD_TIPO_GUIA { get; set; }
    public string? NUM_GUIA { get; set; }
    public int NUM_LINEAS { get; set; }
    public bool FLG_ASOCIADA { get; set; }
}

public class RegistroCompraIncidenciaGuiaAdjunta
{
    public decimal NUM_MOVIMIENTO_ALMACEN { get; set; }
    public DateTime? FEC_MOVIMENTO_ALMACEN { get; set; }
    public string? DES_TIPO_MOVIMIENTO { get; set; }
    public string? COD_TIPO_GUIA { get; set; }
    public string? NUM_GUIA { get; set; }
    public int NUM_LINEAS_VINCULADAS { get; set; }
}

/// <summary>Datos para editar una línea de la nota: la línea de la factura afectada y la cantidad del proveedor según la guía.</summary>
public class RegistroCompraIncidenciaLineaGuia
{
    /// <summary>El tipo de operación admite guías: FLG_INCIDENCIA_PRODUCTO y (FLG_DEVOLUCION o FLG_CARGO).</summary>
    public bool FLG_CON_GUIA { get; set; }
    public decimal? NUM_REGISTRO_ASOCIADO { get; set; }
    public string? COD_TIPO_COMPROBANTE_PAGO { get; set; }
    public string? NUM_COMPROBANTE_PAGO { get; set; }
    public int? NUM_SECUENCIA_ASOCIADO { get; set; }
    public string? COD_PRODUCTO { get; set; }
    public string? DES_PRODUCTO { get; set; }
    public string? COD_PRODUCTO_ENTIDAD { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public decimal? CAN_FACTURADA { get; set; }
    public decimal? PRE_UNITARIO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public decimal? VAL_FACTURADO { get; set; }
    /// <summary>Suma de las cantidades de las líneas de la(s) guía(s) vinculadas a la línea de la nota.</summary>
    public decimal? CAN_GUIA { get; set; }
    public string? NUM_GUIAS { get; set; }
}

public class RegistroCompraIncidenciaAnulacionDto
{
    [Required] public decimal? NUM_REGISTRO_ORIGEN { get; set; }
}


/// <summary>Factura de anticipo (tipo de operación con FLG_ANTICIPO): disponible para aplicar a la factura, o ya aplicada.</summary>
public class RegistroCompraAnticipo
{
    public decimal NUM_REGISTRO { get; set; }
    public string? COD_TIPO_COMPROBANTE_PAGO { get; set; }
    public string? NUM_COMPROBANTE_PAGO { get; set; }
    public DateTime? FEC_COMPROBANTE_PAGO { get; set; }
    public string? COD_MONEDA { get; set; }
    public decimal VAL_TOTAL { get; set; }
    public string? DES_TIPO_OPERACION { get; set; }
    /// <summary>Lo escrito en las líneas del anticipo (descripción de la línea o del producto), unidas con ' | '.</summary>
    public string? DES_DETALLE { get; set; }
    /// <summary>Estado del anticipo: alias (REG, VAL, APR, CON, ANU...) y su descripción.</summary>
    public string? COD_ESTADO_ALIAS { get; set; }
    public string? DES_ESTADO { get; set; }
}
