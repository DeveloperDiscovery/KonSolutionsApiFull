using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad TIPOS_CUENTA_CORRIENTE - tipos equivalentes a la BD.</summary>
public class TiposCuentaCorriente
{
    public string COD_TIPO_CUENTA_CORRIENTE { get; set; } = string.Empty;
    public string DES_TIPO_CUENTA_CORRIENTE { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_CONCEPTO_REMUNERATIVO { get; set; } = string.Empty;
}

public class TiposCuentaCorrienteInsertDto
{
    [Required] public string COD_TIPO_CUENTA_CORRIENTE { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CUENTA_CORRIENTE { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposCuentaCorrienteUpdateDto
{
    [Required] public string COD_TIPO_CUENTA_CORRIENTE { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CUENTA_CORRIENTE { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposCuentaCorrienteSearchDto
{
    public string? Search { get; set; }
}