using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad TIPOS_PERSONAL - tipos equivalentes a la BD.</summary>
public class TiposPersonal
{
    public string COD_TIPO_PERSONAL { get; set; } = string.Empty;
    public string DES_TIPO_PERSONAL { get; set; } = string.Empty;
    public string COD_PREFIJO_ID { get; set; } = string.Empty;
}

public class TiposPersonalInsertDto
{
    [Required] public string COD_TIPO_PERSONAL { get; set; } = string.Empty;
    [Required] public string DES_TIPO_PERSONAL { get; set; } = string.Empty;
    [Required] public string COD_PREFIJO_ID { get; set; } = string.Empty;
}

public class TiposPersonalUpdateDto
{
    [Required] public string COD_TIPO_PERSONAL { get; set; } = string.Empty;
    [Required] public string DES_TIPO_PERSONAL { get; set; } = string.Empty;
    [Required] public string COD_PREFIJO_ID { get; set; } = string.Empty;
}

public class TiposPersonalSearchDto
{
    public string? Search { get; set; }
}