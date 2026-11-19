using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad CAJAS_SALDOS - tipos equivalentes a la BD.</summary>
public class CajasSaldos
{
    public string COD_CAJA { get; set; } = string.Empty;
    public DateTime? FEC_CIERRE { get; set; }
    public string COD_TURNO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_CAJERO { get; set; }
    public decimal? VAL_SALDO_INICIAL { get; set; }
    public decimal? VAL_MOVIMIENTOS_INGRESO { get; set; }
    public decimal? VAL_MOVIMIENTOS_SALIDA { get; set; }
    public decimal? VAL_SALDO_FINAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CAJA { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TURNO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class CajasSaldosInsertDto
{
    [Required] public string COD_CAJA { get; set; } = string.Empty;
    public DateTime? FEC_CIERRE { get; set; }
    [Required] public string COD_TURNO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_CAJERO { get; set; }
    public decimal? VAL_SALDO_INICIAL { get; set; }
    public decimal? VAL_MOVIMIENTOS_INGRESO { get; set; }
    public decimal? VAL_MOVIMIENTOS_SALIDA { get; set; }
    public decimal? VAL_SALDO_FINAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CajasSaldosUpdateDto
{
    [Required] public string COD_CAJA { get; set; } = string.Empty;
    public DateTime? FEC_CIERRE { get; set; }
    [Required] public string COD_TURNO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_CAJERO { get; set; }
    public decimal? VAL_SALDO_INICIAL { get; set; }
    public decimal? VAL_MOVIMIENTOS_INGRESO { get; set; }
    public decimal? VAL_MOVIMIENTOS_SALIDA { get; set; }
    public decimal? VAL_SALDO_FINAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CajasSaldosSearchDto
{
    public string? Search { get; set; }
}