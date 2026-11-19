using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad UNIDADES_MEDIDA_EQUIVALENCIAS - tipos equivalentes a la BD.</summary>
public class UnidadesMedidaEquivalencias
{
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA_EQUIVALENTE { get; set; } = string.Empty;
    public decimal? VAL_EQUIVALENTE { get; set; }
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
}

public class UnidadesMedidaEquivalenciasInsertDto
{
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_EQUIVALENTE { get; set; } = string.Empty;
    [Required(ErrorMessage = "El factor de equivalencia es obligatorio.")]
    [Range(typeof(decimal), "0.0000000001", "99999999.9999999999", ParseLimitsInInvariantCulture = true, ErrorMessage = "El factor de equivalencia tiene que ser mayor que cero y menor que 100000000.")]
    public decimal? VAL_EQUIVALENTE { get; set; }
}

public class UnidadesMedidaEquivalenciasUpdateDto
{
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_EQUIVALENTE { get; set; } = string.Empty;
    [Required(ErrorMessage = "El factor de equivalencia es obligatorio.")]
    [Range(typeof(decimal), "0.0000000001", "99999999.9999999999", ParseLimitsInInvariantCulture = true, ErrorMessage = "El factor de equivalencia tiene que ser mayor que cero y menor que 100000000.")]
    public decimal? VAL_EQUIVALENTE { get; set; }
}

public class UnidadesMedidaEquivalenciasSearchDto
{
    public string? Search { get; set; }
}