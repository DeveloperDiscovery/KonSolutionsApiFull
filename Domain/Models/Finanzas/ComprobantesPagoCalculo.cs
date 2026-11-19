using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad COMPROBANTES_PAGO_CALCULO - tipos equivalentes a la BD.</summary>
public class ComprobantesPagoCalculo
{
    public decimal? NUM_REGISTRO { get; set; }
    public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    public decimal? TAS_IMPUESTO { get; set; }
    public decimal? VAL_CALCULO { get; set; }
    public decimal? NUM_REGISTRO_DESTINO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
}

public class ComprobantesPagoCalculoInsertDto
{
    public decimal? NUM_REGISTRO { get; set; }
    [Required] public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    public decimal? TAS_IMPUESTO { get; set; }
    public decimal? VAL_CALCULO { get; set; }
    public decimal? NUM_REGISTRO_DESTINO { get; set; }
}

public class ComprobantesPagoCalculoUpdateDto
{
    public decimal? NUM_REGISTRO { get; set; }
    [Required] public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    public decimal? TAS_IMPUESTO { get; set; }
    public decimal? VAL_CALCULO { get; set; }
    public decimal? NUM_REGISTRO_DESTINO { get; set; }
}

public class ComprobantesPagoCalculoSearchDto
{
    public string? Search { get; set; }
}