using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad PLANILLA_REMUNERACIONES - tipos equivalentes a la BD.</summary>
public class PlanillaRemuneraciones
{
    public int? NUM_PLANILLA_REMUNERATIVA { get; set; }
    public string DES_PLANILLA_REMUNERATIVA { get; set; } = string.Empty;
    public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    public int? NUM_PERIODO_REMUNERATIVO { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class PlanillaRemuneracionesInsertDto
{
    public int? NUM_PLANILLA_REMUNERATIVA { get; set; }
    [Required] public string DES_PLANILLA_REMUNERATIVA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    public int? NUM_PERIODO_REMUNERATIVO { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PlanillaRemuneracionesUpdateDto
{
    public int? NUM_PLANILLA_REMUNERATIVA { get; set; }
    [Required] public string DES_PLANILLA_REMUNERATIVA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    public int? NUM_PERIODO_REMUNERATIVO { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PlanillaRemuneracionesSearchDto
{
    public string? Search { get; set; }
}