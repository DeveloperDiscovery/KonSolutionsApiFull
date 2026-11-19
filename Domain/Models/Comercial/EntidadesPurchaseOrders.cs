using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>COMERCIAL.ENTIDADES_PURCHARSE_ORDERS — la orden de compra que manda el cliente.
/// Clave (COD_ENTIDAD, NUM_PURCHASE_ORDER).
///
/// El nombre de la tabla tiene el typo "PURCHARSE" y así está en la base, así que los SP y las
/// constantes lo respetan. Las clases de C# van con la ortografía correcta: es lo que se lee en
/// la pantalla y en el resto del código, y arrastrar el error no lo arregla.</summary>
public class EntidadesPurchaseOrders
{
    public decimal? COD_ENTIDAD                 { get; set; }
    [Required] public string NUM_PURCHASE_ORDER { get; set; } = "";
    public string? COD_DISENO                   { get; set; }
    public string? DES_DISENO_ENTIDAD           { get; set; }
    public DateTime? FEC_DISENO_ENTIDAD         { get; set; }
    public bool?  FLG_ITERAR_EMBALAJE           { get; set; }
    public bool?  FLG_PURCHASE_ORDER_GENERICA   { get; set; }

    // Temporada y colección del cliente — COMERCIAL.ENTIDADES_TEMPORADAS[_COLECCIONES].
    // Las dos admiten NULL: una PO genérica, o una cargada antes de que existieran las
    // temporadas, no tiene por qué pertenecer a ninguna.
    //
    // La colección NO existe sin su temporada: su clave es (COD_ENTIDAD, NUM_TEMPORADA,
    // NUM_COLECCION). Por eso NUM_COLECCION con NUM_TEMPORADA en NULL es un estado inválido,
    // y la pantalla lo impide.
    public int?   NUM_TEMPORADA                 { get; set; }
    public int?   NUM_COLECCION                 { get; set; }
    public int?   COD_TIPO_ESTADO               { get; set; }
    public int?   COD_ESTADO                    { get; set; }
    public string COD_USUARIO_REGISTRO          { get; set; } = "";
    public string COD_ESTACION_REGISTRO         { get; set; } = "";
    public DateTime? FEC_REGISTRO               { get; set; }
    public string COD_USUARIO_ACTUALIZACION     { get; set; } = "";
    public string COD_ESTACION_ACTUALIZACION    { get; set; } = "";
    public DateTime? FEC_ACTUALIZACION          { get; set; }

    // Solo lectura: los traen los SP por LEFT JOIN a ESTANDAR.ESTADOS y ESTANDAR.ENTIDADES.
    public string? DES_ESTADO        { get; set; }
    public string? DES_BACKCOLOR     { get; set; }
    public string? DES_FORECOLOR     { get; set; }
    public string? DES_NOMBRE_COMPLETO { get; set; }
    public string? DES_COMERCIAL     { get; set; }
    public string? DES_TEMPORADA     { get; set; }
    public string? DES_COLECCION     { get; set; }
}

/// <summary>COMERCIAL.ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO — cómo sale embalado
/// cada despacho de la orden. Clave (COD_ENTIDAD, NUM_PURCHASE_ORDER, NUM_SECUENCIA).</summary>
public class EntidadesPurchaseOrdersDespacho
{
    public decimal? COD_ENTIDAD        { get; set; }
    public string NUM_PURCHASE_ORDER   { get; set; } = "";
    public int?   NUM_SECUENCIA        { get; set; }
    public decimal? CAN_EMBALAJE       { get; set; }
    public string? COD_PRODUCTO_EMBALAJE_INNERPACK  { get; set; }
    public string? COD_PRODUCTO_EMBALAJE_MASTERPACK { get; set; }
    public string COD_USUARIO_REGISTRO       { get; set; } = "";
    public string COD_ESTACION_REGISTRO      { get; set; } = "";
    public DateTime? FEC_REGISTRO            { get; set; }
    public string COD_USUARIO_ACTUALIZACION  { get; set; } = "";
    public string COD_ESTACION_ACTUALIZACION { get; set; } = "";
    public DateTime? FEC_ACTUALIZACION       { get; set; }

    // Solo lectura (LEFT JOIN a la cabecera y a ESTANDAR.PRODUCTOS del innerpack).
    public string? DES_DISENO_ENTIDAD { get; set; }
    public string? DES_PRODUCTO       { get; set; }
    public string? DES_DETALLADA      { get; set; }
}

