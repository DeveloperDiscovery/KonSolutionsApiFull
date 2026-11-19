using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad TIPOS_EVALUACION - tipos equivalentes a la BD.</summary>
public class TiposEvaluacion
{
    public string COD_TIPO_EVALUACION { get; set; } = string.Empty;
    public string DES_TIPO_EVALUACION { get; set; } = string.Empty;
    public bool? FLG_DESEMPENO { get; set; }
}

public class TiposEvaluacionInsertDto
{
    [Required] public string COD_TIPO_EVALUACION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_EVALUACION { get; set; } = string.Empty;
    public bool? FLG_DESEMPENO { get; set; }
}

public class TiposEvaluacionUpdateDto
{
    [Required] public string COD_TIPO_EVALUACION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_EVALUACION { get; set; } = string.Empty;
    public bool? FLG_DESEMPENO { get; set; }
}

public class TiposEvaluacionSearchDto
{
    public string? Search { get; set; }
}