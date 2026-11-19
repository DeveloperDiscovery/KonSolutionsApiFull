using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>LOGISTICA.GUIAS_REMISION: la guía de remisión electrónica, una fila por guía.
///
/// Es un documento legal: el número (serie y correlativo), el XML firmado y la respuesta de SUNAT
/// no cambian aunque el movimiento se toque después. La API nunca devuelve el XML ni el CDR en la
/// ficha —son pesados—: la ficha sólo dice si existen (FLG_TIENE_XML, FLG_TIENE_CDR) y se piden
/// aparte.</summary>
public class GuiaRemision
{
    public int NUM_GUIA_REMISION { get; set; }
    /// <summary>La guía tal como se escribe: T001-00000017.</summary>
    public string NUM_GUIA { get; set; } = string.Empty;
    public decimal NUM_MOVIMIENTO_ALMACEN { get; set; }
    public string COD_SERIE { get; set; } = string.Empty;
    public int NUM_CORRELATIVO { get; set; }
    public string COD_TIPO_COMPROBANTE_SUNAT { get; set; } = string.Empty;
    public DateTime FEC_HORA_EMISION { get; set; }
    public DateTime? FEC_INICIO_TRASLADO { get; set; }
    public string? COD_MODALIDAD_TRASLADO { get; set; }
    public string? COD_MOTIVO_SUNAT { get; set; }
    public string? COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT { get; set; }
    public string? NUM_DOCUMENTO_DESTINATARIO { get; set; }
    public string? DES_NOMBRE_DESTINATARIO { get; set; }
    public string? NUM_PLACA_PRINCIPAL { get; set; }
    public decimal? CAN_PESO_BRUTO_TOTAL { get; set; }
    public string? COD_UNIDAD_PESO_SUNAT { get; set; }
    public short COD_TIPO_ESTADO { get; set; }
    public short COD_ESTADO { get; set; }
    public string? DES_ESTADO { get; set; }
    /// <summary>PEN, ENV, ACE, REC o ANU: lo estable. Los códigos numéricos de estado se pueden renumerar.</summary>
    public string? COD_ESTADO_ALIAS { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }
    public bool FLG_VIGENTE { get; set; }
    public int NUM_INTENTOS { get; set; }
    public bool FLG_TIENE_XML { get; set; }
    public bool FLG_TIENE_CDR { get; set; }
    public string? DES_HASH_XML { get; set; }
    public string? DES_HASH_ZIP { get; set; }
    public string? NUM_TICKET { get; set; }
    public string? COD_RESPUESTA_SUNAT { get; set; }
    public string? DES_RESPUESTA_SUNAT { get; set; }
    public DateTime? FEC_ACEPTACION { get; set; }
    public string? DES_TEXTO_QR { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

/// <summary>LOGISTICA.GUIAS_REMISION_ENVIOS: la bitácora. Una fila por cada vez que la guía se
/// manda a SUNAT, sea aceptada, rechazada o sin respuesta todavía.</summary>
public class GuiaRemisionEnvio
{
    public int NUM_GUIA_REMISION { get; set; }
    public int NUM_INTENTO { get; set; }
    public DateTime FEC_HORA_ENVIO { get; set; }
    public string? NUM_TICKET { get; set; }
    public DateTime? FEC_HORA_RESPUESTA { get; set; }
    public string? COD_RESPUESTA_SUNAT { get; set; }
    public string? DES_RESPUESTA_SUNAT { get; set; }
    public string? DES_HASH_ZIP { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
}

/// <summary>El XML firmado o el CDR de una guía, para descargar.</summary>
public class GuiaRemisionArchivo
{
    public int NUM_GUIA_REMISION { get; set; }
    public string NUM_GUIA { get; set; } = string.Empty;
    public byte[]? Contenido { get; set; }
}

/// <summary>Los datos de la guía que se copian a la tabla "para buscar y listar": se arman al emitir
/// con lo que devuelve PROC_LOGISTICA_REPORTE_MOVIMIENTOS en ese momento.</summary>
public class GuiaRemisionDatos
{
    public DateTime? FEC_INICIO_TRASLADO { get; set; }
    public string? COD_MODALIDAD_TRASLADO { get; set; }
    public string? COD_MOTIVO_SUNAT { get; set; }
    public string? COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT { get; set; }
    public string? NUM_DOCUMENTO_DESTINATARIO { get; set; }
    public string? DES_NOMBRE_DESTINATARIO { get; set; }
    public string? NUM_PLACA_PRINCIPAL { get; set; }
    public decimal? CAN_PESO_BRUTO_TOTAL { get; set; }
    public string? COD_UNIDAD_PESO_SUNAT { get; set; }
}

/// <summary>Crear la guía de un movimiento: toma el número y la deja PENDIENTE. COD_SERIE es
/// opcional: sin él se usa la serie que corresponde al movimiento (ver la función
/// FNC_LOGISTICA_GUIA_SERIES_PARA_MOVIMIENTO).</summary>
public class GuiaRemisionCrearDto : GuiaRemisionDatos
{
    [Required] public decimal NUM_MOVIMIENTO_ALMACEN { get; set; }
    public string? COD_SERIE { get; set; }
}

/// <summary>Reintentar una guía rechazada: vuelve a PENDIENTE con el mismo número y los datos
/// actualizados del movimiento.</summary>
public class GuiaRemisionReintentarDto : GuiaRemisionDatos
{
    [Required] public int NUM_GUIA_REMISION { get; set; }
}

public class GuiaRemisionXmlDto
{
    [Required] public int NUM_GUIA_REMISION { get; set; }
    [Required] public byte[] ARC_XML_FIRMADO { get; set; } = Array.Empty<byte>();
    public string? DES_HASH_XML { get; set; }
    public string? DES_HASH_ZIP { get; set; }
    public string? DES_TEXTO_QR { get; set; }
}

public class GuiaRemisionEnvioDto
{
    [Required] public int NUM_GUIA_REMISION { get; set; }
    [Required] public string NUM_TICKET { get; set; } = string.Empty;
    public string? DES_HASH_ZIP { get; set; }
}

public class GuiaRemisionRespuestaDto
{
    [Required] public int NUM_GUIA_REMISION { get; set; }
    public bool FLG_ACEPTADA { get; set; }
    public string? COD_RESPUESTA_SUNAT { get; set; }
    public string? DES_RESPUESTA_SUNAT { get; set; }
    public byte[]? ARC_CDR { get; set; }
    public string? DES_HASH_ZIP { get; set; }
}

/// <summary>Lo que se elige al emitir y no sale del movimiento (LOGISTICA.GUIAS_REMISION_OPCIONES). Se guarda con la guía para que
/// un reintento mande lo mismo. Sólo se cambia mientras la guía está PENDIENTE.</summary>
public class GuiaRemisionOpcionesDto
{
    /// <summary>Traslado en vehículos de categoría M1 o L: SUNAT no exige los datos del vehículo ni del conductor.</summary>
    public bool FLG_TRASLADO_M1L { get; set; }
    /// <summary>Transbordo programado: el traslado cambia de vehículo en el camino.</summary>
    public bool FLG_TRANSBORDO_PROGRAMADO { get; set; }
    /// <summary>Documento relacionado (opcional): va completo (tipo, descripción del tipo y número) o no va.</summary>
    public string? COD_TIPO_DOCUMENTO_RELACIONADO { get; set; }
    public string? DES_TIPO_DOCUMENTO_RELACIONADO { get; set; }
    public string? NUM_DOCUMENTO_RELACIONADO { get; set; }
    public string? NUM_RUC_EMISOR_RELACIONADO { get; set; }
}

public class GuiaRemisionOpciones : GuiaRemisionOpcionesDto
{
    public int NUM_GUIA_REMISION { get; set; }
}

/// <summary>Emitir la guía de un movimiento: la crea (o sigue con la pendiente), arma y firma el XML, lo manda a SUNAT y consulta el resultado.
/// COD_SERIE es opcional (ver <see cref="GuiaRemisionCrearDto"/>). Las opciones, también: sin ellas se usan las ya guardadas.</summary>
public class GuiaRemisionEmitirDto
{
    [Required] public decimal NUM_MOVIMIENTO_ALMACEN { get; set; }
    public string? COD_SERIE { get; set; }
    public GuiaRemisionOpcionesDto? OPCIONES { get; set; }
}

/// <summary>El cuerpo (opcional) de Reintentar: las opciones nuevas, si se quieren cambiar.</summary>
public class GuiaRemisionReintentarEmisionDto
{
    public GuiaRemisionOpcionesDto? OPCIONES { get; set; }
}

/// <summary>Cómo terminó una emisión, una consulta o un reintento. La respuesta de SUNAT, aceptada o rechazada, es un resultado y no un error:
/// el error es no poder llegar a SUNAT, y eso sale como fallo de la API con la guía como quedó.</summary>
public static class GuiaEmisionResultados
{
    /// <summary>SUNAT aceptó la guía (con su constancia).</summary>
    public const string Aceptada = "ACEPTADA";
    /// <summary>SUNAT rechazó la guía: se corrige lo que dice la respuesta y se usa Reintentar.</summary>
    public const string Rechazada = "RECHAZADA";
    /// <summary>SUNAT recibió la guía pero todavía la procesa: se pregunta de nuevo con Consultar.</summary>
    public const string EnProceso = "EN_PROCESO";
}

public class GuiaRemisionEmisionResultado
{
    public GuiaRemision? GUIA { get; set; }
    /// <summary>ACEPTADA, RECHAZADA o EN_PROCESO (ver <see cref="GuiaEmisionResultados"/>).</summary>
    public string COD_RESULTADO { get; set; } = string.Empty;
    public string DES_MENSAJE { get; set; } = string.Empty;
    public string? COD_RESPUESTA_SUNAT { get; set; }
    public string? DES_RESPUESTA_SUNAT { get; set; }
}

/// <summary>Si el movimiento se puede modificar o ya no: una guía vigente ENVIADA o ACEPTADA lo congela (ver el script
/// 08_GUIAS_REMISION_BLOQUEO_MOVIMIENTO.sql). El mensaje es el mismo que da la base al intentar modificarlo.</summary>
public class GuiaRemisionBloqueo
{
    public decimal NUM_MOVIMIENTO_ALMACEN { get; set; }
    public bool FLG_BLOQUEADO { get; set; }
    public string? DES_MENSAJE { get; set; }
}

/// <summary>Un registro de un catálogo al que le falta un dato que la guía necesita (un código SUNAT, un ubigeo, el RUC de la empresa).
/// Sólo aparecen los que usan las salidas: lo que ninguna salida usa no estorba. Ver PROC_LOGISTICA_GUIAS_REMISION_CATALOGOS_REVISAR.</summary>
public class GuiaRemisionCatalogoPendiente
{
    public int NUM_ORDEN { get; set; }
    /// <summary>Motivos, Unidades de medida, Tipos de documento de identidad, Sucursales, Direcciones de entidades, Productos o Empresa.</summary>
    public string DES_CATALOGO { get; set; } = string.Empty;
    public string? COD_REGISTRO { get; set; }
    public string? DES_REGISTRO { get; set; }
    public string DES_FALTA { get; set; } = string.Empty;
    /// <summary>Dónde se corrige.</summary>
    public string DES_DONDE { get; set; } = string.Empty;
    /// <summary>Cuántas salidas (o líneas, en unidades y productos) lo usan.</summary>
    public int NUM_USOS { get; set; }
    /// <summary>Verdadero: una guía que lo use no se puede emitir. Falso: es un aviso.</summary>
    public bool FLG_IMPIDE_EMITIR { get; set; }
}

/// <summary>Filtros de la bandeja de guías.</summary>
public class GuiaRemisionFiltroDto
{
    public DateTime? FEC_DESDE { get; set; }
    public DateTime? FEC_HASTA { get; set; }
    public string? COD_ESTADO_ALIAS { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
}

// ───────────── LOGISTICA.GUIAS_REMISION_SERIES ─────────────

/// <summary>De dónde sale el número: una fila por serie (T001) con su último correlativo.</summary>
public class GuiaRemisionSerie
{
    public string COD_SERIE { get; set; } = string.Empty;
    public string? DES_SERIE { get; set; }
    public string? COD_SUCURSAL { get; set; }
    public string? DES_SUCURSAL { get; set; }
    public int NUM_ULTIMO_CORRELATIVO { get; set; }
    /// <summary>El número que recibiría la próxima guía de esta serie (informativo).</summary>
    public string? NUM_SIGUIENTE_GUIA { get; set; }
    public bool FLG_ACTIVA { get; set; }
    public int NUM_GUIAS { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class GuiaRemisionSerieInsertDto
{
    [Required] public string COD_SERIE { get; set; } = string.Empty;
    public string? DES_SERIE { get; set; }
    public string? COD_SUCURSAL { get; set; }
    public int NUM_ULTIMO_CORRELATIVO { get; set; }
    public bool FLG_ACTIVA { get; set; } = true;
}

public class GuiaRemisionSerieUpdateDto
{
    [Required] public string COD_SERIE { get; set; } = string.Empty;
    public string? DES_SERIE { get; set; }
    public string? COD_SUCURSAL { get; set; }
    public int NUM_ULTIMO_CORRELATIVO { get; set; }
    public bool FLG_ACTIVA { get; set; } = true;
}
