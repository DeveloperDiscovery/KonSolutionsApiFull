using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad TIPOS_CONCEPTO_REMUNERATIVO - tipos equivalentes a la BD.</summary>
public class TiposConceptoRemunerativo
{
    public string COD_TIPO_REMUNERATIVO { get; set; } = string.Empty;
    public string DES_TIPO_REMUNERATIVO { get; set; } = string.Empty;
}

public class TiposConceptoRemunerativoInsertDto
{
    [Required] public string COD_TIPO_REMUNERATIVO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_REMUNERATIVO { get; set; } = string.Empty;
}

public class TiposConceptoRemunerativoUpdateDto
{
    [Required] public string COD_TIPO_REMUNERATIVO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_REMUNERATIVO { get; set; } = string.Empty;
}

public class TiposConceptoRemunerativoSearchDto
{
    public string? Search { get; set; }
}