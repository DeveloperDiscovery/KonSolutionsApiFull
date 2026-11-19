using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad PLANILLAS_REMUNERACIONES_DETALLE - tipos equivalentes a la BD.</summary>
public class PlanillasRemuneracionesDetalle
{
    public int? NUM_PLANILLA_REMUNERATIVA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA_TIEMPO { get; set; } = string.Empty;
    public int? CAN_TIEMPO { get; set; }
    public decimal? VAL_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? TAS_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_CONCEPTO_REMUNERATIVO { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
}

public class PlanillasRemuneracionesDetalleInsertDto
{
    public int? NUM_PLANILLA_REMUNERATIVA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_TIEMPO { get; set; } = string.Empty;
    public int? CAN_TIEMPO { get; set; }
    public decimal? VAL_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? TAS_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PlanillasRemuneracionesDetalleUpdateDto
{
    public int? NUM_PLANILLA_REMUNERATIVA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_TIEMPO { get; set; } = string.Empty;
    public int? CAN_TIEMPO { get; set; }
    public decimal? VAL_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? TAS_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PlanillasRemuneracionesDetalleSearchDto
{
    public string? Search { get; set; }
}