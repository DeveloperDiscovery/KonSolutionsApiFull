using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_UNIDAD_NEGOCIO - tipos equivalentes a la BD.</summary>
public class TiposUnidadNegocio
{
    public string COD_TIPO_UNIDAD_NEGOCIO { get; set; } = string.Empty;
    public string DES_TIPO_UNIDAD_NEGOCIO { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
}

public class TiposUnidadNegocioInsertDto
{
    [Required] public string COD_TIPO_UNIDAD_NEGOCIO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_UNIDAD_NEGOCIO { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposUnidadNegocioUpdateDto
{
    [Required] public string COD_TIPO_UNIDAD_NEGOCIO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_UNIDAD_NEGOCIO { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposUnidadNegocioSearchDto
{
    public string? Search { get; set; }
}