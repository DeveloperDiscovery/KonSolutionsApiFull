using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad INVENTARIOS_ALMACENES_PRODUCTOS - tipos equivalentes a la BD.
/// Clave compuesta NUM_INVENTARIO + COD_ALMACEN + COD_PRODUCTO. Tabla de asignación
/// pura: el SP _INSERT hace upsert internamente, no hay SP _UPDATE.
/// NOTA: reemplaza una versión anterior de este mismo nombre que tenía una forma
/// distinta (clave con COD_UBICACION_ALMACEN/NUM_LOTE_CONTROL y SP _UPDATE) que no
/// se correspondía con LOGISTICA.INVENTARIOS_ALMACENES_PRODUCTOS actual — quedó
/// huérfana (no referenciada desde el frontend), se reescribió para calzar con el
/// dump de SPs de este submódulo.</summary>
public class InventariosAlmacenesProductos
{
    public int? NUM_INVENTARIO { get; set; }
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }

    // ── Solo lectura: traído por SHOWALL/SHOWBYID/SHOWCOMBOX/SEARCH vía JOIN ──
    public string? DES_PRODUCTO { get; set; }
    public string? DES_DETALLADA { get; set; }
    public string? DES_OBSERVACION { get; set; }
}

public class InventariosAlmacenesProductosInsertDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class InventariosAlmacenesProductosSearchDto
{
    public int? NUM_INVENTARIO { get; set; }
    public string? COD_ALMACEN { get; set; }
    public string? Search { get; set; }
}
