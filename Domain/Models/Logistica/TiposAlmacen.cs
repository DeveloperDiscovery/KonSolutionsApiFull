using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad TIPOS_ALMACEN - tipos equivalentes a la BD.</summary>
public class TiposAlmacen
{
    public string COD_TIPO_ALMACEN { get; set; } = string.Empty;
    public string DES_TIPO_ALMACEN { get; set; } = string.Empty;
    public bool? FLG_REGULAR { get; set; }
    public bool? FLG_TRANSITO { get; set; }
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_PRODUCTO_TERMINADO { get; set; }
}

public class TiposAlmacenInsertDto
{
    [Required] public string COD_TIPO_ALMACEN { get; set; } = string.Empty;
    [Required] public string DES_TIPO_ALMACEN { get; set; } = string.Empty;
    public bool? FLG_REGULAR { get; set; }
    public bool? FLG_TRANSITO { get; set; }
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_PRODUCTO_TERMINADO { get; set; }
}

public class TiposAlmacenUpdateDto
{
    [Required] public string COD_TIPO_ALMACEN { get; set; } = string.Empty;
    [Required] public string DES_TIPO_ALMACEN { get; set; } = string.Empty;
    public bool? FLG_REGULAR { get; set; }
    public bool? FLG_TRANSITO { get; set; }
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_PRODUCTO_TERMINADO { get; set; }
}

public class TiposAlmacenSearchDto
{
    public string? Search { get; set; }
}