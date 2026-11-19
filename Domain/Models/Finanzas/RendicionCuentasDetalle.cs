using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad RENDICION_CUENTAS_DETALLE - tipos equivalentes a la BD.</summary>
public class RendicionCuentasDetalle
{
    public int? NUM_RENDICION_CUENTAS { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_TIPO_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    public string COD_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public string NUM_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public DateTime? FEC_COMPROBANTE_PAGO { get; set; }
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal? NUM_REGISTRO { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_OBSERVACIONES { get; set; } = string.Empty;
    public string DES_RENDICION_CUENTAS { get; set; } = string.Empty;
    public string DES_TIPO_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    public string DES_TIPO_OPERACION { get; set; } = string.Empty;
}

public class RendicionCuentasDetalleInsertDto
{
    public int? NUM_RENDICION_CUENTAS { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    [Required] public string COD_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string NUM_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public DateTime? FEC_COMPROBANTE_PAGO { get; set; }
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal? NUM_REGISTRO { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RendicionCuentasDetalleUpdateDto
{
    public int? NUM_RENDICION_CUENTAS { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    [Required] public string COD_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string NUM_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public DateTime? FEC_COMPROBANTE_PAGO { get; set; }
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal? NUM_REGISTRO { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RendicionCuentasDetalleSearchDto
{
    public string? Search { get; set; }
}