using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Costos;

/// <summary>Entidad COSTOS_PRODUCCION - tipos equivalentes a la BD.</summary>
public class CostosProduccion
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    public string COD_SUB_ELEMENTO_COSTO { get; set; } = string.Empty;
    public decimal? VAL_COSTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_SUB_ELEMENTO_COSTO { get; set; } = string.Empty;
}

public class CostosProduccionInsertDto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    [Required] public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_ELEMENTO_COSTO { get; set; } = string.Empty;
    public decimal? VAL_COSTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CostosProduccionUpdateDto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    [Required] public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_ELEMENTO_COSTO { get; set; } = string.Empty;
    public decimal? VAL_COSTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CostosProduccionSearchDto
{
    public string? Search { get; set; }
}