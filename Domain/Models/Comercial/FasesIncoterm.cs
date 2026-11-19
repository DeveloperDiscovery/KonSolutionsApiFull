using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad FASES_INCOTERM - tipos equivalentes a la BD.</summary>
public class FasesIncoterm
{
    public string COD_FASE_INCOTERM { get; set; } = string.Empty;
    public string DES_FASE_INCOTERM { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class FasesIncotermInsertDto
{
    [Required] public string COD_FASE_INCOTERM { get; set; } = string.Empty;
    [Required] public string DES_FASE_INCOTERM { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class FasesIncotermUpdateDto
{
    [Required] public string COD_FASE_INCOTERM { get; set; } = string.Empty;
    [Required] public string DES_FASE_INCOTERM { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class FasesIncotermSearchDto
{
    public string? Search { get; set; }
}