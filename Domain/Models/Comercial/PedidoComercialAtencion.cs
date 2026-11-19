namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>PROC_COMERCIAL_PEDIDOS_COMERCIALES_ATENCION: cuánto de un pedido comercial (por ejemplo una orden de compra) ya se atendió.
/// Misma cuenta que PROC_COMERCIAL_REPORTE_ORDENES_COMPRA: POR_ATENDIDO = atendida * 100 / pedida (0 si no hay cantidad pedida; puede pasar de 100).</summary>
public class PedidoComercialAtencion
{
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal NUM_PEDIDO_COMERCIAL { get; set; }
    public int NUM_LINEAS { get; set; }
    public decimal CAN_PEDIDA { get; set; }
    public decimal CAN_ATENDIDA { get; set; }
    public decimal POR_ATENDIDO { get; set; }
}
