using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad TIPOS_ESPECIFICACION_TECNICA - tipos equivalentes a la BD.</summary>
public class TiposEspecificacionTecnica
{
    public string COD_TIPO_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string DES_TIPO_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public bool? FLG_LISTA { get; set; }
    public bool? FLG_ENTERO { get; set; }
    public bool? FLG_NUMERICO { get; set; }
    public bool? FLG_CADENA { get; set; }
    public bool? FLG_FECHA_HORA { get; set; }
    public bool? FLG_FECHA { get; set; }
    public bool? FLG_HORA { get; set; }
    public bool? FLG_LOGICO { get; set; }
    public bool? FLG_COLOR { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class TiposEspecificacionTecnicaInsertDto
{
    [Required] public string COD_TIPO_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public bool? FLG_LISTA { get; set; }
    public bool? FLG_ENTERO { get; set; }
    public bool? FLG_NUMERICO { get; set; }
    public bool? FLG_CADENA { get; set; }
    public bool? FLG_FECHA_HORA { get; set; }
    public bool? FLG_FECHA { get; set; }
    public bool? FLG_HORA { get; set; }
    public bool? FLG_LOGICO { get; set; }
    public bool? FLG_COLOR { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class TiposEspecificacionTecnicaUpdateDto
{
    [Required] public string COD_TIPO_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public bool? FLG_LISTA { get; set; }
    public bool? FLG_ENTERO { get; set; }
    public bool? FLG_NUMERICO { get; set; }
    public bool? FLG_CADENA { get; set; }
    public bool? FLG_FECHA_HORA { get; set; }
    public bool? FLG_FECHA { get; set; }
    public bool? FLG_HORA { get; set; }
    public bool? FLG_LOGICO { get; set; }
    public bool? FLG_COLOR { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class TiposEspecificacionTecnicaSearchDto
{
    public string? Search { get; set; }
}