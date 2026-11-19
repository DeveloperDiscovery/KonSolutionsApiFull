using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad INVENTARIOS_ALMACENES_IMAGEN - tipos equivalentes a la BD. Clave
/// compuesta larga (9 columnas). Tabla SIN CRUD manual: la puebla un proceso todavía
/// no implementado (toma de imagen de stock). Se construye la capa completa
/// (Domain/Repository/Service/Controller con los 6 verbos) por consistencia
/// arquitectónica con el resto del proyecto, pero el frontend solo consume Showall
/// de solo lectura.</summary>
public class InventariosAlmacenesImagen
{
    public int? NUM_INVENTARIO { get; set; }
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public string COD_PROCESO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public decimal? CAN_PRODUCTO_CONTEO { get; set; }
    public decimal? CAN_PRODUCTO_CONTEO_DIFERENCIA { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public decimal? CAN_PRODUCTO_INVENTARIADA { get; set; }
    public decimal? CAN_PRODUCTO_DIFERENCIA { get; set; }
}

public class InventariosAlmacenesImagenInsertDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public string? NUM_LOTE_CONTROL { get; set; }
    public string? COD_PROCESO { get; set; }
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public decimal? CAN_PRODUCTO_CONTEO { get; set; }
    public decimal? CAN_PRODUCTO_CONTEO_DIFERENCIA { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public decimal? CAN_PRODUCTO_INVENTARIADA { get; set; }
    public decimal? CAN_PRODUCTO_DIFERENCIA { get; set; }
}

public class InventariosAlmacenesImagenUpdateDto : InventariosAlmacenesImagenInsertDto { }

public class InventariosAlmacenesImagenSearchDto
{
    public int? NUM_INVENTARIO { get; set; }
    public string? COD_ALMACEN { get; set; }
    public string? Search { get; set; }
}
