using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad PLANILLAS_REMUNERACIONES_PERSONAL - tipos equivalentes a la BD.</summary>
public class PlanillasRemuneracionesPersonal
{
    public int? NUM_PLANILLA_REMUNERATIVA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_NETO_PAGAR { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PLANILLA_REMUNERATIVA { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class PlanillasRemuneracionesPersonalInsertDto
{
    public int? NUM_PLANILLA_REMUNERATIVA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_NETO_PAGAR { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PlanillasRemuneracionesPersonalUpdateDto
{
    public int? NUM_PLANILLA_REMUNERATIVA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_NETO_PAGAR { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PlanillasRemuneracionesPersonalSearchDto
{
    public string? Search { get; set; }
}