using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Safety;

/// <summary>Entidad PRODUCTOS_TAGS - tipos equivalentes a la BD.</summary>
public class ProductosTags
{
    public string COD_PRODUCTO_TAG { get; set; } = string.Empty;
    public string COD_TIPO_TAG { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_ASIGNADA { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
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
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_MOTIVO { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_TAG { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class ProductosTagsInsertDto
{
    [Required] public string COD_PRODUCTO_TAG { get; set; } = string.Empty;
    [Required] public string COD_TIPO_TAG { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_ASIGNADA { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProductosTagsUpdateDto
{
    [Required] public string COD_PRODUCTO_TAG { get; set; } = string.Empty;
    [Required] public string COD_TIPO_TAG { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_ASIGNADA { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProductosTagsSearchDto
{
    public string? Search { get; set; }
}