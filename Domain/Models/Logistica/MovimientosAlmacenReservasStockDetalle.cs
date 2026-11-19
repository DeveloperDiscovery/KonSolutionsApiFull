using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>LOGISTICA.MOVIMIENTOS_ALMACEN_RESERVAS_STOCK_DETALLE
///
/// Enlace a nivel LINEA con una reserva. Reservas es el unico origen que enlaza en los dos niveles, y por eso se puede tomar una reserva entera o lineas sueltas.</summary>
public class MovimientosAlmacenReservasStockDetalle
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_RESERVA_STOCK { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
}

public class MovimientosAlmacenReservasStockDetalleInsertDto
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_RESERVA_STOCK { get; set; }
    public int? NUM_SECUENCIA { get; set; }
}

public class MovimientosAlmacenReservasStockDetalleSearchDto
{
    public string? Search { get; set; }
}
