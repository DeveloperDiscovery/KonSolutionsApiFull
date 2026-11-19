using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad EVALUACIONES_DESEMPENO_METRICAS - tipos equivalentes a la BD.</summary>
public class EvaluacionesDesempenoMetricas
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    public int? COD_AREA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL_EVALUADO { get; set; }
    public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    public string COD_COMPETENCIA { get; set; } = string.Empty;
    public int? COD_CARGO { get; set; }
    public int? VAL_POTENCIAL { get; set; }
    public int? VAL_EVALUADO { get; set; }
    public decimal? TAS_EVALUACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_AREA { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_DETALLE { get; set; } = string.Empty;
    public string DES_EVALUACION_DESEMPENO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class EvaluacionesDesempenoMetricasInsertDto
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    public int? COD_AREA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL_EVALUADO { get; set; }
    [Required] public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    [Required] public string COD_COMPETENCIA { get; set; } = string.Empty;
    public int? COD_CARGO { get; set; }
    public int? VAL_POTENCIAL { get; set; }
    public int? VAL_EVALUADO { get; set; }
    public decimal? TAS_EVALUACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EvaluacionesDesempenoMetricasUpdateDto
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    public int? COD_AREA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL_EVALUADO { get; set; }
    [Required] public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    [Required] public string COD_COMPETENCIA { get; set; } = string.Empty;
    public int? COD_CARGO { get; set; }
    public int? VAL_POTENCIAL { get; set; }
    public int? VAL_EVALUADO { get; set; }
    public decimal? TAS_EVALUACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EvaluacionesDesempenoMetricasSearchDto
{
    public string? Search { get; set; }
}