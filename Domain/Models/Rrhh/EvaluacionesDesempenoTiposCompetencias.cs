using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad EVALUACIONES_DESEMPENO_TIPOS_COMPETENCIAS - tipos equivalentes a la BD.</summary>
public class EvaluacionesDesempenoTiposCompetencias
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    public string DES_DETALLE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_EVALUACION_DESEMPENO { get; set; } = string.Empty;
    public string DES_TIPO_COMPETENCIA { get; set; } = string.Empty;
}

public class EvaluacionesDesempenoTiposCompetenciasInsertDto
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    [Required] public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    [Required] public string DES_DETALLE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EvaluacionesDesempenoTiposCompetenciasUpdateDto
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    [Required] public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    [Required] public string DES_DETALLE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EvaluacionesDesempenoTiposCompetenciasSearchDto
{
    public string? Search { get; set; }
}