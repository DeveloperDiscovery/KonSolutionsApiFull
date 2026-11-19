using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad PLANILLAS_REMUNERACIONES_FORMULAS - tipos equivalentes a la BD.</summary>
public class PlanillasRemuneracionesFormulas
{
    public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public int? NUM_SECUENCIA_FORMULA { get; set; }
    public int? COD_CLASE_ELEMENTO_FORMULA { get; set; }
    public int? COD_TIPO_ELEMENTO_FORMULA { get; set; }
    public string DES_ELEMENTO_FORMULA { get; set; } = string.Empty;
    public string DES_ELEMENTO_MASCARA_FORMULA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? NUM_ORDEN_CALCULO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_FORMULA { get; set; } = string.Empty;
    public string DES_FORMULA_MASCARA { get; set; } = string.Empty;
}

public class PlanillasRemuneracionesFormulasInsertDto
{
    [Required] public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public int? NUM_SECUENCIA_FORMULA { get; set; }
    public int? COD_CLASE_ELEMENTO_FORMULA { get; set; }
    public int? COD_TIPO_ELEMENTO_FORMULA { get; set; }
    [Required] public string DES_ELEMENTO_FORMULA { get; set; } = string.Empty;
    [Required] public string DES_ELEMENTO_MASCARA_FORMULA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? NUM_ORDEN_CALCULO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PlanillasRemuneracionesFormulasUpdateDto
{
    [Required] public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public int? NUM_SECUENCIA_FORMULA { get; set; }
    public int? COD_CLASE_ELEMENTO_FORMULA { get; set; }
    public int? COD_TIPO_ELEMENTO_FORMULA { get; set; }
    [Required] public string DES_ELEMENTO_FORMULA { get; set; } = string.Empty;
    [Required] public string DES_ELEMENTO_MASCARA_FORMULA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? NUM_ORDEN_CALCULO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PlanillasRemuneracionesFormulasSearchDto
{
    public string? Search { get; set; }
}