using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad INCOTERMS_FASES - tipos equivalentes a la BD.</summary>
public class IncotermsFases
{
    public string COD_INCOTERM { get; set; } = string.Empty;
    public string COD_FASE_INCOTERM { get; set; } = string.Empty;
    public bool? FLG_VENDEDOR { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public string DES_FASE_INCOTERM { get; set; } = string.Empty;
    public string DES_INCOTERM { get; set; } = string.Empty;
}

public class IncotermsFasesInsertDto
{
    [Required] public string COD_INCOTERM { get; set; } = string.Empty;
    [Required] public string COD_FASE_INCOTERM { get; set; } = string.Empty;
    public bool? FLG_VENDEDOR { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class IncotermsFasesUpdateDto
{
    [Required] public string COD_INCOTERM { get; set; } = string.Empty;
    [Required] public string COD_FASE_INCOTERM { get; set; } = string.Empty;
    public bool? FLG_VENDEDOR { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class IncotermsFasesSearchDto
{
    public string? Search { get; set; }
}