using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Sunat;

/// <summary>Entidad CUBSO_SUNAT - tipos equivalentes a la BD.</summary>
public class CubsoSunat
{
    public string COD_CUBSO { get; set; } = string.Empty;
    public string? DES_CUBSO { get; set; }
    public string? DES_CUBSO_ES { get; set; }
    public string? COD_CUBSO_PARENT { get; set; }
    public string? COD_UNSPS { get; set; }
}

public class CubsoSunatInsertDto
{
    [Required] public string COD_CUBSO { get; set; } = string.Empty;
    public string? DES_CUBSO { get; set; }
    public string? DES_CUBSO_ES { get; set; }
    public string? COD_CUBSO_PARENT { get; set; }
    public string? COD_UNSPS { get; set; }
}

public class CubsoSunatUpdateDto
{
    [Required] public string COD_CUBSO { get; set; } = string.Empty;
    public string? DES_CUBSO { get; set; }
    public string? DES_CUBSO_ES { get; set; }
    public string? COD_CUBSO_PARENT { get; set; }
    public string? COD_UNSPS { get; set; }
}

public class CubsoSunatSearchDto
{
    public string? Search { get; set; }
}
