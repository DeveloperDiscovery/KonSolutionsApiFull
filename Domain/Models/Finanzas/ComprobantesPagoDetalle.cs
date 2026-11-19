using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad COMPROBANTES_PAGO_DETALLE - tipos equivalentes a la BD.</summary>
public class ComprobantesPagoDetalle
{
    public decimal? NUM_REGISTRO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_SECUDARIO { get; set; }
    public string COD_UNIDAD_MEDIDA_SECUNDARIO { get; set; } = string.Empty;
    public int? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public decimal? PRE_UNITARIO { get; set; }
    public decimal? VAL_DESCUENTO { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA_PEDIDO_COMERCIAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_DESCUENTO { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
}

public class ComprobantesPagoDetalleInsertDto
{
    public decimal? NUM_REGISTRO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_SECUDARIO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_SECUNDARIO { get; set; } = string.Empty;
    public int? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public decimal? PRE_UNITARIO { get; set; }
    public decimal? VAL_DESCUENTO { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA_PEDIDO_COMERCIAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComprobantesPagoDetalleUpdateDto
{
    public decimal? NUM_REGISTRO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_SECUDARIO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_SECUNDARIO { get; set; } = string.Empty;
    public int? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public decimal? PRE_UNITARIO { get; set; }
    public decimal? VAL_DESCUENTO { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA_PEDIDO_COMERCIAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComprobantesPagoDetalleSearchDto
{
    public string? Search { get; set; }
}