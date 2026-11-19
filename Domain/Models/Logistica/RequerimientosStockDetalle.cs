using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad REQUERIMIENTOS_STOCK_DETALLE - tipos equivalentes a la BD.</summary>
public class RequerimientosStockDetalle
{
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;

    // ── Proceso y orden de producción ────────────────────────────────────────────────
    // Columnas nuevas del detalle. En los SP de alta y modificación van con default NULL,
    // así que un ítem que no es de producción sigue grabándose igual que siempre.
    //
    // El proceso SÍ lo elige el usuario (por eso el LOADCOMBOS trae PROCESOS_SHOWCOMBOX como
    // séptimo resultset). Los tres de orden de producción NO: los escribe el proceso de
    // producción cuando el requerimiento nace de una orden. Viajan en el modelo para no
    // perderlos al grabar —mismo criterio que NUM_SECUENCIA_ORIGINAL—, pero no se editan.
    public string? COD_PROCESO { get; set; }
    public int?    NUM_ORDEN_PRODUCCION { get; set; }
    public short?  NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public byte?   NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDA { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDA_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_RESERVADA { get; set; }
    public decimal? CAN_PRODUCTO_RESERVADA_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_REQUERIMIENTO_COMPRA { get; set; }
    public decimal? CAN_PRODUCTO_REQUERIMIENTO_COMPRA_SECUNDARIA { get; set; }

    // No se muestra en el detalle: solo circula por la API. En un alta viaja en null; en una
    // modificación, el valor que ya trajo el SHOWALL viaja de vuelta tal cual (el modelo lo
    // conserva solo, sin que la pantalla lo toque).
    public short? NUM_SECUENCIA_ORIGINAL { get; set; }

    // El SP de alta y el de modificación lo exigen, sin default. Nulable porque la columna
    // admite NULL en la tabla.
    public string? COD_ALMACEN { get; set; }
    public string? COD_UBICACION_ALMACEN { get; set; }
    public string DES_LOTE_CONTROL { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_REQUERIMIENTO_STOCK { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string DES_ALMACEN { get; set; } = string.Empty;

    // La arma el LEFT JOIN contra PRODUCCION.PROCESOS. Sólo lectura.
    public string? DES_PROCESO { get; set; }
}

/// <summary>El lote y las unidades admiten NULL en la tabla: el lote porque hay ítems que
/// no se manejan por lote, y las unidades porque las define el producto y no todos tienen
/// una secundaria. Tenerlos como obligatorios rechazaba altas que la base acepta.</summary>
public class RequerimientosStockDetalleInsertDto
{
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;

    // Ver el comentario de la entidad: el proceso lo elige el usuario, los tres de orden de
    // producción sólo viajan de ida y vuelta para no perderse al grabar.
    public string? COD_PROCESO { get; set; }
    public int?    NUM_ORDEN_PRODUCCION { get; set; }
    public short?  NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public byte?   NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDA { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDA_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_RESERVADA { get; set; }
    public decimal? CAN_PRODUCTO_RESERVADA_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_REQUERIMIENTO_COMPRA { get; set; }
    public decimal? CAN_PRODUCTO_REQUERIMIENTO_COMPRA_SECUNDARIA { get; set; }
    public short? NUM_SECUENCIA_ORIGINAL { get; set; }
    public string? COD_ALMACEN { get; set; }
    public string? COD_UBICACION_ALMACEN { get; set; }
}

public class RequerimientosStockDetalleUpdateDto
{
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;

    // Ver el comentario de la entidad: el proceso lo elige el usuario, los tres de orden de
    // producción sólo viajan de ida y vuelta para no perderse al grabar.
    public string? COD_PROCESO { get; set; }
    public int?    NUM_ORDEN_PRODUCCION { get; set; }
    public short?  NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public byte?   NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDA { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDA_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_RESERVADA { get; set; }
    public decimal? CAN_PRODUCTO_RESERVADA_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_REQUERIMIENTO_COMPRA { get; set; }
    public decimal? CAN_PRODUCTO_REQUERIMIENTO_COMPRA_SECUNDARIA { get; set; }
    public short? NUM_SECUENCIA_ORIGINAL { get; set; }
    public string? COD_ALMACEN { get; set; }
    public string? COD_UBICACION_ALMACEN { get; set; }
}

public class RequerimientosStockDetalleSearchDto
{
    public string? Search { get; set; }
}