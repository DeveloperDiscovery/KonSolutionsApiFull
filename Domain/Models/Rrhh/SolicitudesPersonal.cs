using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad SOLICITUDES_PERSONAL - tipos equivalentes a la BD.</summary>
public class SolicitudesPersonal
{
    public int? NUM_SOLICITUD_PERSONAL { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public DateTime? FEC_SOLICITUD { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_SOLICITADO { get; set; }
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public int? NUM_AUTORIZACION { get; set; }
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
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_CONCEPTO_REMUNERATIVO { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_PLANILLA_REMUNERATIVA { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public int? NUM_PLANILLA_REMUNERATIVA { get; set; }
}

public class SolicitudesPersonalInsertDto
{
    public int? NUM_SOLICITUD_PERSONAL { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public DateTime? FEC_SOLICITUD { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_SOLICITADO { get; set; }
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesPersonalUpdateDto
{
    public int? NUM_SOLICITUD_PERSONAL { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public DateTime? FEC_SOLICITUD { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_SOLICITADO { get; set; }
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SolicitudesPersonalSearchDto
{
    public string? Search { get; set; }
}