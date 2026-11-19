using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad REGIMENES_LABORALES - tipos equivalentes a la BD.</summary>
public class RegimenesLaborales
{
    public string COD_REGIMEN_LABORAL { get; set; } = string.Empty;
    public string DES_REGIMEN_LABORAL { get; set; } = string.Empty;
}

public class RegimenesLaboralesInsertDto
{
    [Required] public string COD_REGIMEN_LABORAL { get; set; } = string.Empty;
    [Required] public string DES_REGIMEN_LABORAL { get; set; } = string.Empty;
}

public class RegimenesLaboralesUpdateDto
{
    [Required] public string COD_REGIMEN_LABORAL { get; set; } = string.Empty;
    [Required] public string DES_REGIMEN_LABORAL { get; set; } = string.Empty;
}

public class RegimenesLaboralesSearchDto
{
    public string? Search { get; set; }
}