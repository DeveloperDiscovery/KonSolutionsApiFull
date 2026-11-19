using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad EVALUACIONES_DESEMPENO_COMPETENCIAS - tipos equivalentes a la BD.</summary>
public class EvaluacionesDesempenoCompetencias
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    public string COD_COMPETENCIA { get; set; } = string.Empty;
    public int? NUM_NIVEL_EVALUACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_COMPETENCIA { get; set; } = string.Empty;
    public string DES_DETALLE { get; set; } = string.Empty;
}

public class EvaluacionesDesempenoCompetenciasInsertDto
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    [Required] public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    [Required] public string COD_COMPETENCIA { get; set; } = string.Empty;
    public int? NUM_NIVEL_EVALUACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EvaluacionesDesempenoCompetenciasUpdateDto
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    [Required] public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    [Required] public string COD_COMPETENCIA { get; set; } = string.Empty;
    public int? NUM_NIVEL_EVALUACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EvaluacionesDesempenoCompetenciasSearchDto
{
    public string? Search { get; set; }
}