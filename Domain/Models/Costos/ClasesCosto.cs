using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Costos;

/// <summary>Entidad CLASES_COSTO - tipos equivalentes a la BD.</summary>
public class ClasesCosto
{
    public string COD_CLASE_COSTO { get; set; } = string.Empty;
    public string DES_CLASE_COSTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ClasesCostoInsertDto
{
    [Required] public string COD_CLASE_COSTO { get; set; } = string.Empty;
    [Required] public string DES_CLASE_COSTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ClasesCostoUpdateDto
{
    [Required] public string COD_CLASE_COSTO { get; set; } = string.Empty;
    [Required] public string DES_CLASE_COSTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ClasesCostoSearchDto
{
    public string? Search { get; set; }
}