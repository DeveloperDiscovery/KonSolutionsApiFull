using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_CATALOGO_PRECIOS - tipos equivalentes a la BD.</summary>
public class TiposCatalogoPrecios
{
    public string COD_TIPO_CATALOGO_PRECIO { get; set; } = string.Empty;
    public string DES_TIPO_CATALOGO_PRECIO { get; set; } = string.Empty;
    public bool? FLG_CATALOGO_MATRIZ { get; set; }
}

public class TiposCatalogoPreciosInsertDto
{
    [Required] public string COD_TIPO_CATALOGO_PRECIO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CATALOGO_PRECIO { get; set; } = string.Empty;
    public bool? FLG_CATALOGO_MATRIZ { get; set; }
}

public class TiposCatalogoPreciosUpdateDto
{
    [Required] public string COD_TIPO_CATALOGO_PRECIO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CATALOGO_PRECIO { get; set; } = string.Empty;
    public bool? FLG_CATALOGO_MATRIZ { get; set; }
}

public class TiposCatalogoPreciosSearchDto
{
    public string? Search { get; set; }
}