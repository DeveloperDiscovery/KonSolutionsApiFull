using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad PRODUCTOS_TIPOS_PRECIO - tipos equivalentes a la BD.</summary>
public class ProductosTiposPrecio
{
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public int? COD_TIPO_PRECIO { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_PRECIO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_PRECIO { get; set; } = string.Empty;
}

public class ProductosTiposPrecioInsertDto
{
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public int? COD_TIPO_PRECIO { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_PRECIO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProductosTiposPrecioUpdateDto
{
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public int? COD_TIPO_PRECIO { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_PRECIO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProductosTiposPrecioSearchDto
{
    public string? Search { get; set; }
}