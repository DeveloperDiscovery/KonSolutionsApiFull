using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_POS - tipos equivalentes a la BD.</summary>
public class TiposPos
{
    public string COD_TIPO_POS { get; set; } = string.Empty;
    public string DES_TIPO_POS { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
    public bool? FLG_VENTA_MENUDEO { get; set; }
    public bool? FLG_PEDIDO_VENTA_RESTAURANTE { get; set; }
    public bool? FLG_PEDIDO_VENTA_POR_MAYOR { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
}

public class TiposPosInsertDto
{
    [Required] public string COD_TIPO_POS { get; set; } = string.Empty;
    [Required] public string DES_TIPO_POS { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
    public bool? FLG_VENTA_MENUDEO { get; set; }
    public bool? FLG_PEDIDO_VENTA_RESTAURANTE { get; set; }
    public bool? FLG_PEDIDO_VENTA_POR_MAYOR { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposPosUpdateDto
{
    [Required] public string COD_TIPO_POS { get; set; } = string.Empty;
    [Required] public string DES_TIPO_POS { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
    public bool? FLG_VENTA_MENUDEO { get; set; }
    public bool? FLG_PEDIDO_VENTA_RESTAURANTE { get; set; }
    public bool? FLG_PEDIDO_VENTA_POR_MAYOR { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposPosSearchDto
{
    public string? Search { get; set; }
}