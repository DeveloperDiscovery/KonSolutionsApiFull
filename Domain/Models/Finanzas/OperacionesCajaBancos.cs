using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad OPERACIONES_CAJA_BANCOS - tipos equivalentes a la BD.</summary>
public class OperacionesCajaBancos
{
    public decimal? NUM_OPERACION { get; set; }
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public DateTime? FEC_OPERACION { get; set; }
    public string COD_BANCO { get; set; } = string.Empty;
    public string NUM_CUENTA_BANCARIA { get; set; } = string.Empty;
    public decimal? NUM_OPERACION_BANCARIA { get; set; }
    public string COD_CAJA { get; set; } = string.Empty;
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public string NUM_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_RENDICION_CUENTAS { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public decimal? VAL_OPERACION { get; set; }
    public string COD_BANCO_DESTINO { get; set; } = string.Empty;
    public string NUM_CUENTA_BANCARIA_DESTINO { get; set; } = string.Empty;
    public string COD_CAJA_DESTINO { get; set; } = string.Empty;
    public string NUM_PERIODO_CONTABLE { get; set; } = string.Empty;
    public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public int? NUM_ASIENTO_CONTABLE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CAJA { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_TIPO_OPERACION { get; set; } = string.Empty;
}

public class OperacionesCajaBancosInsertDto
{
    public decimal? NUM_OPERACION { get; set; }
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public DateTime? FEC_OPERACION { get; set; }
    [Required] public string COD_BANCO { get; set; } = string.Empty;
    [Required] public string NUM_CUENTA_BANCARIA { get; set; } = string.Empty;
    public decimal? NUM_OPERACION_BANCARIA { get; set; }
    [Required] public string COD_CAJA { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string NUM_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_RENDICION_CUENTAS { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public decimal? VAL_OPERACION { get; set; }
    [Required] public string COD_BANCO_DESTINO { get; set; } = string.Empty;
    [Required] public string NUM_CUENTA_BANCARIA_DESTINO { get; set; } = string.Empty;
    [Required] public string COD_CAJA_DESTINO { get; set; } = string.Empty;
    [Required] public string NUM_PERIODO_CONTABLE { get; set; } = string.Empty;
    [Required] public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public int? NUM_ASIENTO_CONTABLE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class OperacionesCajaBancosUpdateDto
{
    public decimal? NUM_OPERACION { get; set; }
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public DateTime? FEC_OPERACION { get; set; }
    [Required] public string COD_BANCO { get; set; } = string.Empty;
    [Required] public string NUM_CUENTA_BANCARIA { get; set; } = string.Empty;
    public decimal? NUM_OPERACION_BANCARIA { get; set; }
    [Required] public string COD_CAJA { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string NUM_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_RENDICION_CUENTAS { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public decimal? VAL_OPERACION { get; set; }
    [Required] public string COD_BANCO_DESTINO { get; set; } = string.Empty;
    [Required] public string NUM_CUENTA_BANCARIA_DESTINO { get; set; } = string.Empty;
    [Required] public string COD_CAJA_DESTINO { get; set; } = string.Empty;
    [Required] public string NUM_PERIODO_CONTABLE { get; set; } = string.Empty;
    [Required] public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public int? NUM_ASIENTO_CONTABLE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class OperacionesCajaBancosSearchDto
{
    public string? Search { get; set; }
}