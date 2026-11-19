using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad INVENTARIOS_IMAGEN - tipos equivalentes a la BD.</summary>
public class InventariosImagen
{
    public int? NUM_INVENTARIO { get; set; }
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_INVENTARIADA { get; set; }
    public decimal? CAN_PRODUCTO_DIFERENCIA { get; set; }
}

public class InventariosImagenInsertDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_INVENTARIADA { get; set; }
    public decimal? CAN_PRODUCTO_DIFERENCIA { get; set; }
}

public class InventariosImagenUpdateDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_INVENTARIADA { get; set; }
    public decimal? CAN_PRODUCTO_DIFERENCIA { get; set; }
}

public class InventariosImagenSearchDto
{
    public string? Search { get; set; }
}