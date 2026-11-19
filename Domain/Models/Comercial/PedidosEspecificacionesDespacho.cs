using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad PEDIDOS_ESPECIFICACIONES_DESPACHO_ - tipos equivalentes a la BD.</summary>
public class PedidosEspecificacionesDespacho
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? CAN_EMBALAJE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PEDIDO_COMERCIAL { get; set; } = string.Empty;
}

public class PedidosEspecificacionesDespachoInsertDto
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? CAN_EMBALAJE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PedidosEspecificacionesDespachoUpdateDto
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? CAN_EMBALAJE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PedidosEspecificacionesDespachoSearchDto
{
    public string? Search { get; set; }
}