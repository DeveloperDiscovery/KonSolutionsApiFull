using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Costos;

/// <summary>Entidad SUB_ELEMENTOS_COSTO - tipos equivalentes a la BD.</summary>
public class SubElementosCosto
{
    public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    public string COD_SUB_ELEMENTO_COSTO { get; set; } = string.Empty;
    public string DES_SUB_ELEMENTO_COSTO { get; set; } = string.Empty;
    public string COD_CATEGORIA_COMPORTAMIENTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
    public string DES_CATEGORIA_COMPORTAMIENTO { get; set; } = string.Empty;
    public string DES_ELEMENTO_COSTO { get; set; } = string.Empty;
}

public class SubElementosCostoInsertDto
{
    [Required] public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string DES_SUB_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_CATEGORIA_COMPORTAMIENTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class SubElementosCostoUpdateDto
{
    [Required] public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string DES_SUB_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_CATEGORIA_COMPORTAMIENTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class SubElementosCostoSearchDto
{
    public string? Search { get; set; }
}