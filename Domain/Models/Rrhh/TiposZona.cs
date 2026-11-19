using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad TIPOS_ZONA - tipos equivalentes a la BD.</summary>
public class TiposZona
{
    public string COD_TIPO_ZONA { get; set; } = string.Empty;
    public string DES_TIPO_ZONA { get; set; } = string.Empty;
}

public class TiposZonaInsertDto
{
    [Required] public string COD_TIPO_ZONA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_ZONA { get; set; } = string.Empty;
}

public class TiposZonaUpdateDto
{
    [Required] public string COD_TIPO_ZONA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_ZONA { get; set; } = string.Empty;
}

public class TiposZonaSearchDto
{
    public string? Search { get; set; }
}