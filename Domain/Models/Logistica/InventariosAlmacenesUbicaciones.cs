using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad INVENTARIOS_ALMACENES_UBICACIONES - tipos equivalentes a la BD.
/// Clave compuesta NUM_INVENTARIO + COD_ALMACEN + COD_UBICACION_ALMACEN. Tabla de
/// asignación pura: el SP _INSERT hace upsert internamente, no hay SP _UPDATE.</summary>
public class InventariosAlmacenesUbicaciones
{
    public int? NUM_INVENTARIO { get; set; }
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }

    // ── Solo lectura: traído por SHOWALL/SHOWBYID/SHOWCOMBOX/SEARCH vía JOIN ──
    public string? DES_UBICACION_ALMACEN { get; set; }
    public string? DES_POLIGONO { get; set; }
    public string? DES_OBSERVACION { get; set; }
}

public class InventariosAlmacenesUbicacionesInsertDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class InventariosAlmacenesUbicacionesSearchDto
{
    public int? NUM_INVENTARIO { get; set; }
    public string? COD_ALMACEN { get; set; }
    public string? Search { get; set; }
}
