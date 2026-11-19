using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Planeamiento;

/// <summary>Entidad GRUPOS_PLANEAMIENTOS_PEDIDOS - tipos equivalentes a la BD.</summary>
public class GruposPlaneamientosPedidos
{
    public int? COD_GRUPO_PLANEAMIENTO { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_GRUPO_PLANEAMIENTO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PEDIDO_COMERCIAL { get; set; } = string.Empty;
}

public class GruposPlaneamientosPedidosInsertDto
{
    public int? COD_GRUPO_PLANEAMIENTO { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposPlaneamientosPedidosUpdateDto
{
    public int? COD_GRUPO_PLANEAMIENTO { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposPlaneamientosPedidosSearchDto
{
    public string? Search { get; set; }
}