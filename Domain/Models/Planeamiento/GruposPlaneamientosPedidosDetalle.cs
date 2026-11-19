using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Planeamiento;

/// <summary>Entidad GRUPOS_PLANEAMIENTOS_PEDIDOS_DETALLE - tipos equivalentes a la BD.</summary>
public class GruposPlaneamientosPedidosDetalle
{
    public int? COD_GRUPO_PLANEAMIENTO { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_DISENO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
}

public class GruposPlaneamientosPedidosDetalleInsertDto
{
    public int? COD_GRUPO_PLANEAMIENTO { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposPlaneamientosPedidosDetalleUpdateDto
{
    public int? COD_GRUPO_PLANEAMIENTO { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposPlaneamientosPedidosDetalleSearchDto
{
    public string? Search { get; set; }
}