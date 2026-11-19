using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>LOGISTICA.MOVIMIENTOS_ALMACEN_RESERVAS_STOCK
///
/// Enlace entre un movimiento y las reservas de stock que atiende, a nivel documento.</summary>
public class MovimientosAlmacenReservasStock
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_RESERVA_STOCK { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_RESERVA_STOCK { get; set; } = string.Empty;
    public DateTime? FEC_RESERVA_STOCK { get; set; }
}

public class MovimientosAlmacenReservasStockInsertDto
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_RESERVA_STOCK { get; set; }
}

public class MovimientosAlmacenReservasStockSearchDto
{
    public string? Search { get; set; }
}
