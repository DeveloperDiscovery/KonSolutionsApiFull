using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad SOLICITUDES_RECLUTAMIENTO_FUNCIONES - tipos equivalentes a la BD.</summary>
public class SolicitudesReclutamientoFunciones
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public string COD_FUNCION { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_DETALLE { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoFuncionesInsertDto
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    [Required] public string COD_FUNCION { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoFuncionesUpdateDto
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    [Required] public string COD_FUNCION { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoFuncionesSearchDto
{
    public string? Search { get; set; }
}