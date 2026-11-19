using System.ComponentModel.DataAnnotations;
using KonSolutionsApi.Common;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad PRODUCTOS - tipos equivalentes a la BD.</summary>
public class Productos
{
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_PROPIETARIA { get; set; }
    public string COD_DISENO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    // Unidad en la que el almacen despacha el producto. Puede diferir de la de venta:
    // se vende por metro y se despacha por rollo.
    public string? COD_UNIDAD_MEDIDA_DESPACHO_ALMACEN { get; set; }
    public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string COD_PARTIDA_ARANCELARIA { get; set; } = string.Empty;
    public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    public string COD_TIPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_CUBSO { get; set; } = string.Empty;
    /// <summary>El producto no se puede fraccionar: rollos, cajas, contenedores. Cuando esta
    /// en true, la reserva toma la ubicacion ENTERA aunque sobre, porque el bulto sale completo
    /// del almacen y lo que sobra ya no esta ahi para otro.
    ///
    /// Vive en el PRODUCTO y no en el proceso —donde estaba— porque que algo no se pueda partir
    /// es una propiedad de lo que se guarda: el mismo proceso puede dejar rollos y kilos a
    /// granel. Lo lee PROC_LOGISTICA_REQUERIMIENTOS_STOCK_MAKERESERVA.</summary>
    public bool FLG_INDIVISIBLE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CUBSO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    /// <summary>Nombre comercial de la entidad propietaria (ESTANDAR.ENTIDADES.DES_COMERCIAL).
    /// Solo lectura: lo arma el SELECT de los SP, no se graba desde aquí.</summary>
    public string DES_ENTIDAD_PROPIETARIA { get; set; } = string.Empty;
}

