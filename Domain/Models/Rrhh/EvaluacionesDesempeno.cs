using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad EVALUACIONES_DESEMPENO - tipos equivalentes a la BD.</summary>
public class EvaluacionesDesempeno
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    public string DES_EVALUACION_DESEMPENO { get; set; } = string.Empty;
    public string COD_TIPO_EVALUACION { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public bool? FLG_EVALUA_GERENCIA { get; set; }
    public bool? FLG_EVALUA_JEFATURA { get; set; }
    public bool? FLG_EVALUA_SUBORDINADO { get; set; }
    public string DES_DETALLE { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_AUTORIZACION { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PATH_FILE { get; set; } = string.Empty;
    public string DES_QUERY_EXECUTE_NO { get; set; } = string.Empty;
    public string DES_QUERY_EXECUTE_SI { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TIPO_EVALUACION { get; set; } = string.Empty;
}

public class EvaluacionesDesempenoInsertDto
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    [Required] public string DES_EVALUACION_DESEMPENO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_EVALUACION { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public bool? FLG_EVALUA_GERENCIA { get; set; }
    public bool? FLG_EVALUA_JEFATURA { get; set; }
    public bool? FLG_EVALUA_SUBORDINADO { get; set; }
    [Required] public string DES_DETALLE { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EvaluacionesDesempenoUpdateDto
{
    public int? NUM_EVALUACION_DESEMPENO { get; set; }
    [Required] public string DES_EVALUACION_DESEMPENO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_EVALUACION { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public bool? FLG_EVALUA_GERENCIA { get; set; }
    public bool? FLG_EVALUA_JEFATURA { get; set; }
    public bool? FLG_EVALUA_SUBORDINADO { get; set; }
    [Required] public string DES_DETALLE { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EvaluacionesDesempenoSearchDto
{
    public string? Search { get; set; }
}