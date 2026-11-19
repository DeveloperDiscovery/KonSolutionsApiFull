using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Costos;

/// <summary>Entidad CLASES_PRODUCTOS - tipos equivalentes a la BD.</summary>
public class ClasesProductos
{
    public string COD_CLASE_PRODUCTO { get; set; } = string.Empty;
    public string DES_CLASE_PRODUCTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ClasesProductosInsertDto
{
    [Required] public string COD_CLASE_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_CLASE_PRODUCTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ClasesProductosUpdateDto
{
    [Required] public string COD_CLASE_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_CLASE_PRODUCTO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ClasesProductosSearchDto
{
    public string? Search { get; set; }
}