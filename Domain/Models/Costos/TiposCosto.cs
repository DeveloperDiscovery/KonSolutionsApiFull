using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Costos;

/// <summary>Entidad TIPOS_COSTO - tipos equivalentes a la BD.</summary>
public class TiposCosto
{
    public string COD_TIPO_COSTO { get; set; } = string.Empty;
    public string DES_TIPO_COSTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposCostoInsertDto
{
    [Required] public string COD_TIPO_COSTO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_COSTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposCostoUpdateDto
{
    [Required] public string COD_TIPO_COSTO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_COSTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposCostoSearchDto
{
    public string? Search { get; set; }
}