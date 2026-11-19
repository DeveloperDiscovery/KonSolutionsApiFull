using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad OPERACIONES_CAJA_BANCOS_DETALLE_CALCULO - tipos equivalentes a la BD.</summary>
public class OperacionesCajaBancosDetalleCalculo
{
    public decimal? NUM_OPERACION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    public decimal? TAS_IMPUESTO { get; set; }
    public decimal? VAL_CALCULO { get; set; }
    public decimal? NUM_OPERACION_DESTINO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_TIPO_IMPUESTO { get; set; } = string.Empty;
    public string DES_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
}

public class OperacionesCajaBancosDetalleCalculoInsertDto
{
    public decimal? NUM_OPERACION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    public decimal? TAS_IMPUESTO { get; set; }
    public decimal? VAL_CALCULO { get; set; }
    public decimal? NUM_OPERACION_DESTINO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class OperacionesCajaBancosDetalleCalculoUpdateDto
{
    public decimal? NUM_OPERACION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    public decimal? TAS_IMPUESTO { get; set; }
    public decimal? VAL_CALCULO { get; set; }
    public decimal? NUM_OPERACION_DESTINO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class OperacionesCajaBancosDetalleCalculoSearchDto
{
    public string? Search { get; set; }
}