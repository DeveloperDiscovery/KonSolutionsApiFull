using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad SOLICITUDES_RECLUTAMIENTO_COMPETENCIAS - tipos equivalentes a la BD.</summary>
public class SolicitudesReclutamientoCompetencias
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public string COD_COMPETENCIA { get; set; } = string.Empty;
    public decimal? TAS_PESO_COMPETENCIA { get; set; }
    public bool? FLG_OPCIONAL { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_DETALLE { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoCompetenciasInsertDto
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    [Required] public string COD_COMPETENCIA { get; set; } = string.Empty;
    public decimal? TAS_PESO_COMPETENCIA { get; set; }
    public bool? FLG_OPCIONAL { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoCompetenciasUpdateDto
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    [Required] public string COD_COMPETENCIA { get; set; } = string.Empty;
    public decimal? TAS_PESO_COMPETENCIA { get; set; }
    public bool? FLG_OPCIONAL { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoCompetenciasSearchDto
{
    public string? Search { get; set; }
}