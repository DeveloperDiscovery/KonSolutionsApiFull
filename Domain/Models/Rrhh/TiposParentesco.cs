using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad TIPOS_PARENTESCO - tipos equivalentes a la BD.</summary>
public class TiposParentesco
{
    public string COD_TIPO_PARENTESCO { get; set; } = string.Empty;
    public string DES_TIPO_PARENTESCO { get; set; } = string.Empty;
}

public class TiposParentescoInsertDto
{
    [Required] public string COD_TIPO_PARENTESCO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_PARENTESCO { get; set; } = string.Empty;
}

public class TiposParentescoUpdateDto
{
    [Required] public string COD_TIPO_PARENTESCO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_PARENTESCO { get; set; } = string.Empty;
}

public class TiposParentescoSearchDto
{
    public string? Search { get; set; }
}