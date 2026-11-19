using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad SOLICITUDES_RECLUTAMIENTO_TIPOS_EVALUACION - tipos equivalentes a la BD.</summary>
public class SolicitudesReclutamientoTiposEvaluacion
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public string COD_TIPO_EVALUACION { get; set; } = string.Empty;
    public decimal? TAS_PESO_COMPETENCIA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_DETALLE { get; set; } = string.Empty;
    public string DES_TIPO_EVALUACION { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoTiposEvaluacionInsertDto
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    [Required] public string COD_TIPO_EVALUACION { get; set; } = string.Empty;
    public decimal? TAS_PESO_COMPETENCIA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoTiposEvaluacionUpdateDto
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    [Required] public string COD_TIPO_EVALUACION { get; set; } = string.Empty;
    public decimal? TAS_PESO_COMPETENCIA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoTiposEvaluacionSearchDto
{
    public string? Search { get; set; }
}