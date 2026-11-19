using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad RESERVAS_STOCK_DETALLE - tipos equivalentes a la BD.</summary>
public class ReservasStockDetalle
{
    public int? NUM_RESERVA_STOCK { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
    // La orden de producción de la que sale esta reserva. Se completa sola al elegir la
    // ubicación de stock (ese renglón de existencias ya la trae) — no se tipea.
    public string? COD_PROCESO { get; set; }
    // string? y NO string = "": la columna admite NULL en la tabla, y Dapper, cuando lee un
    // NULL contra una propiedad no nulable, deja el valor por defecto — o sea "". La API
    // devolvia "" donde la base tiene NULL, la pantalla lo mandaba de vuelta tal cual y el
    // alta viajaba con cadena vacia. Se veia en el payload: nuM_LOTE_CONTROL: "".
    public string? NUM_LOTE_CONTROL { get; set; }
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public short? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public byte? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    // Idem: no todos los productos tienen unidad secundaria, y la columna admite NULL.
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDA { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDA_SECUNDARIA { get; set; }

    // Las trae resueltas el procedimiento por JOIN: sólo se muestran.
    public string? DES_ALMACEN { get; set; }
    public string? DES_UBICACION_ALMACEN { get; set; }
    public string? DES_PRODUCTO { get; set; }
    public string? DES_DETALLADA { get; set; }
    public string? DES_UNIDAD_MEDIDA { get; set; }
    public string? DES_LOTE_CONTROL { get; set; }
    public string? DES_ORDEN_PRODUCCION { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_RESERVA_STOCK { get; set; } = string.Empty;
}

/// <summary>El lote y las unidades admiten NULL en la tabla: el lote porque hay ítems que
/// no se manejan por lote, y las unidades porque las define el producto y no todos tienen
/// una secundaria. Tenerlos como obligatorios rechazaba altas que la base acepta.</summary>
public class ReservasStockDetalleInsertDto
{
    public int? NUM_RESERVA_STOCK { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public string? COD_PROCESO { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public short? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public byte? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDA { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDA_SECUNDARIA { get; set; }
}

public class ReservasStockDetalleUpdateDto
{
    public int? NUM_RESERVA_STOCK { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public string? COD_PROCESO { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public short? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public byte? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDA { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDA_SECUNDARIA { get; set; }
}

public class ReservasStockDetalleSearchDto
{
    public string? Search { get; set; }
}