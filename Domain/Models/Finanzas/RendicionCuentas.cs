using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad RENDICION_CUENTAS - tipos equivalentes a la BD.</summary>
public class RendicionCuentas
{
    public int? NUM_RENDICION_CUENTAS { get; set; }
    public string DES_RENDICION_CUENTAS { get; set; } = string.Empty;
    public string COD_CAJA { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_RENDIDORA { get; set; }
    public DateTime? FEC_RENDICION_CUENTAS { get; set; }
    public decimal? NUM_OPERACION_RENDICION { get; set; }
    public int? NUM_PROYECTO { get; set; }
    public decimal? VAL_RENDICION { get; set; }
    public decimal? VAL_RENDIDO { get; set; }
    public decimal? VAL_PENDIENTE { get; set; }
    public string DES_OBSERVACIONES { get; set; } = string.Empty;
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
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PROYECTO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public decimal? NUM_OPERACION { get; set; }
}

public class RendicionCuentasInsertDto
{
    public int? NUM_RENDICION_CUENTAS { get; set; }
    [Required] public string DES_RENDICION_CUENTAS { get; set; } = string.Empty;
    [Required] public string COD_CAJA { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_RENDIDORA { get; set; }
    public DateTime? FEC_RENDICION_CUENTAS { get; set; }
    public decimal? NUM_OPERACION_RENDICION { get; set; }
    public int? NUM_PROYECTO { get; set; }
    public decimal? VAL_RENDICION { get; set; }
    public decimal? VAL_RENDIDO { get; set; }
    public decimal? VAL_PENDIENTE { get; set; }
    [Required] public string DES_OBSERVACIONES { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RendicionCuentasUpdateDto
{
    public int? NUM_RENDICION_CUENTAS { get; set; }
    [Required] public string DES_RENDICION_CUENTAS { get; set; } = string.Empty;
    [Required] public string COD_CAJA { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_RENDIDORA { get; set; }
    public DateTime? FEC_RENDICION_CUENTAS { get; set; }
    public decimal? NUM_OPERACION_RENDICION { get; set; }
    public int? NUM_PROYECTO { get; set; }
    public decimal? VAL_RENDICION { get; set; }
    public decimal? VAL_RENDIDO { get; set; }
    public decimal? VAL_PENDIENTE { get; set; }
    [Required] public string DES_OBSERVACIONES { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RendicionCuentasSearchDto
{
    public string? Search { get; set; }
}