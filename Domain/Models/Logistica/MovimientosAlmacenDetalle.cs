using System.ComponentModel.DataAnnotations;
using KonSolutionsApi.Common;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad MOVIMIENTOS_ALMACEN_DETALLE - tipos equivalentes a la BD.</summary>
public class MovimientosAlmacenDetalle
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_SECUNDARIO { get; set; }
    public string COD_UNIDAD_MEDIDA_SECUNDARIO { get; set; } = string.Empty;
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;
    public decimal? NUM_MOVIMIENTO_ALMACEN_SINCRONIZADO { get; set; }
    public string COD_ALMACEN_SINCRONIZADO { get; set; } = string.Empty;
    public string COD_UBICACION_ALMACEN_SINCRONIZADO { get; set; } = string.Empty;
    public int? NUM_REQUERIMIENTO_STOCK_ATENDIDO { get; set; }
    public int? NUM_RESERVA_STOCK_ATENDIDO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_UBICACION_ALMACEN { get; set; } = string.Empty;
    // Rack, fila, columna y otra ubicación se quitaron: eran columnas de la ubicación que
    // ya no existen. El mueble hoy vive en LOGISTICA.CONTENEDORES_ALMACENAJE_CASILLEROS.
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_DIRECCION_DESTINO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;

    // ── Unidad secundaria, lote y orden de producción ────────────────────────
    // Espejo de lo que el SHOWALL devuelve. Tiene que existir en LOS DOS modelos —éste y
    // el de Shared—: un campo que está en uno solo se pierde al serializar, sin error.
    public string  DES_UNIDAD_MEDIDA_SECUNDARIO { get; set; } = string.Empty;
    public string? NUM_LOTE_CONTROL            { get; set; }
    public string? COD_PROCESO                 { get; set; }
    public string? DES_PROCESO                 { get; set; }
    public int?    NUM_ORDEN_PRODUCCION        { get; set; }
    public string? DES_ORDEN_PRODUCCION        { get; set; }
    public short?  NUM_ORDEN_PRODUCCION_ITEM   { get; set; }
    public byte?   NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }

    // ── De dónde viene la línea ──────────────────────────────────────────────
    // Deciden si la línea se puede tocar a mano. Tiene que estar en LOS DOS modelos: un
    // campo que exista en uno solo se pierde al serializar y la pantalla lo ve en null,
    // que es justo el valor que habilitaría el borrado.
    public int?    NUM_SECUENCIA_REQUERIMIENTO_STOCK { get; set; }
    public int?    NUM_SECUENCIA_PEDIDO_COMERCIAL    { get; set; }
    public int?    MUM_PEDIDO_COMERCIAL              { get; set; }
    public int?    NUM_SECUENCIA_RESERVA_STOCK       { get; set; }
    public string? COD_CLASE_OPERACION               { get; set; }
    public string? COD_TIPO_OPERACION                { get; set; }
}

public class MovimientosAlmacenDetalleInsertDto
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_SECUNDARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;

    // Anulables: una línea que no es de transferencia no tiene destino, muchas no llevan unidad
    // secundaria, y el lote, el proceso y la orden los completa el SP con los de Configuración
    // General. Con <Nullable>enable</Nullable> un string no anulable es obligatorio aunque no
    // lleve [Required], y el alta se rechazaba antes de llegar al SP.
    public string?  COD_UNIDAD_MEDIDA_SECUNDARIO        { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN_SINCRONIZADO { get; set; }
    public int?     NUM_SECUENCIA_ALMACEN_SINCRONIZADO  { get; set; }
    public string?  COD_ALMACEN_SINCRONIZADO            { get; set; }
    public string?  COD_UBICACION_ALMACEN_SINCRONIZADO  { get; set; }
    public string?  NUM_LOTE_CONTROL                    { get; set; }
    public string?  COD_PROCESO                         { get; set; }
    public int?     NUM_ORDEN_PRODUCCION                { get; set; }
    public short?   NUM_ORDEN_PRODUCCION_ITEM           { get; set; }
    public byte?    NUM_ORDEN_PRODUCCION_UNIDAD         { get; set; }

    // Enlaces con el documento de origen. El SP los recibe todos; antes no se mandaban y la
    // llamada fallaba con "expects parameter '@NUM_LOTE_CONTROL', which was not supplied".
    public int?     NUM_REQUERIMIENTO_STOCK_ATENDIDO    { get; set; }
    public int?     NUM_SECUENCIA_REQUERIMIENTO_STOCK   { get; set; }
    public int?     NUM_RESERVA_STOCK_ATENDIDO          { get; set; }
    public int?     NUM_SECUENCIA_RESERVA_STOCK         { get; set; }
    public string?  COD_CLASE_OPERACION                 { get; set; }
    public string?  COD_TIPO_OPERACION                  { get; set; }
    public int?     MUM_PEDIDO_COMERCIAL                { get; set; }
    public int?     NUM_SECUENCIA_PEDIDO_COMERCIAL      { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
}

/// <summary>Mismos campos que el alta. En el SP, lo que llega en null o vacío se queda como
/// estaba en la línea.</summary>
public class MovimientosAlmacenDetalleUpdateDto : MovimientosAlmacenDetalleInsertDto
{
}

public class MovimientosAlmacenDetalleSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Una clave de stock con disponible en el almacén del movimiento
/// (PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_STOCK_DISPONIBLE). De acá salen las líneas de una
/// salida cargada a mano.</summary>
public class MovimientosAlmacenDetalleStockDisponible
{
    public string?  COD_ALMACEN                  { get; set; }
    public string?  COD_UBICACION_ALMACEN        { get; set; }
    public string?  DES_UBICACION_ALMACEN        { get; set; }
    public string?  COD_PRODUCTO                 { get; set; }
    public string?  DES_PRODUCTO                 { get; set; }
    public string?  NUM_LOTE_CONTROL             { get; set; }
    public string?  COD_PROCESO                  { get; set; }
    public int?     NUM_ORDEN_PRODUCCION         { get; set; }
    public short?   NUM_ORDEN_PRODUCCION_ITEM    { get; set; }
    public byte?    NUM_ORDEN_PRODUCCION_UNIDAD  { get; set; }
    public string?  COD_UNIDAD_MEDIDA            { get; set; }
    public string?  COD_UNIDAD_MEDIDA_SECUNDARIO { get; set; }
    public decimal? CAN_PRODUCTO                 { get; set; }
    public decimal? CAN_DISPONIBLE               { get; set; }
    public decimal? CAN_DISPONIBLE_SECUNDARIO    { get; set; }
}

/// <summary>Un producto con sus unidades, para cargar a mano la línea de un ingreso.</summary>
public class MovimientosAlmacenDetalleProducto
{
    public string? COD_PRODUCTO                 { get; set; }
    public string? DES_PRODUCTO                 { get; set; }
    public string? COD_UNIDAD_MEDIDA            { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIO { get; set; }
}

/// <summary>Los combos de la línea (PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_LOAD_COMBOS), en el
/// orden de sus tres resultsets.</summary>
public class MovimientosAlmacenDetalleCombosLinea
{
    public List<ComboboxItem> Ubicaciones        { get; set; } = new();
    public List<ComboboxItem> UbicacionesDestino { get; set; } = new();
    public List<MovimientosAlmacenDetalleProducto> Productos { get; set; } = new();
}
