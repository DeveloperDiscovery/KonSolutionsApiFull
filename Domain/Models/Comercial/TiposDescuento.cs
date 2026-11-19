using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_DESCUENTO - tipos equivalentes a la BD.</summary>
public class TiposDescuento
{
    public int? COD_TIPO_DESCUENTO { get; set; }
    public string DES_TIPO_DESCUENTO { get; set; } = string.Empty;
    public decimal? TAS_DESCUENTO { get; set; }
}

public class TiposDescuentoInsertDto
{
    public int? COD_TIPO_DESCUENTO { get; set; }
    [Required] public string DES_TIPO_DESCUENTO { get; set; } = string.Empty;
    public decimal? TAS_DESCUENTO { get; set; }
}

public class TiposDescuentoUpdateDto
{
    public int? COD_TIPO_DESCUENTO { get; set; }
    [Required] public string DES_TIPO_DESCUENTO { get; set; } = string.Empty;
    public decimal? TAS_DESCUENTO { get; set; }
}

public class TiposDescuentoSearchDto
{
    public string? Search { get; set; }
}