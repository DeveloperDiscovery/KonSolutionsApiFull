using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad CATALOGOS_PRECIOS_DETALLE - tipos equivalentes a la BD.</summary>
public class CatalogosPreciosDetalle
{
    public int? NUM_CATALOGO_PRECIOS { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string COD_SKU_INTERMEDIARIO { get; set; } = string.Empty;
    public string DES_SKU_INTERMEDIARIO { get; set; } = string.Empty;
    public decimal? PRE_UNITARIO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CATALOGO_PRECIOS { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
}

public class CatalogosPreciosDetalleInsertDto
{
    public int? NUM_CATALOGO_PRECIOS { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_SKU_INTERMEDIARIO { get; set; } = string.Empty;
    [Required] public string DES_SKU_INTERMEDIARIO { get; set; } = string.Empty;
    public decimal? PRE_UNITARIO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CatalogosPreciosDetalleUpdateDto
{
    public int? NUM_CATALOGO_PRECIOS { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_SKU_INTERMEDIARIO { get; set; } = string.Empty;
    [Required] public string DES_SKU_INTERMEDIARIO { get; set; } = string.Empty;
    public decimal? PRE_UNITARIO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CatalogosPreciosDetalleSearchDto
{
    public string? Search { get; set; }
}