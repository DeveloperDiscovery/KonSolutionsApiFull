namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Cabecera de una orden de compra vista desde un movimiento de almacén.
///
/// La MISMA forma sirve para los dos lados de la pantalla —disponibles y asignados—
/// porque los dos SP devuelven exactamente las mismas columnas. Tener un solo modelo es
/// lo que permite que las dos grillas sean el mismo componente: si mañana se separan, se
/// separan también acá, y no antes.</summary>
public class MovimientoAlmacenOrdenCompra
{
    // Clase y tipo de operación de la orden. Vienen en la fila desde que el SP de
    // DISPONIBLES los devuelve, y son lo que el SP de detalle exige: así el detalle se
    // pide con los valores de lo que el usuario marcó, sin configuración de por medio.
    // Del lado ASIGNADOS llegan en null y no hacen falta: ese detalle no los recibe.
    public string?   COD_CLASE_OPERACION  { get; set; }
    public string?   COD_TIPO_OPERACION   { get; set; }

    /// <summary>Descripción del tipo de operación. Llega sólo del lado DISPONIBLES: es el
    /// único SP que hace el JOIN con ESTANDAR.TIPOS_OPERACION. Del lado asignados queda en
    /// null y la grilla de ese lado no muestra la columna.</summary>
    public string?   DES_TIPO_OPERACION   { get; set; }
    public int       NUM_PEDIDO_COMERCIAL { get; set; }
    public string?   DES_PEDIDO_COMERCIAL { get; set; }
    public DateTime? FEC_PEDIDO           { get; set; }
    public DateTime? FEC_ENTREGA          { get; set; }

    // Estado con sus colores (ESTANDAR.ESTADOS), igual que en el resto del sistema.
    public string?   DES_ESTADO           { get; set; }
    public string?   DES_BACKCOLOR        { get; set; }
    public string?   DES_FORECOLOR        { get; set; }
}

/// <summary>Línea de una orden de compra vista desde un movimiento.
///
/// También compartida por los dos lados, con una diferencia que importa: el SP de
/// DISPONIBLES devuelve además lo atendido y lo pendiente; el de ASIGNADOS no, porque ahí
/// la cantidad que interesa es la que se asignó a este movimiento. Las dos columnas de
/// más quedan en null para el lado asignado y la pantalla no las muestra.</summary>
public class MovimientoAlmacenOrdenCompraDetalle
{
    public int      NUM_PEDIDO_COMERCIAL { get; set; }
    public int      NUM_SECUENCIA        { get; set; }

    /// <summary>Clase y tipo de operación de la orden a la que pertenece la línea.
    ///
    /// Las devuelve el SP de ASIGNADOS, que las tiene en su propia tabla. Del lado
    /// DISPONIBLES vienen en null, y ahí se toman de la cabecera.
    ///
    /// Hacen falta para QUITAR: el SP necesita saber a qué documento pertenece la línea, y
    /// del lado asignados la CABECERA las trae en null —su SP no hace el JOIN con
    /// TIPOS_OPERACION—, así que la única fuente confiable es la propia línea.</summary>
    public string?  COD_CLASE_OPERACION { get; set; }
    public string?  COD_TIPO_OPERACION { get; set; }
    public string?  COD_PRODUCTO         { get; set; }
    public string?  DES_PRODUCTO         { get; set; }
    public string?  DES_UNIDAD_MEDIDA    { get; set; }
    public decimal? CAN_PRODUCTO         { get; set; }

    // Sólo del lado DISPONIBLES.
    public decimal? CAN_PRODUCTO_ATENDIDO           { get; set; }
    public decimal? CAN_PRODUCTO_ATENDIDO_PENDIENTE { get; set; }

    /// <summary>Lo que se va a recibir en ESTE movimiento. El SP la devuelve en cero
    /// (CAN_PRODUCTO * 0): es un campo para llenar, no un dato guardado. La escribe el
    /// usuario en la grilla y viaja hacia el alta cuando se cablee.</summary>
    public decimal? CAN_PRODUCTO_RECIBIDA           { get; set; }

