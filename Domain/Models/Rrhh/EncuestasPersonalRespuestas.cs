using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad ENCUESTAS_PERSONAL_RESPUESTAS - tipos equivalentes a la BD.</summary>
public class EncuestasPersonalRespuestas
{
    public int? NUM_ENCUESTA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_PREGUNTA { get; set; }
    public int? NUM_RESPUESTA { get; set; }
    public string VAL_PARRAFO { get; set; } = string.Empty;
    public bool? VAL_LOGICO { get; set; }
    public DateTime? VAL_FECHA { get; set; }
    public DateTime? VAL_HORA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PREGUNTA { get; set; } = string.Empty;
}

public class EncuestasPersonalRespuestasInsertDto
{
    public int? NUM_ENCUESTA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_PREGUNTA { get; set; }
    public int? NUM_RESPUESTA { get; set; }
    [Required] public string VAL_PARRAFO { get; set; } = string.Empty;
    public bool? VAL_LOGICO { get; set; }
    public DateTime? VAL_FECHA { get; set; }
    public DateTime? VAL_HORA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EncuestasPersonalRespuestasUpdateDto
{
    public int? NUM_ENCUESTA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_PREGUNTA { get; set; }
    public int? NUM_RESPUESTA { get; set; }
    [Required] public string VAL_PARRAFO { get; set; } = string.Empty;
    public bool? VAL_LOGICO { get; set; }
    public DateTime? VAL_FECHA { get; set; }
    public DateTime? VAL_HORA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EncuestasPersonalRespuestasSearchDto
{
    public string? Search { get; set; }
}