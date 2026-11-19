using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad BENEFICIOS_LABORALES - tipos equivalentes a la BD.</summary>
public class BeneficiosLaborales
{
    public string COD_BENEFICIO_LABORAL { get; set; } = string.Empty;
    public string DES_BENEFICIO_LABORAL { get; set; } = string.Empty;
}

public class BeneficiosLaboralesInsertDto
{
    [Required] public string COD_BENEFICIO_LABORAL { get; set; } = string.Empty;
    [Required] public string DES_BENEFICIO_LABORAL { get; set; } = string.Empty;
}

public class BeneficiosLaboralesUpdateDto
{
    [Required] public string COD_BENEFICIO_LABORAL { get; set; } = string.Empty;
    [Required] public string DES_BENEFICIO_LABORAL { get; set; } = string.Empty;
}

public class BeneficiosLaboralesSearchDto
{
    public string? Search { get; set; }
}