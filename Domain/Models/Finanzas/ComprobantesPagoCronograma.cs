using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad COMPROBANTES_PAGO_CRONOGRAMA - tipos equivalentes a la BD.</summary>
public class ComprobantesPagoCronograma
{
    public decimal? NUM_REGISTRO { get; set; }
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? VAL_PENDIENTE { get; set; }
    public decimal? VAL_PAGADO { get; set; }
    public int? COD_TIPO_ESTADO_PAGO { get; set; }
    public int? COD_ESTADO_PAGO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class ComprobantesPagoCronogramaInsertDto
{
    public decimal? NUM_REGISTRO { get; set; }
    public DateTime? FEC_VENCIMIENTO { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? VAL_PENDIENTE { get; set; }
    public decimal? VAL_PAGADO { get; set; }
    public int? COD_TIPO_ESTADO_PAGO { get; set; }
    public int? COD_ESTADO_PAGO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComprobantesPagoCronogramaUpdateDto
{
    public decimal? NUM_REGISTRO { get; set; }
    public DateTime? FEC_VENCIMIENTO { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? FAC_TIPO_CAMBIO { get; set; }
    public decimal? VAL_PENDIENTE { get; set; }
    public decimal? VAL_PAGADO { get; set; }
    public int? COD_TIPO_ESTADO_PAGO { get; set; }
    public int? COD_ESTADO_PAGO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComprobantesPagoCronogramaSearchDto
{
    public string? Search { get; set; }
}