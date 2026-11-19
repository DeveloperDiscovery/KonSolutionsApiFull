using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Safety;

/// <summary>Entidad TIPOS_TAG - tipos equivalentes a la BD.</summary>
public class TiposTag
{
    public string COD_TIPO_TAG { get; set; } = string.Empty;
    public string DES_TIPO_TAG { get; set; } = string.Empty;
    public bool? FLG_PRODUCTO { get; set; }
}

public class TiposTagInsertDto
{
    [Required] public string COD_TIPO_TAG { get; set; } = string.Empty;
    [Required] public string DES_TIPO_TAG { get; set; } = string.Empty;
    public bool? FLG_PRODUCTO { get; set; }
}

public class TiposTagUpdateDto
{
    [Required] public string COD_TIPO_TAG { get; set; } = string.Empty;
    [Required] public string DES_TIPO_TAG { get; set; } = string.Empty;
    public bool? FLG_PRODUCTO { get; set; }
}

public class TiposTagSearchDto
{
    public string? Search { get; set; }
}