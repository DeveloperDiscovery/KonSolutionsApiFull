using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Activos;

/// <summary>Entidad ACTIVOS_VALORIZACION - tipos equivalentes a la BD.</summary>
public class ActivosValorizacion
{
    public string COD_ACTIVO { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    public int? NUM_PERIODO_CONTABLE { get; set; }
    public decimal? COD_ENTIDAD_PROVEEDOR { get; set; }
    public DateTime? FEC_ADQUISICION { get; set; }
    public DateTime? FEC_VALORIZACION { get; set; }
    public decimal? VAL_VALORIZACION { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? VAL_DEPRECIACION_ACUMULADA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ACTIVO { get; set; } = string.Empty;
    public string DES_MARCA { get; set; } = string.Empty;
    public string DES_MODELO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_SERIE { get; set; } = string.Empty;
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
    public string DES_PERIODO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public int? NUM_PERIODO { get; set; }
}

public class ActivosValorizacionInsertDto
{
    [Required] public string COD_ACTIVO { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    public int? NUM_PERIODO_CONTABLE { get; set; }
    public decimal? COD_ENTIDAD_PROVEEDOR { get; set; }
    public DateTime? FEC_ADQUISICION { get; set; }
    public DateTime? FEC_VALORIZACION { get; set; }
    public decimal? VAL_VALORIZACION { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? VAL_DEPRECIACION_ACUMULADA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ActivosValorizacionUpdateDto
{
    [Required] public string COD_ACTIVO { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    public int? NUM_PERIODO_CONTABLE { get; set; }
    public decimal? COD_ENTIDAD_PROVEEDOR { get; set; }
    public DateTime? FEC_ADQUISICION { get; set; }
    public DateTime? FEC_VALORIZACION { get; set; }
    public decimal? VAL_VALORIZACION { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? VAL_DEPRECIACION_ACUMULADA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ActivosValorizacionSearchDto
{
    public string? Search { get; set; }
}