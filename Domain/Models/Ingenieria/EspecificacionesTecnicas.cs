using System.ComponentModel.DataAnnotations;
using KonSolutionsApi.Common;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad ESPECIFICACIONES_TECNICAS - tipos equivalentes a la BD.</summary>
public class EspecificacionesTecnicas
{
    public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string? DES_ESPECIFICACION_TECNICA { get; set; }
    public string? DES_ESPECIFICACION_TECNICA_SINGULAR { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_PADRE { get; set; }
    public string? COD_TIPO_ESPECIFICACION_TECNICA { get; set; }
    public bool? FLG_HAS_CHILDREN { get; set; }
    public bool? FLG_LIST { get; set; }
    public bool? FLG_IS_CHILDREN { get; set; }
    public bool? FLG_DEPENDS_ENTITY { get; set; }
    public bool? FLG_MULTIPLE_CHOICE { get; set; }
    public bool? FLG_ITEMS_WITH_VALUE { get; set; }
    public bool? FLG_PRODUCT_ASSEMBLY { get; set; }
    public bool? FLG_QUALITY_CONTROL { get; set; }
    public bool? FLG_OBSERVACION { get; set; }
    public bool? FLG_RANGE { get; set; }
    public bool? FLG_EDITABLE_ORDER_TAKING { get; set; }
    public bool? FLG_IS_COLOR { get; set; }
    public string? COD_CLASE_DEFECTO_TECNICO { get; set; }
    public string? COD_TIPO_DEFECTO_TECNICO_DEFECTO { get; set; }
    public string? DES_PREFIJO { get; set; }
    public string? DES_SUFIJO { get; set; }
    public int? CAN_ENTERO { get; set; }
    public int? CAN_DECIMAL { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public string? COD_UNIDAD_MEDIDA_DIVISORA { get; set; }
    public string? COD_UNIDAD_MEDIDA_ITEM { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_CLASE_DEFECTO_TECNICO { get; set; } = string.Empty;
    public string DES_DEFECTO_TECNICO { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string DES_TIPO_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
}

public class EspecificacionesTecnicasInsertDto
{
    public string? COD_ESPECIFICACION_TECNICA { get; set; }
    [Required] public string DES_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    [Required] public string DES_ESPECIFICACION_TECNICA_SINGULAR { get; set; } = string.Empty;
    public string? COD_ESPECIFICACION_TECNICA_PADRE { get; set; }
    [Required] public string COD_TIPO_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public bool? FLG_HAS_CHILDREN { get; set; }
    public bool? FLG_LIST { get; set; }
    public bool? FLG_IS_CHILDREN { get; set; }
    public bool? FLG_DEPENDS_ENTITY { get; set; }
    public bool? FLG_MULTIPLE_CHOICE { get; set; }
    public bool? FLG_ITEMS_WITH_VALUE { get; set; }
    public bool? FLG_PRODUCT_ASSEMBLY { get; set; }
    public bool? FLG_QUALITY_CONTROL { get; set; }
    public bool? FLG_OBSERVACION { get; set; }
    public bool? FLG_RANGE { get; set; }
    public bool? FLG_EDITABLE_ORDER_TAKING { get; set; }
    public bool? FLG_IS_COLOR { get; set; }
    public string? COD_CLASE_DEFECTO_TECNICO { get; set; }
    public string? COD_TIPO_DEFECTO_TECNICO_DEFECTO { get; set; }
    public string? DES_PREFIJO { get; set; }
    public string? DES_SUFIJO { get; set; }
    public int? CAN_ENTERO { get; set; }
    public int? CAN_DECIMAL { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public string? COD_UNIDAD_MEDIDA_DIVISORA { get; set; }
    public string? COD_UNIDAD_MEDIDA_ITEM { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EspecificacionesTecnicasSearchDto
{
    public string? Search { get; set; }
}

public class EspecificacionesTecnicasLoadCombosResult
{
    public int                CodTipoEstado       { get; set; }
    public List<ComboboxItem> ClasesDefecto       { get; set; } = new();
    public List<ComboboxItem> TiposEspecificacion { get; set; } = new();
    public List<EstadoItem>   Estados             { get; set; } = new();
    public List<ComboboxItem> UnidadesMedida      { get; set; } = new();
}
