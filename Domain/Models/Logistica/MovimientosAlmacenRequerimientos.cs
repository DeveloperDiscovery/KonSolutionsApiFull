namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Cabecera de un requerimiento de stock visto desde un movimiento de almacén.
///
/// Una sola forma para los dos lados —disponibles y asignados—, porque los dos SP
/// devuelven las mismas columnas.</summary>
public class MovimientoAlmacenRequerimiento
{
    public int       NUM_REQUERIMIENTO_STOCK { get; set; }
    public string?   DES_REQUERIMIENTO_STOCK { get; set; }
    public DateTime? FEC_REQUERIMIENTO_STOCK { get; set; }
    public string?   COD_TIPO_REQUERIMIENTO  { get; set; }
    public string?   DES_TIPO_REQUERIMIENTO  { get; set; }
    public string?   COD_CENTRO_COSTO_REQUIRIENTE_ORIGINAL { get; set; }
    public string?   DES_CENTRO_COSTO_REQUIRIENTE_ORIGINAL { get; set; }
    public int?      COD_ESTADO              { get; set; }
    public string?   DES_ESTADO              { get; set; }
    public string?   DES_BACKCOLOR           { get; set; }
    public string?   DES_FORECOLOR           { get; set; }
}

/// <summary>Línea de un requerimiento vista desde un movimiento.
///
/// Los dos SP comparten las primeras columnas; el de DISPONIBLES agrega lo que hace falta
/// para decidir: lo pendiente, la ubicación que el criterio eligió, el disponible de esa
/// ubicación, el total elegible y una cobertura ya resuelta. Del lado asignados esas
/// quedan en null y la grilla no las muestra.</summary>
public class MovimientoAlmacenRequerimientoDetalle
{
    public int      NUM_REQUERIMIENTO_STOCK { get; set; }
    public int      NUM_SECUENCIA           { get; set; }
    public string?  COD_ALMACEN             { get; set; }
    public string?  DES_ALMACEN             { get; set; }
    public string?  COD_PRODUCTO            { get; set; }
    public string?  DES_PRODUCTO            { get; set; }
    public string?  COD_UNIDAD_MEDIDA       { get; set; }
    public string?  DES_UNIDAD_MEDIDA       { get; set; }
    public decimal? CAN_PRODUCTO            { get; set; }
    public string?  COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public string?  DES_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_SECUNDARIA { get; set; }

    // ── Sólo del lado DISPONIBLES ────────────────────────────────────────────
    public decimal? CAN_PENDIENTE           { get; set; }

    /// <summary>Cuánto se despacha en ESTE movimiento. El SP de disponibles NO la devuelve:
    /// la escribe el usuario en la grilla. Se declara igual, para que el modelo de la API y
    /// el compartido sean el mismo y la propiedad ya esté cuando se cablee el alta — un campo
    /// que existe de un solo lado se pierde al serializar y nadie se entera.</summary>
    public decimal? CAN_PRODUCTO_DESPACHAR { get; set; }
    // NUM_LOTE_CONTROL es varchar en la base (el código de lote, ej. "5677-324424"), no un
    // número: declararlo int? hacía que Dapper reventara la llamada entera apenas el SP
    // devolvía un lote real ("Error parsing column 12 (NUM_LOTE_CONTROL=... - String)"),
    // que es justo el error que se veía en consola. Mismo criterio que LotesControl,
    // StocksAlmacenUbicacion y el resto de las tablas que ya usan este código como texto.
    public string?  NUM_LOTE_CONTROL        { get; set; }
    public string?  COD_PROCESO             { get; set; }
    public int?     NUM_ORDEN_PRODUCCION    { get; set; }
    public int?     NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int?     NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }

    public string?  COD_UBICACION_ALMACEN_ASIGNADA { get; set; }
    public string?  DES_UBICACION_ALMACEN_ASIGNADA { get; set; }
    public string?  COD_ALMACEN_ASIGNADO           { get; set; }
    public string?  NUM_LOTE_CONTROL_ASIGNADO      { get; set; }
    public string?  COD_PROCESO_ASIGNADO           { get; set; }
    public int?     NUM_ORDEN_PRODUCCION_ASIGNADA  { get; set; }
    public int?     NUM_ORDEN_PRODUCCION_ITEM_ASIGNADO   { get; set; }
    public int?     NUM_ORDEN_PRODUCCION_UNIDAD_ASIGNADA { get; set; }
    public decimal? CAN_DISPONIBLE_ASIGNADA        { get; set; }
    public decimal? CAN_DISPONIBLE_TOTAL           { get; set; }
    public decimal? CAN_ATENDER                    { get; set; }
    public decimal? CAN_PENDIENTE_ATENDER          { get; set; }

    /// <summary>COMPLETO · ASIGNADA · PARCIAL · SIN STOCK. La resuelve el SP: es una
    /// decisión de negocio y tiene que decir lo mismo en la pantalla y en un reporte.</summary>
    public string?  DES_COBERTURA           { get; set; }
}
