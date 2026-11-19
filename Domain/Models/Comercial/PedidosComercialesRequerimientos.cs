using System.ComponentModel.DataAnnotations;

namespace KonSolutionsApi.Domain.Models.Comercial;

// Vínculo entre un pedido comercial y los requerimientos de stock que lo originaron.
// Son dos niveles, en dos tablas:
//
//   COMERCIAL.PEDIDOS_COMERCIALES_REQUERIMIENTOS_STOCK
//       el pedido ← el requerimiento (cabecera con cabecera)
//   COMERCIAL.PEDIDOS_COMERCIALES_DETALLE_REQUERIMIENTOS_STOCK_DETALLE
//       la línea del pedido ← la línea del requerimiento
//
// El segundo nivel es el que permite acumular: varias líneas de requerimiento pueden
// apuntar a una misma línea de pedido, y ahí queda de dónde salió cada cantidad.

/// <summary>Un requerimiento de stock registrado, tal como lo lista el SP _REGISTRADOS.
/// FLG_SECCION indica si ya está vinculado al pedido que se está mirando.</summary>
public class RequerimientoStockRegistrado
{
    public bool FLG_SECCION { get; set; }
    public int NUM_REQUERIMIENTO_STOCK { get; set; }
    public string? DES_REQUERIMIENTO_STOCK { get; set; }
    public DateTime? FEC_REQUERIMIENTO_STOCK { get; set; }
    public string? COD_TIPO_REQUERIMIENTO { get; set; }
    public string? DES_TIPO_REQUERIMIENTO { get; set; }
}

/// <summary>Una línea de requerimiento registrada.
/// FLG_SECCION repite el estado de la cabecera; FLG_SECCION_DETALLE es el que importa
/// acá: dice si esta línea concreta ya fue incorporada al pedido.</summary>
public class RequerimientoStockDetalleRegistrado
{
    public bool FLG_SECCION { get; set; }
    public bool FLG_SECCION_DETALLE { get; set; }
    /// <summary>Línea del PEDIDO en la que entró esta línea de requerimiento. Null si
    /// todavía no se incorporó. Hace falta para poder deshacer el vínculo.</summary>
    public int? NUM_SECUENCIA_PEDIDO { get; set; }
    public int NUM_REQUERIMIENTO_STOCK { get; set; }
    public int NUM_SECUENCIA { get; set; }
    public string COD_PRODUCTO { get; set; } = "";
    public string? DES_PRODUCTO { get; set; }
    /// <summary>Lote de control de la línea del requerimiento (lo devuelve PROC_LOGISTICA_REQUERIMIENTOS_STOCK_DETALLE_REGISTRADOS).</summary>
    public string? NUM_LOTE_CONTROL { get; set; }

    // ── Proceso y orden de producción ────────────────────────────────────────────────
    // El SP los devuelve desde que el detalle del requerimiento los tiene. Sin estas
    // propiedades, Dapper los descartaba en silencio: no fallaba nada, pero en el panel dos
    // líneas del MISMO producto para órdenes distintas se veían idénticas, y el operador no
    // tenía con qué distinguirlas al marcar.
    public string? COD_PROCESO { get; set; }
    public string? DES_PROCESO { get; set; }
    public int?    NUM_ORDEN_PRODUCCION { get; set; }
    public short?  NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public byte?   NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    // ── Cantidades — el SP le sacó el typo a las dos, ya no hace falta copiarlo: ────
    //   CAN_REQUERIDA        "Cantidad Requerida"
    //   CAN_PRODUCTO         "Cantidad Propia"      -> va directo a DETALLE.CAN_PRODUCTO
    //   CAN_PRODUCTO_ENTIDAD "Cantidad Proveedor"   -> va directo a DETALLE.CAN_PRODUCTO_ENTIDAD
    // Mapeo 1 a 1 por nombre entre este SP y PEDIDOS_COMERCIALES_DETALLE: al incorporar no
    // hay que recalcular nada del lado del cliente, solo sumar lo que ya viene resuelto.

