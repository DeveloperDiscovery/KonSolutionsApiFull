using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

public class TiposOperacionCalculoAfectados
{
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    public string COD_TIPO_VALOR_CALCULO_AFECTADO { get; set; } = string.Empty;
    public string DES_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
}

public class TiposOperacionCalculoAfectadosInsertDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_VALOR_CALCULO_AFECTADO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposOperacionCalculoAfectadosSearchDto
{
    public string? Search { get; set; }
}