/// <summary>COMERCIAL.…_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS — qué exige cada
/// despacho. Clave (COD_ENTIDAD, NUM_PURCHASE_ORDER, NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA):
/// una especificación no se repite dentro del mismo despacho.</summary>
public class EntidadesPurchaseOrdersEspecificacionTecnica
{
    public decimal? COD_ENTIDAD      { get; set; }
    public string NUM_PURCHASE_ORDER { get; set; } = "";
    public int?   NUM_SECUENCIA      { get; set; }
    public string COD_ESPECIFICACION_TECNICA { get; set; } = "";
    public int?   COD_ITEM_TECNICO   { get; set; }
    public bool?  FLG_HEADER         { get; set; }
    public string COD_USUARIO_REGISTRO       { get; set; } = "";
    public string COD_ESTACION_REGISTRO      { get; set; } = "";
    public DateTime? FEC_REGISTRO            { get; set; }
    public string COD_USUARIO_ACTUALIZACION  { get; set; } = "";
    public string COD_ESTACION_ACTUALIZACION { get; set; } = "";
    public DateTime? FEC_ACTUALIZACION       { get; set; }
}


// ── DTOs · Cabecera ─────────────────────────────────────────────────────────
public class EntidadesPurchaseOrdersSearchDto
{
    public decimal COD_ENTIDAD { get; set; }
    public string  Search      { get; set; } = "";
}

public class EntidadesPurchaseOrdersInsertDto
{
    public decimal COD_ENTIDAD { get; set; }
    [Required] public string NUM_PURCHASE_ORDER { get; set; } = "";
    public string? COD_DISENO                 { get; set; }
    public string? DES_DISENO_ENTIDAD         { get; set; }
    public DateTime? FEC_DISENO_ENTIDAD       { get; set; }
    public bool? FLG_ITERAR_EMBALAJE          { get; set; }
    public bool? FLG_PURCHASE_ORDER_GENERICA  { get; set; }
    public int?  NUM_TEMPORADA                { get; set; }
    public int?  NUM_COLECCION                { get; set; }
    public int?  COD_TIPO_ESTADO              { get; set; }
    public int?  COD_ESTADO                   { get; set; }
}

public class EntidadesPurchaseOrdersUpdateDto : EntidadesPurchaseOrdersInsertDto { }


// ── DTOs · Despacho ─────────────────────────────────────────────────────────
public class EntidadesPurchaseOrdersDespachoSearchDto
{
    public decimal COD_ENTIDAD { get; set; }
    [Required] public string NUM_PURCHASE_ORDER { get; set; } = "";
    public string Search { get; set; } = "";
}

/// <summary>El alta manda NUM_SECUENCIA porque el SP no la calcula: la pone el llamador.
/// Ver el LEEME — es una limitación conocida del procedimiento, no un olvido de acá.</summary>
public class EntidadesPurchaseOrdersDespachoInsertDto
{
    public decimal COD_ENTIDAD { get; set; }
    [Required] public string NUM_PURCHASE_ORDER { get; set; } = "";
    public int NUM_SECUENCIA   { get; set; }
    public decimal? CAN_EMBALAJE { get; set; }
    public string? COD_PRODUCTO_EMBALAJE_INNERPACK  { get; set; }
    public string? COD_PRODUCTO_EMBALAJE_MASTERPACK { get; set; }
}

public class EntidadesPurchaseOrdersDespachoUpdateDto : EntidadesPurchaseOrdersDespachoInsertDto { }


// ── DTOs · Especificación técnica ───────────────────────────────────────────
public class EntidadesPurchaseOrdersEspecificacionTecnicaSearchDto
{
    public decimal COD_ENTIDAD { get; set; }
    [Required] public string NUM_PURCHASE_ORDER { get; set; } = "";
    public int NUM_SECUENCIA { get; set; }
    public string Search { get; set; } = "";
}

public class EntidadesPurchaseOrdersEspecificacionTecnicaInsertDto
{
    public decimal COD_ENTIDAD { get; set; }
    [Required] public string NUM_PURCHASE_ORDER { get; set; } = "";
    public int NUM_SECUENCIA { get; set; }
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = "";
    public int?  COD_ITEM_TECNICO { get; set; }
    public bool? FLG_HEADER       { get; set; }
}

public class EntidadesPurchaseOrdersEspecificacionTecnicaUpdateDto
    : EntidadesPurchaseOrdersEspecificacionTecnicaInsertDto { }
