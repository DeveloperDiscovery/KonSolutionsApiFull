using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad TIPOS_EMBALAJE - tipos equivalentes a la BD.</summary>
public class TiposEmbalaje
{
    public string COD_TIPO_EMBALAJE { get; set; } = string.Empty;
    public string DES_TIPO_EMBALAJE { get; set; } = string.Empty;
    public bool? FLG_INNER_PACK { get; set; }
}

public class TiposEmbalajeInsertDto
{
    [Required] public string COD_TIPO_EMBALAJE { get; set; } = string.Empty;
    [Required] public string DES_TIPO_EMBALAJE { get; set; } = string.Empty;
    public bool? FLG_INNER_PACK { get; set; }
}

public class TiposEmbalajeUpdateDto
{
    [Required] public string COD_TIPO_EMBALAJE { get; set; } = string.Empty;
    [Required] public string DES_TIPO_EMBALAJE { get; set; } = string.Empty;
    public bool? FLG_INNER_PACK { get; set; }
}

public class TiposEmbalajeSearchDto
{
    public string? Search { get; set; }
}