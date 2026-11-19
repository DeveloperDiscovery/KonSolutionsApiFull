using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad UBIGEOS - tipos equivalentes a la BD.</summary>
public class Ubigeos
{
    public string COD_UBIGEO { get; set; } = string.Empty;
    public string DES_UBIGEO { get; set; } = string.Empty;
    public bool? FLG_DEPARTAMENTO { get; set; }
    public bool? FLG_PROVINCIA { get; set; }
    public bool? FLG_DISTRITO { get; set; }
}

public class UbigeosInsertDto
{
    [Required] public string COD_UBIGEO { get; set; } = string.Empty;
    [Required] public string DES_UBIGEO { get; set; } = string.Empty;
    public bool? FLG_DEPARTAMENTO { get; set; }
    public bool? FLG_PROVINCIA { get; set; }
    public bool? FLG_DISTRITO { get; set; }
}

public class UbigeosUpdateDto
{
    [Required] public string COD_UBIGEO { get; set; } = string.Empty;
    [Required] public string DES_UBIGEO { get; set; } = string.Empty;
    public bool? FLG_DEPARTAMENTO { get; set; }
    public bool? FLG_PROVINCIA { get; set; }
    public bool? FLG_DISTRITO { get; set; }
}

public class UbigeosSearchDto
{
    public string? Search { get; set; }
}