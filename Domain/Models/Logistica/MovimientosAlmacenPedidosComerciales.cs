using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>LOGISTICA.MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES
///
/// Enlace entre un movimiento y las ordenes de compra que atiende, a nivel documento.</summary>
public class MovimientosAlmacenPedidosComerciales
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_PEDIDO_COMERCIAL { get; set; } = string.Empty;
    public DateTime? FEC_PEDIDO_COMERCIAL { get; set; }
}

public class MovimientosAlmacenPedidosComercialesInsertDto
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
}

public class MovimientosAlmacenPedidosComercialesSearchDto
{
    public string? Search { get; set; }
}
