using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad TIPOS_MERMA - tipos equivalentes a la BD.</summary>
public class TiposMerma
{
    public string COD_TIPO_MERMA { get; set; } = string.Empty;
    public string DES_TIPO_MERMA { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_LOGISTICA { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposMermaInsertDto
{
    [Required] public string COD_TIPO_MERMA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_MERMA { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_LOGISTICA { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposMermaUpdateDto
{
    [Required] public string COD_TIPO_MERMA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_MERMA { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_LOGISTICA { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposMermaSearchDto
{
    public string? Search { get; set; }
}