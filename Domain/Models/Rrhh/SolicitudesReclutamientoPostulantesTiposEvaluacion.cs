using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad SOLICITUDES_RECLUTAMIENTO_POSTULANTES_TIPOS_EVALUACION - tipos equivalentes a la BD.</summary>
public class SolicitudesReclutamientoPostulantesTiposEvaluacion
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public decimal? COD_ENTIDAD_POSTULANTE { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_TIPO_EVALUACION { get; set; } = string.Empty;
    public string DES_EVALUACION { get; set; } = string.Empty;
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
}

public class SolicitudesReclutamientoPostulantesTiposEvaluacionInsertDto
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public decimal? COD_ENTIDAD_POSTULANTE { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_EVALUACION { get; set; } = string.Empty;
    [Required] public string DES_EVALUACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoPostulantesTiposEvaluacionUpdateDto
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public decimal? COD_ENTIDAD_POSTULANTE { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_EVALUACION { get; set; } = string.Empty;
    [Required] public string DES_EVALUACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoPostulantesTiposEvaluacionSearchDto
{
    public string? Search { get; set; }
}