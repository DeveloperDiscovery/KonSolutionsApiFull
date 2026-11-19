using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad EMBALADO_EMBALAJE_DETALLE - tipos equivalentes a la BD.</summary>
public class EmbaladoEmbalajeDetalle
{
    public decimal? NUM_EMBALADO { get; set; }
    public int? NUM_SECUENCIA_EMBALAJE { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA_PEDIDO { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? NUM_PRODUCTO_UNICO { get; set; }
    public decimal? CAN_EMBALADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PEDIDO_COMERCIAL { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public decimal? NUM_EMBALAJE { get; set; }
    public int? NUM_SECUENCIA { get; set; }
}

public class EmbaladoEmbalajeDetalleInsertDto
{
    public decimal? NUM_EMBALADO { get; set; }
    public int? NUM_SECUENCIA_EMBALAJE { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA_PEDIDO { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? NUM_PRODUCTO_UNICO { get; set; }
    public decimal? CAN_EMBALADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EmbaladoEmbalajeDetalleUpdateDto
{
    public decimal? NUM_EMBALADO { get; set; }
    public int? NUM_SECUENCIA_EMBALAJE { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA_PEDIDO { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? NUM_PRODUCTO_UNICO { get; set; }
    public decimal? CAN_EMBALADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EmbaladoEmbalajeDetalleSearchDto
{
    public string? Search { get; set; }
}