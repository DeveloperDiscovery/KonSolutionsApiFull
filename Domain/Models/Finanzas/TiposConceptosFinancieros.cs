using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad TIPOS_CONCEPTOS_FINANCIEROS - tipos equivalentes a la BD.</summary>
public class TiposConceptosFinancieros
{
    public string COD_TIPO_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    public string DES_TIPO_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
}

public class TiposConceptosFinancierosInsertDto
{
    [Required] public string COD_TIPO_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
}

public class TiposConceptosFinancierosUpdateDto
{
    [Required] public string COD_TIPO_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
}

public class TiposConceptosFinancierosSearchDto
{
    public string? Search { get; set; }
}