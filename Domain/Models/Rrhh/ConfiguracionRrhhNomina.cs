using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad CONFIGURACION_RRHH_NOMINA - tipos equivalentes a la BD.</summary>
public class ConfiguracionRrhhNomina
{
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_VACACIONES { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_GRATIFICACIONES { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_CTS { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_UTILIDADADES { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_SOLICITUDES { get; set; }
    public string COD_TIPO_REMUNERATIVO_SOLICTUDES { get; set; } = string.Empty;
    public string COD_TIPO_ENTIDAD_POSTULANTE { get; set; } = string.Empty;
    public string COD_TIPO_ENTIDAD_ACADEMICA { get; set; } = string.Empty;
    public string COD_REGIMEN_LABORAL { get; set; } = string.Empty;
    public int? CAN_DIAS_ALERTA_VENCIMIENTO_CONTRATOS { get; set; }
    public int? CAN_DIAS_ALERTA_VENCIMIENTO_VACACIONES { get; set; }
    public string COD_GRUPO_PRODUCTO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    public string COD_FAMILIA_PRODUCTO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    public bool? FLG_PREFIJO_ID { get; set; }
    public int? CAN_LONGITUD_ID { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_CONCEPTO_REMUNERATIVO { get; set; } = string.Empty;
    public string DES_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string DES_REGIMEN_LABORAL { get; set; } = string.Empty;
    public string DES_TIPO_REMUNERATIVO { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string COD_TIPO_REMUNERATIVO { get; set; } = string.Empty;
}

public class ConfiguracionRrhhNominaInsertDto
{
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_VACACIONES { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_GRATIFICACIONES { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_CTS { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_UTILIDADADES { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_SOLICITUDES { get; set; }
    [Required] public string COD_TIPO_REMUNERATIVO_SOLICTUDES { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD_POSTULANTE { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD_ACADEMICA { get; set; } = string.Empty;
    [Required] public string COD_REGIMEN_LABORAL { get; set; } = string.Empty;
    public int? CAN_DIAS_ALERTA_VENCIMIENTO_CONTRATOS { get; set; }
    public int? CAN_DIAS_ALERTA_VENCIMIENTO_VACACIONES { get; set; }
    [Required] public string COD_GRUPO_PRODUCTO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    public bool? FLG_PREFIJO_ID { get; set; }
    public int? CAN_LONGITUD_ID { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ConfiguracionRrhhNominaUpdateDto
{
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_VACACIONES { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_GRATIFICACIONES { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_CTS { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_UTILIDADADES { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO_SOLICITUDES { get; set; }
    [Required] public string COD_TIPO_REMUNERATIVO_SOLICTUDES { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD_POSTULANTE { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD_ACADEMICA { get; set; } = string.Empty;
    [Required] public string COD_REGIMEN_LABORAL { get; set; } = string.Empty;
    public int? CAN_DIAS_ALERTA_VENCIMIENTO_CONTRATOS { get; set; }
    public int? CAN_DIAS_ALERTA_VENCIMIENTO_VACACIONES { get; set; }
    [Required] public string COD_GRUPO_PRODUCTO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    public bool? FLG_PREFIJO_ID { get; set; }
    public int? CAN_LONGITUD_ID { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ConfiguracionRrhhNominaSearchDto
{
    public string? Search { get; set; }
}