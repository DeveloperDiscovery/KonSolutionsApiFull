using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad EVALUACIONES_DESEMPENO_PERSONAL_EVALUACION - tipos equivalentes a la BD.</summary>
public class EvaluacionesDesempenoPersonalEvaluacion
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL_EVALUADOR { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL_EVALUADO { get; set; }
    public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    public string COD_COMPETENCIA { get; set; } = string.Empty;
    public int? NUM_NIVEL_EVALUACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_DETALLE { get; set; } = string.Empty;
    public string DES_NIVEL_EVALUACION { get; set; } = string.Empty;
}

public class EvaluacionesDesempenoPersonalEvaluacionInsertDto
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL_EVALUADOR { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL_EVALUADO { get; set; }
    [Required] public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    [Required] public string COD_COMPETENCIA { get; set; } = string.Empty;
    public int? NUM_NIVEL_EVALUACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EvaluacionesDesempenoPersonalEvaluacionUpdateDto
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL_EVALUADOR { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL_EVALUADO { get; set; }
    [Required] public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    [Required] public string COD_COMPETENCIA { get; set; } = string.Empty;
    public int? NUM_NIVEL_EVALUACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EvaluacionesDesempenoPersonalEvaluacionSearchDto
{
    public string? Search { get; set; }
}