using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad ESTRUCTURA_ESPECIFICACION_TECNICA - tipos equivalentes a la BD.</summary>
public class EstructuraEspecificacionTecnica
{
    public int? NUM_ESTRUCTURA_ESPECIFICACION_TECNICA { get; set; }
    public string COD_ESTRUCTURA_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string DES_ESTRUCTURA_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string COD_TABLA_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? NUM_ESTRUCTURA_ESPECIFICACION_TECNICA_PADRE { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_FOREING_KEY { get; set; } = string.Empty;
    public string DES_SET { get; set; } = string.Empty;
}

public class EstructuraEspecificacionTecnicaInsertDto
{
    public int? NUM_ESTRUCTURA_ESPECIFICACION_TECNICA { get; set; }
    [Required] public string COD_ESTRUCTURA_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    [Required] public string DES_ESTRUCTURA_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    [Required] public string COD_TABLA_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? NUM_ESTRUCTURA_ESPECIFICACION_TECNICA_PADRE { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EstructuraEspecificacionTecnicaUpdateDto
{
    public int? NUM_ESTRUCTURA_ESPECIFICACION_TECNICA { get; set; }
    [Required] public string COD_ESTRUCTURA_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    [Required] public string DES_ESTRUCTURA_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    [Required] public string COD_TABLA_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? NUM_ESTRUCTURA_ESPECIFICACION_TECNICA_PADRE { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EstructuraEspecificacionTecnicaSearchDto
{
    public string? Search { get; set; }
}