using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Solicitudes;

/// <summary>Entidad SOLICITUDES - tipos equivalentes a la BD.</summary>
public class Solicitudes
{
    public int? NUM_SOLICITUD { get; set; }
    public string DES_SOLICITUD { get; set; } = string.Empty;
    public DateTime? FEC_SOLICITUD { get; set; }
    public decimal? COD_ENTIDAD_SOLICITANTE { get; set; }
    public decimal? COD_ENTIDAD_ATENCION { get; set; }
    public string COD_PRIORIDAD { get; set; } = string.Empty;
    public string COD_TIPO_SOLICITUD { get; set; } = string.Empty;
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
    public string DES_PRIORIDAD { get; set; } = string.Empty;
    public string DES_TIPO_SOLICITUD { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class SolicitudesInsertDto
{
    public int? NUM_SOLICITUD { get; set; }
    [Required] public string DES_SOLICITUD { get; set; } = string.Empty;
    public DateTime? FEC_SOLICITUD { get; set; }
    public decimal? COD_ENTIDAD_SOLICITANTE { get; set; }
    public decimal? COD_ENTIDAD_ATENCION { get; set; }
    [Required] public string COD_PRIORIDAD { get; set; } = string.Empty;
    [Required] public string COD_TIPO_SOLICITUD { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesUpdateDto
{
    public int? NUM_SOLICITUD { get; set; }
    [Required] public string DES_SOLICITUD { get; set; } = string.Empty;
    public DateTime? FEC_SOLICITUD { get; set; }
    public decimal? COD_ENTIDAD_SOLICITANTE { get; set; }
    public decimal? COD_ENTIDAD_ATENCION { get; set; }
    [Required] public string COD_PRIORIDAD { get; set; } = string.Empty;
    [Required] public string COD_TIPO_SOLICITUD { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesSearchDto
{
    public string? Search { get; set; }
}