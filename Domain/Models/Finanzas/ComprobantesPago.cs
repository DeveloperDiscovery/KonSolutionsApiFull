using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad COMPROBANTES_PAGO - tipos equivalentes a la BD.</summary>
public class ComprobantesPago
{
    public decimal? NUM_REGISTRO { get; set; }
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public int? NUM_SECUENCIA_DIRECCION { get; set; }
    public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public string NUM_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public DateTime? FEC_COMPROBANTE_PAGO { get; set; }
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public DateTime? FEC_RECEPCION { get; set; }
    public DateTime? FEC_DEVOLUCION { get; set; }
    public DateTime? FEC_EMBARQUE { get; set; }
    public DateTime? FEC_COMERCIAL { get; set; }
    public DateTime? FEC_REGISTRO_CAJA { get; set; }
    public string COD_CAJA { get; set; } = string.Empty;
    public int? NUM_UNIDAD_COMERCIAL { get; set; }
    public int? NUM_PROYECTO { get; set; }
    public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public int? COD_FORMA_PAGO { get; set; }
    public int? COD_CONDICION_PAGO { get; set; }
    public int? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public string COD_INCOTERM { get; set; } = string.Empty;
    public string COD_CENTRO_COSTO_SOLICITANTE { get; set; } = string.Empty;
    public string COD_CENTRO_COSTO_DESTINO { get; set; } = string.Empty;
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_VENDEDOR { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_PERIODO_CONTABLE { get; set; }
    public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public int? NUM_ASIENTO_CONTABLE { get; set; }
    public int? COD_TIPO_ESTADO_PAGO { get; set; }
    public int? COD_ESTADO_PAGO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CENTRO_COSTO { get; set; } = string.Empty;
    public string DES_CONDICION_PAGO { get; set; } = string.Empty;
    public string DES_DIRECCION { get; set; } = string.Empty;
    public string DES_INTERIOR { get; set; } = string.Empty;
    public string DES_KILOMETRO { get; set; } = string.Empty;
    public string DES_LOTE { get; set; } = string.Empty;
    public string DES_MANZANA { get; set; } = string.Empty;
    public string DES_NOMBRE_VIA { get; set; } = string.Empty;
    public string DES_NOMBRE_ZONA { get; set; } = string.Empty;
    public string DES_NUMERACION { get; set; } = string.Empty;
    public string DES_REFERENCIA { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_FORMA_PAGO { get; set; } = string.Empty;
    public string DES_INCOTERM { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_PERIODO { get; set; } = string.Empty;
    public string DES_PROYECTO { get; set; } = string.Empty;
    /// <summary>Dirección de la SUCURSAL. Lleva el sufijo porque el comprobante ya trae la
    /// dirección de la entidad en DES_DIRECCION: dos columnas con el mismo nombre se pisan
    /// entre sí y gana la última, así que la sucursal necesita nombre propio.</summary>
    public string DES_DIRECCION_SUCURSALES { get; set; } = string.Empty;
    public string DES_LINK_PATH_FACTURACION { get; set; } = string.Empty;
    public string DES_SUCURSAL { get; set; } = string.Empty;
    public string DES_TOKEN_FACTURACION { get; set; } = string.Empty;
    public string DES_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public string DES_TIPO_OPERACION { get; set; } = string.Empty;
    public string DES_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    public int? NUM_PERIODO { get; set; }
}

public class ComprobantesPagoInsertDto
{
    public decimal? NUM_REGISTRO { get; set; }
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public int? NUM_SECUENCIA_DIRECCION { get; set; }
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string NUM_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public DateTime? FEC_COMPROBANTE_PAGO { get; set; }
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public DateTime? FEC_RECEPCION { get; set; }
    public DateTime? FEC_DEVOLUCION { get; set; }
    public DateTime? FEC_EMBARQUE { get; set; }
    public DateTime? FEC_COMERCIAL { get; set; }
    public DateTime? FEC_REGISTRO_CAJA { get; set; }
    [Required] public string COD_CAJA { get; set; } = string.Empty;
    public int? NUM_UNIDAD_COMERCIAL { get; set; }
    public int? NUM_PROYECTO { get; set; }
    [Required] public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public int? COD_FORMA_PAGO { get; set; }
    public int? COD_CONDICION_PAGO { get; set; }
    public int? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    [Required] public string COD_INCOTERM { get; set; } = string.Empty;
    [Required] public string COD_CENTRO_COSTO_SOLICITANTE { get; set; } = string.Empty;
    [Required] public string COD_CENTRO_COSTO_DESTINO { get; set; } = string.Empty;
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_VENDEDOR { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_PERIODO_CONTABLE { get; set; }
    [Required] public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public int? NUM_ASIENTO_CONTABLE { get; set; }
    public int? COD_TIPO_ESTADO_PAGO { get; set; }
    public int? COD_ESTADO_PAGO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComprobantesPagoUpdateDto
{
    public decimal? NUM_REGISTRO { get; set; }
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public int? NUM_SECUENCIA_DIRECCION { get; set; }
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string NUM_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public DateTime? FEC_COMPROBANTE_PAGO { get; set; }
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public DateTime? FEC_RECEPCION { get; set; }
    public DateTime? FEC_DEVOLUCION { get; set; }
    public DateTime? FEC_EMBARQUE { get; set; }
    public DateTime? FEC_COMERCIAL { get; set; }
    public DateTime? FEC_REGISTRO_CAJA { get; set; }
    [Required] public string COD_CAJA { get; set; } = string.Empty;
    public int? NUM_UNIDAD_COMERCIAL { get; set; }
    public int? NUM_PROYECTO { get; set; }
    [Required] public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public int? COD_FORMA_PAGO { get; set; }
    public int? COD_CONDICION_PAGO { get; set; }
    public int? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    [Required] public string COD_INCOTERM { get; set; } = string.Empty;
    [Required] public string COD_CENTRO_COSTO_SOLICITANTE { get; set; } = string.Empty;
    [Required] public string COD_CENTRO_COSTO_DESTINO { get; set; } = string.Empty;
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_VENDEDOR { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_PERIODO_CONTABLE { get; set; }
    [Required] public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public int? NUM_ASIENTO_CONTABLE { get; set; }
    public int? COD_TIPO_ESTADO_PAGO { get; set; }
    public int? COD_ESTADO_PAGO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComprobantesPagoSearchDto
{
    public string? Search { get; set; }
}