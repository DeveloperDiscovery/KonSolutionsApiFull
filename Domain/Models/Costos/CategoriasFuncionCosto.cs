using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Costos;

/// <summary>Entidad CATEGORIAS_FUNCION_COSTO - tipos equivalentes a la BD.</summary>
public class CategoriasFuncionCosto
{
    public string COD_CATEGORIA_FUNCION { get; set; } = string.Empty;
    public string DES_CATEGORIA_FUNCION { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class CategoriasFuncionCostoInsertDto
{
    [Required] public string COD_CATEGORIA_FUNCION { get; set; } = string.Empty;
    [Required] public string DES_CATEGORIA_FUNCION { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class CategoriasFuncionCostoUpdateDto
{
    [Required] public string COD_CATEGORIA_FUNCION { get; set; } = string.Empty;
    [Required] public string DES_CATEGORIA_FUNCION { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class CategoriasFuncionCostoSearchDto
{
    public string? Search { get; set; }
}