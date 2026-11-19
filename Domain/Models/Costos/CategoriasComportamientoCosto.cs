using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Costos;

/// <summary>Entidad CATEGORIAS_COMPORTAMIENTO_COSTO - tipos equivalentes a la BD.</summary>
public class CategoriasComportamientoCosto
{
    public string COD_CATEGORIA_COMPORTAMIENTO { get; set; } = string.Empty;
    public string DES_CATEGORIA_COMPORTAMIENTO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class CategoriasComportamientoCostoInsertDto
{
    [Required] public string COD_CATEGORIA_COMPORTAMIENTO { get; set; } = string.Empty;
    [Required] public string DES_CATEGORIA_COMPORTAMIENTO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class CategoriasComportamientoCostoUpdateDto
{
    [Required] public string COD_CATEGORIA_COMPORTAMIENTO { get; set; } = string.Empty;
    [Required] public string DES_CATEGORIA_COMPORTAMIENTO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class CategoriasComportamientoCostoSearchDto
{
    public string? Search { get; set; }
}