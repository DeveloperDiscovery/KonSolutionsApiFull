using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad ENCUESTAS_PREGUNTAS - tipos equivalentes a la BD.</summary>
public class EncuestasPreguntas
{
    public int? NUM_ENCUESTA { get; set; }
    public int? NUM_PREGUNTA { get; set; }
    public string DES_PREGUNTA { get; set; } = string.Empty;
    public bool? FLG_PARRAFO { get; set; }
    public bool? FLG_SINO { get; set; }
    public bool? FLG_NUMERO { get; set; }
    public bool? FLG_OPCION_SIMPLE { get; set; }
    public bool? FLG_OPCION_MULTIPLE { get; set; }
    public bool? FLG_CALIFICACION { get; set; }
    public bool? FLG_FECHA { get; set; }
    public bool? FLG_HORA { get; set; }
    public int? NUM_CALIFICACION_MINIMO { get; set; }
    public int? NUM_CALIFICACION_MAXIMO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ECUENSTA { get; set; } = string.Empty;
}

public class EncuestasPreguntasInsertDto
{
    public int? NUM_ENCUESTA { get; set; }
    public int? NUM_PREGUNTA { get; set; }
    [Required] public string DES_PREGUNTA { get; set; } = string.Empty;
    public bool? FLG_PARRAFO { get; set; }
    public bool? FLG_SINO { get; set; }
    public bool? FLG_NUMERO { get; set; }
    public bool? FLG_OPCION_SIMPLE { get; set; }
    public bool? FLG_OPCION_MULTIPLE { get; set; }
    public bool? FLG_CALIFICACION { get; set; }
    public bool? FLG_FECHA { get; set; }
    public bool? FLG_HORA { get; set; }
    public int? NUM_CALIFICACION_MINIMO { get; set; }
    public int? NUM_CALIFICACION_MAXIMO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EncuestasPreguntasUpdateDto
{
    public int? NUM_ENCUESTA { get; set; }
    public int? NUM_PREGUNTA { get; set; }
    [Required] public string DES_PREGUNTA { get; set; } = string.Empty;
    public bool? FLG_PARRAFO { get; set; }
    public bool? FLG_SINO { get; set; }
    public bool? FLG_NUMERO { get; set; }
    public bool? FLG_OPCION_SIMPLE { get; set; }
    public bool? FLG_OPCION_MULTIPLE { get; set; }
    public bool? FLG_CALIFICACION { get; set; }
    public bool? FLG_FECHA { get; set; }
    public bool? FLG_HORA { get; set; }
    public int? NUM_CALIFICACION_MINIMO { get; set; }
    public int? NUM_CALIFICACION_MAXIMO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EncuestasPreguntasSearchDto
{
    public string? Search { get; set; }
}