using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PERSONAL_CUENTA_CORRIENTE_DETALLE - tipos equivalentes a la BD.</summary>
public class PersonalCuentaCorrienteDetalle
{
    public int? NUM_CUENTA_CORRIENTE { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_AMORTIZACION { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_AMORTIZACION { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_DETALLE { get; set; } = string.Empty;
}

public class PersonalCuentaCorrienteDetalleInsertDto
{
    public int? NUM_CUENTA_CORRIENTE { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_AMORTIZACION { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_AMORTIZACION { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalCuentaCorrienteDetalleUpdateDto
{
    public int? NUM_CUENTA_CORRIENTE { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_AMORTIZACION { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_AMORTIZACION { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalCuentaCorrienteDetalleSearchDto
{
    public string? Search { get; set; }
}