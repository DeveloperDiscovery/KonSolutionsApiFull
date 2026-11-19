using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_TRANSPORTE (COMERCIAL) - tipos equivalentes a la BD.</summary>
public class ComercialTiposTransporte
{
    public string COD_TIPO_TRANSPORTE { get; set; } = string.Empty;
    public string DES_TIPO_TRANSPORTE { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ComercialTiposTransporteInsertDto
{
    [Required] public string COD_TIPO_TRANSPORTE { get; set; } = string.Empty;
    [Required] public string DES_TIPO_TRANSPORTE { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ComercialTiposTransporteUpdateDto
{
    [Required] public string COD_TIPO_TRANSPORTE { get; set; } = string.Empty;
    [Required] public string DES_TIPO_TRANSPORTE { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ComercialTiposTransporteSearchDto
{
    public string? Search { get; set; }
}