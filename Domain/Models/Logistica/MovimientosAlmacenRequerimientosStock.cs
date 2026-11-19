using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>LOGISTICA.MOVIMIENTOS_ALMACEN_REQUERIMIENTOS_STOCK
///
/// Enlace entre un movimiento y los requerimientos de stock que atiende. Solo a nivel documento: este origen no tiene enlace por linea.</summary>
public class MovimientosAlmacenRequerimientosStock
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_REQUERIMIENTO_STOCK { get; set; } = string.Empty;
    public DateTime? FEC_REQUERIMIENTO_STOCK { get; set; }
}

public class MovimientosAlmacenRequerimientosStockInsertDto
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
}

public class MovimientosAlmacenRequerimientosStockSearchDto
{
    public string? Search { get; set; }
}
