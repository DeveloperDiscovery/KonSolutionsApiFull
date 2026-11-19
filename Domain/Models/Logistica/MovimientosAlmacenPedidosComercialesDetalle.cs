using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>LOGISTICA.MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_DETALLE
///
/// Enlace a nivel LINEA con una orden de compra.</summary>
public class MovimientosAlmacenPedidosComercialesDetalle
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
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

public class MovimientosAlmacenPedidosComercialesDetalleInsertDto
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
}

public class MovimientosAlmacenPedidosComercialesDetalleSearchDto
{
    public string? Search { get; set; }
}
