using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Planeamiento;

/// <summary>Entidad TIPOS_OPERACION_PRODUCCION - tipos equivalentes a la BD.</summary>
public class TiposOperacionProduccion
{
    public string COD_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string DES_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string COD_CLASE_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string COD_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string DES_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string DES_CLASE_OPERACION_PRODUCCION { get; set; } = string.Empty;
}

public class TiposOperacionProduccionInsertDto
{
    [Required] public string COD_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_CLASE_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposOperacionProduccionUpdateDto
{
    [Required] public string COD_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_CLASE_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposOperacionProduccionSearchDto
{
    public string? Search { get; set; }
}