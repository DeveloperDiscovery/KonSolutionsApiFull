using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad PEDIDOS_COMERCIALES_MOVIMIENTOS_ALMACEN - tipos equivalentes a la BD.</summary>
public class PedidosComercialesMovimientosAlmacen
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA_PEDIDO { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_SECUENCIA_MOVIMIENTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
}

public class PedidosComercialesMovimientosAlmacenInsertDto
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA_PEDIDO { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_SECUENCIA_MOVIMIENTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PedidosComercialesMovimientosAlmacenUpdateDto
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA_PEDIDO { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_SECUENCIA_MOVIMIENTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PedidosComercialesMovimientosAlmacenSearchDto
{
    public string? Search { get; set; }
}