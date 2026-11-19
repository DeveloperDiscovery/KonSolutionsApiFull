using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad TIPOS_VIA - tipos equivalentes a la BD.</summary>
public class TiposVia
{
    public string COD_TIPO_VIA { get; set; } = string.Empty;
    public string DES_TIPO_VIA { get; set; } = string.Empty;
}

public class TiposViaInsertDto
{
    [Required] public string COD_TIPO_VIA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_VIA { get; set; } = string.Empty;
}

public class TiposViaUpdateDto
{
    [Required] public string COD_TIPO_VIA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_VIA { get; set; } = string.Empty;
}

public class TiposViaSearchDto
{
    public string? Search { get; set; }
}