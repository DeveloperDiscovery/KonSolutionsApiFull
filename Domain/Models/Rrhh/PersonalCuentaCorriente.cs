using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PERSONAL_CUENTA_CORRIENTE - tipos equivalentes a la BD.</summary>
public class PersonalCuentaCorriente
{
    public int? NUM_CUENTA_CORRIENTE { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public string COD_TIPO_CUENTA_CORRIENTE { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO_CARGO { get; set; }
    public DateTime? FEC_CUENTA_CORRIENTE { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_IMPORTE { get; set; }
    public decimal? VAL_AMORTIZACION { get; set; }
    public decimal? VAL_SALDO { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public string DES_DETALLE { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_CONCEPTO_REMUNERATIVO { get; set; } = string.Empty;
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
    public string DES_MOTIVO { get; set; } = string.Empty;
    public string DES_TIPO_CUENTA_CORRIENTE { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
}

public class PersonalCuentaCorrienteInsertDto
{
    public int? NUM_CUENTA_CORRIENTE { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    [Required] public string COD_TIPO_CUENTA_CORRIENTE { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO_CARGO { get; set; }
    public DateTime? FEC_CUENTA_CORRIENTE { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_IMPORTE { get; set; }
    public decimal? VAL_AMORTIZACION { get; set; }
    public decimal? VAL_SALDO { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    [Required] public string DES_DETALLE { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalCuentaCorrienteUpdateDto
{
    public int? NUM_CUENTA_CORRIENTE { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    [Required] public string COD_TIPO_CUENTA_CORRIENTE { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO_CARGO { get; set; }
    public DateTime? FEC_CUENTA_CORRIENTE { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_IMPORTE { get; set; }
    public decimal? VAL_AMORTIZACION { get; set; }
    public decimal? VAL_SALDO { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    [Required] public string DES_DETALLE { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalCuentaCorrienteSearchDto
{
    public string? Search { get; set; }
}