public class ProductosInsertDto
{
    // Al crear, COD_PRODUCTO se manda "" (el SP lo arma internamente) — sin [Required]
    // porque esa validación rechaza string vacío por defecto.
    public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_DETALLADA { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_PROPIETARIA { get; set; }
    // string? (no solo sin [Required]): con Nullable habilitado, ASP.NET Core trata un
    // "string" no anulable como obligatorio IMPLÍCITAMENTE si el JSON no lo trae o lo
    // trae en null — pasa exactamente eso cuando el combo se deja sin elegir. Por eso
    // estos campos opcionales deben ser string?, no alcanza con solo quitar [Required].
    public string? COD_DISENO { get; set; }
    public int? NUM_VERSION { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    // Unidad en la que el almacen despacha el producto. Puede diferir de la de venta:
    // se vende por metro y se despacha por rollo.
    //
    // OBLIGATORIA. No alcanza con [Required]: tiene que ser string NO ANULABLE, por lo
    // mismo que explica el comentario de COD_DISENO mas arriba. La columna en la base
    // sigue siendo NULL para no romper los productos que ya existen; quien exige el
    // dato es la aplicacion, aca y en el combo del dialogo.
    [Required] public string COD_UNIDAD_MEDIDA_DESPACHO_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string? COD_PARTIDA_ARANCELARIA { get; set; }
    public string? COD_TIPO_VALOR_CALCULO { get; set; }
    [Required] public string COD_TIPO_PRODUCTO { get; set; } = string.Empty;
    public string? COD_CUBSO { get; set; }
    /// <summary>El producto no se puede fraccionar: rollos, cajas, contenedores. Cuando esta
    /// en true, la reserva toma la ubicacion ENTERA aunque sobre, porque el bulto sale completo
    /// del almacen y lo que sobra ya no esta ahi para otro.
    ///
    /// Vive en el PRODUCTO y no en el proceso —donde estaba— porque que algo no se pueda partir
    /// es una propiedad de lo que se guarda: el mismo proceso puede dejar rollos y kilos a
    /// granel. Lo lee PROC_LOGISTICA_REQUERIMIENTOS_STOCK_MAKERESERVA.</summary>
    public bool FLG_INDIVISIBLE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProductosUpdateDto
{
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_DETALLADA { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_PROPIETARIA { get; set; }
    // Los cinco campos de aca abajo NO son obligatorios, aunque este DTO naciera con
    // [Required] en todos: hay productos reales sin diseno, sin CUBSO, sin partida
    // arancelaria, sin unidad secundaria y sin tipo de valor de calculo. Con [Required]
    // el PUT los rechazaba antes de llegar a la base. Van como string? por el mismo
    // motivo de nullabilidad que explica el InsertDto.
    public string? COD_DISENO { get; set; }
    public int? NUM_VERSION { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    // Unidad en la que el almacen despacha el producto. Puede diferir de la de venta:
    // se vende por metro y se despacha por rollo.
    //
    // OBLIGATORIA. No alcanza con [Required]: tiene que ser string NO ANULABLE, por lo
    // mismo que explica el comentario de COD_DISENO mas arriba. La columna en la base
    // sigue siendo NULL para no romper los productos que ya existen; quien exige el
    // dato es la aplicacion, aca y en el combo del dialogo.
    [Required] public string COD_UNIDAD_MEDIDA_DESPACHO_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string? COD_PARTIDA_ARANCELARIA { get; set; }
    public string? COD_TIPO_VALOR_CALCULO { get; set; }
    [Required] public string COD_TIPO_PRODUCTO { get; set; } = string.Empty;
    public string? COD_CUBSO { get; set; }
    /// <summary>El producto no se puede fraccionar: rollos, cajas, contenedores. Cuando esta
    /// en true, la reserva toma la ubicacion ENTERA aunque sobre, porque el bulto sale completo
    /// del almacen y lo que sobra ya no esta ahi para otro.
    ///
    /// Vive en el PRODUCTO y no en el proceso —donde estaba— porque que algo no se pueda partir
    /// es una propiedad de lo que se guarda: el mismo proceso puede dejar rollos y kilos a
    /// granel. Lo lee PROC_LOGISTICA_REQUERIMIENTOS_STOCK_MAKERESERVA.</summary>
    public bool FLG_INDIVISIBLE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

/// <summary>Una fila del arbol del panel de filtro: una especificacion con UNO de sus
/// items. El arbol se arma en el cliente agrupando por COD_ESPECIFICACION_TECNICA — el SP
/// devuelve plano porque un resultset plano es lo que Dapper mapea sin ceremonia.</summary>
public class ProductoEspecificacionArbol
{
    public string  COD_ESPECIFICACION_TECNICA       { get; set; } = string.Empty;
    public string? DES_ESPECIFICACION_TECNICA       { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_PADRE { get; set; }
    public string? COD_TIPO_ESPECIFICACION_TECNICA  { get; set; }
    public int?    NUM_ORDEN_ESPECIFICACION         { get; set; }

    /// <summary>'I' = item de lista/color (se filtra por COD_ITEM_TECNICO).
    /// 'V' = valor de una especificacion numerica o logica (se filtra por su texto).</summary>
    public string? TIPO_NODO        { get; set; }
    public int?    COD_ITEM_TECNICO { get; set; }
    /// <summary>Lo que se muestra en el nodo: la descripcion del item, o el valor.</summary>
    public string? DES_VALOR        { get; set; }
    /// <summary>Hex del color, cuando la especificacion es de tipo COLOR. NULL en las demas.</summary>
    public string? DES_COLOR        { get; set; }
    public int?    NUM_ORDEN_ITEM   { get; set; }

    /// <summary>Cuantos productos tienen este item, dentro de la clasificacion vigente.</summary>
    public int     CAN_PRODUCTOS    { get; set; }
}

public class ProductosSearchDto
{
    public string? Search { get; set; }

    // Filtros por clasificacion. NULL o vacio significa "no filtres por esto".
    // El repositorio convierte el string vacio a NULL antes de mandarlo al SP:
    // el combo devuelve "" al limpiarse, y "" no es un codigo valido — filtraria
    // por un grupo inexistente y no devolveria nada.
    public string? COD_GRUPO_PRODUCTO       { get; set; }
    public string? COD_FAMILIA_PRODUCTO     { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO { get; set; }

    /// <summary>Checks del panel de especificaciones: 'esp:item;esp:item'.
    ///
    /// Viaja como cadena y no como lista porque asi se manda en la query string sin
    /// repetir el parametro N veces, y porque el SP la parte con STRING_SPLIT. El formato
    /// lo arma el componente del panel, no el usuario.</summary>
    public string? FILTRO_ESPECIFICACIONES { get; set; }

    /// <summary>Recorta el árbol de especificaciones a los productos que tienen algo en
    /// el almacén: stock, reservado, en tránsito, inmovilizado o disponible.
    ///
    /// Lo usa Consultas de Stock. Sin esto el panel ofrece colores y tallas que existen
    /// en el catálogo pero que hoy no tienen ni una unidad, y marcarlos deja la grilla
    /// vacía sin que se vea por qué.
    ///
    /// NULL y false son lo mismo —el árbol completo—, que es como lo llama Productos.</summary>
    public bool? SOLO_CON_EXISTENCIAS { get; set; }
}

/// <summary>
/// Resultado de PROC_ESTANDAR_PRODUCTOS_LOAD_COMBOS — invoca en cadena los SHOWCOMBOX
/// de Grupos, Unidades de Medida, Tipos de Producto, Diseños, Entidades, Cubso y Estados.
/// Algunos de esos SPs no alias sus columnas como Id/Texto, por eso el repositorio mapea
/// manualmente esos result sets (ver ProductosRepository.LoadcombosAsync).
/// </summary>
public class ProductosLoadCombosResult
{
    public int                CodTipoEstado  { get; set; }
    public List<ComboboxItem> Grupos         { get; set; } = new();
    public List<ComboboxItem> UnidadesMedida { get; set; } = new();
    public List<ComboboxItem> TiposProducto  { get; set; } = new();
    public List<ComboboxItem> Disenos        { get; set; } = new();
    public List<ComboboxItem> Entidades      { get; set; } = new();
    public List<ComboboxItem> Cubso          { get; set; } = new();
    public List<EstadoItem>   Estados        { get; set; } = new();
}