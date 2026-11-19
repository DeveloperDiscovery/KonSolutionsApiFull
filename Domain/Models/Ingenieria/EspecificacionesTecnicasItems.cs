using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad ESPECIFICACIONES_TECNICAS_ITEMS - tipos equivalentes a la BD.</summary>
public class EspecificacionesTecnicasItems
{
    public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    public string DES_ITEM_TECNICO { get; set; } = string.Empty;
    public string DES_ITEM_TECNICO_SINGULAR { get; set; } = string.Empty;
    public string VAL_ITEM_TECNICO_LISTA { get; set; } = string.Empty;
    public int? NUM_ESTRUCTURA_ESPECIFICACION_TECNICA { get; set; }
    public string COD_ESPECIFICACION_TECNICA_PADRE { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO_PADRE { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_DEFAULT { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string DES_PREFIJO { get; set; } = string.Empty;
    public string DES_SINGULAR { get; set; } = string.Empty;
    public string DES_SUFIJO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_DEPENDIENTE { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_DEPENDIENTE { get; set; }
    public int? COD_ITEM_TECNICO_DEPENDIENTE { get; set; }
    public string? DES_COLOR { get; set; }
    public bool? FLG_HABILITADO { get; set; }
    public decimal? VAL_NUMERICO { get; set; }
}

public class EspecificacionesTecnicasItemsInsertDto
{
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    [Required] public string DES_ITEM_TECNICO { get; set; } = string.Empty;
    [Required] public string DES_ITEM_TECNICO_SINGULAR { get; set; } = string.Empty;
    [Required] public string VAL_ITEM_TECNICO_LISTA { get; set; } = string.Empty;
    public int? NUM_ESTRUCTURA_ESPECIFICACION_TECNICA { get; set; }
    [Required] public string COD_ESPECIFICACION_TECNICA_PADRE { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO_PADRE { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_DEFAULT { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_DEPENDIENTE { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_DEPENDIENTE { get; set; }
    public int? COD_ITEM_TECNICO_DEPENDIENTE { get; set; }
    public string? DES_COLOR { get; set; }
    public bool? FLG_HABILITADO { get; set; }
    public decimal? VAL_NUMERICO { get; set; }
}

public class EspecificacionesTecnicasItemsUpdateDto
{
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    [Required] public string DES_ITEM_TECNICO { get; set; } = string.Empty;
    [Required] public string DES_ITEM_TECNICO_SINGULAR { get; set; } = string.Empty;
    [Required] public string VAL_ITEM_TECNICO_LISTA { get; set; } = string.Empty;
    public int? NUM_ESTRUCTURA_ESPECIFICACION_TECNICA { get; set; }
    [Required] public string COD_ESPECIFICACION_TECNICA_PADRE { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO_PADRE { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_DEFAULT { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_DEPENDIENTE { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_DEPENDIENTE { get; set; }
    public int? COD_ITEM_TECNICO_DEPENDIENTE { get; set; }
    public string? DES_COLOR { get; set; }
    public bool? FLG_HABILITADO { get; set; }
    public decimal? VAL_NUMERICO { get; set; }
}

public class EspecificacionesTecnicasItemsSearchDto
{
    public string? Search { get; set; }
}