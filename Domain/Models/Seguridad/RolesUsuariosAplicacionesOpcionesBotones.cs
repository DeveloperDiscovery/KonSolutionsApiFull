using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>Entidad ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES - tipos equivalentes a la BD.</summary>
public class RolesUsuariosAplicacionesOpcionesBotones
{
    public string COD_ROL_USUARIO { get; set; } = string.Empty;
    public string COD_APLICACION { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public string COD_BOTON_OPCION { get; set; } = string.Empty;
    public bool? FLG_HABILITADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_BOTON_OPCION { get; set; } = string.Empty;
    public string DES_TOOLTIPTEXT { get; set; } = string.Empty;
    public bool? FLG_HEADER { get; set; }
    public bool? FLG_DIALOG { get; set; }
    public bool? FLG_GRILLA { get; set; }
    public bool? FLG_ACCION_PUT { get; set; }
    public bool? FLG_ACCION_POST { get; set; }
    public bool? FLG_ACCION_DELETE { get; set; }
    public bool? FLG_ACCION_GET { get; set; }
    public string? DES_METODO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

/// <summary>
/// Resultado de PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES_TREEVIEW.
/// Trae, para el rol del usuario indicado, TODOS los botones de TODAS las opciones
/// en una sola llamada (sin parámetros de aplicación/opción) — usado al login para
/// precargar permisos en memoria y evitar una llamada por cada pantalla.
/// </summary>
public class RolesUsuariosAplicacionesOpcionesBotonesTreeviewItem
{
    public string COD_ROL_USUARIO { get; set; } = string.Empty;
    public string COD_APLICACION { get; set; } = string.Empty;
    public string DES_APLICACION { get; set; } = string.Empty;
    // Tipo de la opción (módulo, carpeta, formulario, etc.) — distingue nodos de
    // navegación de pantallas reales en el árbol del menú.
    public string? COD_TIPO_OPCION_APLICACION { get; set; }
    public string? DES_TIPO_OPCION_APLICACION { get; set; }
    public string? COD_OPCION_APLICACION_PADRE { get; set; }
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public string DES_OPCION_APLICACION { get; set; } = string.Empty;
    public string DES_FORMULARIO { get; set; } = string.Empty;
    // Nullable: el SP ahora parte de ROLES_USUARIOS_APLICACIONES_OPCIONES con
    // LEFT JOIN hacia botones — una opción sin botones configurados aún aparece
    // en el resultado, con estas columnas en NULL.
    public string? COD_BOTON_OPCION { get; set; }
    public string? DES_BOTON_OPCION { get; set; }
    public string? DES_TOOLTIPTEXT { get; set; }
    public string? DES_ALIAS { get; set; }
    public bool? FLG_HABILITADO { get; set; }
    public bool? FLG_HEADER { get; set; }
    public bool? FLG_DIALOG { get; set; }
    public bool? FLG_GRILLA { get; set; }
    public bool? FLG_ACCION_PUT { get; set; }
    public bool? FLG_ACCION_POST { get; set; }
    public bool? FLG_ACCION_DELETE { get; set; }
    public bool? FLG_ACCION_GET { get; set; }
    public string? DES_METODO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public string  DES_BACKCOLOR_APLICACION { get; set; } = "";
    public string  DES_FORECOLOR_APLICACION { get; set; } = "";
    public byte[]? IMG_ICONO_APLICACION     { get; set; }
    public string  DES_BACKCOLOR_OPCION     { get; set; } = "";
    public string  DES_FORECOLOR_OPCION     { get; set; } = "";
    public byte[]? IMG_ICONO_OPCION         { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

/// <summary>
/// Resultado 1/3 del TREEVIEW (deduplicado): una fila por aplicación, con su ícono
/// UNA sola vez — antes viajaba repetido en cada fila de botón de esa app.
/// </summary>
public class AplicacionTreeviewItem
{
    public string  COD_APLICACION           { get; set; } = string.Empty;
    public string  DES_APLICACION           { get; set; } = string.Empty;
    public string  DES_BACKCOLOR_APLICACION { get; set; } = "";
    public string  DES_FORECOLOR_APLICACION { get; set; } = "";
    public byte[]? IMG_ICONO_APLICACION     { get; set; }
}

/// <summary>
/// Resultado 2/3 del TREEVIEW (deduplicado): una fila por opción, con su ícono
/// UNA sola vez — antes viajaba repetido en cada fila de botón de esa opción.
/// </summary>
public class OpcionTreeviewItem
{
    public string  COD_APLICACION              { get; set; } = string.Empty;
    public string? COD_TIPO_OPCION_APLICACION  { get; set; }
    public string? DES_TIPO_OPCION_APLICACION  { get; set; }
    public string? COD_OPCION_APLICACION_PADRE { get; set; }
    public string  COD_OPCION_APLICACION       { get; set; } = string.Empty;
    public string  DES_OPCION_APLICACION       { get; set; } = string.Empty;
    public string  DES_FORMULARIO              { get; set; } = string.Empty;
    public string  DES_BACKCOLOR_OPCION        { get; set; } = "";
    public string  DES_FORECOLOR_OPCION        { get; set; } = "";
    public byte[]? IMG_ICONO_OPCION            { get; set; }
    public bool?   FLG_TECLADO_VIRTUAL         { get; set; }
    public bool?   FLG_EXIGE_PASSWORD          { get; set; }
}

/// <summary>Resultado 3/3 del TREEVIEW: un botón por fila (sin colores ni íconos —
/// esos ya vienen en AplicacionTreeviewItem/OpcionTreeviewItem).</summary>
public class BotonTreeviewItemLite
{
    public string  COD_APLICACION              { get; set; } = string.Empty;
    public string? COD_OPCION_APLICACION_PADRE { get; set; }
    public string  COD_OPCION_APLICACION       { get; set; } = string.Empty;
    public string? COD_BOTON_OPCION            { get; set; }
    public string? DES_BOTON_OPCION            { get; set; }
    public string? DES_TOOLTIPTEXT             { get; set; }
    public string? DES_ALIAS                   { get; set; }
    public bool?   FLG_HABILITADO              { get; set; }
    public bool?   FLG_HEADER                  { get; set; }
    public bool?   FLG_DIALOG                  { get; set; }
    public bool?   FLG_GRILLA                  { get; set; }
    public bool?   FLG_ACCION_PUT              { get; set; }
    public bool?   FLG_ACCION_POST             { get; set; }
    public bool?   FLG_ACCION_DELETE           { get; set; }
    public bool?   FLG_ACCION_GET              { get; set; }
    public string? DES_METODO                  { get; set; }
    public int?    NUM_ORDEN_PRESENTACION      { get; set; }
}

/// <summary>Envoltorio de los 3 resultados del TREEVIEW deduplicado.</summary>
public class TreeviewResult
{
    public List<AplicacionTreeviewItem> Aplicaciones { get; set; } = new();
    public List<OpcionTreeviewItem>     Opciones     { get; set; } = new();
    public List<BotonTreeviewItemLite>  Botones      { get; set; } = new();
}

public class RolesUsuariosAplicacionesOpcionesBotonesInsertDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_APLICACION { get; set; } = string.Empty;
    [Required] public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    [Required] public string COD_BOTON_OPCION { get; set; } = string.Empty;
    public bool? FLG_HABILITADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RolesUsuariosAplicacionesOpcionesBotonesUpdateDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_APLICACION { get; set; } = string.Empty;
    [Required] public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    [Required] public string COD_BOTON_OPCION { get; set; } = string.Empty;
    public bool? FLG_HABILITADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RolesUsuariosAplicacionesOpcionesBotonesSearchDto
{
    public string? Search { get; set; }
}

// ───── Resultado de PROC_..._TREEVIEWLOGOS (experimento: íconos aparte del texto) ─────

/// <summary>1/4: ícono de cada aplicación del rol.</summary>
public class AplicacionLogoItem
{
    public string  COD_APLICACION       { get; set; } = string.Empty;
    // Deduplicación (ver IconoItem más abajo): si viene ICONO_ID, la imagen real se busca en
    // el 4to resultset (Iconos) por ese id — IMG_ICONO_APLICACION queda null hasta resolverla
    // en el frontend (MenuService.ResolverIconos).
    public int?    ICONO_ID             { get; set; }
    public byte[]? IMG_ICONO_APLICACION { get; set; }
}

/// <summary>2/4: ícono de cada opción del rol.</summary>
public class OpcionLogoItem
{
    public string  COD_APLICACION              { get; set; } = string.Empty;
    public string? COD_OPCION_APLICACION_PADRE { get; set; }
    public string  COD_OPCION_APLICACION       { get; set; } = string.Empty;
    public int?    ICONO_ID                    { get; set; }
    public byte[]? IMG_ICONO_OPCION            { get; set; }
}

/// <summary>3/4: ícono propio de un botón específico — cuando venga NULL, el frontend
/// cae al ícono calculado por nombre/flags (ver AccionesBotones.IconoEstandar).</summary>
public class BotonLogoItem
{
    public string  COD_APLICACION        { get; set; } = string.Empty;
    public string  COD_OPCION_APLICACION { get; set; } = string.Empty;
    public string? COD_BOTON_OPCION      { get; set; }
    public int?    ICONO_ID              { get; set; }
    public byte[]? IMG_ICONO             { get; set; }
}

/// <summary>4/4: catálogo deduplicado de imágenes referenciadas por ICONO_ID — la misma
/// imagen (ej. el ícono de "Nuevo", usado por decenas de botones) viaja UNA sola vez en vez
/// de repetida en cada fila de Aplicaciones/Opciones/Botones.</summary>
public class IconoItem
{
    public int     ICONO_ID  { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

/// <summary>Envoltorio de los resultados del TREEVIEWLOGOS.</summary>
public class TreeviewLogosResult
{
    public List<AplicacionLogoItem> Aplicaciones { get; set; } = new();
    public List<OpcionLogoItem>     Opciones     { get; set; } = new();
    public List<BotonLogoItem>      Botones      { get; set; } = new();
    public List<IconoItem>          Iconos       { get; set; } = new();
}