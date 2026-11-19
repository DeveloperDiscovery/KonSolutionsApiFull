using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad INCOTERMS - tipos equivalentes a la BD.</summary>
public class Incoterms
{
    public string COD_INCOTERM { get; set; } = string.Empty;
    public string DES_INCOTERM { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class IncotermsInsertDto
{
    [Required] public string COD_INCOTERM { get; set; } = string.Empty;
    [Required] public string DES_INCOTERM { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class IncotermsUpdateDto
{
    [Required] public string COD_INCOTERM { get; set; } = string.Empty;
    [Required] public string DES_INCOTERM { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class IncotermsSearchDto
{
    public string? Search { get; set; }
}