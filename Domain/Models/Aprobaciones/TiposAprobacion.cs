using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Aprobaciones;

/// <summary>Entidad TIPOS_APROBACION - tipos equivalentes a la BD.</summary>
public class TiposAprobacion
{
    public string COD_TIPO_APROBACION { get; set; } = string.Empty;
    public string DES_TIPO_APROBACION { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposAprobacionInsertDto
{
    [Required] public string COD_TIPO_APROBACION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_APROBACION { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposAprobacionUpdateDto
{
    [Required] public string COD_TIPO_APROBACION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_APROBACION { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposAprobacionSearchDto
{
    public string? Search { get; set; }
}