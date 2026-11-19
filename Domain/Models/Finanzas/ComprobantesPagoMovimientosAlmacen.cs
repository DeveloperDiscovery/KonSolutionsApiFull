using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad COMPROBANTES_PAGO_MOVIMIENTOS_ALMACEN - tipos equivalentes a la BD.</summary>
public class ComprobantesPagoMovimientosAlmacen
{
    public decimal? NUM_REGISTRO { get; set; }
    public int? NUM_SECUENCIA_COMPROBANTE { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_SECUENCIA_MOVIMIENTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
}

public class ComprobantesPagoMovimientosAlmacenInsertDto
{
    public decimal? NUM_REGISTRO { get; set; }
    public int? NUM_SECUENCIA_COMPROBANTE { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_SECUENCIA_MOVIMIENTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComprobantesPagoMovimientosAlmacenUpdateDto
{
    public decimal? NUM_REGISTRO { get; set; }
    public int? NUM_SECUENCIA_COMPROBANTE { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_SECUENCIA_MOVIMIENTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComprobantesPagoMovimientosAlmacenSearchDto
{
    public string? Search { get; set; }
}