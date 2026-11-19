using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>COMERCIAL.PEDIDOS_COMERCIALES_DETALLE — líneas del pedido (Fase 2).</summary>
public class PedidosComercialesDetalle
{
    public decimal? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string? COD_DISENO { get; set; }
    public int? NUM_VERSION { get; set; }
    public string? COD_PRODUCTO { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    // ── Proceso y orden de producción ────────────────────────────────────────────
    // Las cuatro columnas ya existían en COMERCIAL.PEDIDOS_COMERCIALES_DETALLE y los SP ya
    // las leían y grababan; lo que faltaba era el camino de ida y vuelta por el código.
    //
    // Van ANULABLES, y los tres números con el tipo exacto de la columna (int / smallint /
    // tinyint => int? / short? / byte?). Declararlos no anulables rompería la lectura de todo
    // el detalle existente, que los tiene en NULL — es el mismo accidente de FLG_MULTI_LOAD.
    public string? COD_PROCESO { get; set; }
    public int?    NUM_ORDEN_PRODUCCION { get; set; }
    public short?  NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public byte?   NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    /// <summary>Descripciones que desnormalizan SHOWALL / SHOWBYID / SEARCH (PRODUCCION.PROCESOS
    /// y PLANEAMIENTO.ORDEN_PRODUCCION). Son de sólo lectura: no viajan en el alta.</summary>
    public string? DES_PROCESO { get; set; }
    public string? DES_ORDEN_PRODUCCION { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public decimal? CAN_PRODUCTO_SECUDARIO { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIO { get; set; }
    public string? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public decimal? PRE_UNITARIO { get; set; }
    public decimal? VAL_DESCUENTO { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    /// <summary>"Cantidad Proveedor" — caption pedida por el usuario. CAN_PRODUCTO (arriba)
    /// es "Cantidad Propia". El SP calcula VAL_TOTAL como PRE_UNITARIO * CAN_PRODUCTO_ENTIDAD,
    /// no con CAN_PRODUCTO.</summary>
    public decimal? CAN_PRODUCTO_ENTIDAD { get; set; }
    /// <summary>"UM Proveedor" — unidad de CAN_PRODUCTO_ENTIDAD. Desnormalizada por el
    /// SHOWALL desde COMERCIAL.ENTIDADES_PRODUCTOS.COD_UNIDAD_MEDIDA_ATENCION (alias "j" del
    /// SP) — no es columna propia de PEDIDOS_COMERCIALES_DETALLE. El nombre repite el typo
    /// tal cual lo devuelve el SP (MEDIDAD, con dos D) porque Dapper mapea por nombre exacto.</summary>
    public string? COD_UNIDAD_MEDIDAD_ENTIDAD { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDO { get; set; }
    public decimal? CAN_PRODUCTO_SECUNDARIO_ATENDIDO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }

    // Desnormalizados del SP
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_LOTE_CONTROL { get; set; } = string.Empty;
    public string DES_OBSERVACION_PEDIDOS_COMERCIALES { get; set; } = string.Empty;
    public string DES_PEDIDO_COMERCIAL { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_DESCUENTO { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
}

public class PedidosComercialesDetalleInsertDto
{
    // La clave del detalle son cuatro columnas. Clase y tipo de operación faltaban acá,
    // y el SP las exige: sin ellas no se puede identificar la línea.
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    [Required] public decimal NUM_PEDIDO_COMERCIAL { get; set; }
    [Required] public int NUM_SECUENCIA { get; set; }
    public string? COD_DISENO { get; set; }
    public int? NUM_VERSION { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public string? NUM_LOTE_CONTROL { get; set; }
    // ── Proceso y orden de producción ────────────────────────────────────────────
    // Las cuatro columnas ya existían en COMERCIAL.PEDIDOS_COMERCIALES_DETALLE y los SP ya
    // las leían y grababan; lo que faltaba era el camino de ida y vuelta por el código.
    //
    // Van ANULABLES, y los tres números con el tipo exacto de la columna (int / smallint /
    // tinyint => int? / short? / byte?). Declararlos no anulables rompería la lectura de todo
    // el detalle existente, que los tiene en NULL — es el mismo accidente de FLG_MULTI_LOAD.
    public string? COD_PROCESO { get; set; }
    public int?    NUM_ORDEN_PRODUCCION { get; set; }
    public short?  NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public byte?   NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public decimal? CAN_PRODUCTO_SECUDARIO { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIO { get; set; }
    public string? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public decimal? PRE_UNITARIO { get; set; }
    public decimal? VAL_DESCUENTO { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    /// <summary>Obligatoria para el SP desde este cambio: sin ella, Insert/Update de la
    /// línea fallan con "expects parameter '@CAN_PRODUCTO_ENTIDAD'".</summary>
    public decimal? CAN_PRODUCTO_ENTIDAD { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDO { get; set; }
    public decimal? CAN_PRODUCTO_SECUNDARIO_ATENDIDO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    // Nullable a propósito: el controlador les pone el usuario y la estación de la sesión,
    // pero lo hace DESPUÉS de validar el ModelState. Como string no-nullable, la validación
    // implícita los exigía en el JSON y devolvía 400 antes de que el controlador llegara a
    // asignarlos — el front nunca los manda, ni tiene por qué.
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
}

public class PedidosComercialesDetalleUpdateDto : PedidosComercialesDetalleInsertDto { }

public class PedidosComercialesDetalleSearchDto
{
    // El SP ahora exige también clase y tipo de operación: la clave del pedido son las tres
    // columnas, no solo el número (dos pedidos de clases distintas pueden compartir número).
    [Required] public string CodClaseOperacion { get; set; } = string.Empty;
    [Required] public string CodTipoOperacion { get; set; } = string.Empty;
    [Required] public decimal NumPedidoComercial { get; set; }
    public string? Search { get; set; }
}
