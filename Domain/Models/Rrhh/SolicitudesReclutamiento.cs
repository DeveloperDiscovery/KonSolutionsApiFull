using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad SOLICITUDES_RECLUTAMIENTO - tipos equivalentes a la BD.</summary>
public class SolicitudesReclutamiento
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public decimal? COD_ENTIDAD_SOLICITANTE { get; set; }
    public DateTime? FEC_SOLICITUD { get; set; }
    public DateTime? FEC_REQUERIDA { get; set; }
    public int? COD_AREA { get; set; }
    public int? COD_CARGO { get; set; }
    public int? CAN_PUESTOS { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_REMUNERACION_PROPUESTA { get; set; }
    public string DES_DETALLE { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION_SOLICTUD { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_AREA { get; set; } = string.Empty;
    public string DES_AUTORIZACION { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PATH_FILE { get; set; } = string.Empty;
    public string DES_QUERY_EXECUTE_NO { get; set; } = string.Empty;
    public string DES_QUERY_EXECUTE_SI { get; set; } = string.Empty;
    public string DES_CARGO { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_MOTIVO { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
}

public class SolicitudesReclutamientoInsertDto
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public decimal? COD_ENTIDAD_SOLICITANTE { get; set; }
    public DateTime? FEC_SOLICITUD { get; set; }
    public DateTime? FEC_REQUERIDA { get; set; }
    public int? COD_AREA { get; set; }
    public int? COD_CARGO { get; set; }
    public int? CAN_PUESTOS { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_REMUNERACION_PROPUESTA { get; set; }
    [Required] public string DES_DETALLE { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION_SOLICTUD { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoUpdateDto
{
    public int? NUM_SOLICITUD_RECLUTAMIENTO { get; set; }
    public decimal? COD_ENTIDAD_SOLICITANTE { get; set; }
    public DateTime? FEC_SOLICITUD { get; set; }
    public DateTime? FEC_REQUERIDA { get; set; }
    public int? COD_AREA { get; set; }
    public int? COD_CARGO { get; set; }
    public int? CAN_PUESTOS { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_REMUNERACION_PROPUESTA { get; set; }
    [Required] public string DES_DETALLE { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION_SOLICTUD { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesReclutamientoSearchDto
{
    public string? Search { get; set; }
}