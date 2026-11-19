using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad TIPOS_RUTA_PRODUCCION - tipos equivalentes a la BD.</summary>
public class TiposRutaProduccion
{
    public string COD_TIPO_RUTA_PRODUCCION { get; set; } = string.Empty;
    public string DES_TIPO_RUTA_PRODUCCION { get; set; } = string.Empty;
    public bool? FLG_DESARROLLO { get; set; }
    public bool? FLG_COTIZACION { get; set; }
    public bool? FLG_PRODUCCION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposRutaProduccionInsertDto
{
    [Required] public string COD_TIPO_RUTA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_RUTA_PRODUCCION { get; set; } = string.Empty;
    public bool? FLG_DESARROLLO { get; set; }
    public bool? FLG_COTIZACION { get; set; }
    public bool? FLG_PRODUCCION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposRutaProduccionUpdateDto
{
    [Required] public string COD_TIPO_RUTA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_RUTA_PRODUCCION { get; set; } = string.Empty;
    public bool? FLG_DESARROLLO { get; set; }
    public bool? FLG_COTIZACION { get; set; }
    public bool? FLG_PRODUCCION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposRutaProduccionSearchDto
{
    public string? Search { get; set; }
}