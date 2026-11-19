using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>COMERCIAL.PEDIDOS_COMERCIALES — Fase 1: solo cabecera (sin Detalle/Cálculo/
/// Movimientos de Almacén/Especificaciones Técnicas/Secuencias, que quedan para una fase 2).
/// Una misma tabla/pantalla sirve para Proformas, Órdenes de Compra, Pedidos de Venta, etc.,
/// distinguidos por COD_CLASE_OPERACION (ver DES_PARAMETRO en SEGURIDAD.APLICACIONES_OPCIONES,
/// mismo mecanismo que Entidades/Clientes/Proveedores).</summary>
public class PedidosComerciales
{
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal? NUM_PEDIDO_COMERCIAL { get; set; }
    public string DES_PEDIDO_COMERCIAL { get; set; } = string.Empty;
    public string? COD_TIPO_COMPROBANTE_PAGO { get; set; }
    public string? NUM_DOCUMENTO { get; set; }
    public decimal? NUM_PROYECTO { get; set; }
    public string? COD_TIPO_PEDIDO_COMERCIAL { get; set; }
    public DateTime? FEC_PEDIDO { get; set; }
    public DateTime? FEC_VECIMIENTO { get; set; }
    public DateTime? FEC_ENTREGA { get; set; }
    public DateTime? FEC_ENTREGA_PUBLICA { get; set; }
    public string? COD_TIPO_ENTIDAD { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public decimal? NUM_TEMPORADA { get; set; }
    public int? NUM_COLECCION { get; set; }
    public string? COD_MONEDA { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? NUM_UNIDAD_COMERCIAL { get; set; }
    public decimal? NUM_SECUENCIA_DIRECCION { get; set; }
    public string? COD_CENTRO_COSTO_SOLICITANTE { get; set; }
    public string? COD_CENTRO_COSTO_DESTINO { get; set; }
    public string? COD_SUCURSAL { get; set; }
    public string? COD_FORMA_PAGO { get; set; }
    public string? COD_CONDICION_PAGO { get; set; }
    public string? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    public decimal? COD_ENTIDAD_VENDEDOR { get; set; }
    public string? NUM_PURCHASE_ORDER { get; set; }
    public string? COD_PAIS_DESTINO { get; set; }
    public decimal? NUM_LOTE_CONTROL { get; set; }
    public decimal? NUM_RENDICION_CUENTAS { get; set; }
    public string? COD_TIPO_MOTIVO { get; set; }
    public string? COD_MOTIVO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public decimal? NUM_PEDIDO_COMERCIAL_RELACIONADO { get; set; }
    public string? NUM_TRACKING { get; set; }
    public string? COD_INCOTERM { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }

    // Desnormalizados del SP (joins) — nombres tal cual los alias-ea el SHOWALL/SHOWBYID.
    public string DES_CENTRO_COSTO { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_PREFIJO_USUARIO_COMERCIAL { get; set; } = string.Empty;
    public string DES_CONDICION_PAGO { get; set; } = string.Empty;
    public string DES_DISENO_ENTIDAD { get; set; } = string.Empty;
    public string DES_FORMA_PAGO { get; set; } = string.Empty;
    public string DES_INCOTERM { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_MOTIVO { get; set; } = string.Empty;
    public string DES_BANDERA_EMOJI { get; set; } = string.Empty;
    public string DES_CAPITAL { get; set; } = string.Empty;
    public string DES_PAIS { get; set; } = string.Empty;
    public string DES_PAIS_INGLES { get; set; } = string.Empty;
    public string DES_PAIS_NATIVO { get; set; } = string.Empty;
    public string DES_REGION { get; set; } = string.Empty;
    public string DES_SUBREGION { get; set; } = string.Empty;
    public string DES_PROYECTO { get; set; } = string.Empty;
    public string DES_TIPO_DESCUENTO { get; set; } = string.Empty;
    public string DES_TIPO_PEDIDO_COMERCIAL { get; set; } = string.Empty;
    public string DES_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    public string DES_TEMPORADA { get; set; } = string.Empty;
    public string DES_COLECCION { get; set; } = string.Empty;
    public string DES_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_DIRECCION { get; set; } = string.Empty;
    public string DES_INTERIOR { get; set; } = string.Empty;
    public string DES_KILOMETRO { get; set; } = string.Empty;
    public string DES_LOTE { get; set; } = string.Empty;
    public string DES_MANZANA { get; set; } = string.Empty;
    public string DES_NOMBRE_VIA { get; set; } = string.Empty;
    public string DES_NOMBRE_ZONA { get; set; } = string.Empty;
    public string DES_NUMERACION { get; set; } = string.Empty;
    public string DES_REFERENCIA { get; set; } = string.Empty;
    public string DES_RENDICION_CUENTAS { get; set; } = string.Empty;
    public string DES_ENTIDAD_VENDEDOR { get; set; } = string.Empty;
    /// <summary>Dirección de entrega ya resuelta por el SP (sucursal o entidad, según
    /// FLG_POSITIVO del tipo de operación).</summary>
    public string DES_DIRECCION_SUCURSAL { get; set; } = string.Empty;
    /// <summary>true si el pedido tiene líneas vinculadas a requerimientos de stock. Lo calcula el SP.</summary>
    public bool? FLG_POR_REQUERIMIENTO { get; set; }

    // ── Operación de origen ──────────────────────────────────────────────────
    // Cuando el tipo de operación de este pedido está sincronizado con otro (por ejemplo,
    // una Orden de Compra que nació de una Proforma), el SP trae también la clase y el tipo
    // de operación de ORIGEN, vía ESTANDAR.TIPOS_OPERACION.COD_CLASE_OPERACION_SINCRONIZADA /
    // COD_TIPO_OPERACION_SINCRONIZADA. Todo null si el tipo de operación no sincroniza con
    // ningún otro.
    public string? COD_CLASE_OPERACION_ORIGEN { get; set; }
    public string? DES_CLASE_OPERACION_ORIGEN { get; set; }
    public string? COD_TIPO_OPERACION_ORIGEN { get; set; }
    public string? DES_TIPO_OPERACION_ORIGEN { get; set; }
    /// <summary>Los cuatro campos de arriba, ya armados en un solo texto por el propio SP
    /// ("ORDC-Órdenes de Compra / OC-Orden de Compra [Nº 127]"). Null si el tipo de operación
    /// no sincroniza con ningún otro — no hay pedido de origen que mostrar.</summary>
    public string? DES_PEDIDO_ORIGEN { get; set; }

    // ── Notificación en curso (ENTORNO.NOTIFICACIONES) ───────────────────────
    // Sólo lectura: la escribe el flujo de aprobación, no la pantalla. El documento
    // guarda el número y el resto viene por JOIN, incluidos los colores del estado,
    // que salen de ESTANDAR.ESTADOS como los de cualquier otro estado del sistema.
    public long?    NUM_NOTIFICACION            { get; set; }
    public string?  DES_NOTIFICACION            { get; set; }
    public DateTime? FEC_NOTIFICACION           { get; set; }
    public string?  DES_ESTADO_NOTIFICACION     { get; set; }
    public string?  DES_BACKCOLOR_NOTIFICACION  { get; set; }
    public string?  DES_FORECOLOR_NOTIFICACION  { get; set; }

}

public class PedidosComercialesInsertDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    /// <summary>0 (o null) para alta nueva — el SP INSERT autonumera dentro de la clase/tipo
    /// de operación. Un valor existente hace que el SP actualice ese registro en vez de crear
    /// uno nuevo (comportamiento "upsert" tal cual viene del SP, no se cambió acá).</summary>
    public decimal? NUM_PEDIDO_COMERCIAL { get; set; }
    [Required] public string DES_PEDIDO_COMERCIAL { get; set; } = string.Empty;
    public string? COD_TIPO_COMPROBANTE_PAGO { get; set; }
    public string? NUM_DOCUMENTO { get; set; }
    public decimal? NUM_PROYECTO { get; set; }
    public string? COD_TIPO_PEDIDO_COMERCIAL { get; set; }
    public DateTime? FEC_PEDIDO { get; set; }
    public DateTime? FEC_VECIMIENTO { get; set; }
    public DateTime? FEC_ENTREGA { get; set; }
    public DateTime? FEC_ENTREGA_PUBLICA { get; set; }
    public string? COD_TIPO_ENTIDAD { get; set; }
    [Required] public decimal? COD_ENTIDAD { get; set; }
    public decimal? NUM_TEMPORADA { get; set; }
    public int? NUM_COLECCION { get; set; }
    public string? COD_MONEDA { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? NUM_UNIDAD_COMERCIAL { get; set; }
    public decimal? NUM_SECUENCIA_DIRECCION { get; set; }
    public string? COD_CENTRO_COSTO_SOLICITANTE { get; set; }
    public string? COD_CENTRO_COSTO_DESTINO { get; set; }
    public string? COD_SUCURSAL { get; set; }
    public string? COD_FORMA_PAGO { get; set; }
    public string? COD_CONDICION_PAGO { get; set; }
    public string? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    public decimal? COD_ENTIDAD_VENDEDOR { get; set; }
    public string? NUM_PURCHASE_ORDER { get; set; }
    public string? COD_PAIS_DESTINO { get; set; }
    public decimal? NUM_LOTE_CONTROL { get; set; }
    public decimal? NUM_RENDICION_CUENTAS { get; set; }
    public string? COD_TIPO_MOTIVO { get; set; }
    public string? COD_MOTIVO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public decimal? NUM_PEDIDO_COMERCIAL_RELACIONADO { get; set; }
    public string? NUM_TRACKING { get; set; }
    public string? COD_INCOTERM { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

/// <summary>Igual que Insert, pero exige NUM_PEDIDO_COMERCIAL (edición de un registro
/// existente) — PROC_COMERCIAL_PEDIDOS_COMERCIALES_UPDATE no autonumera ni crea.</summary>
public class PedidosComercialesUpdateDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    [Required] public decimal NUM_PEDIDO_COMERCIAL { get; set; }
    [Required] public string DES_PEDIDO_COMERCIAL { get; set; } = string.Empty;
    public string? COD_TIPO_COMPROBANTE_PAGO { get; set; }
    public string? NUM_DOCUMENTO { get; set; }
    public decimal? NUM_PROYECTO { get; set; }
    public string? COD_TIPO_PEDIDO_COMERCIAL { get; set; }
    public DateTime? FEC_PEDIDO { get; set; }
    public DateTime? FEC_VECIMIENTO { get; set; }
    public DateTime? FEC_ENTREGA { get; set; }
    public DateTime? FEC_ENTREGA_PUBLICA { get; set; }
    public string? COD_TIPO_ENTIDAD { get; set; }
    [Required] public decimal? COD_ENTIDAD { get; set; }
    public decimal? NUM_TEMPORADA { get; set; }
    public int? NUM_COLECCION { get; set; }
    public string? COD_MONEDA { get; set; }
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? NUM_UNIDAD_COMERCIAL { get; set; }
    public decimal? NUM_SECUENCIA_DIRECCION { get; set; }
    public string? COD_CENTRO_COSTO_SOLICITANTE { get; set; }
    public string? COD_CENTRO_COSTO_DESTINO { get; set; }
    public string? COD_SUCURSAL { get; set; }
    public string? COD_FORMA_PAGO { get; set; }
    public string? COD_CONDICION_PAGO { get; set; }
    public string? COD_TIPO_DESCUENTO { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public decimal? VAL_TOTAL { get; set; }
    public decimal? COD_ENTIDAD_VENDEDOR { get; set; }
    public string? NUM_PURCHASE_ORDER { get; set; }
    public string? COD_PAIS_DESTINO { get; set; }
    public decimal? NUM_LOTE_CONTROL { get; set; }
    public decimal? NUM_RENDICION_CUENTAS { get; set; }
    public string? COD_TIPO_MOTIVO { get; set; }
    public string? COD_MOTIVO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public decimal? NUM_PEDIDO_COMERCIAL_RELACIONADO { get; set; }
    public string? NUM_TRACKING { get; set; }
    public string? COD_INCOTERM { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class PedidosComercialesSearchDto
{
    [Required] public string CodClaseOperacion { get; set; } = string.Empty;
    [Required] public string CodTipoOperacion { get; set; } = string.Empty;
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_COMERCIAL_PEDIDOS_COMERCIALES_LOADCOMBOS: los nueve
/// catálogos que la cabecera del Pedido Comercial necesita al abrirse, en un solo viaje.
/// El orden de las propiedades acá refleja el orden de los result sets del SP.</summary>
public class PedidosComercialesLoadCombosResult
{
    public string CodClaseOperacion { get; set; } = string.Empty;

    /// <summary>Tipado y no ComboboxItem a propósito: el SHOWCOMBOX de Tipos de Operación
    /// devuelve además los flags (positivo, comercio exterior, centro de costo, proyecto,
    /// lote de control) y el tipo de estado relacionado, y la cabecera los necesita para
    /// decidir qué campos habilita. Reducirlos a Id/Texto habría roto esas reglas.</summary>
    public List<TipoOperacionDeClase> TiposOperacion { get; set; } = new();
    /// <summary>Todos los pares (tipo de operación → tipo de comprobante) de la clase.
    /// La pantalla filtra en memoria al elegir el Tipo de Operación, así no vuelve al
    /// servidor cada vez que el usuario cambia de tipo.</summary>
    public List<TipoComprobantePagoDeOperacion> TiposComprobantePago { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Paises           { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Incoterms        { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Monedas          { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> FormasPago       { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> CondicionesPago  { get; set; } = new();
    /// <summary>Lleva la tasa además del código y la descripción: al elegir un tipo de
    /// descuento la pantalla completa sola el campo Tasa, que es de sólo lectura.</summary>
    public List<TipoDescuentoConTasa> TiposDescuento                  { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Proyectos        { get; set; } = new();
}

public class TipoComprobantePagoDeOperacion
{
    public string COD_TIPO_OPERACION        { get; set; } = string.Empty;
    public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public string DES_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
}

public class TipoDescuentoConTasa
{
    public int?     COD_TIPO_DESCUENTO { get; set; }
    public string   DES_TIPO_DESCUENTO { get; set; } = string.Empty;
    public decimal? TAS_DESCUENTO      { get; set; }
}

public class TipoOperacionDeClase
{
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public string DES_TIPO_OPERACION { get; set; } = string.Empty;
    public int?   COD_TIPO_ESTADO_RELACIONADO { get; set; }
    public string? COD_TIPO_ENTIDAD { get; set; }
    public bool? FLG_POSITIVO          { get; set; }
    public bool? FLG_COMERCIO_EXTERIOR { get; set; }
    public bool? FLG_CENTRO_COSTO      { get; set; }
    public bool? FLG_PROYECTO          { get; set; }
    public bool? FLG_LOTE_CONTROL      { get; set; }
    // El SHOWCOMBOX de Tipos de Operación ya trae estas dos vía JOIN a
    // COMERCIAL.TIPOS_PEDIDO_COMERCIAL: sólo faltaba declararlas acá para que Dapper
    // las mapee y el pedido las herede al elegir el Tipo de Operación.
    public string? COD_TIPO_PEDIDO_COMERCIAL { get; set; }
    public string? DES_TIPO_PEDIDO_COMERCIAL { get; set; }
}