    /// <summary>Cantidad tal como está en la línea del requerimiento, sin convertir
    /// ("Cantidad Requerida").</summary>
    public decimal? CAN_REQUERIDA { get; set; }
    /// <summary>Factor de COMERCIAL.ENTIDADES_PRODUCTOS para pasar de la unidad del
    /// requerimiento a la propia. 1 si el producto no tiene conversión configurada.</summary>
    public decimal? FAC_CONVERSION { get; set; }
    /// <summary>CEILING(CAN_REQUERIDA / FAC_CONVERSION) * FAC_CONVERSION ("Cantidad Propia")
    /// — la cantidad requerida, redondeada hacia arriba al múltiplo de FAC_CONVERSION más
    /// cercano. Va directo a COMERCIAL.PEDIDOS_COMERCIALES_DETALLE.CAN_PRODUCTO.</summary>
    public decimal? CAN_PRODUCTO { get; set; }
    /// <summary>CEILING(CAN_REQUERIDA / FAC_CONVERSION) ("Cantidad Proveedor") — cuántas
    /// unidades del proveedor hacen falta, redondeado hacia arriba: nunca se pide una
    /// fracción. Va directo a COMERCIAL.PEDIDOS_COMERCIALES_DETALLE.CAN_PRODUCTO_ENTIDAD.</summary>
    public decimal? CAN_PRODUCTO_ENTIDAD { get; set; }
    /// <summary>Unidad de CAN_PRODUCTO_ENTIDAD (ENTIDADES_PRODUCTOS.COD_UNIDAD_MEDIDA_ATENCION).
    /// Con una sola D — a diferencia de COD_UNIDAD_MEDIDAD_ENTIDAD (con dos D) que usa el
    /// SHOWALL del detalle del pedido: son dos SP distintos, cada uno con su propio alias,
    /// y acá va tal cual lo escribió éste.</summary>
    public string? COD_UNIDAD_MEDIDA_ENTIDAD { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCTO_SECUNDARIA { get; set; }

    // Precio del proveedor para ese producto, resuelto por el SP en
    // ENTIDADES_PRODUCTOS_TIPOS_PRECIOS: toma el tipo de precio que tiene configurado el
    // TIPO DE OPERACIÓN del pedido, y de ahí sale VAL_PRECIO_ENTIDAD. DES_TIPO_PRECIO dice
    // cuál se aplicó, que es lo que hace auditable el número.
    //
    // COD_MONEDA es la del PRECIO (ENTIDADES_PRODUCTOS) y DES_MONEDA la del PEDIDO. Antes
    // el SP las traía de tablas distintas sin convertir; ahora sí lo hace: si coinciden deja
    // VAL_PRECIO tal cual, y si no, lo convierte con FNCN_MONEDAS_TIPO_CAMBIO a la moneda
    // del pedido. VAL_PRECIO_ENTIDAD ya viene en la moneda del pedido en los dos casos.
    public string? DES_TIPO_PRECIO { get; set; }
    public string? COD_MONEDA { get; set; }
    public string? DES_MONEDA { get; set; }
    public decimal? VAL_PRECIO_ENTIDAD { get; set; }
}

/// <summary>Vínculo de cabecera ya grabado.</summary>
public class PedidoComercialRequerimientoStock
{
    public string COD_CLASE_OPERACION { get; set; } = "";
    public string COD_TIPO_OPERACION { get; set; } = "";
    public decimal NUM_PEDIDO_COMERCIAL { get; set; }
    public int NUM_REQUERIMIENTO_STOCK { get; set; }
    public string? DES_REQUERIMIENTO_STOCK { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
}

public class PedidoComercialRequerimientoStockDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = "";
    [Required] public string COD_TIPO_OPERACION { get; set; } = "";
    [Required] public decimal NUM_PEDIDO_COMERCIAL { get; set; }
    [Required] public int NUM_REQUERIMIENTO_STOCK { get; set; }
}

/// <summary>Vínculo de detalle ya grabado: qué línea de requerimiento entró en qué
/// línea del pedido, y con cuánta cantidad.</summary>
public class PedidoComercialDetalleRequerimientoStockDetalle
{
    public string COD_CLASE_OPERACION { get; set; } = "";
    public string COD_TIPO_OPERACION { get; set; } = "";
    public decimal NUM_PEDIDO_COMERCIAL { get; set; }
    public int NUM_SECUENCIA { get; set; }
    public int NUM_REQUERIMIENTO_STOCK { get; set; }
    public int NUM_SECUENCIA_REQUERIMIENTO_STOCK { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public decimal? CAN_PRODUCTO_SECUDARIO { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIO { get; set; }
    public string? COD_PRODUCTO { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
}

public class PedidoComercialDetalleRequerimientoStockDetalleDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = "";
    [Required] public string COD_TIPO_OPERACION { get; set; } = "";
    [Required] public decimal NUM_PEDIDO_COMERCIAL { get; set; }
    [Required] public int NUM_SECUENCIA { get; set; }
    [Required] public int NUM_REQUERIMIENTO_STOCK { get; set; }
    [Required] public int NUM_SECUENCIA_REQUERIMIENTO_STOCK { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public decimal? CAN_PRODUCTO_SECUDARIO { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIO { get; set; }
}
