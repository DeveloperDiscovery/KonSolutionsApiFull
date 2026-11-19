using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad ENCUESTAS_PREGUNTAS_RESPUESTAS - tipos equivalentes a la BD.</summary>
public class EncuestasPreguntasRespuestas
{
    public int? NUM_ENCUESTA { get; set; }
    public int? NUM_PREGUNTA { get; set; }
    public int? NUM_RESPUESTA { get; set; }
    public decimal? DES_RESPUESTA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_PREGUNTA { get; set; } = string.Empty;
}

public class EncuestasPreguntasRespuestasInsertDto
{
    public int? NUM_ENCUESTA { get; set; }
    public int? NUM_PREGUNTA { get; set; }
    public int? NUM_RESPUESTA { get; set; }
    public decimal? DES_RESPUESTA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EncuestasPreguntasRespuestasUpdateDto
{
    public int? NUM_ENCUESTA { get; set; }
    public int? NUM_PREGUNTA { get; set; }
    public int? NUM_RESPUESTA { get; set; }
    public decimal? DES_RESPUESTA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EncuestasPreguntasRespuestasSearchDto
{
    public string? Search { get; set; }
}