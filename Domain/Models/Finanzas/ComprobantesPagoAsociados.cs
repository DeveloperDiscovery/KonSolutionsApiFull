using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad COMPROBANTES_PAGO_ASOCIADOS - tipos equivalentes a la BD.</summary>
public class ComprobantesPagoAsociados
{
    public decimal? NUM_REGISTRO { get; set; }
    public decimal? NUM_REGISTRO_ASOCIADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
}

public class ComprobantesPagoAsociadosInsertDto
{
    public decimal? NUM_REGISTRO { get; set; }
    public decimal? NUM_REGISTRO_ASOCIADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComprobantesPagoAsociadosUpdateDto
{
    public decimal? NUM_REGISTRO { get; set; }
    public decimal? NUM_REGISTRO_ASOCIADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComprobantesPagoAsociadosSearchDto
{
    public string? Search { get; set; }
}