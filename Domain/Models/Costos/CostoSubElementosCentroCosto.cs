using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Costos;

/// <summary>Entidad COSTO_SUB_ELEMENTOS_CENTRO_COSTO - tipos equivalentes a la BD.</summary>
public class CostoSubElementosCentroCosto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    public string COD_SUB_ELEMENTO_COSTO { get; set; } = string.Empty;
    public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public decimal? VAL_COSTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CENTRO_COSTO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_SUB_ELEMENTO_COSTO { get; set; } = string.Empty;
}

public class CostoSubElementosCentroCostoInsertDto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    [Required] public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public decimal? VAL_COSTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CostoSubElementosCentroCostoUpdateDto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    [Required] public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public decimal? VAL_COSTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CostoSubElementosCentroCostoSearchDto
{
    public string? Search { get; set; }
}