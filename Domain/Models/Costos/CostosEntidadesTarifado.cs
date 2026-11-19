using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Costos;

/// <summary>Entidad COSTOS_ENTIDADES_TARIFADO - tipos equivalentes a la BD.</summary>
public class CostosEntidadesTarifado
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    public string COD_PROCESO { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public decimal? VAL_TARIFADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_CENTRO_COSTO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
}

public class CostosEntidadesTarifadoInsertDto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public decimal? VAL_TARIFADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CostosEntidadesTarifadoUpdateDto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public decimal? VAL_TARIFADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CostosEntidadesTarifadoSearchDto
{
    public string? Search { get; set; }
}