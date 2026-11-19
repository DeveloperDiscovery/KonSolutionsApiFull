using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad SEXOS - tipos equivalentes a la BD.</summary>
public class Sexos
{
    public string COD_SEXO { get; set; } = string.Empty;
    public string DES_SEXO { get; set; } = string.Empty;
}

public class SexosInsertDto
{
    [Required] public string COD_SEXO { get; set; } = string.Empty;
    [Required] public string DES_SEXO { get; set; } = string.Empty;
}

public class SexosUpdateDto
{
    [Required] public string COD_SEXO { get; set; } = string.Empty;
    [Required] public string DES_SEXO { get; set; } = string.Empty;
}

public class SexosSearchDto
{
    public string? Search { get; set; }
}