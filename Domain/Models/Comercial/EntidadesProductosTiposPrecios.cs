using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad ENTIDADES_PRODUCTOS_TIPOS_PRECIOS - tipos equivalentes a la BD.</summary>
public class EntidadesProductosTiposPrecios
{
    public decimal? COD_ENTIDAD { get; set; }
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
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_PRODUCTO_ENTIDAD { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_TIPO_PRECIO { get; set; } = string.Empty;
}

public class EntidadesProductosTiposPreciosInsertDto
{
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public int? COD_TIPO_PRECIO { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_PRECIO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesProductosTiposPreciosUpdateDto
{
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public int? COD_TIPO_PRECIO { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_PRECIO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesProductosTiposPreciosSearchDto
{
    public string? Search { get; set; }
}