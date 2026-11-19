using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad INVENTARIOS_ALMACENES_PUNTO_TOMA_CONTEO - tipos equivalentes a la BD.
/// Clave compuesta NUM_INVENTARIO + COD_ALMACEN + NUM_PUNTO_TOMA_INVENTARIO + NUM_SECUENCIA
/// (líneas de conteo tomadas dentro de un punto de conteo). NUM_SECUENCIA es AUTOMÁTICO: el
/// SP _INSERT hace `SELECT @NUM_SECUENCIA = MAX(...) + 1` internamente — mismo criterio que
/// ya resuelve NUM_PUNTO_TOMA_INVENTARIO en el módulo padre (InventariosAlmacenesPuntoToma):
/// el valor se envía igual (puede ir null en el alta) y el SP lo recalcula, ignorando/pisando
/// lo recibido.
///
/// SIN colisión de nombres: a diferencia del padre, la tabla base de este SP no tiene columna
/// propia "DES_OBSERVACION" — la única que trae el SELECT es la del JOIN al punto de toma
/// padre (b.DES_OBSERVACION). No hace falta mapeo manual por ordinal: Dapper automático
/// alcanza.</summary>
public class InventariosAlmacenesPuntoTomaConteo
{
    // Auditoria (la muestra PanelAuditoria en el dialogo)
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public int? NUM_INVENTARIO { get; set; }
    public string COD_ALMACEN { get; set; } = string.Empty;
    public byte? NUM_PUNTO_TOMA_INVENTARIO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string? COD_UBICACION_ALMACEN { get; set; }
    public string? COD_PRODUCTO { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    public string? COD_PROCESO { get; set; }
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public byte? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }

    // ── Solo lectura: traído por SHOWALL/SHOWBYID/SHOWCOMBOX/SEARCH vía JOIN ──
    /// <summary>DES_OBSERVACION del INVENTARIOS_ALMACENES_PUNTO_TOMA padre (join b).</summary>
    public string? DES_OBSERVACION { get; set; }
    /// <summary>Del producto contado (join c, ESTANDAR.PRODUCTOS).</summary>
    public string? DES_DETALLADA { get; set; }
    public string? DES_PRODUCTO { get; set; }
    /// <summary>De la ubicación en el almacén (join d, LOGISTICA.ALMACENES_UBICACIONES_ALMACEN).</summary>
    public string? DES_POLIGONO { get; set; }
    public string? DES_UBICACION_ALMACEN { get; set; }
    /// <summary>Del proceso (join e, PRODUCCION.PROCESOS).</summary>
    public string? DES_PROCESO { get; set; }
    /// <summary>Del lote de control (join f, LOGISTICA.LOTES_CONTROL).</summary>
    public string? NUM_LOTE_CONTROL_ENTIDAD { get; set; }
    /// <summary>Del almacén (join g, LOGISTICA.ALMACENES) — solo en _SEARCH por ahora.</summary>
    public string? DES_ALMACEN { get; set; }
    /// <summary>"{DES_ALMACEN}/{DES_UBICACION_ALMACEN}" ya armado por el SP.</summary>
    public string? DES_ALMACEN_UBIACION { get; set; }
}

public class InventariosAlmacenesPuntoTomaConteoInsertDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public byte? NUM_PUNTO_TOMA_INVENTARIO { get; set; }
    /// <summary>Automático (MAX+1 dentro del punto de toma) — el SP lo recalcula internamente,
    /// se envía tal cual llegue (típicamente null en el alta).</summary>
    public int? NUM_SECUENCIA { get; set; }
    public string? COD_UBICACION_ALMACEN { get; set; }
    public string? COD_PRODUCTO { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    public string? COD_PROCESO { get; set; }
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public byte? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    /// <summary>Códigos crudos leídos por RFID/lector de código de barras — el SP _INSERT los
    /// usa para resolver Producto/Lote/Proceso/Orden del lado servidor (por si el cliente los
    /// mandó vacíos o desactualizados); ver PROC_..._CONTEO_INSERT.</summary>
    public string? COD_STICKER_RFID { get; set; }
    public string? COD_STICKER_UBICACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class InventariosAlmacenesPuntoTomaConteoUpdateDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public byte? NUM_PUNTO_TOMA_INVENTARIO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string? COD_UBICACION_ALMACEN { get; set; }
    public string? COD_PRODUCTO { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    public string? COD_PROCESO { get; set; }
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public byte? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class InventariosAlmacenesPuntoTomaConteoSearchDto
{
    public int? NUM_INVENTARIO { get; set; }
    public string? COD_ALMACEN { get; set; }
    public byte? NUM_PUNTO_TOMA_INVENTARIO { get; set; }
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_READRFIDCODEBAR —
/// lee un sticker de ubicación + un sticker RFID/código de barras y resuelve Producto, Lote,
/// Proceso, Orden/Item/Unidad, Almacén/Ubicación y la Unidad de Medida, para autocompletar la
/// línea de conteo sin que el usuario tipee nada. FLG_MEDICION indica si la unidad de medida
/// del producto es indivisible (COD_TIPO_UNIDAD_MEDIDA = 'UNI'): en ese caso el conteo se
/// graba automático con Cantidad = 1 apenas se lee el RFID/código de barras; si no, hay que
/// digitar la cantidad y grabar manualmente.</summary>
public class InventariosAlmacenesPuntoTomaReadRfidCodebarResult
{
    public string? COD_ALMACEN { get; set; }
    public string? DES_ALMACEN { get; set; }
    public string? COD_UBICACION_ALMACEN { get; set; }
    public string? DES_UBICACION_ALMACEN { get; set; }
    public string? COD_PRODUCTO { get; set; }
    public string? DES_PRODUCTO { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    public string? NUM_LOTE_CONTROL_ENTIDAD { get; set; }
    public string? COD_PROCESO { get; set; }
    public string? DES_PROCESO { get; set; }
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public byte? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public string? COD_TIPO_UNIDAD_MEDIDA { get; set; }
    public bool? FLG_MEDICION { get; set; }
}

public class InventariosAlmacenesPuntoTomaReadRfidCodebarDto
{
    public string? COD_STICKER_UBICACION { get; set; }
    public string? COD_STICKER_RFID { get; set; }
}
