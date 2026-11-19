using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Networking;

/// <summary>Entidad LIQUIDACIONES_COMISION_VENTA_DETALLE - tipos equivalentes a la BD.</summary>
public class LiquidacionesComisionVentaDetalle
{
    public int? NUM_LIQUIDACION_COMISION_VENTA { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? NUM_REGISTRO_COMPROBANTE_PAGO { get; set; }
    public int? NUM_SECUENCIA_COMPROBANTE_PAGO { get; set; }
    public DateTime? FEC_VENTA { get; set; }
    public DateTime? FEC_ENTREGA { get; set; }
    public string COD_CATEGORIA_COMISION { get; set; } = string.Empty;
    public string COD_TIPO_CATEGORIA_COMISION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_ASESOR_COMERCIAL { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public decimal? TAS_COMISION_VENTA { get; set; }
    public decimal? TAS_COMISON_REFERENCIADA { get; set; }
    public decimal? VAL_COMISION_VENTA { get; set; }
    public decimal? VAL_COMISON_REFERENCIADA { get; set; }
    public decimal? VAL_COMISION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_CATEGORIA_COMISION { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_TIPO_COMISION { get; set; } = string.Empty;
    public decimal? NUM_REGISTRO { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
}

public class LiquidacionesComisionVentaDetalleInsertDto
{
    public int? NUM_LIQUIDACION_COMISION_VENTA { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? NUM_REGISTRO_COMPROBANTE_PAGO { get; set; }
    public int? NUM_SECUENCIA_COMPROBANTE_PAGO { get; set; }
    public DateTime? FEC_VENTA { get; set; }
    public DateTime? FEC_ENTREGA { get; set; }
    [Required] public string COD_CATEGORIA_COMISION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_CATEGORIA_COMISION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_ASESOR_COMERCIAL { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public decimal? TAS_COMISION_VENTA { get; set; }
    public decimal? TAS_COMISON_REFERENCIADA { get; set; }
    public decimal? VAL_COMISION_VENTA { get; set; }
    public decimal? VAL_COMISON_REFERENCIADA { get; set; }
    public decimal? VAL_COMISION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class LiquidacionesComisionVentaDetalleUpdateDto
{
    public int? NUM_LIQUIDACION_COMISION_VENTA { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? NUM_REGISTRO_COMPROBANTE_PAGO { get; set; }
    public int? NUM_SECUENCIA_COMPROBANTE_PAGO { get; set; }
    public DateTime? FEC_VENTA { get; set; }
    public DateTime? FEC_ENTREGA { get; set; }
    [Required] public string COD_CATEGORIA_COMISION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_CATEGORIA_COMISION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_ASESOR_COMERCIAL { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public decimal? TAS_COMISION_VENTA { get; set; }
    public decimal? TAS_COMISON_REFERENCIADA { get; set; }
    public decimal? VAL_COMISION_VENTA { get; set; }
    public decimal? VAL_COMISON_REFERENCIADA { get; set; }
    public decimal? VAL_COMISION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class LiquidacionesComisionVentaDetalleSearchDto
{
    public string? Search { get; set; }
}