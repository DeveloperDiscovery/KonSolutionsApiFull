using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad REGLAS_COMBO - tipos equivalentes a la BD.</summary>
public class ReglasCombo
{
    public string COD_REGLA_COMBOS { get; set; } = string.Empty;
    public string DES_REGLA_COMBOS { get; set; } = string.Empty;
    public bool? FLG_FIJO { get; set; }
    public bool? FLG_VERSUS { get; set; }
}

public class ReglasComboInsertDto
{
    [Required] public string COD_REGLA_COMBOS { get; set; } = string.Empty;
    [Required] public string DES_REGLA_COMBOS { get; set; } = string.Empty;
    public bool? FLG_FIJO { get; set; }
    public bool? FLG_VERSUS { get; set; }
}

public class ReglasComboUpdateDto
{
    [Required] public string COD_REGLA_COMBOS { get; set; } = string.Empty;
    [Required] public string DES_REGLA_COMBOS { get; set; } = string.Empty;
    public bool? FLG_FIJO { get; set; }
    public bool? FLG_VERSUS { get; set; }
}

public class ReglasComboSearchDto
{
    public string? Search { get; set; }
}