using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Produccion;

/// <summary>Entidad ORDENES_PRODUCCION_UNIDADES_PEDIDOS_COMERCIALES - tipos equivalentes a la BD.</summary>
public class OrdenesProduccionUnidadesPedidosComerciales
{
    public string COD_PROCESO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public decimal? CAN_PRODUCIR { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PEDIDO_COMERCIAL { get; set; } = string.Empty;
}

public class OrdenesProduccionUnidadesPedidosComercialesInsertDto
{
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public decimal? CAN_PRODUCIR { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class OrdenesProduccionUnidadesPedidosComercialesUpdateDto
{
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public decimal? CAN_PRODUCIR { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class OrdenesProduccionUnidadesPedidosComercialesSearchDto
{
    public string? Search { get; set; }
}