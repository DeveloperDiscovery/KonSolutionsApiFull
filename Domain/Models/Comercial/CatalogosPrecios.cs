using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad CATALOGOS_PRECIOS - tipos equivalentes a la BD.</summary>
public class CatalogosPrecios
{
    public int? NUM_CATALOGO_PRECIOS { get; set; }
    public string DES_CATALOGO_PRECIOS { get; set; } = string.Empty;
    public int? NUM_PERIODO_VENTAS { get; set; }
    public string COD_TIPO_CATALOGO_PRECIO { get; set; } = string.Empty;
    public string COD_MONEDA { get; set; } = string.Empty;
    public int? NUM_UNIDAD_COMERCIAL { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_TIPO_CATALOGO_PRECIO { get; set; } = string.Empty;
    public string DES_PERIODO { get; set; } = string.Empty;
    public int? NUM_UNIDAD_NEGOCIO { get; set; }
    public int? NUM_PERIODO { get; set; }
}

public class CatalogosPreciosInsertDto
{
    public int? NUM_CATALOGO_PRECIOS { get; set; }
    [Required] public string DES_CATALOGO_PRECIOS { get; set; } = string.Empty;
    public int? NUM_PERIODO_VENTAS { get; set; }
    [Required] public string COD_TIPO_CATALOGO_PRECIO { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public int? NUM_UNIDAD_COMERCIAL { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CatalogosPreciosUpdateDto
{
    public int? NUM_CATALOGO_PRECIOS { get; set; }
    [Required] public string DES_CATALOGO_PRECIOS { get; set; } = string.Empty;
    public int? NUM_PERIODO_VENTAS { get; set; }
    [Required] public string COD_TIPO_CATALOGO_PRECIO { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public int? NUM_UNIDAD_COMERCIAL { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CatalogosPreciosSearchDto
{
    public string? Search { get; set; }
}