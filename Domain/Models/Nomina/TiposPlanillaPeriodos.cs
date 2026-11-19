using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad TIPOS_PLANILLA_PERIODOS - tipos equivalentes a la BD.</summary>
public class TiposPlanillaPeriodos
{
    public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    public string COD_TIPO_PERIODO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_TIPO_PLANILLA { get; set; } = string.Empty;
    public string DES_PREFIJO { get; set; } = string.Empty;
    public string DES_TIPO_PERIODO { get; set; } = string.Empty;
}

public class TiposPlanillaPeriodosInsertDto
{
    [Required] public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PERIODO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposPlanillaPeriodosUpdateDto
{
    [Required] public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PERIODO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposPlanillaPeriodosSearchDto
{
    public string? Search { get; set; }
}