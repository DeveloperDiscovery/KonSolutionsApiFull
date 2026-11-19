using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>
/// Entidad INGENIERIA.GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA - configuración de
/// qué Especificaciones Técnicas aplican a una Sub Familia de Producto. Solo se asigna a
/// nivel de Especificación Técnica completa (COD_ITEM_TECNICO siempre queda NULL — no se
/// asigna a nivel de Ítem puntual, confirmado con Pedro).
/// </summary>
public class GruposFamiliasSubFamiliasEspecificacionTecnica
{
    public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    public string? VAL_ESPECIFICACION_TENICA { get; set; }
    public decimal? VAL_MINIMO { get; set; }
    public decimal? VAL_MAXIMO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_INCLUIR_DESCRIPCION { get; set; }
    public bool? FLG_REGISTRO_COMERCIAL { get; set; }
    public bool? FLG_FICHA_TECNICA { get; set; }
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public string? COD_TIPO_REGISTRO_DATOS { get; set; }
    public int? COD_GRUPO_ESPECIFICACION_TECNICA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string? DES_SUB_FAMILIA_PRODUCTO { get; set; }
    public string? DES_TIPO_REGISTRO_DATOS { get; set; }
    public string? DES_GRUPO_ESPECIFICACION_TECNICA { get; set; }
}

public class GruposFamiliasSubFamiliasEspecificacionTecnicaInsertDto
{
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    // string? (opcional): con Nullable habilitado, un "string" no anulable se trata como
    // obligatorio implícito si el JSON llega en null — este campo es genuinamente opcional.
    public string? VAL_ESPECIFICACION_TENICA { get; set; }
    public decimal? VAL_MINIMO { get; set; }
    public decimal? VAL_MAXIMO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_INCLUIR_DESCRIPCION { get; set; }
    public bool? FLG_REGISTRO_COMERCIAL { get; set; }
    public bool? FLG_FICHA_TECNICA { get; set; }
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public string? COD_TIPO_REGISTRO_DATOS { get; set; }
    public int? COD_GRUPO_ESPECIFICACION_TECNICA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposFamiliasSubFamiliasEspecificacionTecnicaSearchDto
{
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
}

/// <summary>Una Sub Familia destino al copiar las Especificaciones Técnicas asignadas.</summary>
public class GruposFamiliasSubFamiliasEspecificacionTecnicaDestino
{
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
}

/// <summary>
/// Copia todas las Especificaciones Técnicas ya asignadas en (COD_GRUPO_PRODUCTO,
/// COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO) — el origen — hacia una o más
/// Sub Familias destino.
/// </summary>
public class GruposFamiliasSubFamiliasEspecificacionTecnicaCopiarDto
{
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public List<GruposFamiliasSubFamiliasEspecificacionTecnicaDestino> Destinos { get; set; } = new();
}

/// <summary>Reporte de la copia — mismo espíritu que el de "Copiar a otro Tipo" de Estados.</summary>
public class GruposFamiliasSubFamiliasEspecificacionTecnicaCopiarResultado
{
    public int TotalCopiados { get; set; }
    public int TotalErrores { get; set; }
    public List<string> Detalles { get; set; } = new();
}

/// <summary>
/// Fila genérica del árbol de configuración devuelto por
/// PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA_TREELIST — 2 niveles
/// (Especificación Técnica / Ítem Técnico) bajo el mismo patrón COD_KEY/COD_KEY_FATHER usado
/// en el árbol de Especificaciones/Ítems Técnicos por Entidad. Solo los nodos de nivel 1
/// (COD_KEY_TYPE == "1") son asignables — los Ítems son solo informativos.
/// </summary>
public class GruposFamiliasSubFamiliasEspecificacionTecnicaTreeList
{
    public string COD_KEY { get; set; } = string.Empty;
    public string? DES_KEY { get; set; }
    public string? COD_KEY_FATHER { get; set; }
    public string COD_KEY_TYPE { get; set; } = string.Empty;
    public string? DES_KEY_TYPE { get; set; }
    public bool? FLG_ASIGNADO { get; set; }
    public string? VAL_ESPECIFICACION_TENICA { get; set; }
    public decimal? VAL_MINIMO { get; set; }
    public decimal? VAL_MAXIMO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string? DES_ESTADO { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_INCLUIR_DESCRIPCION { get; set; }
    public bool? FLG_REGISTRO_COMERCIAL { get; set; }
    public bool? FLG_FICHA_TECNICA { get; set; }
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public string? COD_TIPO_ESPECIFICACION_TECNICA { get; set; }
    public string? DES_TIPO_ESPECIFICACION_TECNICA { get; set; }
    public string? DES_ESPECIFICACION_TECNICA_SINGULAR { get; set; }
    public string? COD_TIPO_REGISTRO_DATOS { get; set; }
    public string? DES_TIPO_REGISTRO_DATOS { get; set; }
    public int? COD_GRUPO_ESPECIFICACION_TECNICA { get; set; }
    public string? DES_GRUPO_ESPECIFICACION_TECNICA { get; set; }

    /// <summary>Nodos hijos — se arma en memoria a partir de COD_KEY_FATHER, no viene del API.</summary>
    public List<GruposFamiliasSubFamiliasEspecificacionTecnicaTreeList> Hijos { get; set; } = new();
}

/// <summary>Resultado de
/// PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA_LOADCOMBOS: Tipos de
/// Registro de Datos + Grupos de Especificación Técnica + Estados (filtrados por
/// @COD_TIPO_ESTADO) en un solo viaje para GruposEspecificacionTecnicaDialog.razor.</summary>
public class GruposFamiliasSubFamiliasEspecificacionTecnicaLoadCombosResult
{
    public int CodTipoEstado { get; set; }
    public List<KonSolutionsApi.Common.ComboboxItem> TiposRegistroDatos       { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> GruposEspecificacionTecnica { get; set; } = new();
    public List<KonSolutionsApi.Common.EstadoItem>   Estados                  { get; set; } = new();
}
