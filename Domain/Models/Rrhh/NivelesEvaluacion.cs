using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad NIVELES_EVALUACION - tipos equivalentes a la BD.</summary>
public class NivelesEvaluacion
{
    public int? NUM_NIVEL_EVALUACION { get; set; }
    public string DES_NIVEL_EVALUACION { get; set; } = string.Empty;
    public int? VAL_EVALUACION { get; set; }
}

public class NivelesEvaluacionInsertDto
{
    public int? NUM_NIVEL_EVALUACION { get; set; }
    [Required] public string DES_NIVEL_EVALUACION { get; set; } = string.Empty;
    public int? VAL_EVALUACION { get; set; }
}

public class NivelesEvaluacionUpdateDto
{
    public int? NUM_NIVEL_EVALUACION { get; set; }
    [Required] public string DES_NIVEL_EVALUACION { get; set; } = string.Empty;
    public int? VAL_EVALUACION { get; set; }
}

public class NivelesEvaluacionSearchDto
{
    public string? Search { get; set; }
}