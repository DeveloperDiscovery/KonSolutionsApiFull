using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad OPERACIONES_CAJA_BANCOS_DETALLE - tipos equivalentes a la BD.</summary>
public class OperacionesCajaBancosDetalle
{
    public decimal? NUM_OPERACION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? NUM_REGISTRO_COMPROBANTES { get; set; }
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal? NUM_OPERACION_BANCARIA { get; set; }
    public int? COD_FORMA_PAGO { get; set; }
    public decimal? VAL_OPERACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public decimal? NUM_REGISTRO { get; set; }
}

public class OperacionesCajaBancosDetalleInsertDto
{
    public decimal? NUM_OPERACION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? NUM_REGISTRO_COMPROBANTES { get; set; }
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal? NUM_OPERACION_BANCARIA { get; set; }
    public int? COD_FORMA_PAGO { get; set; }
    public decimal? VAL_OPERACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class OperacionesCajaBancosDetalleUpdateDto
{
    public decimal? NUM_OPERACION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? NUM_REGISTRO_COMPROBANTES { get; set; }
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal? NUM_OPERACION_BANCARIA { get; set; }
    public int? COD_FORMA_PAGO { get; set; }
    public decimal? VAL_OPERACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class OperacionesCajaBancosDetalleSearchDto
{
    public string? Search { get; set; }
}