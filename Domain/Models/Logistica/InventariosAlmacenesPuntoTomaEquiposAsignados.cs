using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad INVENTARIOS_ALMACENES_PUNTO_TOMA_EQUIPOS_ASIGNADOS - tipos equivalentes a
/// la BD. Clave compuesta NUM_INVENTARIO + COD_ALMACEN + NUM_PUNTO_TOMA_INVENTARIO +
/// COD_PRODUCTO_EQUIPO (equipo — un producto — asignado a un punto de conteo). El código de
/// equipo NO es automático: lo elige el usuario desde un combo/búsqueda de Productos, y el SP
/// _INSERT es alta pura (si la clave ya existe, error KEY_REGISTRO_EXISTENTE).
///
/// SIN colisión de nombres: la tabla base no tiene columna propia "DES_OBSERVACION" — la única
/// que trae el SELECT es la del JOIN al punto de toma padre (c.DES_OBSERVACION). Dapper
/// automático alcanza, no hace falta mapeo manual por ordinal.</summary>
public class InventariosAlmacenesPuntoTomaEquiposAsignados
{
    // Auditoria (la muestra PanelAuditoria en el dialogo)
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public int? NUM_INVENTARIO { get; set; }
    public string COD_ALMACEN { get; set; } = string.Empty;
    public byte? NUM_PUNTO_TOMA_INVENTARIO { get; set; }
    public string COD_PRODUCTO_EQUIPO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }

    // ── Solo lectura: traído por SHOWALL/SHOWBYID/SHOWCOMBOX/SEARCH vía JOIN ──
    /// <summary>Del equipo (join b, ESTANDAR.PRODUCTOS por COD_PRODUCTO_EQUIPO=COD_PRODUCTO).</summary>
    public string? DES_DETALLADA { get; set; }
    public string? DES_PRODUCTO { get; set; }
    /// <summary>DES_OBSERVACION del INVENTARIOS_ALMACENES_PUNTO_TOMA padre (join c).</summary>
    public string? DES_OBSERVACION { get; set; }
}

public class InventariosAlmacenesPuntoTomaEquiposAsignadosInsertDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public byte? NUM_PUNTO_TOMA_INVENTARIO { get; set; }
    [Required] public string COD_PRODUCTO_EQUIPO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class InventariosAlmacenesPuntoTomaEquiposAsignadosUpdateDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public byte? NUM_PUNTO_TOMA_INVENTARIO { get; set; }
    [Required] public string COD_PRODUCTO_EQUIPO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class InventariosAlmacenesPuntoTomaEquiposAsignadosSearchDto
{
    public int? NUM_INVENTARIO { get; set; }
    public string? COD_ALMACEN { get; set; }
    public byte? NUM_PUNTO_TOMA_INVENTARIO { get; set; }
    public string? Search { get; set; }
}
