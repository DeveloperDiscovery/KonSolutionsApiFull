using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad SOLICITUDES_RECLUTAMIENTO_POSTULANTES - tipos equivalentes a la BD.</summary>
public class SolicitudesReclutamientoPostulantes
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public decimal? COD_ENTIDAD_POSTULANTE { get; set; }
    public string DES_EVALUACION { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION_APROBACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_AUTORIZACION { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PATH_FILE { get; set; } = string.Empty;
    public string DES_QUERY_EXECUTE_NO { get; set; } = string.Empty;
    public string DES_QUERY_EXECUTE_SI { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_DETALLE { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
}

public class SolicitudesReclutamientoPostulantesInsertDto
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public decimal? COD_ENTIDAD_POSTULANTE { get; set; }
    [Required] public string DES_EVALUACION { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION_APROBACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoPostulantesUpdateDto
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public decimal? COD_ENTIDAD_POSTULANTE { get; set; }
    [Required] public string DES_EVALUACION { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION_APROBACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoPostulantesSearchDto
{
    public string? Search { get; set; }
}