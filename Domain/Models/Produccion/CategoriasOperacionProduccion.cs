using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Produccion;

/// <summary>Entidad CATEGORIAS_OPERACION_PRODUCCION - tipos equivalentes a la BD.</summary>
public class CategoriasOperacionProduccion
{
    public string COD_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string DES_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class CategoriasOperacionProduccionInsertDto
{
    [Required] public string COD_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string DES_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class CategoriasOperacionProduccionUpdateDto
{
    [Required] public string COD_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string DES_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class CategoriasOperacionProduccionSearchDto
{
    public string? Search { get; set; }
}