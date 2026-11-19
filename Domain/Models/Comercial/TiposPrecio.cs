using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_PRECIO - tipos equivalentes a la BD.</summary>
public class TiposPrecio
{
    public int? COD_TIPO_PRECIO { get; set; }
    public string DES_TIPO_PRECIO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class TiposPrecioInsertDto
{
    public int? COD_TIPO_PRECIO { get; set; }
    [Required] public string DES_TIPO_PRECIO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposPrecioUpdateDto
{
    public int? COD_TIPO_PRECIO { get; set; }
    [Required] public string DES_TIPO_PRECIO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposPrecioSearchDto
{
    public string? Search { get; set; }
}