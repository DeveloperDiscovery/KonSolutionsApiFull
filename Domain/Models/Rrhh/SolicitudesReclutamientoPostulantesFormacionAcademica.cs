using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad SOLICITUDES_RECLUTAMIENTO_POSTULANTES_FORMACION_ACADEMICA - tipos equivalentes a la BD.</summary>
public class SolicitudesReclutamientoPostulantesFormacionAcademica
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public decimal? COD_ENTIDAD_POSTULANTE { get; set; }
    public string COD_CARRERA_ACADEMICA { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_TERMINO { get; set; }
    public string COD_TITULO_ACADEMICO { get; set; } = string.Empty;
    public string COD_RANKING_ACADEMICO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CARRERA_ACADEMICA { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_RANKING_ACADEMICO { get; set; } = string.Empty;
    public string DES_EVALUACION { get; set; } = string.Empty;
    public string DES_TITULO_ACADEMICO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoPostulantesFormacionAcademicaInsertDto
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public decimal? COD_ENTIDAD_POSTULANTE { get; set; }
    [Required] public string COD_CARRERA_ACADEMICA { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_TERMINO { get; set; }
    [Required] public string COD_TITULO_ACADEMICO { get; set; } = string.Empty;
    [Required] public string COD_RANKING_ACADEMICO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoPostulantesFormacionAcademicaUpdateDto
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public decimal? COD_ENTIDAD_POSTULANTE { get; set; }
    [Required] public string COD_CARRERA_ACADEMICA { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_TERMINO { get; set; }
    [Required] public string COD_TITULO_ACADEMICO { get; set; } = string.Empty;
    [Required] public string COD_RANKING_ACADEMICO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoPostulantesFormacionAcademicaSearchDto
{
    public string? Search { get; set; }
}