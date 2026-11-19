using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_UNIDAD_COMERCIAL - tipos equivalentes a la BD.</summary>
public class TiposUnidadComercial
{
    public string COD_TIPO_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    public string DES_TIPO_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    public bool? FLG_MESA { get; set; }
    public bool? FLG_STAND { get; set; }
    public byte[]? IMG_PICTURE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
}

public class TiposUnidadComercialInsertDto
{
    [Required] public string COD_TIPO_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    [Required] public string DES_TIPO_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    public bool? FLG_MESA { get; set; }
    public bool? FLG_STAND { get; set; }
    public byte[]? IMG_PICTURE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposUnidadComercialUpdateDto
{
    [Required] public string COD_TIPO_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    [Required] public string DES_TIPO_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    public bool? FLG_MESA { get; set; }
    public bool? FLG_STAND { get; set; }
    public byte[]? IMG_PICTURE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposUnidadComercialSearchDto
{
    public string? Search { get; set; }
}