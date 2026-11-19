using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad SUB_CANALES_VENTA_PRODUCTOS_FIJOS - tipos equivalentes a la BD.</summary>
public class SubCanalesVentaProductosFijos
{
    public int? NUM_UNIDAD_NEGOCIO { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
    public string COD_PRODUCTO_FIJO { get; set; } = string.Empty;
    public string DES_PRODUCTO_ALIAS { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
}

public class SubCanalesVentaProductosFijosInsertDto
{
    public int? NUM_UNIDAD_NEGOCIO { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
    [Required] public string COD_PRODUCTO_FIJO { get; set; } = string.Empty;
    [Required] public string DES_PRODUCTO_ALIAS { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SubCanalesVentaProductosFijosUpdateDto
{
    public int? NUM_UNIDAD_NEGOCIO { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
    [Required] public string COD_PRODUCTO_FIJO { get; set; } = string.Empty;
    [Required] public string DES_PRODUCTO_ALIAS { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SubCanalesVentaProductosFijosSearchDto
{
    public string? Search { get; set; }
}