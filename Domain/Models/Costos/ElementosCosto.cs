using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Costos;

/// <summary>Entidad ELEMENTOS_COSTO - tipos equivalentes a la BD.</summary>
public class ElementosCosto
{
    public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    public string DES_ELEMENTO_COSTO { get; set; } = string.Empty;
    public string COD_CATEGORIA_FUNCION { get; set; } = string.Empty;
    public string COD_TIPO_COSTO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string DES_CATEGORIA_FUNCION { get; set; } = string.Empty;
    public string DES_TIPO_COSTO { get; set; } = string.Empty;
}

public class ElementosCostoInsertDto
{
    [Required] public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string DES_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_CATEGORIA_FUNCION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_COSTO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class ElementosCostoUpdateDto
{
    [Required] public string COD_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string DES_ELEMENTO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_CATEGORIA_FUNCION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_COSTO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class ElementosCostoSearchDto
{
    public string? Search { get; set; }
}