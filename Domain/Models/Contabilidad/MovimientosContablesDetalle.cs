using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Contabilidad;

/// <summary>Entidad MOVIMIENTOS_CONTABLES_DETALLE - tipos equivalentes a la BD.</summary>
public class MovimientosContablesDetalle
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public int? NUM_ASIENTO_CONTABLE { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public string NUM_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public DateTime? FEC_COMPROBANTE_PAGO { get; set; }
    public DateTime? FEC_VENCMIENTO { get; set; }
    public decimal? COD_ENTIDAD_CONTABLE { get; set; }
    public int? NUM_PROYECTO { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public string COD_CUENTA_NATURALEZA { get; set; } = string.Empty;
    public decimal? VAL_IMPORTE_CONTABLE { get; set; }
    public decimal? VAL_IMPORTE_CONTABLE_ME { get; set; }
    public string DES_GLOSA_CONTABLE_DETALLE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CENTRO_COSTO { get; set; } = string.Empty;
    public string DES_CUENTA_CONTABLE { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_GLOSA_CONTABLE { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PROYECTO { get; set; } = string.Empty;
    public string DES_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class MovimientosContablesDetalleInsertDto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    [Required] public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public int? NUM_ASIENTO_CONTABLE { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    [Required] public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string NUM_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public DateTime? FEC_COMPROBANTE_PAGO { get; set; }
    public DateTime? FEC_VENCMIENTO { get; set; }
    public decimal? COD_ENTIDAD_CONTABLE { get; set; }
    public int? NUM_PROYECTO { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    [Required] public string COD_CUENTA_NATURALEZA { get; set; } = string.Empty;
    public decimal? VAL_IMPORTE_CONTABLE { get; set; }
    public decimal? VAL_IMPORTE_CONTABLE_ME { get; set; }
    [Required] public string DES_GLOSA_CONTABLE_DETALLE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MovimientosContablesDetalleUpdateDto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    [Required] public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public int? NUM_ASIENTO_CONTABLE { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    [Required] public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string NUM_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public DateTime? FEC_COMPROBANTE_PAGO { get; set; }
    public DateTime? FEC_VENCMIENTO { get; set; }
    public decimal? COD_ENTIDAD_CONTABLE { get; set; }
    public int? NUM_PROYECTO { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    [Required] public string COD_CUENTA_NATURALEZA { get; set; } = string.Empty;
    public decimal? VAL_IMPORTE_CONTABLE { get; set; }
    public decimal? VAL_IMPORTE_CONTABLE_ME { get; set; }
    [Required] public string DES_GLOSA_CONTABLE_DETALLE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MovimientosContablesDetalleSearchDto
{
    public string? Search { get; set; }
}