using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad COMPROBANTES_PAGO_APLICADOS - tipos equivalentes a la BD.</summary>
public class ComprobantesPagoAplicados
{
    public decimal? NUM_REGISTRO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? NUM_REGISTRO_ASOCIADO { get; set; }
    public int? NUM_SECUENCIA_ASOCIADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
}

public class ComprobantesPagoAplicadosInsertDto
{
    public decimal? NUM_REGISTRO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? NUM_REGISTRO_ASOCIADO { get; set; }
    public int? NUM_SECUENCIA_ASOCIADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComprobantesPagoAplicadosUpdateDto
{
    public decimal? NUM_REGISTRO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? NUM_REGISTRO_ASOCIADO { get; set; }
    public int? NUM_SECUENCIA_ASOCIADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComprobantesPagoAplicadosSearchDto
{
    public string? Search { get; set; }
}