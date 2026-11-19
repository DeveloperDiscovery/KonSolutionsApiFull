using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Costos;

/// <summary>Entidad CLASES_ELEMENTOS_COSTOS - tipos equivalentes a la BD.</summary>
public class ClasesElementosCostos
{
    public string COD_CLASE_COSTO { get; set; } = string.Empty;
    public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
    public string DES_CLASE_COSTO { get; set; } = string.Empty;
    public string DES_ELEMENTO_COSTO { get; set; } = string.Empty;
}

public class ClasesElementosCostosInsertDto
{
    [Required] public string COD_CLASE_COSTO { get; set; } = string.Empty;
    [Required] public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ClasesElementosCostosUpdateDto
{
    [Required] public string COD_CLASE_COSTO { get; set; } = string.Empty;
    [Required] public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ClasesElementosCostosSearchDto
{
    public string? Search { get; set; }
}