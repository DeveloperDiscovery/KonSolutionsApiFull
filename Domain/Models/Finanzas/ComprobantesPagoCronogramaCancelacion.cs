using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad COMPROBANTES_PAGO_CRONOGRAMA_CANCELACION - tipos equivalentes a la BD.</summary>
public class ComprobantesPagoCronogramaCancelacion
{
    public decimal? NUM_REGISTRO { get; set; }
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FORMA_PAGO { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? VAL_CANCELACION { get; set; }
    public DateTime? FEC_CANCELACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
}

public class ComprobantesPagoCronogramaCancelacionInsertDto
{
    public decimal? NUM_REGISTRO { get; set; }
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FORMA_PAGO { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? VAL_CANCELACION { get; set; }
    public DateTime? FEC_CANCELACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComprobantesPagoCronogramaCancelacionUpdateDto
{
    public decimal? NUM_REGISTRO { get; set; }
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FORMA_PAGO { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? VAL_CANCELACION { get; set; }
    public DateTime? FEC_CANCELACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComprobantesPagoCronogramaCancelacionSearchDto
{
    public string? Search { get; set; }
}