    // ── Lote, proceso y orden de producción ──────────────────────────────────
    // Los devuelve el SP de DISPONIBLES desde la línea del pedido. Anulables: una compra
    // que no es de consumo de producción no los lleva, y eso es lo normal, no un error.
    public string?  NUM_LOTE_CONTROL            { get; set; }
    public string?  COD_PROCESO                 { get; set; }
    public string?  DES_PROCESO                 { get; set; }
    public int?     NUM_ORDEN_PRODUCCION        { get; set; }
    public string?  DES_ORDEN_PRODUCCION        { get; set; }
    public short?   NUM_ORDEN_PRODUCCION_ITEM   { get; set; }
    public byte?    NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }

    // ── Dónde entra la mercadería ────────────────────────────────────────────
    // El almacén sale de la CABECERA del movimiento —el SP lo resuelve y lo repite en cada
    // fila—, así que no se elige: es el mismo para todas. La ubicación sí se elige, y viene
    // PROPUESTA: el SP intenta primero el contenedor marcado como nodo de distribución y
    // después la ubicación donde ese producto ya tiene stock.
    public string?  COD_ALMACEN             { get; set; }
    public string?  COD_UBICACION_ALMACEN   { get; set; }
    public string?  DES_UBICACION_ALMACEN   { get; set; }
}

/// <summary>Lo que la pantalla manda al cambiar el lote o la ubicación de una línea.
///
/// Los dos van juntos en un solo DTO —y en un solo SP— porque el usuario los elige en el
/// mismo diálogo y porque el SP los escribe con un UPDATE de dos columnas. Separarlos serían
/// dos viajes para una sola decisión.
///
/// Anulables los dos: quitar el lote o dejar la ubicación en blanco son acciones válidas, y
/// distinguir "no lo mandó" de "lo puso en vacío" no aporta nada acá — la pantalla siempre
/// manda el estado completo de la línea.</summary>
public class LoteUbicacionLineaDto
{
    public string COD_CLASE_OPERACION { get; set; } = "";
    public string COD_TIPO_OPERACION { get; set; } = "";
    public decimal NUM_PEDIDO_COMERCIAL { get; set; }
    public int NUM_SECUENCIA { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    public string? COD_UBICACION_ALMACEN { get; set; }
}

/// <summary>Las líneas marcadas, ya en el formato que el SP espera.
///
/// Es un objeto con UNA cadena y no la cadena suelta porque el cuerpo de un POST con un
/// string pelado obliga a mandarlo entre comillas y se rompe en cuanto alguien prueba desde
/// otra herramienta. Con un objeto, el JSON es obvio: {"lineas": "P2S1S2_P10S1"}.</summary>
public class LineasSeleccionadasDto
{
    /// <summary>A qué documento pertenecen las líneas. OBLIGATORIOS: la numeración de pedidos
    /// se reinicia por clase de operación, así que el número solo no identifica nada — el
    /// pedido 2 de Órdenes de Compra y el 2 de Proformas son documentos distintos.</summary>
    public string COD_CLASE_OPERACION { get; set; } = "";
    public string COD_TIPO_OPERACION { get; set; } = "";

    public string Lineas { get; set; } = "";
}

/// <summary>Lo mismo, para la pestaña de Requerimientos.
///
/// NO reutiliza LineasSeleccionadasDto porque los dos campos que lo distinguen son
/// justamente los que acá no existen: un requerimiento no tiene clase ni tipo de
/// operación. En cambio lleva el criterio, que ahí no significa nada.
///
/// El CRITERIO viaja porque decide DE DÓNDE sale la mercadería. En una compra la
/// ubicación la elige quien recibe; en un requerimiento se busca donde hay stock, y el
/// criterio es esa búsqueda. Si no se mandara el mismo que usó la pantalla, el SP podría
/// sacar de una ubicación distinta de la que el usuario vio en la grilla.</summary>
public class RequerimientoLineasSeleccionadasDto
{
    public string Lineas { get; set; } = "";

    /// <summary>MAYOR | MENOR | LOTE. El mismo que la pantalla usó para armar la grilla.</summary>
    public string COD_CRITERIO { get; set; } = "MAYOR";
}
