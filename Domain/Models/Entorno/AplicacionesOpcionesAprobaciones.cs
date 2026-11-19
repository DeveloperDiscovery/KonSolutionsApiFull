using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>ENTORNO.APLICACIONES_OPCIONES_APROBACIONES — un nivel de la cadena de aprobación
/// de una opción de menú. La clave es (COD_APLICACION, COD_OPCION_APLICACION, NUM_SECUENCIA).
///
/// COD_AREA, COD_CARGO y NUM_ORDEN_APROBACION son TINYINT en la base y NUM_SECUENCIA SMALLINT;
/// acá viajan como int? para no obligar al cliente a distinguir anchos que en JSON no existen.
/// SQL Server convierte al ancho real al ejecutar el SP.</summary>
public class AplicacionesOpcionesAprobaciones
{
    public string COD_APLICACION         { get; set; } = "";
    public string COD_OPCION_APLICACION  { get; set; } = "";
    public int?   NUM_SECUENCIA          { get; set; }
    public int?   COD_AREA               { get; set; }
    public int?   COD_CARGO              { get; set; }
    public string? COD_MONEDA            { get; set; }
    public decimal? VAL_MONTO_MINIMO     { get; set; }
    public decimal? VAL_MONTO_MAXIMO     { get; set; }
    public bool?  FLG_HABILITADO         { get; set; }
    public bool?  FLG_APROBADOR          { get; set; }
    public bool?  FLG_NOTIFICADO         { get; set; }
    public bool?  FLG_APROBADOR_ULTIMO   { get; set; }
    public int?   NUM_ORDEN_APROBACION   { get; set; }
    public string COD_USUARIO_REGISTRO   { get; set; } = "";
    public string COD_ESTACION_REGISTRO  { get; set; } = "";
    public DateTime? FEC_REGISTRO        { get; set; }
    public string COD_USUARIO_ACTUALIZACION  { get; set; } = "";
    public string COD_ESTACION_ACTUALIZACION { get; set; } = "";
    public DateTime? FEC_ACTUALIZACION   { get; set; }
}

/// <summary>Cuántos niveles tiene configurada cada opción. Sale de un SP propio que agrupa
/// solo esta tabla: sirve para marcar en la grilla de opciones cuáles ya tienen flujo, sin
/// pedir el detalle de las 200 opciones una por una.</summary>
public class AplicacionesOpcionesAprobacionesResumen
{
    public string COD_APLICACION        { get; set; } = "";
    public string COD_OPCION_APLICACION { get; set; } = "";
    public int    CAN_NIVELES           { get; set; }
    public int    CAN_APROBADORES       { get; set; }
    public int    CAN_NOTIFICADOS       { get; set; }
}

public class AplicacionesOpcionesAprobacionesSearchDto
{
    [Required] public string COD_APLICACION        { get; set; } = "";
    [Required] public string COD_OPCION_APLICACION { get; set; } = "";
    public string Search { get; set; } = "";
}

/// <summary>Alta. NUM_SECUENCIA no se manda: lo asigna el SP.</summary>
public class AplicacionesOpcionesAprobacionesInsertDto
{
    [Required] public string COD_APLICACION        { get; set; } = "";
    [Required] public string COD_OPCION_APLICACION { get; set; } = "";
    public int?  COD_AREA             { get; set; }
    public int?  COD_CARGO            { get; set; }
    public string? COD_MONEDA         { get; set; }
    public decimal? VAL_MONTO_MINIMO  { get; set; }
    public decimal? VAL_MONTO_MAXIMO  { get; set; }
    public bool? FLG_HABILITADO       { get; set; }
    public bool? FLG_APROBADOR        { get; set; }
    public bool? FLG_NOTIFICADO       { get; set; }
    public bool? FLG_APROBADOR_ULTIMO { get; set; }
    public int?  NUM_ORDEN_APROBACION { get; set; }
}

/// <summary>Modificación. NUM_SECUENCIA sí es obligatorio: es parte de la clave.</summary>
public class AplicacionesOpcionesAprobacionesUpdateDto
{
    [Required] public string COD_APLICACION        { get; set; } = "";
    [Required] public string COD_OPCION_APLICACION { get; set; } = "";
    [Required] public int    NUM_SECUENCIA         { get; set; }
    public int?  COD_AREA             { get; set; }
    public int?  COD_CARGO            { get; set; }
    public string? COD_MONEDA         { get; set; }
    public decimal? VAL_MONTO_MINIMO  { get; set; }
    public decimal? VAL_MONTO_MAXIMO  { get; set; }
    public bool? FLG_HABILITADO       { get; set; }
    public bool? FLG_APROBADOR        { get; set; }
    public bool? FLG_NOTIFICADO       { get; set; }
    public bool? FLG_APROBADOR_ULTIMO { get; set; }
    public int?  NUM_ORDEN_APROBACION { get; set; }
}
