using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad CLASES_ELEMENTO_FORMULA - tipos equivalentes a la BD.</summary>
public class ClasesElementoFormula
{
    public int? COD_CLASE_ELEMENTO_FORMULA { get; set; }
    public string DES_CLASE_ELEMENTO_FORMULA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class ClasesElementoFormulaInsertDto
{
    public int? COD_CLASE_ELEMENTO_FORMULA { get; set; }
    [Required] public string DES_CLASE_ELEMENTO_FORMULA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class ClasesElementoFormulaUpdateDto
{
    public int? COD_CLASE_ELEMENTO_FORMULA { get; set; }
    [Required] public string DES_CLASE_ELEMENTO_FORMULA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class ClasesElementoFormulaSearchDto
{
    public string? Search { get; set; }
}