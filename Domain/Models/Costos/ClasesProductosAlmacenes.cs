using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Costos;

/// <summary>Entidad CLASES_PRODUCTOS_ALMACENES - tipos equivalentes a la BD.</summary>
public class ClasesProductosAlmacenes
{
    public string COD_CLASE_PRODUCTO { get; set; } = string.Empty;
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ABREVIADA { get; set; } = string.Empty;
    public string DES_ALMACEN { get; set; } = string.Empty;
    public string DES_CLASE_PRODUCTO { get; set; } = string.Empty;
}

public class ClasesProductosAlmacenesInsertDto
{
    [Required] public string COD_CLASE_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ClasesProductosAlmacenesUpdateDto
{
    [Required] public string COD_CLASE_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ClasesProductosAlmacenesSearchDto
{
    public string? Search { get; set; }
}