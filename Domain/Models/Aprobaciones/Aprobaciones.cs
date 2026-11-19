using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Aprobaciones;

/// <summary>Entidad APROBACIONES - tipos equivalentes a la BD.</summary>
public class Aprobaciones
{
    public int? NUM_APROBACION { get; set; }
    public string DES_APROBACION { get; set; } = string.Empty;
    public DateTime? FEC_SOLICITADO { get; set; }
    public DateTime? FEC_EVALUACION_MAXIMA { get; set; }
    public DateTime? FEC_EVALUACION_EFECTUADA { get; set; }
    public decimal? COD_ENTIDAD_DESARROLLADORA { get; set; }
    public decimal? COD_ENTIDAD_EVALUADORA { get; set; }
    public string COD_TIPO_APROBACION { get; set; } = string.Empty;
    public int? NUM_SOLICITUD { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_SOLICITUD { get; set; } = string.Empty;
    public string DES_TIPO_APROBACION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class AprobacionesInsertDto
{
    public int? NUM_APROBACION { get; set; }
    [Required] public string DES_APROBACION { get; set; } = string.Empty;
    public DateTime? FEC_SOLICITADO { get; set; }
    public DateTime? FEC_EVALUACION_MAXIMA { get; set; }
    public DateTime? FEC_EVALUACION_EFECTUADA { get; set; }
    public decimal? COD_ENTIDAD_DESARROLLADORA { get; set; }
    public decimal? COD_ENTIDAD_EVALUADORA { get; set; }
    [Required] public string COD_TIPO_APROBACION { get; set; } = string.Empty;
    public int? NUM_SOLICITUD { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AprobacionesUpdateDto
{
    public int? NUM_APROBACION { get; set; }
    [Required] public string DES_APROBACION { get; set; } = string.Empty;
    public DateTime? FEC_SOLICITADO { get; set; }
    public DateTime? FEC_EVALUACION_MAXIMA { get; set; }
    public DateTime? FEC_EVALUACION_EFECTUADA { get; set; }
    public decimal? COD_ENTIDAD_DESARROLLADORA { get; set; }
    public decimal? COD_ENTIDAD_EVALUADORA { get; set; }
    [Required] public string COD_TIPO_APROBACION { get; set; } = string.Empty;
    public int? NUM_SOLICITUD { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AprobacionesSearchDto
{
    public string? Search { get; set; }
}